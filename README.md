# ModelWrapper ![Nuget](https://img.shields.io/nuget/v/ModelWrapper)

**ModelWrapper** is a C# (.NET) library designed for Web API projects.  
It simplifies the use of request and response ViewModels by providing a unified way to work with your domain model (`TModel`).

Author: **Ítalo Silveira**  
License: **MIT**  
Company: **BAYSOFT**

---

## 🔍 Overview

In RESTful applications, developers usually define separate classes for entities, requests, and responses.  
**ModelWrapper** provides a generic foundation for these models, allowing you to:

- Create **request ViewModels** inheriting from `WrapRequest<TModel>`, reusing all base entity properties.  
- Create **response ViewModels** inheriting from `WrapResponse<TModel>`, exposing specific fields in a strongly-typed way.  
- Avoid code duplication and keep your domain and API models consistent.  
- Centralize shared logic and metadata between request and response models.

---

## ✅ Main Features

- `WrapRequest<TModel>` — Base class for request ViewModels inheriting all `TModel` properties.  
- `WrapResponse<TModel>` — Base class for response ViewModels with extendable typed properties.  
- Built for RESTful APIs in C#.  
- Available on NuGet: [`ModelWrapper`](https://www.nuget.org/packages/ModelWrapper) (version 9.0.10.0).  
- Can be extended or combined with AutoMapper or other mapping frameworks.

---

## 🚀 Installation

### Using NuGet Package Manager

```powershell
Install-Package ModelWrapper -Version 9.0.10.0
```

### Using .NET CLI
```bash
dotnet add package ModelWrapper --version 9.0.10.0
```

## 💻 Example Usage

```csharp
// Domain model
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// Request ViewModel
public class ProductRequest : WrapRequest<Product>
{
    public string Notes { get; set; }
}

// Response ViewModel
public class ProductResponse : WrapResponse<Product>
{
    public string Category { get; set; }
}

// Controller
[HttpPost]
public ActionResult<ProductResponse> Create([FromBody] ProductRequest req)
{
    var product = new Product { Name = req.Name, Price = req.Price };

    // Save entity, etc.

    var resp = new ProductResponse();
    resp.MapFrom(product); // depending on the library’s implementation
    resp.Category = "Example";
    return Ok(resp);
}
```

## 📚 Documentation
- Repository: [`https://github.com/isilveira/ModelWrapper`](https://github.com/isilveira/ModelWrapper)
- NuGet Package: [`https://www.nuget.org/packages/ModelWrapper`](https://www.nuget.org/packages/ModelWrapper)
- License: [`MIT`](https://mit-license.org/)

## 💡 Benefits
- Eliminates duplication between entities and ViewModels.
- Provides consistent request/response structure.
- Simplifies API maintenance and refactoring.
- Works seamlessly with mapping tools such as AutoMapper.
- Ideal for clean architectures and layered applications.

## 🧠 Best Practices
- Use WrapRequest<TModel> for input models and WrapResponse<TModel> for output models.
- Keep business rules and validation logic outside the wrappers.
- If you use AutoMapper, configure profiles that recognize wrapper types.
- Use wrappers to ensure consistency across multiple services and controllers.

## 🤝 Contributing
Contributions are welcome!
Feel free to open an issue or submit a pull request directly on the repository.

Suggestions and feature improvements are appreciated.

## 🧾 License
This project is licensed under the MIT License.

You are free to use, modify, and distribute it, provided you credit the original author.
