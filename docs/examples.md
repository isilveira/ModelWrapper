# Exemplos completos

Referência prática reunindo instalação, commands e CRUD.

---

## 1. Setup

```csharp
// Program.cs
using ModelWrapper.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(/* ... */);

builder.Services
    .AddModelWrapper()
    .AddDefaultReturnedCollectionSize(50)
    .AddMinimumReturnedCollectionSize(10)
    .AddMaximumReturnedCollectionSize(200)
    .AddQueryTermsMinimumSize(3)
    .AddByDefaultInStringSeparator(";")
    .AddSuppressedTerms(new List<string> { "de", "da", "do", "a", "o" });

var app = builder.Build();
app.MapControllers();
app.Run();
```

---

## 2. Modelo e requests

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class GetProductsRequest : WrapRequest<Product>
{
    public GetProductsRequest()
    {
        ConfigKeys(x => x.Id);
        ConfigDefaultReturnSizeOfCollections(20);
    }
}

public class CreateProductRequest : WrapRequest<Product>
{
    public CreateProductRequest()
    {
        ConfigKeys(x => x.Id);
        ConfigSuppressedProperties(x => x.Id);
        ConfigSuppressedProperties(x => x.CreatedAt);
    }
}

public class UpdateProductRequest : WrapRequest<Product>
{
    public UpdateProductRequest()
    {
        ConfigKeys(x => x.Id);
        ConfigSuppressedProperties(x => x.CreatedAt);
    }
}

public class PatchProductRequest : WrapRequest<Product>
{
    public PatchProductRequest()
    {
        ConfigKeys(x => x.Id);
        ConfigSuppressedProperties(x => x.CreatedAt);
    }
}
```

---

## 3. Controller

```csharp
using Microsoft.AspNetCore.Mvc;
using ModelWrapper;
using ModelWrapper.Extensions.FullSearch;
using ModelWrapper.Extensions.Patch;
using ModelWrapper.Extensions.Post;
using ModelWrapper.Extensions.Put;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProductsController(AppDbContext db) => _db = db;

    /// <summary>Lista com FullSearch (filtro, busca, ordem, página, select, aggregate).</summary>
    [HttpGet]
    public ActionResult<WrapResponse<Product>> Get([FromQuery] GetProductsRequest request)
    {
        var data = _db.Products
            .AsQueryable()
            .FullSearch(request, out long count)
            .ToList();

        return Ok(new WrapResponse<Product>(request, data, resultCount: count));
    }

    [HttpGet("{id:int}")]
    public ActionResult<WrapResponse<Product>> GetById(int id, [FromQuery] GetProductsRequest request)
    {
        var entity = _db.Products.Find(id);
        if (entity is null)
            return NotFound(new WrapResponse<Product>(404, 404, request, null, message: "Product not found"));

        return Ok(new WrapResponse<Product>(request, entity));
    }

    [HttpPost]
    public ActionResult<WrapResponse<Product>> Create(CreateProductRequest request)
    {
        var entity = request.Post();
        entity.CreatedAt = DateTime.UtcNow;

        _db.Products.Add(entity);
        _db.SaveChanges();

        return Ok(new WrapResponse<Product>(request, entity));
    }

    [HttpPut("{id:int}")]
    public ActionResult<WrapResponse<Product>> Update(int id, UpdateProductRequest request)
    {
        var entity = _db.Products.Find(id);
        if (entity is null)
            return NotFound(new WrapResponse<Product>(404, 404, request, null, message: "Product not found"));

        entity = request.Put(entity);
        _db.SaveChanges();

        return Ok(new WrapResponse<Product>(request, entity));
    }

    [HttpPatch("{id:int}")]
    public ActionResult<WrapResponse<Product>> Patch(int id, PatchProductRequest request)
    {
        var entity = _db.Products.Find(id);
        if (entity is null)
            return NotFound(new WrapResponse<Product>(404, 404, request, null, message: "Product not found"));

        entity = request.Patch(entity);
        _db.SaveChanges();

        return Ok(new WrapResponse<Product>(request, entity));
    }
}
```

---

## 4. Chamadas HTTP de referência

### Listagem rica

```http
GET /api/products
  ?pageSize=20
  &pageNumber=1
  &orderBy=price
  &order=descending
  &query=phone
  &queryStrict=false
  &name_Contains=Sam
  &price_GreaterThanOrEqual=100
  &price_LessThan=2000
  &category_In=Mobile;Tablet
  &active=true
  &responseProperties=id
  &responseProperties=name
  &responseProperties=price
  &responseProperties=category
  &aggregateBy=category
  &AggregateSum=price
  &AggregateAvg=price
```

### Filtros isolados

```http
GET /api/products?name_StartsWith=Gal
GET /api/products?id_In=1;2;3;5
GET /api/products?createdAt_GreaterThanOrEqual=2025-01-01&orderBy=createdAt&order=descending
GET /api/products?category_Not=Legacy&active=true
```

### Busca

```http
GET /api/products?query=wireless%20earbuds
GET /api/products?query=galaxy%20s24&queryPhrase=true
GET /api/products?query=sam%20phone&queryStrict=true
```

### Paginação e ordenação

```http
GET /api/products?pageSize=10&pageNumber=3&orderBy=name&order=ascending
```

### Criar

```http
POST /api/products
Content-Type: application/json

{
  "name": "Galaxy S24",
  "price": 4500.00,
  "category": "Mobile",
  "active": true
}
```

### Atualizar (PUT)

```http
PUT /api/products/10
Content-Type: application/json

{
  "name": "Galaxy S24 Ultra",
  "price": 6200.00,
  "category": "Mobile",
  "active": true
}
```

### Atualizar parcial (PATCH)

```http
PATCH /api/products/10
Content-Type: application/json

{
  "active": false
}
```

---

## 5. Pipeline manual (sem FullSearch)

```csharp
using ModelWrapper.Extensions.Filter;
using ModelWrapper.Extensions.Search;
using ModelWrapper.Extensions.Ordination;
using ModelWrapper.Extensions.Count;
using ModelWrapper.Extensions.Aggregation;
using ModelWrapper.Extensions.Pagination;
using ModelWrapper.Extensions.Select;

var query = _db.Products.AsQueryable()
    .Filter(request)
    .Search(request)
    .OrderBy(request);

query.LongCount(out long count);

var data = query
    .Aggregate(request)
    .Page(request)
    .Select(request)
    .ToList();
```

---

## 6. Mapa rápido parâmetro → efeito

| Query | Efeito |
|-------|--------|
| `pageSize` / `pageNumber` | Paginação |
| `order` / `orderBy` | Ordenação |
| `query` / `queryStrict` / `queryPhrase` | Busca textual |
| `responseProperties` | Projeção de campos |
| `aggregateBy` / `AggregateMin` / `AggregateMax` / `AggregateSum` / `AggregateAvg` | Agregados no request |
| `name`, `name_Contains`, `price_GreaterThan`, `id_In`, … | Filtros |

Documentação detalhada: [Comandos](commands/README.md).
