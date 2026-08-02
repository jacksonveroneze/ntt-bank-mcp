# Application

Casos de uso: orquestram Domain + abstrações de repositório (nunca
implementações concretas de Infrastructure).

Regras gerais estão em `/CLAUDE.md` (raiz). Este arquivo cobre só o que é
específico desta camada.

## Estrutura por feature

Cada caso de uso vive em `Application/<Feature>/<UseCase>/` com estes
arquivos (ver `Customers/GetCustomer/` como referência):

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

## Regras

- Repositórios só via interface em `Application/Abstractions/Repositories/`.
  Application nunca referencia `Infrastructure` diretamente (a direção de
  dependência da solução não permitiria de qualquer forma, mas a interface é
  o ponto de extensão correto).
- Logging sempre via `[LoggerMessage]` source-generated — ver
  `Application/Extensions/LogMessagesExtensions.cs`. Não chamar
  `ILogger.LogInformation`/`LogError` etc. diretamente.
- Retorno de todo caso de uso é `JacksonVeroneze.NET.Result.Result<T>`.
  Exceptions não são usadas para erros de negócio esperados (ex. "não
  encontrado") — isso é um `Result` de falha com o `DomainErrors`
  correspondente.
- Mapster está configurado com `RequireExplicitMapping=true`
  (`Infrastructure/Extensions/MapperExtensions.cs`): todo mapeamento precisa
  de um `IRegister` explícito, não existe mapeamento implícito por convenção.
