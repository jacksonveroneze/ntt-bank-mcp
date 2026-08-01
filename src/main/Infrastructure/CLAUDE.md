# Infrastructure

Implementações concretas: repositórios, clientes HTTP externos, DI, logging,
telemetria, configuração, autenticação/autorização.

Regras gerais estão em `/CLAUDE.md` (raiz). Este arquivo cobre só o que é
específico desta camada.

## Registro de serviços

`Extensions/AppServicesExtensions.cs` é o ponto único para registrar um novo
repositório + caso de uso na injeção de dependência. Ao adicionar uma
feature nova, registre ali (não espalhe `services.AddScoped<...>()` por
outros arquivos).

## Padrão de extensões

Cada responsabilidade de bootstrap tem seu próprio arquivo em
`Extensions/*.cs` (a maioria marcada `[ExcludeFromCodeCoverage]` por ser
código de wiring/config, não lógica de negócio):

- `LoggingExtensions.cs` — Serilog.
- `AuthenticationExtensions.cs` / `AuthorizationExtensions.cs` — JWT Bearer +
  policies baseadas em claim de escopo.
- `HttpClientExtensions.cs` — Refit + Duende client-credentials (OAuth2) +
  `AddStandardResilienceHandler()` (Polly).
- `OpenTelemetryExtensions.cs` — métricas (Prometheus) + tracing.
- `AppConfigurationExtensions.cs` — binding de config via `IOptions<T>`.
- `CultureExtensions.cs` — força cultura `pt-BR`.
- `MapperExtensions.cs` — configuração global do Mapster.

Ao adicionar uma nova integração externa (novo cliente HTTP, novo provedor
de telemetria, etc.), siga o mesmo padrão: um arquivo de extensão dedicado,
não lógica inline em `Program.cs` ou nos builders.

## Regras

- Clientes HTTP externos sempre via Refit (interface tipada, ex.
  `INttBankApi`) + resiliência (`AddStandardResilienceHandler`) — nunca
  `HttpClient` cru sem policy de resiliência.
- Configuração nova sempre via `IOptions<T>` +
  `.Bind().ValidateDataAnnotations().ValidateOnStart()`. Nunca ler
  `IConfiguration` diretamente fora desta camada.
