# Comando: Filtro

Aplica predicados tipados sobre propriedades de `TModel`, combinados com **AND**. Múltiplos valores para a mesma chave (query repetida ou lista) tendem a ser combinados com **OR**.

**Extensão:** `source.Filter(request)`  
**Namespace:** `ModelWrapper.Extensions.Filter`  
**Eco no response:** `RequestObject["filterProperties"]`

---

## Como funciona

Para cada propriedade pública de `TModel` (não suprimida), o binder reconhece chaves no formato:

```text
{propriedadeEmCamelCase}{sufixoDoCritério}
```

Exemplos para `Product.Name` e `Product.Price`:

| Query | Critério |
|-------|----------|
| `name=John` | Equal |
| `name_Not=John` | Not equal |
| `name_Contains=jo` | Contains |
| `price_GreaterThan=10` | Greater than |

---

## Critérios e opções

### Todos os tipos

| Critério | Sufixo | Exemplo | Descrição |
|----------|--------|---------|-----------|
| Equal | *(nenhum)* | `name=Phone` | Igualdade |
| Not equal | `_Not` | `name_Not=Phone` | Diferente |
| In | `_In` | `id_In=1;2;3` | Valor ∈ lista |
| Not in | `_NotIn` | `id_NotIn=1;2` | Valor ∉ lista |

### Strings e `char`

| Critério | Sufixo | Exemplo |
|----------|--------|---------|
| Contains | `_Contains` | `name_Contains=sam` |
| Not contains | `_NotContains` | `name_NotContains=test` |
| Starts with | `_StartsWith` | `name_StartsWith=Sam` |
| Not starts with | `_NotStartsWith` | `name_NotStartsWith=X` |
| Ends with | `_EndsWith` | `name_EndsWith=Pro` |
| Not ends with | `_NotEndsWith` | `name_NotEndsWith=Lite` |

### Numéricos, `DateTime` e `TimeSpan`

Tipos: `int`, `long`, `float`, `double`, `decimal`, `DateTime`, `TimeSpan` (e nullables).

| Critério | Sufixo | Exemplo |
|----------|--------|---------|
| Greater than | `_GreaterThan` | `price_GreaterThan=99.9` |
| Greater or equal | `_GreaterThanOrEqual` | `price_GreaterThanOrEqual=100` |
| Less than | `_LessThan` | `createdAt_LessThan=2026-01-01` |
| Less or equal | `_LessThanOrEqual` | `price_LessThanOrEqual=500` |

---

## Listas (`_In` / `_NotIn`)

Duas formas:

1. **Separador** (padrão `;`, configurável com `AddByDefaultInStringSeparator`):

```http
GET /products?status_In=Active;Draft;Pending
```

2. **Parâmetro repetido:**

```http
GET /products?id_In=1&id_In=2&id_In=3
```

---

## Exemplos HTTP

```http
# Preço entre 100 e 500, nome contendo "phone", categoria Mobile ou Tablet
GET /api/products
  ?name_Contains=phone
  &price_GreaterThanOrEqual=100
  &price_LessThanOrEqual=500
  &category_In=Mobile;Tablet

# Excluir IDs
GET /api/products?id_NotIn=5;8;13

# Data de criação a partir de
GET /api/products?createdAt_GreaterThanOrEqual=2025-01-01
```

---

## Uso no código

```csharp
using ModelWrapper.Extensions.Filter;

var query = _db.Products.AsQueryable().Filter(request);
```

### Programático (sem query string)

```csharp
request.ClearFilters();
request.AddFilter("name_Contains", "phone");
request.AddFilter("price_GreaterThanOrEqual", "100");
```

### No response

```csharp
var filters = response.Filters();
var nameFilter = response.GetFilterProperty("name_Contains");
```

---

## Observações

- Propriedades em `ConfigSuppressedProperties` **não** entram nos filtros.
- Equal (`?name=...`) também atualiza o `Model` interno do request.
- Vários filtros diferentes são **AND**; vários valores no mesmo critério são tratados como lista (OR no predicado gerado).

← [Comandos](README.md) · Próximo: [Busca](search.md)
