# ModelWrapper - Current version 1.1.0

>**ModelWrapper** is a set of tools for enriching model object manipulation, allowing all data sent to the server to be captured by WrapRequest<TModel>, feeding the model object (TModel) and allowing all other information to be extracted and used by extended functionality. - *isilveria*.

*Purpose*
----
> O ModelWrapper surgiu com o intuito simplificar a utilização de ViewModels criando uma classe base que deverá ser usada nas ViewModels do projeto permitindo que a ViewModel tenha todos os atributos originais da entidade, além de extender funcionalidades à ViewModel.
    
*Functionalities*
----
> **WrapRequest<Model>** - Classe base para view models de requisições que extende todas as propriedades da entidade (TModel) e implementa funcionalidades como os verbos basicos do HTTP.
**WrapResponse<TModel>** - Classe base para view models de respostas que extende todas as propriedades da entidade (TModel) que foram configuradas para retorno e implementa metodos para obter os dados de retorno tipados.

*Usage*
----
>...

*GitHub*
----
> https://github.com/isilveira/ModelWrapper

*Nuget*
----
> https://www.nuget.org/packages/ModelWrapper

*Package Manager*
----
> ```PM> Install-Package ModelWrapper -Version 1.1.0```

*.NET CLI*
----
> ```> dotnet add package ModelWrapper --version 1.1.0```

*Packet*
----
> ```> paket add ModelWrapper --version 1.1.0```
