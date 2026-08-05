---
name: new-repository
description: Use quando o usuário pedir para criar/adicionar um repositório
  novo (abstração em Application + implementação em Infrastructure) e,
  quando necessário, os métodos correspondentes na interface Refit
  INttBankApi, neste repositório.
---

# new-repository

Scaffold do primeiro elo da cadeia repositório → use case → tool: cria o
Domain Result (se ainda não existir), a interface do repositório em
`Application/Abstractions/Repositories/`, a implementação em
`Infrastructure/Repositories/`, os métodos correspondentes em
`Infrastructure/HttpClients/INttBankApi.cs` e o registro em
`Infrastructure/Extensions/AppServicesExtensions.cs`. Use
`ICustomerRepository`/`CustomerRepository` e `IAccountRepository`/
`AccountRepository` como referência de estrutura, e os `CLAUDE.md` de
`Domain`, `Application` e `Infrastructure` como fonte da verdade — leia-os
antes de gerar código.

**Escopo:** cria só Domain Result + repositório (Application abstraction +
Infrastructure implementation) + rota(s) Refit + registro em DI. Não cria
use case, tool MCP, validator nem `DomainErrors` — se algo disso for pedido
junto, **crie o repositório, alerte que o restante é escopo de
`new-usecase`/`new-mcp-tool`, e pare por aí**.

**Fonte da verdade:** os `CLAUDE.md` mandam, não o código existente. Onde o
exemplo-âncora (`CustomerRepository`/`AccountRepository`) divergir de um
`CLAUDE.md`, siga o `CLAUDE.md` e **liste a divergência como alerta** (ver
§13 da raiz) — é referência de estrutura, não de conformidade.

## 0. Requisitos (pergunte o que faltar, não assuma)

1. **Aggregate/feature** (ex. `Card`, `Loan`) e se o repositório é novo ou
   está ganhando um método a mais em um repositório existente.
2. **Rota(s) HTTP**: verbo, path exato, query params (se houver) — o
   usuário deve fornecer, não adivinhe a rota upstream.
3. **Shape do retorno**: item único (`TResult?`, nullable) ou coleção
   (`IReadOnlyCollection<TResult>`)? Isso decide se a implementação usa
   try/catch de `ApiException`/`NotFound` ou repasse direto.
4. **Schema do Result**: se o `Domain/Results/<X>Result.cs` ainda não
   existir, peça os campos e tipos (equivalente ao schema SQL) — não
   invente colunas. Para colunas `varchar` que representam categorias
   fechadas, só crie `enum` em `Domain/Enums/` se o usuário fornecer os
   valores válidos; senão, use `string?`.

## 1. Domain — só se o Result ainda não existir

`Domain/Results/<X>Result.cs`: `sealed record`, `[JsonPropertyName]` em
cada propriedade (camelCase), nullable conforme o schema informado. Sem
lógica, sem dependência de outras camadas (`Domain/CLAUDE.md`).

## 2. Infrastructure — Refit

Adicione o(s) método(s) em `INttBankApi.cs` dentro do `#region` que
corresponde ao **primeiro segmento do path** (não ao tipo de retorno) — ex.
uma rota `/v1/customers/{customerId}/X` entra em `#region Customer`, mesmo
retornando `XResult`, pelo mesmo critério já usado para
`GetCustomerAccountsAsync`. Se o `#region` do aggregate raiz da URL ainda
não existir, crie um novo.

## 3. Application — abstração do repositório

`Application/Abstractions/Repositories/I<Aggregate>Repository.cs`: só
assinatura(s) de método, sem lógica. Nomenclatura `Get<X>ByIdAsync` (item
único) / `Get<X>sBy<Parent>IdAsync` (coleção) — **não** use `List*` aqui;
essa convenção é só da camada de use case (ver `Application/CLAUDE.md` e o
histórico de rename `GetCustomerAccounts` → `ListCustomerAccounts`).

## 4. Infrastructure — implementação + DI

`Infrastructure/Repositories/<Aggregate>Repository.cs`:
`[ExcludeFromCodeCoverage]`, `sealed class` com primary constructor
(`INttBankApi api`).

- Item único: `try/catch (ApiException ex) when (ex.StatusCode is
  HttpStatusCode.NotFound) { return null; }`.
- Coleção: repasse direto, sem try/catch.

Registre em `Infrastructure/Extensions/AppServicesExtensions.cs`
(`services.AddScoped<I<Aggregate>Repository, <Aggregate>Repository>();`),
dentro de um `#region <Aggregate>` — é o único ponto de registro
(`Infrastructure/CLAUDE.md`).

## 5. Verificação

1. Valide contra `Domain/CLAUDE.md`, `Application/CLAUDE.md` (seção de
   repositórios) e `Infrastructure/CLAUDE.md` (registro de serviços) — são
   a fonte da verdade.
2. Confirme que nada foi criado em `Api` nem use case em `Application`, e
   que toda divergência código↔`CLAUDE.md` foi listada como alerta.
3. `dotnet build` para garantir que compila.
4. Resuma os arquivos criados/alterados e os alertas emitidos.
