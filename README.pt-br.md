# ModelWrapper ![Nuget](https://img.shields.io/nuget/v/ModelWrapper)

**ModelWrapper** é uma biblioteca C# (.NET) para Web APIs ASP.NET Core. Ela encapsula o modelo de domínio em ViewModels de requisição/resposta e converte **comandos** da query string (filtro, busca, ordenação, paginação, seleção, agregação) em operações sobre `IQueryable`.

| | |
|---|---|
| **Autor** | Ítalo Silveira |
| **Licença** | MIT |
| **NuGet** | [ModelWrapper](https://www.nuget.org/packages/ModelWrapper) |
| **Target** | .NET 10 (`net10.0`) |

[English README](README.md) · **[Documentação completa →](docs/README.md)**

---

## Por que usar?

- **`WrapRequest<TModel>`** — faz bind de body, form, query e route em um único objeto; reutiliza as propriedades de `TModel`; expõe os comandos de query.
- **`WrapResponse<TModel>`** — envelope padrão de resposta com dados, contagem e metadados da requisição.
- **Comandos de query** — filtros (`name_Contains`, `price_GreaterThan`, …), busca (`query`), ordenação, paginação, seleção de campos e agregações — aplicados com extensões como `FullSearch`.
- **Helpers de CRUD** — `Post()`, `Put()`, `Patch()` copiam valores bound para a entidade.

---

## Instalação rápida

```bash
dotnet add package ModelWrapper
```

```csharp
using ModelWrapper.Middleware;

builder.Services.AddModelWrapper();
```

---

## Documentação

| Seção | Conteúdo |
|-------|----------|
| [Primeiros passos](docs/getting-started.md) | Instalação, DI e primeiro controller |
| [Configuração](docs/configuration.md) | Opções globais e por requisição |
| [WrapRequest](docs/wrap-request.md) / [WrapResponse](docs/wrap-response.md) | Tipos centrais |
| [Comandos](docs/commands/README.md) | Todos os parâmetros de query com exemplos |
| [CRUD](docs/crud.md) | Post / Put / Patch |
| [Exemplos](docs/examples.md) | API completa de referência |

Comece por: **[docs/README.md](docs/README.md)**

---

## Contribuição

Issues e pull requests são bem-vindos no [GitHub](https://github.com/isilveira/ModelWrapper).

## Licença

Este projeto está sob a [licença MIT](LICENSE).
