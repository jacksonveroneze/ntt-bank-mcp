---
name: new-usecase
description: Use quando o usuário pedir para criar/adicionar um novo caso de uso
   (use case) neste repositório. Faz o scaffold apenas da camada Application,
   seguindo o padrão dos casos de uso existentes, e sinaliza (não cria) qualquer
   artefato necessário em Domain ou Infrastructure.
---

# new-usecase

Scaffold de um caso de uso na camada **Application**, seguindo o padrão de
`Application/Customers/GetCustomer/` e as regras de `/CLAUDE.md` (raiz) e do
`CLAUDE.md` de `Application`. **Leia esses arquivos antes de gerar código** —
esta skill assume as convenções deles e não as repete.

**Escopo:** cria só artefatos de `Application`. Não cria Domain, Infrastructure,
endpoints nem MCP tool — se algum for necessário, **alerta e para**.

## 0. Requisitos (pergunte o que faltar, não assuma)

1. **Nome do use case** (ex. `GetCustomerByCpf`) e **feature/aggregate** (ex. `Customer`).
2. **Entrada**: nome, tipo, regra de negócio relevante.
3. **Saída**: campos do Response (nomes + tipos primitivos).
4. **Erro de domínio** do caminho de falha (ex. `DomainErrors.CustomerError.NotFound`):
   existe → referencie; **não existe → alerte e pare** (`DomainErrors.cs` é Domain, fora do escopo).

**Precondição:** o repositório do aggregate (interface em
`Application/Abstractions/Repositories/` **e** implementação em `Infrastructure`)
já deve existir. Se não existir, **alerte e pare** — o use case não fecha só na
Application.

## 1. Domain / 3. Infrastructure

Fora do escopo. Não crie entidades, models, enums, entradas em `DomainErrors.cs`,
repositories ou http clients. Se algo for necessário, **gere um alerta e pare**.

## 2. Application — escopo principal

Crie `Application/<Feature>/<UseCase>/` seguindo o padrão e as regras do
`CLAUDE.md` de Application (o *como* está lá; use `GetCustomer/` como referência).

Artefatos a produzir:

- [ ] `<UseCase>Request.cs`
- [ ] `<UseCase>Response.cs` (flat/primitivos)
- [ ] `I<UseCase>UseCase.cs`
- [ ] `<UseCase>UseCase.cs` (primary constructor com a interface do repo; retorno sempre `Result<T>`)
- [ ] `<UseCase>Mapper.cs` (`IRegister` do Mapster, mapeamento explícito)
- [ ] Log em `Application/Extensions/LogMessagesExtensions.cs` via `[LoggerMessage]`

**Overload de log:** se o identificador **não** for `int` (ex. CPF `string`, como
no próprio `GetCustomerByCpf`), adicione um **overload novo** de `LogNotFound` —
o atual só aceita `(string className, string methodName, int identifier)`. Não
force conversão para `int`.

## 4. Verificação

1. Valide os arquivos contra **todas** as diretrizes do `CLAUDE.md` de Application
   — esta é a verificação canônica (o `CLAUDE.md` é a fonte da verdade).
2. Confirme que nada foi criado em Domain/Infrastructure, que os alertas devidos
   foram emitidos e que as precondições (repositório, `DomainError`) estavam satisfeitas.
3. Resuma os arquivos criados/alterados e os alertas emitidos.