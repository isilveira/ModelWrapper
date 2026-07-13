# Comando: FullSearch

Atalho que encadeia todos os comandos na ordem correta para listagens.

**Extensão:** `source.FullSearch(request, out count)`  
**Namespace:** `ModelWrapper.Extensions.FullSearch`  
**Retorno:** `IQueryable<object>`

---

## Overloads

```csharp
IQueryable<object> FullSearch<TSource>(
    this IQueryable<TSource> source,
    WrapRequest<TSource> request,
    out int count) where TSource : class;

IQueryable<object> FullSearch<TSource>(
    this IQueryable<TSource> source,
    WrapRequest<TSource> request,
    out long count) where TSource : class;
```

Use `out long` quando o volume puder ultrapassar `int.MaxValue`.

---

## Pipeline interno

```text
Filter → Search → OrderBy → Count/LongCount → Aggregate → Page → Select
```

| Etapa | Comando | Documento |
|-------|---------|-----------|
| 1 | `Filter` | [filter.md](filter.md) |
| 2 | `Search` | [search.md](search.md) |
| 3 | `OrderBy` | [ordination.md](ordination.md) |
| 4 | `Count` / `LongCount` | total filtrado → `out count` |
| 5 | `Aggregate` | [aggregation.md](aggregation.md) |
| 6 | `Page` | [pagination.md](pagination.md) |
| 7 | `Select` | [select.md](select.md) |

---

## Exemplo no controller

```csharp
using ModelWrapper;
using ModelWrapper.Extensions.FullSearch;

[HttpGet]
public ActionResult<WrapResponse<Product>> Get([FromQuery] GetProductsRequest request)
{
    var data = _db.Products
        .AsQueryable()
        .FullSearch(request, out long count)
        .ToList();

    return Ok(new WrapResponse<Product>(request, data, resultCount: count));
}
```

---

## Exemplo de URL

```http
GET /api/products
  ?pageSize=20
  &pageNumber=1
  &orderBy=price
  &order=descending
  &query=phone
  &name_Contains=Sam
  &price_GreaterThanOrEqual=100
  &category_In=Mobile;Tablet
  &responseProperties=id
  &responseProperties=name
  &responseProperties=price
  &aggregateBy=category
  &AggregateSum=price
```

---

## Quando usar etapas isoladas

Prefira `FullSearch` em listagens. Use extensões individuais quando precisar de um pipeline diferente, por exemplo:

- Contar sem projetar  
- Agregar sem paginar  
- Filtrar sem busca textual  

```csharp
var onlyFiltered = _db.Products.AsQueryable().Filter(request);
var total = onlyFiltered.Count();
```

← [Agregação](aggregation.md) · Ver também: [Exemplos](../examples.md)
