# Comando: Seleção (Select / responseProperties)

Projeta o resultado para um objeto dinâmico contendo apenas as propriedades solicitadas (ou o conjunto padrão).

**Extensão:** `source.Select(request)` → `IQueryable<object>`  
**Namespace:** `ModelWrapper.Extensions.Select`  
**Eco no response:** `RequestObject["responseProperties"]`

---

## Parâmetros

| Parâmetro | Tipo | Repetível | Descrição |
|-----------|------|-----------|-----------|
| `responseProperties` | `string` | Sim | Nome da propriedade a incluir (camelCase). Aceita caminhos aninhados (`address.city`) |

---

## Comportamento quando omitido

Se `responseProperties` não for enviado:

- Inclui propriedades escalares **não** listadas em `ConfigSuppressedResponseProperties`
- Propriedades complexas / de entidade só entram se `AddByDefaultLoadComplexProperties(true)` e `AddEntityBaseType(...)` estiverem configurados

---

## Exemplos HTTP

```http
# Apenas id, name e price
GET /api/products
  ?responseProperties=id
  &responseProperties=name
  &responseProperties=price

# Propriedade aninhada
GET /api/customers?responseProperties=name&responseProperties=address.city

# Com filtro e paginação
GET /api/products
  ?name_Contains=phone
  &pageSize=10
  &responseProperties=id
  &responseProperties=name
```

---

## Uso no código

```csharp
using ModelWrapper.Extensions.Select;

IQueryable<object> projected = _db.Products
    .AsQueryable()
    .Filter(request)
    .Page(request)
    .Select(request);
```

### Programático

```csharp
request.ClearResponseProperties();
request.SetResponseProperties(new List<string> { "id", "name", "price" });
```

### Suprimir campos sensíveis

```csharp
public GetProductsRequest()
{
    ConfigSuppressedResponseProperties(x => x.InternalCost);
    ConfigSuppressedResponseProperties(x => x.SupplierNotes);
}
```

---

## Observações

- O retorno tipado passa a ser `IQueryable<object>` (projeção dinâmica).
- No `FullSearch`, o `Select` é a **última** etapa do pipeline.
- Configure `AddEntityBaseType` se precisar projetar navegações complexas por padrão.

← [Paginação](pagination.md) · Próximo: [Agregação](aggregation.md)
