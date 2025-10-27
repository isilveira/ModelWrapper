# ModelWrapper

**ModelWrapper** é uma biblioteca para projetos Web API em C# (.NET) que simplifica o uso de modelos de requisição e resposta (ViewModels), permitindo trabalhar de forma unificada com o modelo de domínio (`TModel`).

Autor: **Ítalo Silveira**  
Licença: **MIT**  
Empresa: **BAYSOFT**

---

## 🔍 Visão Geral

Em projetos RESTful é comum definir classes diferentes para representar entidades, requisições e respostas.  
O **ModelWrapper** fornece uma base genérica para esses modelos, permitindo que você:

- Crie *ViewModels* de **requisição** herdando de `WrapRequest<TModel>`, reutilizando todas as propriedades da entidade base.
- Crie *ViewModels* de **resposta** herdando de `WrapResponse<TModel>`, expondo propriedades específicas de forma tipada.
- Evite duplicação e mantenha consistência entre entidades e contratos de API.
- Centralize regras e comportamentos comuns entre requisições e respostas.

---

## ✅ Funcionalidades Principais

- `WrapRequest<TModel>` — Base para *ViewModels* de requisição, herdando todas as propriedades de `TModel`.
- `WrapResponse<TModel>` — Base para *ViewModels* de resposta, herdando propriedades de `TModel` e permitindo extensão.
- Compatível com APIs RESTful em C#.
- Distribuído via NuGet: [`ModelWrapper`](https://www.nuget.org/packages/ModelWrapper) (versão 3.0.0).
- Permite extensão para uso com bibliotecas de mapeamento como AutoMapper.

---

## 🚀 Instalação

### Via NuGet Package Manager

```powershell
Install-Package ModelWrapper -Version 3.0.0
```

### Via .NET CLI
```
dotnet add package ModelWrapper --version 3.0.0
```

### 💻 Exemplo de Uso
```
// Modelo de domínio
public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
}

// ViewModel de requisição
public class ProdutoRequest : WrapRequest<Produto>
{
    public string Observacao { get; set; }
}

// ViewModel de resposta
public class ProdutoResponse : WrapResponse<Produto>
{
    public string Categoria { get; set; }
}

// Controller
[HttpPost]
public ActionResult<ProdutoResponse> Criar([FromBody] ProdutoRequest req)
{
    var produto = new Produto { Nome = req.Nome, Preco = req.Preco };

    // Persistir entidade, etc.

    var resp = new ProdutoResponse();
    resp.MapFrom(produto); // conforme métodos implementados pela biblioteca
    resp.Categoria = "Exemplo";
    return Ok(resp);
}
```

## 📚 Documentação
- Repositório: https://github.com/isilveira/ModelWrapper
- Pacote NuGet: https://www.nuget.org/packages/ModelWrapper
- Licença: MIT

## 💡 Benefícios
- Evita duplicação entre entidades e ViewModels.
- Padroniza estrutura de requisições e respostas.
- Facilita manutenção e refatoração de APIs.
- Facilmente integrável com frameworks e ferramentas já utilizadas.
- Ideal para arquiteturas limpas e camadas de aplicação bem definidas.

## 🧠 Boas Práticas
- Utilize WrapRequest<TModel> para entrada de dados e WrapResponse<TModel> para saída.
- Prefira manter validações e regras de negócio fora dos wrappers.
- Se estiver usando AutoMapper, configure perfis que reconheçam os wrappers.
- Use os wrappers para manter consistência entre diferentes endpoints e serviços.

## 🤝 Contribuição
Contribuições são bem-vindas!

Abra um issue ou envie um pull request diretamente no repositório.

Sinta-se à vontade para sugerir melhorias, correções ou novos exemplos de uso.

## 🧾 Licença
Este projeto está licenciado sob a MIT License.

Você pode usar, modificar e distribuir livremente, desde que mantenha os créditos ao autor original.
