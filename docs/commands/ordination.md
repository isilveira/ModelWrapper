# Comando: Ordenação (Ordination)

Define o campo e a direção de ordenação aplicados ao `IQueryable`.

**Extensão:** `source.OrderBy(request)`  
**Namespace:** `ModelWrapper.Extensions.Ordination`  
**Eco no response:** `RequestObject["ordenation"]` *(grafia interna: **ordenation**)*

---

## Parâmetros

| Parâmetro | Tipo | Valores | Padrão | Descrição |
|-----------|------|---------|--------|-----------|
| `orderBy` | `string` | Nome de propriedade de `TModel` (case-insensitive) | Primeira chave de `ConfigKeys`, se houver | Campo de ordenação (armazenado em camelCase) |
| `order` | `string` | `ascending` ou `descending` (case-insensitive) | `ascending` | Direção |

---

## Exemplos HTTP

```http
# Preço decrescente
GET /api/products?orderBy=price&order=descending

# Nome crescente (order omitido = ascending)
GET /api/products?orderBy=name

# Com paginação
GET /api/products?orderBy=createdAt&order=descending&pageSize=20&pageNumber=1
```

---

## Uso no código

```csharp
using ModelWrapper.Extensions.Ordination;

var query = _db.Products.AsQueryable().OrderBy(request);
```

### Programático

```csharp
request.ClearOrdination();
request.SetOrdination(order: "descending", orderBy: "price");
```

### No response

```csharp
Ordination ord = response.Ordination();
// ord.OrderBy, ord.Order
```

---

## Observações

- Se `orderBy` for inválido ou ausente, a biblioteca tenta a primeira propriedade configurada em `ConfigKeys(...)`.
- Configure a chave padrão no request:

```csharp
public GetProductsRequest()
{
    ConfigKeys(x => x.Id); // orderBy padrão = id
}
```

← [Busca](search.md) · Próximo: [Paginação](pagination.md)
