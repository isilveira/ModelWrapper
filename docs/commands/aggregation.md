# Comando: Agregação

Calcula agregados (Min, Max, Sum, Avg) sobre o conjunto filtrado e grava o resultado em `RequestObject["aggregationResponse"]`. **Não altera** o formato das linhas retornadas pelo `IQueryable` (não faz `GroupBy` no resultado da lista).

**Extensão:** `source.Aggregate(request)`  
**Namespace:** `ModelWrapper.Extensions.Aggregation`

---

## Parâmetros

| Parâmetro | Tipo | Repetível | Descrição |
|-----------|------|-----------|-----------|
| `aggregateBy` | `string` | Sim | Propriedade(s) usadas como agrupador / dimensão |
| `AggregateMin` | `string` | Sim | Propriedade(s) para mínimo |
| `AggregateMax` | `string` | Sim | Propriedade(s) para máximo |
| `AggregateSum` | `string` | Sim | Propriedade(s) para soma (**somente tipos numéricos**) |
| `AggregateAvg` | `string` | Sim | Propriedade(s) para média (**somente tipos numéricos**) |

Tipos numéricos aceitos em Sum/Avg: `int`, `long`, `float`, `double`, `decimal` (e nullables).

---

## Exemplos HTTP

```http
# Soma e média de preço, agrupando por categoria
GET /api/products
  ?aggregateBy=category
  &AggregateSum=price
  &AggregateAvg=price

# Múltiplos agregadores e propriedades
GET /api/orders
  ?aggregateBy=status
  &aggregateBy=region
  &AggregateMin=total
  &AggregateMax=total
  &AggregateSum=total

# Com filtros (agrega sobre o conjunto filtrado no FullSearch)
GET /api/products
  ?category_In=Mobile;Tablet
  &price_GreaterThan=0
  &aggregateBy=category
  &AggregateSum=price
```

---

## Uso no código

```csharp
using ModelWrapper.Extensions.Aggregation;

var query = _db.Products
    .AsQueryable()
    .Filter(request)
    .Aggregate(request);
```

### Programático

```csharp
using ModelWrapper.Helpers; // AggregatesHelper.Aggregates

request.ClearAggregators();
request.AddAggregator(x => x.Category);

request.ClearAggregates();
request.AddAggregate(AggregatesHelper.Aggregates.Sum, x => x.Price);
request.AddAggregate(AggregatesHelper.Aggregates.Avg, x => x.Price);
request.AddAggregate(AggregatesHelper.Aggregates.Min, x => x.Price);
request.AddAggregate(AggregatesHelper.Aggregates.Max, x => x.Price);
```

Os nomes dos parâmetros de query são `Aggregate` + tipo: `AggregateMin`, `AggregateMax`, `AggregateSum`, `AggregateAvg`.

---

## Onde ler o resultado

Após `Aggregate` / `FullSearch`, consulte o dicionário do request:

```csharp
var requestObject = request.GetRequestObject();
// requestObject["aggregationResponse"] → AggregationResponse
```

Estrutura aproximada:

```json
{
  "aggregateBy": ["Category"],
  "aggregates": [
    {
      "aggregate": "Sum",
      "properties": { "Price": 15000.50 }
    },
    {
      "aggregate": "Avg",
      "properties": { "Price": 375.01 }
    }
  ]
}
```

No `FullSearch`, a agregação roda **depois** do count e **antes** da paginação/seleção.

← [Seleção](select.md) · Próximo: [FullSearch](full-search.md)
