# Primeiros passos

## Requisitos

- .NET 10 (TFM `net10.0`)
- ASP.NET Core Web API
- (Opcional) Entity Framework Core ou qualquer provider `IQueryable`

## Instalação

### .NET CLI

```bash
dotnet add package ModelWrapper --version 10.0.3.0
```

### Package Manager Console

```powershell
Install-Package ModelWrapper -Version 10.0.3.0
```

## Registro no DI

No `Program.cs` (ou `Startup.ConfigureServices`):

```csharp
using ModelWrapper.Middleware;

builder.Services.AddModelWrapper();
```

Com opções (todas opcionais; veja [Configuração](configuration.md)):

```csharp
builder.Services
    .AddModelWrapper()
    .AddDefaultReturnedCollectionSize(50)
    .AddMinimumReturnedCollectionSize(10)
    .AddMaximumReturnedCollectionSize(1000)
    .AddQueryTermsMinimumSize(3)
    .AddByDefaultInStringSeparator(";")
    .AddByDefaultLoadComplexProperties(false);
```

Não há middleware de pipeline HTTP — apenas registro de configuração via `IServiceCollection`. O binding é feito pelo atributo `[ModelBinder]` em `WrapRequest<TModel>`.

## Modelo de domínio

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public bool Active { get; set; }
}
```

## Request e Response

```csharp
using ModelWrapper;

public class GetProductsRequest : WrapRequest<Product>
{
    public GetProductsRequest()
    {
        ConfigKeys(x => x.Id);
    }
}

public class CreateProductRequest : WrapRequest<Product>
{
    public CreateProductRequest()
    {
        ConfigKeys(x => x.Id);
        ConfigSuppressedProperties(x => x.Id); // Id não vem do body no create
    }
}
```

## Controller mínimo

```csharp
using Microsoft.AspNetCore.Mvc;
using ModelWrapper;
using ModelWrapper.Extensions.FullSearch;
using ModelWrapper.Extensions.Post;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProductsController(AppDbContext db) => _db = db;

    [HttpGet]
    public ActionResult<WrapResponse<Product>> Get([FromQuery] GetProductsRequest request)
    {
        var data = _db.Products
            .AsQueryable()
            .FullSearch(request, out long count)
            .ToList();

        return Ok(new WrapResponse<Product>(request, data, resultCount: count));
    }

    [HttpPost]
    public ActionResult<WrapResponse<Product>> Create(CreateProductRequest request)
    {
        var entity = request.Post();
        _db.Products.Add(entity);
        _db.SaveChanges();

        return Ok(new WrapResponse<Product>(request, entity));
    }
}
```

## Próximos passos

1. [Configuração](configuration.md) — ajustar tamanhos de página, termos de busca, etc.
2. [Comandos](commands/README.md) — conhecer cada parâmetro de query
3. [Exemplos](examples.md) — fluxos GET/POST/PUT/PATCH completos
