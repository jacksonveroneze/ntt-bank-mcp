---
name: commit
description: Use quando o usuário pedir para commitar o trabalho feito (ex.
  "faz o commit", "commita essa feature"). Analisa as mudanças, gera uma
  mensagem em Conventional Commits com descrição em português, faz staging
  consciente dos arquivos e commita — sempre após confirmação explícita. Não
  faz push.
---

# commit

Gera um commit padronizado do que foi feito, seguindo Conventional Commits com
descrição em **português**. Faz staging + commit **após confirmação explícita**
do usuário. **Não faz push** e **não faz `git add .`** cego.

**Escopo:** stage dos arquivos relevantes + `git commit`. Fora do escopo: push,
merge, rebase, criar branch, alterar histórico.

## 0. Inspecionar o estado (antes de propor qualquer coisa)

1. `git status` e `git diff` (working tree) + `git diff --staged` — entender o
   que mudou e o que já está staged.
2. Se **não houver mudança** a commitar, avise e pare.
3. **Não prossiga com staging automático de tudo.** Identifique o conjunto de
   arquivos que compõem *uma* unidade lógica de mudança.

## 1. Guard-rails (checar antes de commitar)

- **Sem segredos:** nunca stage `.env` com credenciais reais, connection
  strings, tokens, certificados ou secrets (raiz §10). Se aparecerem no diff,
  **alerte e pare**.
- **Um commit = uma intenção:** se o diff mistura feature + refatoração +
  formatação não relacionadas (raiz §3), **alerte** e proponha dividir em
  commits separados, em vez de um commit "guarda-chuva".
- **Staging explícito:** stage só os arquivos da unidade lógica
  (`git add <paths>`), nunca `git add .` / `-A` sem revisar a lista.

## 2. Montar a mensagem

Formato:

```
<tipo>(<escopo>): <descrição em português, imperativo, minúsculo, sem ponto final>
```

- **`<tipo>`** (mantém a keyword em inglês): `feat`, `fix`, `refactor`, `docs`,
  `test`, `chore`, `perf`, `build`, `ci`, `style`.
- **`<escopo>`** (opcional): a feature/aggregate ou camada afetada, minúsculo
  (ex. `customer`, `application`, `mcp`).
- **`<descrição>`**: resumo do que foi feito, em português, no imperativo
  (~50 caracteres, limite 72).
- **Corpo** (opcional, só quando agrega): o *quê* e o *porquê*, não o *como*.
  Separado por uma linha em branco.

Exemplos:

- `feat(customer): adiciona use case GetCustomerByCpf`
- `feat(mcp): adiciona tool get_customer_by_cpf`
- `fix(application): corrige overload de LogNotFound para identificador string`
- `refactor(customer): torna GetCustomerMapper sealed`

Não adicione rodapé de atribuição (co-authored / gerado por IA) por padrão.

## 3. Confirmação (gate obrigatório)

Apresente ao usuário, e **aguarde "ok" antes de commitar**:

1. A **lista de arquivos** que serão staged.
2. A **mensagem** completa (assunto + corpo, se houver).
3. Alertas emitidos no passo 1 (secrets, mistura de escopo), se houver.

Se o usuário pedir ajuste, refaça e confirme de novo. Não commite sem o "ok".

## 4. Executar

Após confirmação:

1. `git add <paths explícitos>`
2. `git commit -a -m "<assunto>"` (e `-m "<corpo>"` se houver).
3. Rode `git status` e reporte: hash curto, arquivos commitados e o que ficou
   de fora (unstaged/pendente). **Sem push.**