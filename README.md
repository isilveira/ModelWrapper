# ModelWrapper ![Nuget](https://img.shields.io/nuget/v/ModelWrapper)

**ModelWrapper** is a C# (.NET) library for ASP.NET Core Web APIs. It wraps domain models in request/response ViewModels and turns query-string **commands** (filter, search, sort, page, select, aggregate) into `IQueryable` operations.

| | |
|---|---|
| **Author** | Ítalo Silveira |
| **License** | MIT |
| **NuGet** | [ModelWrapper](https://www.nuget.org/packages/ModelWrapper) |
| **Target** | .NET 10 (`net10.0`) |

[Documentação em português](README.pt-br.md) · **[Full documentation →](docs/README.md)**

---

## Why ModelWrapper?

- **`WrapRequest<TModel>`** — binds body, form, query, and route into one object; reuses `TModel` properties; exposes query commands.
- **`WrapResponse<TModel>`** — standard response envelope with data, result count, and echoed request metadata.
- **Query commands** — filter (`name_Contains`, `price_GreaterThan`, …), search (`query`), order, pagination, field selection, and aggregations — applied via extensions such as `FullSearch`.
- **CRUD helpers** — `Post()`, `Put()`, `Patch()` copy bound values onto entities.

---

## Quick install

```bash
dotnet add package ModelWrapper
```

```csharp
using ModelWrapper.Middleware;

builder.Services.AddModelWrapper();
```

---

## Documentation

| Section | Description |
|---------|-------------|
| [Getting started](docs/getting-started.md) | Install, DI registration, first controller |
| [Configuration](docs/configuration.md) | Global and per-request options |
| [WrapRequest](docs/wrap-request.md) / [WrapResponse](docs/wrap-response.md) | Core types |
| [Commands](docs/commands/README.md) | All query parameters with examples |
| [CRUD](docs/crud.md) | Post / Put / Patch |
| [Examples](docs/examples.md) | End-to-end API samples |

Start here: **[docs/README.md](docs/README.md)**

---

## Contributing

Issues and pull requests are welcome on [GitHub](https://github.com/isilveira/ModelWrapper).

## License

This project is licensed under the [MIT License](LICENSE).
