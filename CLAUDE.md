# ntt-bank-mcp

Servidor MCP (Model Context Protocol) bancário em .NET 10 / C# 14, ASP.NET
Core, construído sobre o SDK oficial `ModelContextProtocol.AspNetCore`. Expõe
tools que um modelo (Claude) pode invocar para consultar dados bancários.

Não há README no repositório — este arquivo (e os `CLAUDE.md` de cada camada)
cobrem o papel de onboarding.

## Arquitetura

Clean Architecture com 4 projetos e direção de dependência única:

```
Mcp → Infrastructure → Application → Domain
```

- **Domain** (`src/main/Domain`) — núcleo do domínio, zero dependências de
  outras camadas. Ver `src/main/Domain/CLAUDE.md`.
- **Application** (`src/main/Application`) — casos de uso, orquestra Domain +
  abstrações de repositório. Ver `src/main/Application/CLAUDE.md`.
- **Infrastructure** (`src/main/Infrastructure`) — implementações concretas
  (HTTP clients, DI, logging, telemetria, config). Ver
  `src/main/Infrastructure/CLAUDE.md`.
- **Mcp** (`src/main/Mcp`) — camada de apresentação, hospeda o servidor MCP e
  expõe as tools. Ver `src/main/Mcp/CLAUDE.md` (leitura obrigatória antes de
  criar uma nova tool).

Solução: `src/NttBank.Mcp.slnx` (formato `.slnx`, não `.sln`). Todos os
projetos miram `net10.0` com `Nullable` habilitado (`src/Directory.Build.props`).

## Convenções gerais de código

- Namespaces file-scoped.
- Primary constructors para injeção de dependência (ex.
  `GetCustumerByIdTool(IValidator<GetCustomerRequest> validator, IGetCustomerUseCase useCase)`).
- Classes/records `sealed` por padrão.
- Interfaces prefixadas com `I`, PascalCase.
- Guard clauses com `ArgumentNullException.ThrowIfNull`.
- Uso misto de C# 14 `extension` member blocks (ex.
  `Mcp/Extensions/McpServerBuilderExtensions.cs`,
  `Infrastructure/Extensions/OpenTelemetryExtensions.cs`) e extension methods
  clássicos — siga o estilo já usado no arquivo que estiver editando.
- `var` só para tipos embutidos/óbvios (regra do `.editorconfig`).

## Build, analisadores e testes

- `TreatWarningsAsErrors=true`, `AnalysisMode=All`, `AnalysisLevel=latest`,
  `EnforceCodeStyleInBuild=true` (`src/Directory.Build.props`).
- Analisadores ativos: SonarAnalyzer.CSharp, Meziantou.Analyzer,
  Microsoft.CodeAnalysis.BannedApiAnalyzers,
  Microsoft.VisualStudio.Threading.Analyzers.
- `src/BannedSymbols.txt` proíbe `DateTime.Now` / `DateTimeOffset.Now` (usar
  variantes UTC) e `Newtonsoft.Json` (usar `System.Text.Json`).
- `.editorconfig` (`src/.editorconfig`): 4 espaços, LF, 100 colunas máx.
- Gerenciamento central de versões de pacote:
  `src/Directory.Packages.props` (`ManagePackageVersionsCentrally=true`).

Comandos (executar a partir de `src/`):

```bash
dotnet build NttBank.Mcp.slnx
dotnet test NttBank.Mcp.slnx
```

Não existe projeto de testes registrado na solução ainda (`src/tests/` e
`tests/` só têm `.gitkeep`), embora `Directory.Packages.props` já declare o
stack pretendido: xunit, Moq, FluentAssertions, coverlet.

## Débito técnico conhecido (documentado, não corrigido)

- O build atual falha com 3 erros Sonar `S8969` (remover operador
  null-forgiving) em `Infrastructure/Extensions/LoggingExtensions.cs:25`,
  `HttpClientExtensions.cs:31` e `OpenTelemetryExtensions.cs:40`.
- Mensagens de erro padrão em pt-BR estão hardcoded em
  `Mcp/Util/McpToolResult.cs` e `Mcp/Extensions/ResultExtensions.cs`,
  misturadas com tool descriptions e código em inglês no restante do projeto.
  Padronizar para um único idioma é recomendado numa tarefa futura dedicada.

## Segurança MCP (regras globais — valem para toda tool nova)

- **Least privilege**: toda tool nova declara sua própria
  `[Authorize(Policy = ...)]`. Nunca reaproveitar a policy de outra feature
  sem revisar se o escopo realmente se aplica.
- **Input do modelo não é confiável**: todo argumento recebido via MCP deve
  ser validado no servidor (FluentValidation) antes de qualquer uso — a
  descrição do parâmetro (`[Description]`) não é uma garantia de formato ou
  intenção, é só um hint para o modelo.
- **Nunca vazar detalhes internos** (stack trace, connection string, mensagens
  de exceção cruas) em `CallToolResult` de erro. Usar sempre o envelope
  padronizado (`McpToolResult` / `ResultExtensions`).
- **Descrições de tool/parâmetro são superfície de decisão do modelo**: é a
  partir delas que o modelo decide quando e como invocar a tool. Uma
  descrição imprecisa ou ambígua é um risco de uso indevido, não só um
  problema estético.
