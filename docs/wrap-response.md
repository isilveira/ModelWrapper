# WrapResponse / WrapResponse\<TModel\>

Envelope padronizado de resposta da API. Ecoa o objeto de request processado, os dados e metadados úteis ao cliente.

---

## Propriedades

| Propriedade | Tipo | Descrição |
|-------------|------|-----------|
| `StatusCode` | `int` | Código HTTP (padrão `200` no construtor tipado) |
| `InternalCode` | `int` | Código interno da aplicação (padrão `200`) |
| `ResultCount` | `long` | Quantidade de resultados (útil com paginação / FullSearch) |
| `Message` | `string` | Mensagem (padrão `"Successful operation!"`) |
| `Request` | `object` | Conteúdo de `request.RequestObject` (comandos aplicados) |
| `Data` | `object` | Payload de retorno |
| `Notifications` | `Dictionary<string, object>` | Notificações / erros estruturados (opcional) |

`WrapResponse<TModel>` guarda internamente `OriginalRequest` para permitir helpers de leitura (`Pagination()`, `Filters()`, `Search()`, `Ordination()`).

---

## Construtores

```csharp
// Sucesso (status 200)
var response = new WrapResponse<Product>(request, data, resultCount: count);

// Com mensagem e notificações
var response = new WrapResponse<Product>(
    request,
    data,
    notifications: notifications,
    message: "Produtos listados",
    resultCount: count);

// Com status customizado
var response = new WrapResponse<Product>(
    statusCode: 404,
    internalCode: 40401,
    request,
    data: null,
    message: "Produto não encontrado");

// Não genérico
var response = new WrapResponse(
    statusCode: 200,
    internalCode: 200,
    request: request.GetRequestObject(),
    data: data,
    resultCount: count);
```

Se `resultCount` for omitido, o valor é inferido de `Data` (contagem de coleção, `1` para item único, `0` para `null`).

---

## Exemplo de JSON de resposta

```json
{
  "statusCode": 200,
  "internalCode": 200,
  "resultCount": 42,
  "message": "Successful operation!",
  "request": {
    "pagination": { "size": 20, "number": 1 },
    "ordenation": { "orderBy": "price", "order": "descending" },
    "filterProperties": { "name_Contains": "phone" },
    "searchProperties": { "query": "samsung", "strict": false, "phrase": false },
    "responseProperties": ["id", "name", "price"]
  },
  "data": [
    { "id": 1, "name": "Phone X", "price": 999.90 }
  ],
  "notifications": null
}
```

> A chave de ordenação no `RequestObject` é **`ordenation`** (grafia interna da biblioteca).

---

## Notificações

Chaves públicas em `ModelWrapper.Utilities.Constants`:

| Constante | Valor da chave |
|-----------|----------------|
| `CONST_NOTIFICATIONS_MESSAGE` | `message` |
| `CONST_NOTIFICATIONS_INNER` | `innerNotification` |
| `CONST_NOTIFICATIONS_REQUEST` | `requestNotifications` |
| `CONST_NOTIFICATIONS_ENTITY` | `entityNotifications` |
| `CONST_NOTIFICATIONS_DOMAIN` | `domainNotifications` |

Helpers adicionais: `ModelWrapper.Extensions.GetNotifications`.

---

## Helpers no response

Com `WrapResponse<TModel>` criado a partir do request original:

```csharp
var pagination = response.Pagination();   // Size, Number
var order = response.Ordination();        // OrderBy, Order
var filters = response.Filters();         // List<FilterProperty>
var search = response.Search();           // Query, Strict, Phrase
```

Namespaces: `Pagination`, `Ordination`, `Filter`, `Search`.

---

## Extrair modelo do Data

```csharp
using ModelWrapper.Extensions.GetModel;
using ModelWrapper.Extensions.GetModels;

Product product = response.GetModel();
IList<Product> list = response.GetModels();
```

Próximo: [Comandos](commands/README.md).
