# WrapRequest\<TModel\>

Classe base para ViewModels de requisição. Herda de `DynamicObject` e usa o binder customizado `WrapRequestBinder` para capturar body, form, query e route em um único objeto.

```csharp
[ModelBinder(BinderType = typeof(WrapRequestBinder))]
public class WrapRequest<TModel> : DynamicObject where TModel : class
```

---

## Responsabilidades

1. **Materializar** `TModel` (ou `List<T>` em modo batch) com os dados enviados
2. **Registrar** todas as propriedades recebidas (`AllProperties`) com a origem (`FromBody`, `FromForm`, `FromQuery`, `FromRoute`)
3. **Expor comandos** (filtro, busca, paginação, etc.) via query string
4. **Alimentar** `RequestObject` (ecoado em `WrapResponse.Request`)
5. **Oferecer** helpers de CRUD (`Post`, `Put`, `Patch`) e projeção

---

## Declaração típica

```csharp
public class UpdateProductRequest : WrapRequest<Product>
{
    public UpdateProductRequest()
    {
        ConfigKeys(x => x.Id);
        ConfigSuppressedProperties(x => x.CreatedAt);
    }

    // Propriedades extras além de Product (opcional)
    public string CorrelationId { get; set; }
}
```

Propriedades de `Product` ficam disponíveis dinamicamente no request (acesso tipado em subclasses via members da própria classe ou via `GetModel()`).

---

## Ordem de binding

O `WrapRequestBinder` aplica as fontes nesta ordem:

1. **Form** — se `HasFormContentType`; valor `"null"` vira `null`
2. **Body** — se houver `ContentLength`:
   - JSON **objeto** `{ ... }` → deserializa no request
   - JSON **array** `[ ... ]` → batch, quando `TModel` (ou o argumento genérico da classe) é coleção (`List<T>`, etc.)
3. **Query string** — todas as chaves; chaves repetidas geram múltiplos valores
4. **Route** — valores de rota
5. **`BindComplete()`** — consolida propriedades fornecidas e grava route em `RequestObject["route"]`

Nomes de propriedade são **case-insensitive** ao mapear para o modelo.

---

## Batch

```csharp
public class BatchCreateProductsRequest : WrapRequest<List<Product>>
{
}

// Body:
// [
//   { "name": "A", "price": 10 },
//   { "name": "B", "price": 20 }
// ]
```

Use `GetModels()` para obter a lista materializada.

---

## Métodos úteis

| Método | Descrição |
|--------|-----------|
| `GetModel()` | Retorna a instância interna de `TModel` |
| `GetModels()` | Retorna lista (cenário batch) |
| `GetRequestObject()` | Dicionário com metadados/comandos aplicados |
| `AddProperty(name, value, source)` | Adiciona propriedade manualmente |
| `ConfigKeys` / `ConfigSuppressed*` / `Config*Collection*` | Ver [Configuração](configuration.md) |
| `Project(...)` | Projeta o modelo interno |

---

## Extensões relacionadas

| Namespace | Uso |
|-----------|-----|
| `ModelWrapper.Extensions.Post` | `request.Post()` |
| `ModelWrapper.Extensions.Put` | `request.Put(entity)` |
| `ModelWrapper.Extensions.Patch` | `request.Patch(entity)` |
| `ModelWrapper.Extensions.Filter` | `query.Filter(request)`, `AddFilter`, `ClearFilters` |
| `ModelWrapper.Extensions.Search` | `query.Search(request)`, `SetSearch`, `ClearSearch` |
| `ModelWrapper.Extensions.Ordination` | `query.OrderBy(request)`, `SetOrdination` |
| `ModelWrapper.Extensions.Pagination` | `query.Page(request)`, `SetPagination` |
| `ModelWrapper.Extensions.Select` | `query.Select(request)`, `SetResponseProperties` |
| `ModelWrapper.Extensions.Aggregation` | `query.Aggregate(request)`, `AddAggregator` |
| `ModelWrapper.Extensions.FullSearch` | `query.FullSearch(request, out count)` |

---

## Observação sobre filtros e modelo

Um parâmetro como `?name=John` **preenche** `Model.Name` **e** atua como filtro Equal quando `Filter()` / `FullSearch()` é chamado. Para comandos “puros” (`pageSize`, `query`, `orderBy`, etc.), use os nomes documentados em [Comandos](commands/README.md).

Próximo: [WrapResponse](wrap-response.md).
