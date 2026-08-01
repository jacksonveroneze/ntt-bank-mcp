---
name: new-usecase
description: Use quando o usuário pedir para criar/adicionar um novo caso de uso (use case) na camada Application 
  deste repositório (ntt-bank-mcp), com ou sem exposição via MCP tool. 
  Faz o scaffold de Domain (se necessário), Application e Infrastructure (se necessário), 
  seguindo o padrão do caso de uso existente GetCustomer, sem repetir a arquitetura a cada pedido. 
  Não cria a MCP tool — para expor o use case como tool, use a skill new-mcp-tool depois.
---

# new-usecase

Scaffold de um novo caso de uso completo (Domain + Application +
Infrastructure, conforme necessário), seguindo exatamente o padrão de
`Application/Customers/GetCustomer/` e as regras já documentadas em
`/CLAUDE.md` e nos `CLAUDE.md` de `Domain`, `Application` e `Infrastructure`.
Leia esses arquivos antes de gerar código — esta skill assume as
convenções deles e não as repete integralmente aqui.

Esta skill não cria MCP tool. Se o use case precisa ser exposto para o
modelo invocar, use a skill `new-mcp-tool` depois que este scaffold estiver
pronto — ela assume que `I<UseCase>UseCase` já existe.

## 0. Levantar requisitos

Antes de gerar qualquer arquivo, confirme com o usuário (pergunte o que
faltar — não assuma):

1. **Nome do use case** (ex. `GetCustomerByCpf`) e **feature/aggregate**
   correspondente (ex. `Customer`) — decide os nomes de pasta e classe.
2. **Parâmetros de entrada**: nome, tipo, regra de negócio relevante (a
   validação de formato/input de uma eventual tool MCP fica a cargo da
   skill `new-mcp-tool`, mas o *shape* do Request precisa ser definido
   aqui).
3. **Dados de saída**: campos do Response (nomes + tipos primitivos).
4. **Chamada externa**: é uma nova rota na API existente (`INttBankApi`,
   `Infrastructure/HttpClients/`) ou reaproveita uma chamada já existente?
   Se nova, qual método HTTP + rota?
5. **Erro de domínio**: precisa de um novo erro em `DomainErrors.cs` (ex.
   `NotFound`) ou reaproveita um existente (ex.
   `DomainErrors.CustomerError.NotFound`)?

Não prossiga para os passos seguintes com informação assumida — se algo
crítico não foi dito, pergunte.

## 1. Domain (`src/main/Domain/`)

Só se necessário (novo agregado, novo tipo de erro, ou campo novo em um
Result model já existente):

- Erro novo: adicionar classe estática aninhada em `Errors/DomainErrors.cs`
  seguindo o padrão de `DomainErrors.CustomerError`.
- Result model novo (ou campo novo num existente) em
  `Results/<Aggregate>Result.cs` se o dado retornado pela API externa ainda
  não tem representação de domínio.

Regra: esta camada não pode referenciar ASP.NET Core, o SDK MCP, Refit ou
Mapster (ver `Domain/CLAUDE.md`).

## 2. Application (`src/main/Application/`)

Criar `Application/<Feature>/<UseCase>/` com:

- `<UseCase>Request.cs` — `public sealed record <UseCase>Request(...) : IBaseRequest;`
- `<UseCase>Response.cs` — `public sealed record <UseCase>Response(...);` com campos flat/primitivos.
- `I<UseCase>UseCase.cs` — `public interface I<UseCase>UseCase : IUseCase<<UseCase>Request, <UseCase>Response>;`
- `<UseCase>UseCase.cs` — implementação:
  - Primary constructor injetando `I<Aggregate>Repository` (ou o repositório existente).
  - Chama o repositório, mapeia o `Result<DomainModel>` para `Result<Response>` (via Mapster, `IRegister`).
  - Logging via `[LoggerMessage]` — adicionar entradas em
    `Application/Extensions/LogMessagesExtensions.cs`. Se o identificador
    de negócio não for `int` (ex. CPF em `string`), adicione um **overload
    novo** de `LogNotFound` (ou uma mensagem própria) em vez de forçar
    conversão para o overload existente — o overload atual só aceita
    `string className, string methodName, int identifier`.
  - Retorno sempre `Result<T>`, nunca lança exception para erro de negócio esperado.
- `<UseCase>Mapper.cs` — `IRegister` do Mapster mapeando Domain Result model → Response.

Se precisar de um repositório novo (não existe ainda para esse agregado):
adicionar a interface em `Application/Abstractions/Repositories/I<Aggregate>Repository.cs`.

## 3. Infrastructure (`src/main/Infrastructure/`)

Só se o passo 2 introduziu um repositório novo ou uma chamada externa nova:

- Se a chamada é numa API já usada: adicionar o método na interface Refit
  existente (`HttpClients/INttBankApi.cs`).
- Se é uma API nova: criar uma nova interface Refit em `HttpClients/` seguindo
  o mesmo padrão (registro de resiliência/auth em
  `Extensions/HttpClientExtensions.cs`).
- Implementar/estender `Repositories/<Aggregate>Repository.cs` chamando o
  client Refit e mapeando a resposta HTTP para o Result model do Domain
  (ex. capturar `ApiException` com `HttpStatusCode.NotFound` e retornar `null`).
- Registrar o repositório (se novo) + o caso de uso em
  `Extensions/AppServicesExtensions.cs` — este é o único lugar onde isso deve
  ser registrado.

## 4. Verificação

1. `cd src && dotnet build NttBank.Mcp.slnx` — a build deve compilar sem
   novos erros/warnings introduzidos pelo use case novo. (Débito técnico
   pré-existente dos 3 erros `S8969` é esperado e não é responsabilidade
   desta skill corrigir.)
2. Conferir que:
   - O use case retorna sempre `Result<T>` (nenhum `throw` para erro de
     negócio esperado).
   - Logging usa `[LoggerMessage]`, nunca `ILogger.Log*` direto.
   - Mapster tem `IRegister` explícito cobrindo todo campo do Response.
3. Resumir para o usuário os arquivos criados/alterados por camada, e
   avisar que, se o use case precisa ser exposto ao modelo, o próximo passo
   é rodar a skill `new-mcp-tool`.
