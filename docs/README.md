# Documentação ModelWrapper

Biblioteca C# (.NET) para Web APIs que unifica ViewModels de requisição e resposta em torno do modelo de domínio (`TModel`), convertendo parâmetros de query string em operações sobre `IQueryable`.

| | |
|---|---|
| **Pacote NuGet** | [ModelWrapper](https://www.nuget.org/packages/ModelWrapper) |
| **Versão atual** | 10.0.3.0 |
| **Target** | `net10.0` |
| **Licença** | MIT |
| **Autor** | Ítalo Silveira |

---

## Índice

| Documento | Conteúdo |
|-----------|----------|
| [Primeiros passos](getting-started.md) | Instalação, registro no DI e uso básico |
| [Configuração](configuration.md) | Opções globais e por requisição |
| [WrapRequest](wrap-request.md) | Binding, propriedades e configuração do request |
| [WrapResponse](wrap-response.md) | Envelope de resposta padronizado |
| [Comandos](commands/README.md) | Visão geral dos comandos de query |
| [Filtro](commands/filter.md) | Critérios `name`, `name_Contains`, `price_GreaterThan`, etc. |
| [Busca](commands/search.md) | `query`, `queryStrict`, `queryPhrase` |
| [Ordenação](commands/ordination.md) | `order`, `orderBy` |
| [Paginação](commands/pagination.md) | `pageSize`, `pageNumber` |
| [Seleção](commands/select.md) | `responseProperties` |
| [Agregação](commands/aggregation.md) | `aggregateBy`, `AggregateMin/Max/Sum/Avg` |
| [FullSearch](commands/full-search.md) | Pipeline completo Filter → Search → … → Select |
| [CRUD](crud.md) | Extensões `Post()`, `Put()`, `Patch()` |
| [Exemplos](examples.md) | Controllers e URLs de referência |

---

## O que o ModelWrapper resolve

Em APIs REST é comum duplicar propriedades entre entidade, request e response. Com ModelWrapper:

1. Você herda de `WrapRequest<TModel>` e recebe automaticamente as propriedades de `TModel`, além dos **comandos** via query string.
2. Você devolve dados com `WrapResponse<TModel>`, incluindo metadados da requisição (filtros aplicados, paginação, etc.).
3. Extensões de `IQueryable` aplicam filtro, busca, ordenação, paginação, projeção e agregação sem montar expressões manualmente.

```csharp
var data = _db.Products
    .AsQueryable()
    .FullSearch(request, out long count)
    .ToList();

return new WrapResponse<Product>(request, data, resultCount: count);
```

Exemplo de URL equivalente:

```http
GET /products?pageSize=20&pageNumber=1&orderBy=price&order=descending
    &name_Contains=phone&price_GreaterThanOrEqual=100
    &query=samsung&responseProperties=id&responseProperties=name&responseProperties=price
```
