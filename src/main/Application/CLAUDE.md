# Application

Casos de uso: orquestram Domain + abstrações de repositório (nunca
implementações concretas de Infrastructure).

Regras gerais estão em `/CLAUDE.md` (raiz). Este arquivo cobre só o que é
específico desta camada.

## Estrutura por feature

Cada caso de uso vive em `Application/<Feature>/<UseCase>/` com estes
arquivos (ver `Customers/GetCustomer/` como referência):

- `<UseCase>Request.cs` — `record` que implementa `IBaseRequest`.
- `<UseCase>Response.cs` — `record` com dados primitivos/flat.
- `<UseCase>Mapper.cs` — `IRegister` do Mapster mapeando o Result model do
  Domain para o Response.
- `I<UseCase>UseCase.cs` — estende
  `Application/Abstractions/UseCases/IUseCase<TRequest, TResponse>`.
- `<UseCase>UseCase.cs` — implementação.

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
