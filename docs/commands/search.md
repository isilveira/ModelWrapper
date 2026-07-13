# Comando: Busca (Search)

Busca textual em propriedades escalares do modelo (e propriedades simples aninhadas). Os termos passam por normalização (minúsculas, remoção de caracteres/termos suprimidos e filtro por tamanho).

**Extensão:** `source.Search(request)`  
**Namespace:** `ModelWrapper.Extensions.Search`  
**Eco no response:** `RequestObject["searchProperties"]`

---

## Parâmetros

| Parâmetro | Tipo | Padrão | Descrição |
|-----------|------|--------|-----------|
| `query` | `string` | *(vazio)* | Texto a buscar |
| `queryStrict` | `bool` | `false` | Se `true`, **todos** os termos devem existir **na mesma propriedade** (AND estrito). Se `false`, termos podem combinar em OR entre propriedades |
| `queryPhrase` | `bool` | `false` | Se `true`, trata `query` como um único token (não divide por espaços) |

---

## Processamento dos termos

1. Texto em minúsculas  
2. Caracteres de `AddSuppressedCharacters` viram espaço  
3. Se `queryPhrase=false`, divide por espaços  
4. Remove termos fora de `QueryTermsMinimumSize` / `QueryTermsMaximumSize` (padrão mínimo: **3**)  
5. Remove termos de `AddSuppressedTerms`  
6. Aplica `Contains` nas propriedades elegíveis (não suprimidas, não coleções)

---

## Exemplos HTTP

```http
# Busca livre: termos "sam" e "phone" (mín. 3 caracteres — "sam" passa)
GET /api/products?query=sam%20phone

# Frase inteira
GET /api/products?query=galaxy%20s24&queryPhrase=true

# Modo estrito: todos os termos na mesma propriedade
GET /api/products?query=wireless%20earbuds&queryStrict=true

# Combinado com filtro e paginação
GET /api/products?query=phone&price_GreaterThan=50&pageSize=20&pageNumber=1
```

---

## Uso no código

```csharp
using ModelWrapper.Extensions.Search;

var query = _db.Products.AsQueryable().Search(request);
```

### Programático

```csharp
request.ClearSearch();
request.SetSearch("galaxy phone", strict: false, phrase: false);
```

### No response

```csharp
Search search = response.Search();
// search.Query, search.Strict, search.Phrase
```

---

## Configuração relacionada

| Opção DI | Efeito |
|----------|--------|
| `AddQueryTermsMinimumSize` | Descarta termos curtos demais |
| `AddQueryTermsMaximumSize` | Descarta termos longos demais |
| `AddSuppressedCharacters` | Remove/substitui caracteres na query |
| `AddSuppressedTerms` | Ignora stopwords |

Ver [Configuração](../configuration.md).

← [Filtro](filter.md) · Próximo: [Ordenação](ordination.md)
