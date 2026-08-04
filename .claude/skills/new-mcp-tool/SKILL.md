---
name: new-mcp-tool
description: Use quando o usuário pedir para criar/adicionar uma nova MCP tool
  neste repositório.
---

# new-mcp-tool

Scaffold de uma MCP tool na camada **Mcp/Api**, seguindo o padrão de
`Tools/Customers/GetCustomerByIdTool.cs` e as regras de `/CLAUDE.md` (raiz —
**incluindo a seção de segurança MCP global**) e do `CLAUDE.md` de `Mcp`. **Leia
esses arquivos antes de gerar código** — esta skill assume as convenções deles e
não as repete.

**Escopo:** cria só artefatos da camada Mcp/Api (tool, validator, registro,
policy/scope). Não cria use cases, Domain nem Infrastructure — se algum for
necessário, **alerta e para**.

**Fonte da verdade:** os `CLAUDE.md` mandam, não o código existente. Onde o
exemplo-âncora (`GetCustomerByIdTool.cs`) divergir de um `CLAUDE.md`, siga o
`CLAUDE.md` e **liste a divergência como alerta** (ver §13 da raiz) — é
referência de estrutura, não de conformidade.

## 0. Requisitos (pergunte o que faltar, não assuma)

1. **Nome da tool** (snake_case, ex. `get_customer_by_id`) + **feature/aggregate**.
2. **Use case** que a tool envolve (ver precondição).
3. **Parâmetros**: nome, tipo e a `[Description]` em **inglês** — é superfície de
   decisão do modelo, não só documentação.
4. **Autorização**: reaproveita policy existente ou precisa de escopo novo?

**Precondição:** o use case (`I<Feature>UseCase` + `<Feature>Request`/`Response`
e o mapeamento) já deve existir em `Application`. Se não existir, **alerte e
pare** (rode a skill `new-usecase` antes) — a tool é fina, só orquestra o use case.

## 1. Tool — escopo principal (`Tools/<Feature>/`)

Crie seguindo o passo a passo do `CLAUDE.md` de Mcp (o *como* está lá; use
`GetCustomerByIdTool.cs` como referência de estrutura).

Artefatos a produzir:

- [ ] Tool `sealed` com `[McpServerToolType]`, primary constructor injetando
  `IValidator<TRequest>` + `I<Feature>UseCase`
- [ ] Constantes name/title/desc (`#region constants`); desc em inglês
- [ ] Método `[McpServerTool]` + `[Description]` por parâmetro + `[Authorize(Policy = ...)]`
- [ ] Validator em `Validators/` (FluentValidation)
- [ ] Registro em `Extensions/McpExtensions.cs` (`.WithTools<NovaTool>()`)
- [ ] Policy/scope em `Security/AuthorizationPolicies.cs` + `AuthorizationScopes.cs` (só se necessário)

**Segurança (não pule — fácil de esquecer):**

- Validar server-side **sempre antes** de chamar o use case
  (`validation.ToCallToolResultError()` se inválido). Descrições e tipos do
  schema são hints para o modelo, não controle de segurança — trate todo
  argumento como não confiável (prompt injection indireta).
- `[Authorize]` obrigatório em toda tool; least privilege; nunca reaproveitar
  policy de outra feature sem revisar o escopo; nunca policy catch-all.
- Erros nunca vazam exception interna — reaproveite `ResultExtensions` /
  `ValidationResultExtensions` / `McpToolResult` + `ExceptionToolFilter`; não
  crie conversão nova.

## 2. Use case / Domain / Infrastructure

Fora do escopo. Se a tool exigir use case novo, artefato de Domain ou de
Infrastructure, **alerte e pare**.

## 3. Verificação

1. Valide contra **todas** as diretrizes do `CLAUDE.md` de Mcp (fonte da verdade):
   `[Authorize]` presente, validação antes do use case, conversão via envelope
   padrão, tool registrada em `McpExtensions.cs`, descrição em inglês.
2. Confirme que nada foi criado fora de Mcp/Api, que a precondição (use case)
   estava satisfeita e que **toda divergência código↔`CLAUDE.md` foi listada
   como alerta** (implementação seguiu o MD).
3. Resuma os arquivos criados/alterados e os alertas emitidos.