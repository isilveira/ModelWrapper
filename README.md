# ModelWrapper - Current version 1.1.0

>***ModelWrapper*** is a set of tools for enriching model object manipulation, allowing all data sent to the server to be captured by WrapRequest\<TModel>, feeding the model object (TModel) and allowing all other information to be extracted and used by extended functionality. - *isilveria*.

*Purpose*
----
> ModelWrapper came up to simplify the use of ViewModels by creating a base class that should be used in the project's ViewModels allowing ViewModel to have all of the entity's original attributes, as well as extend functionality to ViewModel.
    
*Functionalities*
----
> ***WrapRequest\<Model>*** - Base class for request ViewModels that extends all entity properties (TModel) and implements features like HTTP basic verbs.

> ***WrapResponse\<Model>*** - Response ViewModels base class that extends all entity properties (TModel) that have been set to return and implements methods to get typed return data.

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
