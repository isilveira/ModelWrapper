# ModelWrapper - Current version 1.0.1

Model wrapper - Tool wich enrich the experience of data models manipulation in RestFul APIs for C# WebApi projects.
____
*Usage*

Model class:
```
public class PatchCustomerCommand : Wrap<Customer>, IRequest<PatchCustomerCommandResponse>
{
    public PatchCustomerCommand()
    {
        KeyProperty(x => x.CustomerID);
        SuppressProperty(x => x.RegistrationDate);
        SuppressProperty(x => x.Orders);
    }
}
```
Methods:

```
int categoryID = 12;
model.Project(x=>x.CategoryID = categoryID); //Sets the value into the internal object;
```

```
int categoryID = model.Project(x=>x.CategoryID); //Gets the value of the internal object;
```

```
Customer entity = model.Post(); //return a new instance of the entity.
```
```
model.Patch(entity); //Update the supplied properties on entity.
```
```
model.Put(entity); //Update all properties of the entity.
```
____
*GitHub*
----
> https://github.com/isilveira/ModelWrapper

*Nuget*
----
> https://www.nuget.org/packages/ModelWrapper

*Package Manager*
----
> ```PM> Install-Package ModelWrapper -Version 1.0.1```

*.NET CLI*
----
> ```> dotnet add package ModelWrapper --version 1.0.1```

*Packet*
----
> ```> paket add ModelWrapper --version 1.0.1```
