---
name: new-mcp-tool
description: Use quando o usuário pedir para expor um caso de uso já existente (ou recém-criado via new-usecase) como uma nova MCP tool neste repositório (ntt-bank-mcp). Faz o scaffold da camada Mcp (tool, validator, policy se necessária, registro) seguindo o padrão da tool existente get_customer_by_id. Não cria o use case — se ele ainda não existe, use a skill new-usecase primeiro.
---

# new-mcp-tool

Scaffold de uma nova MCP tool na camada de apresentação, expondo um
`I<UseCase>UseCase` que já existe em Application (criado manualmente ou
via skill `new-usecase`). Segue exatamente o padrão de
`Mcp/Tools/Customer/GetCustumerByIdTool.cs` e as regras já documentadas em
`/CLAUDE.md` (raiz, seção de segurança MCP) e `Mcp/CLAUDE.md`. Leia esses
arquivos antes de gerar código.

Se o use case ainda não existe, pare e rode a skill `new-usecase` primeiro
— esta skill não cria Request/Response/UseCase em Application, nem toca em
Domain ou Infrastructure.

## 0. Levantar requisitos

Antes de gerar qualquer arquivo, confirme com o usuário (pergunte o que
faltar — não assuma):

1. **Nome da tool** (snake_case, ex. `get_customer_by_cpf`) e o
   `I<UseCase>UseCase` que ela vai expor — confirme que ele já existe em
   `Application/<Feature>/<UseCase>/`.
2. **Parâmetros do método MCP**: nome, tipo, regra de validação de input
   (ex. `cpf: string`, formato/regex) — mapeados para os campos do
   `<UseCase>Request`.
3. **Autorização**: reaproveita uma policy/escopo existente
   (`Security/AuthorizationPolicies.cs`) ou precisa de uma nova (ex.
   `accounts.read`)? Nunca reaproveitar uma policy de outra feature "para
   simplificar" — se o escopo for diferente, criar uma nova.

Não prossiga para os passos seguintes com informação assumida — se algo
crítico (nome da tool, parâmetros, autorização) não foi dito, pergunte.

## 1. Mcp (`src/main/Mcp/`)

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

## 2. Verificação

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
3. Resumir para o usuário os arquivos criados/alterados.
