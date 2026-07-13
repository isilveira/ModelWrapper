# Configuração

O ModelWrapper usa um singleton `ConfigurationService`, registrado por `AddModelWrapper()`. As extensões em `ModelWrapper.Middleware` alteram essa configuração global. Cada `WrapRequest<TModel>` também pode sobrescrever parte do comportamento.

---

## Registro global (`IServiceCollection`)

```csharp
using ModelWrapper.Middleware;

services
    .AddModelWrapper()
    .AddDefaultReturnedCollectionSize(50)
    .AddMinimumReturnedCollectionSize(10)
    .AddMaximumReturnedCollectionSize(1000)
    .AddQueryTermsMinimumSize(3)
    .AddQueryTermsMaximumSize(null)
    .AddByDefaultLoadComplexProperties(false)
    .AddSuppressedCharacters(new List<char> { ',', '.' })
    .AddSuppressedTerms(new List<string> { "de", "da", "do", "a", "o" })
    .AddEntityBaseType(typeof(EntityBase))
    .AddByDefaultInStringSeparator(";");
```

### Opções

| Método | O que faz | Padrão |
|--------|-----------|--------|
| `AddModelWrapper()` | Inicializa e registra `ConfigurationService` | — |
| `AddDefaultReturnedCollectionSize(int)` | `pageSize` padrão quando omitido | `50` |
| `AddMinimumReturnedCollectionSize(int)` | Limite inferior de `pageSize` | `10` |
| `AddMaximumReturnedCollectionSize(int)` | Limite superior de `pageSize` | `1000` |
| `AddQueryTermsMinimumSize(int?)` | Tamanho mínimo de cada termo em `query` | `3` |
| `AddQueryTermsMaximumSize(int?)` | Tamanho máximo de cada termo em `query` | `null` (sem limite) |
| `AddByDefaultLoadComplexProperties(bool)` | Inclui propriedades complexas/navegação no `Select` padrão | `false` |
| `AddSuppressedCharacters(IList<char>)` | Caracteres removidos (substituídos por espaço) na busca | lista vazia |
| `AddSuppressedTerms(IList<string>)` | Termos ignorados na busca | lista vazia |
| `AddEntityBaseType(Type)` | Tipo base de entidades (necessário para detectar propriedades complexas no Select) | `null` |
| `AddByDefaultInStringSeparator(string)` | Separador de valores em filtros `_In` / `_NotIn` | `";"` |

---

## Configuração por requisição (`WrapRequest`)

Chamadas tipicamente no construtor do command/request:

```csharp
public class GetProductsRequest : WrapRequest<Product>
{
    public GetProductsRequest()
    {
        ConfigKeys(x => x.Id);

        ConfigDefaultReturnSizeOfCollections(25);
        ConfigMinimumReturnSizeOfCollection(5);
        ConfigMaxReturnSizeOfCollections(200);

        ConfigSuppressedProperties(x => x.InternalNotes);
        ConfigSuppressedResponseProperties(x => x.InternalNotes);
    }
}
```

| Método | Efeito |
|--------|--------|
| `ConfigKeys(expression)` | Define a(s) chave(s) da entidade. Usado como `orderBy` padrão e excluído de cópia em `Post`/`Put` |
| `ConfigDefaultReturnSizeOfCollections(int)` | Sobrescreve o `pageSize` padrão nesta requisição |
| `ConfigMinimumReturnSizeOfCollection(int)` | Sobrescreve o mínimo de `pageSize` |
| `ConfigMaxReturnSizeOfCollections(int)` | Sobrescreve o máximo de `pageSize` |
| `ConfigSuppressedProperties(expression \| string)` | Propriedade excluída de filtro, busca e cópia CRUD |
| `ConfigSuppressedResponseProperties(expression \| string)` | Propriedade excluída da projeção (`Select` / `responseProperties`) |

---

## Relação com os comandos

| Configuração | Afeta |
|--------------|-------|
| Tamanhos de coleção | [Paginação](commands/pagination.md) |
| Tamanho / caracteres / termos de query | [Busca](commands/search.md) |
| Separador `_In` | [Filtro](commands/filter.md) |
| Entity base + load complex | [Seleção](commands/select.md) |
| Keys / suppressed | [CRUD](crud.md), ordenação padrão, filtros |

Veja também: [WrapRequest](wrap-request.md).
