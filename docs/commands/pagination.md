# Comando: Paginação

Aplica `Skip` / `Take` sobre o `IQueryable` com limites configuráveis.

**Extensão:** `source.Page(request)`  
**Namespace:** `ModelWrapper.Extensions.Pagination`  
**Eco no response:** `RequestObject["pagination"]` → `{ size, number }`

---

## Parâmetros

| Parâmetro | Tipo | Padrão | Descrição |
|-----------|------|--------|-----------|
| `pageSize` | `int` | `50` (configurável) | Quantidade de itens por página |
| `pageNumber` | `int` | `1` | Página atual (`0` é tratado como `1`) |

### Limites padrão (globais)

| Limite | Valor padrão | Configuração DI |
|--------|--------------|-----------------|
| Mínimo | `10` | `AddMinimumReturnedCollectionSize` |
| Máximo | `1000` | `AddMaximumReturnedCollectionSize` |
| Padrão | `50` | `AddDefaultReturnedCollectionSize` |

Sobrescreva por request com `ConfigDefaultReturnSizeOfCollections`, `ConfigMinimumReturnSizeOfCollection` e `ConfigMaxReturnSizeOfCollections`.

---

## Comportamento

```csharp
Skip((pageNumber - 1) * pageSize).Take(pageSize)
```

Valores de `pageSize` fora do intervalo `[mínimo, máximo]` são ajustados para os limites.

---

## Exemplos HTTP

```http
# Página 1 com 20 itens
GET /api/products?pageSize=20&pageNumber=1

# Segunda página
GET /api/products?pageSize=20&pageNumber=2

# Só o número da página (usa pageSize padrão)
GET /api/products?pageNumber=3
```

---

## Uso no código

```csharp
using ModelWrapper.Extensions.Pagination;

// Contagem total ANTES de paginar (padrão do FullSearch)
var filtered = _db.Products.AsQueryable()
    .Filter(request)
    .Search(request)
    .OrderBy(request);

int count = filtered.Count();
var page = filtered.Page(request).ToList();
```

### Programático

```csharp
request.ClearPagintation(); // nome do método na API atual
request.SetPagination(pageSize: 20, pageNumber: 2);
```

### No response

```csharp
Pagination pagination = response.Pagination();
// pagination.Size, pagination.Number
```

---

## Contagem vs página

Use `ResultCount` com o total **antes** do `Page`:

```csharp
var data = query.FullSearch(request, out long count).ToList();
return new WrapResponse<Product>(request, data, resultCount: count);
```

← [Ordenação](ordination.md) · Próximo: [Seleção](select.md)
