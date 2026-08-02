# Mcp

Camada de apresentação: hospeda o servidor MCP via SDK oficial
`ModelContextProtocol.AspNetCore`, transporte HTTP stateless, autenticação
JWT Bearer + autorização por policy. É aqui que novas tools nascem.

Regras gerais estão em `/CLAUDE.md` (raiz), incluindo a seção de segurança
MCP global — leia antes de continuar. Este arquivo cobre o passo a passo
específico de criação de tools.

## Padrão de referência

`Tools/Customer/GetCustumerByIdTool.cs` é a tool existente e o padrão a
seguir (nome do arquivo tem um typo histórico — "Custumer" — não repita o
typo em tools novas).

## Passo a passo para criar uma nova tool

1. Nova pasta `Tools/<Feature>/`.
2. Classe `sealed` com `[McpServerToolType]`, construtor primário injetando
   `IValidator<TRequest>` (de `Application`) + `I<Feature>UseCase`.
3. Constantes de nome/título/descrição da tool numa `#region constants`:
   - `<Feature>ToolName` — nome da tool (snake_case, ex. `get_customer_by_id`).
   - `<Feature>ToolTitle` — título legível.
   - `<Feature>ToolDesc` — descrição em **inglês**, clara o bastante para o
     modelo decidir quando invocar a tool (ver seção de segurança no
     `CLAUDE.md` raiz — a descrição é superfície de decisão do modelo, não
     só documentação).
4. Método assíncrono público:
   - `[McpServerTool(Name = ..., Title = ...)]`
   - `[Description(...)]` em cada parâmetro
   - **sempre** `[Authorize(Policy = AuthorizationPolicies.Xxx)]` — nunca
     omitir, nunca reaproveitar policy de outra feature sem revisar o escopo.
5. No corpo do método: montar o `Request`, validar com
   `await validator.ValidateAsync(request, cancellationToken)` e retornar
   `validation.ToCallToolResultError()` se inválido — **antes** de chamar o
   caso de uso.
6. Chamar `await useCase.ExecuteAsync(request, cancellationToken)` e
   converter com `result.ToCallToolResult()`.
7. Registrar a tool em `Extensions/McpExtensions.cs`, encadeando
   `.WithTools<NovaTool>()` na configuração do `AddMcpServer(...)`.
8. Criar o validator correspondente em `Validators/` (FluentValidation).
9. Se a feature precisar de um escopo novo, adicionar em
   `Security/AuthorizationPolicies.cs` e `Security/AuthorizationScopes.cs`.

## Peças de suporte já existentes (reaproveitar, não duplicar)

- `Extensions/ResultExtensions.cs`, `Extensions/ValidationResultExtensions.cs`,
  `Util/McpToolResult.cs` — conversão padronizada de `Result<T>`/erros de
  validação para `CallToolResult` (envelope com `status` + `data`/`error`).
- `Filters/ExceptionToolFilter.cs` (via `AddExceptionFilter()` em
  `Extensions/McpServerBuilderExtensions.cs`) — captura exceptions não
  tratadas e evita que vazem para o cliente MCP.
- `Security/AuthorizationPolicies.cs` / `AuthorizationScopes.cs` — catálogo
  de policies/escopos existentes.

## Segurança (reforço específico desta camada)

- Nunca confiar em parâmetro vindo do cliente MCP sem validação (ex.
  `CustomerId > 0`) — a validação acontece sempre no passo 5, antes de
  qualquer chamada ao caso de uso.
- Toda tool tem sua própria policy de autorização — least privilege por
  escopo (ex. `customers.read` vs. um eventual `customers.write`). Nunca
  criar uma policy "catch-all" que sirva para múltiplas tools por
  conveniência.
- Erros nunca vazam exceptions internas — sempre pelo `ExceptionToolFilter` +
  envelope `McpToolResult`.
- Tratar todo argumento como não confiável mesmo quando "vem do modelo":
  conteúdo malicioso presente em contexto anterior da conversa pode
  manipular o modelo a invocar a tool com argumentos indevidos (prompt
  injection indireta). Validação server-side é a única defesa real —
  descrições e tipos no schema da tool são hints para o modelo, não
  controles de segurança.
