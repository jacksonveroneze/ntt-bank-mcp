---
name: new-mcp-tool
description: Use quando o usuário pedir para criar/adicionar uma nova MCP tool neste repositório (ntt-bank-mcp). Faz o scaffold do vertical slice completo (Domain, Application, Infrastructure, Mcp) seguindo o padrão da tool existente get_customer_by_id, sem que o usuário precise repetir a arquitetura a cada pedido.
---

# new-mcp-tool

Scaffold de uma nova MCP tool completa, camada por camada, seguindo
exatamente o padrão de `Mcp/Tools/Customer/GetCustumerByIdTool.cs` e as
regras já documentadas em `/CLAUDE.md` e nos `CLAUDE.md` de cada camada
(`Domain`, `Application`, `Infrastructure`, `Mcp`). Leia esses arquivos antes
de gerar código — esta skill assume as convenções deles e não as repete
integralmente aqui.

## 0. Levantar requisitos

Antes de gerar qualquer arquivo, confirme com o usuário (pergunte o que
faltar — não assuma):

1. **Nome da tool** (snake_case, ex. `get_account_balance`) e **feature/
   aggregate** correspondente (ex. `Account`) — decide os nomes de pasta e
   classe.
2. **Parâmetros de entrada**: nome, tipo, regra de validação (ex.
   `accountId: int, > 0`).
3. **Dados de saída**: campos do response (nomes + tipos primitivos).
4. **Chamada externa**: é uma nova rota na API existente (`INttBankApi`,
   `Infrastructure/HttpClients/`) ou reaproveita uma chamada já existente?
   Se nova, qual método HTTP + rota?
5. **Erro de domínio**: precisa de um novo erro em `DomainErrors.cs` (ex.
   `NotFound`) ou reaproveita um existente (ex.
   `DomainErrors.CustomerError.NotFound`)?
6. **Autorização**: reaproveita uma policy/escopo existente
   (`Security/AuthorizationPolicies.cs`) ou precisa de um novo (ex.
   `accounts.read`)? Nunca reaproveitar uma policy de outra feature "para
   simplificar" — se o escopo for diferente, criar um novo.

Não prossiga para os passos seguintes com informação assumida — se algo
crítico (nome da tool, parâmetros, autorização) não foi dito, pergunte.

## 1. Domain (`src/main/Domain/`)

Só se necessário (novo agregado ou novo tipo de erro):

- Erro novo: adicionar classe estática aninhada em `Errors/DomainErrors.cs`
  seguindo o padrão de `DomainErrors.CustomerError`.
- Result model novo em `Results/<Aggregate>Result.cs` se o dado retornado
  pela API externa ainda não tem uma representação de domínio.

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
  - Logging via `[LoggerMessage]` (adicionar entradas em
    `Application/Extensions/LogMessagesExtensions.cs`, não usar `ILogger.Log*` direto).
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
- Implementar `Repositories/<Aggregate>Repository.cs` chamando o client Refit
  e mapeando a resposta HTTP para o Result model do Domain.
- Registrar o repositório + o caso de uso (DI) em
  `Extensions/AppServicesExtensions.cs` — este é o único lugar onde isso deve
  ser registrado.

## 4. Mcp (`src/main/Mcp/`)

1. Criar `Tools/<Feature>/<UseCase>Tool.cs`:
   - `sealed class` com `[McpServerToolType]`.
   - Primary constructor injetando `IValidator<<UseCase>Request>` +
     `I<UseCase>UseCase`.
   - `#region constants` com `ToolName` (snake_case), `ToolTitle`, `ToolDesc`
     (em **inglês**, precisa e específica — é o que o modelo usa para decidir
     invocar a tool) e uma descrição por parâmetro.
   - Método `public async Task<CallToolResult> ...Async(...)` com:
     - `[McpServerTool(Name = ..., Title = ...)]`
     - `[Description(...)]` em cada parâmetro
     - `[Authorize(Policy = AuthorizationPolicies.Xxx)]` — **obrigatório**,
       nunca omitir.
     - Corpo: monta o `Request` → `validator.ValidateAsync` → se inválido,
       `return validation.ToCallToolResultError();` → senão
       `await useCase.ExecuteAsync(...)` → `return result.ToCallToolResult();`
2. Criar o validator em `Validators/<UseCase>RequestValidator.cs`
   (FluentValidation, uma regra por parâmetro conforme levantado no passo 0).
3. Se necessário, adicionar policy/escopo novo em
   `Security/AuthorizationPolicies.cs` e `Security/AuthorizationScopes.cs`.
4. Registrar a tool em `Extensions/McpExtensions.cs`, encadeando
   `.WithTools<<UseCase>Tool>()` junto às demais.

## 5. Verificação

1. `cd src && dotnet build NttBank.Mcp.slnx` — a build deve compilar sem
   novos erros/warnings introduzidos pela tool nova. (Se a build já falhar
   por causa do débito técnico pré-existente documentado no `CLAUDE.md`
   raiz — os 3 erros `S8969` — isso é esperado e não é responsabilidade
   desta skill corrigir, a menos que o usuário peça explicitamente.)
2. Conferir que:
   - A tool tem `[Authorize]` com uma policy real (nenhuma tool sem
     autorização).
   - O validator cobre todo parâmetro de entrada.
   - Nenhuma string de erro nova em pt-BR foi introduzida em
     `McpToolResult.cs`/`ResultExtensions.cs` (seguir inglês, conforme nota
     de débito técnico do `Mcp/CLAUDE.md`).
3. Resumir para o usuário os arquivos criados/alterados por camada.
