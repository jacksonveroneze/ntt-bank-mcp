# Domain

Núcleo do domínio. **Zero dependência de outras camadas** — a única
referência externa é o pacote `JacksonVeroneze.NET.Result` (tipo `Result<T>`
usado em toda a solução para representar sucesso/falha sem exceptions).

Regras gerais estão em `/CLAUDE.md` (raiz). Este arquivo cobre só o que é
específico desta camada.

## Erros de domínio

Catálogo estático de erros por agregado em `Errors/DomainErrors.cs`, ex.
`DomainErrors.CustomerError.NotFound`. Ao modelar um novo agregado/feature,
adicione uma classe estática aninhada nova (`DomainErrors.<Agregado>Error`)
em vez de espalhar erros ad-hoc pelo código.

## Result models

Modelos em `Results/` (ex. `CustomerResult`) representam dados vindos de
fora (Infrastructure) já traduzidos para o vocabulário do domínio — não são
DTOs de transporte nem entidades de banco.

## Regra explícita

Esta camada **nunca** referencia ASP.NET Core, o SDK MCP
(`ModelContextProtocol.*`), Refit, Mapster ou qualquer pacote de
infraestrutura. Se uma nova classe aqui parecer precisar de um desses
pacotes, o código provavelmente pertence a Application ou Infrastructure.
