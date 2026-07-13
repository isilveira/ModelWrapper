# CRUD: Post, Put e Patch

Extensões que copiam propriedades do `WrapRequest<TModel>` para uma entidade de domínio, respeitando chaves e propriedades suprimidas.

---

## Comparativo

| Método | Assinatura | Comportamento |
|--------|------------|---------------|
| **Post** | `request.Post()` | Cria **nova** instância de `TModel` e copia propriedades fornecidas (exceto keys e suppressed) |
| **Put** | `request.Put(model)` | Atualiza a entidade existente com o mesmo conjunto de propriedades que o Post (substituição ampla) |
| **Patch** | `request.Patch(model)` | Atualiza **apenas** as propriedades que foram **fornecidas** na requisição (`AllProperties` / supplied) |

Namespaces:

- `ModelWrapper.Extensions.Post`
- `ModelWrapper.Extensions.Put`
- `ModelWrapper.Extensions.Patch`

As propriedades copiadas também são registradas em `RequestObject["model"]`.

---

## Post — criar

```csharp
using ModelWrapper.Extensions.Post;

[HttpPost]
public ActionResult<WrapResponse<Product>> Create(CreateProductRequest request)
{
    var entity = request.Post();

    _db.Products.Add(entity);
    _db.SaveChanges();

    return Ok(new WrapResponse<Product>(request, entity));
}
```

```http
POST /api/products
Content-Type: application/json

{
  "name": "Phone X",
  "price": 999.90,
  "category": "Mobile",
  "active": true
}
```

### Configuração sugerida

```csharp
public class CreateProductRequest : WrapRequest<Product>
{
    public CreateProductRequest()
    {
        ConfigKeys(x => x.Id);
        ConfigSuppressedProperties(x => x.Id);
    }
}
```

---

## Put — atualizar completo

```csharp
using ModelWrapper.Extensions.Put;

[HttpPut("{id}")]
public ActionResult<WrapResponse<Product>> Update(int id, UpdateProductRequest request)
{
    var entity = _db.Products.Find(id);
    if (entity is null)
        return NotFound(new WrapResponse<Product>(404, 404, request, null, message: "Not found"));

    entity = request.Put(entity);
    _db.SaveChanges();

    return Ok(new WrapResponse<Product>(request, entity));
}
```

```http
PUT /api/products/1
Content-Type: application/json

{
  "name": "Phone X Pro",
  "price": 1299.00,
  "category": "Mobile",
  "active": true
}
```

O `id` da rota também é bound no request (`FromRoute`).

---

## Patch — atualizar parcial

```csharp
using ModelWrapper.Extensions.Patch;

[HttpPatch("{id}")]
public ActionResult<WrapResponse<Product>> Patch(int id, PatchProductRequest request)
{
    var entity = _db.Products.Find(id);
    if (entity is null)
        return NotFound(new WrapResponse<Product>(404, 404, request, null, message: "Not found"));

    entity = request.Patch(entity);
    _db.SaveChanges();

    return Ok(new WrapResponse<Product>(request, entity));
}
```

```http
PATCH /api/products/1
Content-Type: application/json

{
  "price": 899.00
}
```

Somente `price` é alterado; `name`, `category`, etc. permanecem iguais.

---

## Batch (lista)

```csharp
public class BatchCreateProductsRequest : WrapRequest<List<Product>> { }

[HttpPost("batch")]
public ActionResult CreateBatch(BatchCreateProductsRequest request)
{
    var items = request.GetModels();
    _db.Products.AddRange(items);
    _db.SaveChanges();

    return Ok(new WrapResponse(200, 200, request.GetRequestObject(), items, resultCount: items.Count));
}
```

```http
POST /api/products/batch
Content-Type: application/json

[
  { "name": "A", "price": 10 },
  { "name": "B", "price": 20 }
]
```

---

## Regras de cópia

Incluídas na cópia:

- Propriedades graváveis de `TModel` presentes no modelo bound

Excluídas:

- Propriedades em `ConfigKeys`
- Propriedades em `ConfigSuppressedProperties`
- No **Patch**, propriedades que **não** foram enviadas na requisição

---

## Form vs JSON

O binder aceita `application/json` e `multipart/form-data` / form. Em form, o valor literal `"null"` é convertido para `null`.

```http
POST /api/products
Content-Type: application/x-www-form-urlencoded

name=Phone+X&price=999.90&category=Mobile
```

Próximo: [Exemplos](examples.md).
