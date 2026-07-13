# Comandos de query

Os **comandos** do ModelWrapper são parâmetros de query string (e, em alguns casos, propriedades ligadas ao request) interpretados pelas extensões de `IQueryable`. Eles permitem filtrar, buscar, ordenar, paginar, projetar e agregar sem escrever expressões à mão.

---

## Índice dos comandos

| Comando | Parâmetros principais | Extensão | Documento |
|---------|----------------------|----------|-----------|
| **Filtro** | `{propriedade}`, `{propriedade}_{Critério}` | `.Filter(request)` | [filter.md](filter.md) |
| **Busca** | `query`, `queryStrict`, `queryPhrase` | `.Search(request)` | [search.md](search.md) |
| **Ordenação** | `order`, `orderBy` | `.OrderBy(request)` | [ordination.md](ordination.md) |
| **Paginação** | `pageSize`, `pageNumber` | `.Page(request)` | [pagination.md](pagination.md) |
| **Seleção** | `responseProperties` | `.Select(request)` | [select.md](select.md) |
| **Agregação** | `aggregateBy`, `AggregateMin/Max/Sum/Avg` | `.Aggregate(request)` | [aggregation.md](aggregation.md) |
| **FullSearch** | todos os acima | `.FullSearch(request, out count)` | [full-search.md](full-search.md) |

---

## Pipeline recomendado

```csharp
using ModelWrapper.Extensions.FullSearch;

var result = source.FullSearch(request, out long count);
```

Equivalente a:

```csharp
source
    .Filter(request)
    .Search(request)
    .OrderBy(request)
    .LongCount(out count)   // ou .Count(out int count)
    .Aggregate(request)
    .Page(request)
    .Select(request);
```

A ordem importa: a contagem ocorre **depois** de filtro/busca/ordenação e **antes** da paginação, para que `ResultCount` reflita o total filtrado.

---

## Exemplo de URL completa

```http
GET /api/products
  ?pageSize=20
  &pageNumber=2
  &orderBy=price
  &order=descending
  &query=phone
  &queryStrict=false
  &queryPhrase=false
  &name_Contains=Sam
  &price_GreaterThanOrEqual=100
  &price_LessThan=2000
  &category_In=Mobile;Tablet
  &responseProperties=id
  &responseProperties=name
  &responseProperties=price
  &aggregateBy=category
  &AggregateSum=price
  &AggregateAvg=price
```

---

## Convenções

- Nomes de propriedades do modelo em **camelCase** na query (`Name` → `name`, `CreatedAt` → `createdAt`).
- Critérios de filtro usam sufixo com underscore: `name_Contains`, `price_GreaterThan`.
- Parâmetros de comando (`pageSize`, `query`, `orderBy`, …) não fazem parte do modelo de domínio.
- Filtros Equal (`?name=...`) também preenchem a propriedade no `Model` interno.

Cada página a seguir detalha o que o comando faz, opções e exemplos.
