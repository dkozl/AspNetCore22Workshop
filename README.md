# Workshop about the features of ASP.NET Core 2.2

## Prerequisites

+ .NET Core SDK 2.2 (2.2.104 at the time of writing) [[download](https://dotnet.microsoft.com/download)]
+ Visual Studio 2017 or Visual Studio Code

## Middleware

+ What is a middleware
+ How to create one

## Health checks

+ What are health checks
+ How do they work
+ How to set it up

### Simple health check to check connection to DB

```cs
async ct =>
{
    using (var connection = new SqlConnection(_configuration.GetConnectionString("BlogDatabase")))
    {
        await connection.OpenAsync(ct);
        using (var command = new SqlCommand("SELECT 1", connection))
        {
            await command.ExecuteScalarAsync(ct);
        }

        return HealthCheckResult.Healthy();
    }
}
```

### Healthcheck setup

```cs
UseHealthChecks("/healthcheck", new HealthCheckOptions {Predicate = hc => hc.Tags.Contains("db")})
```

[Health checks in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-2.2)


Nuget packages:
+ [AspNetCore.HealthChecks.SqlServer](https://www.nuget.org/packages/AspNetCore.HealthChecks.SqlServer/) / [GitHub](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks)

## Configuration

+ How does it work
+ Setup `BlogSettings` options
    + from .json 
    + from .xml
    + from `Action<T>`

## Dependency Injection

+ How to use default DI
+ What are the limitations
+ How to refactor to 3rd party DI (AutoFac)

### AutoFac setup

#### With `ConfigureContainer`

##### Program.cs
```cs
.ConfigureServices(services => services.AddAutofac())
```

##### Startup.cs
```cs
public void ConfigureContainer(ContainerBuilder builder)
{
    builder.RegisterType<BlogRepository>().As<IBlogRepository>();
}
```

#### Without `ConfigureContainer`

```cs
public IServiceProvider ConfigureServices(IServiceCollection services)
{
    //

    var containerBuilder = new ContainerBuilder();
    containerBuilder.Populate(services);
    containerBuilder.RegisterType<BlogRepository>().As<IBlogRepository>();
    return new AutofacServiceProvider(containerBuilder.Build());
}
```

### Nuget packages: 
+ [Autofac.Extensions.DependencyInjection](https://www.nuget.org/packages/Autofac.Extensions.DependencyInjection/) / [Docs](https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html)
+ [Scrutor](https://www.nuget.org/packages/Scrutor/) / [GitHub](https://github.com/khellang/Scrutor)

## Logging

+ Default logging framework
+ Adding 3rd party loggers (NLog)

### Nuget packages: 
+ [NLog.Web.AspNetCore](https://www.nuget.org/packages/NLog.Web.AspNetCore)

## Files abstraction

+ How to abstract dependency of physical file and write testable code

## API Documentation

+ Setup Swagger JSON
+ Setup Swagger UI
+ Create custom `ApiInfo` view  

Nuget packages: 
+ [Swashbuckle.AspNetCore](https://www.nuget.org/packages/swashbuckle.aspnetcore/) / [GitHub](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

## Views

+ Difference between Razor Pages and Views
+ Introduction to View Components

### Razor pages - [Introduction to Razor Pages in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-2.2&tabs=visual-studio)

#### Razor page directive

```
@page
```

#### Page model

```cs
public class BlogModel : PageModel
```

#### GET action

```cs
public async Task OnGetAsync()
{
   //
}
```

### View components - [View components in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components?view=aspnetcore-2.2)

#### View Component class

```cs
[ViewComponent(Name = "Blog")]
public class BlogComponent : ViewComponent
```

#### Invoke action

```cs
public Task<IViewComponentResult> InvokeAsync(Blog blog)
{
   //
}
```

#### View Component usage

```html
<vc:blog blog="blog"></vc:blog>
```

## Testing

+ `Startup.cs` integration testing
+ Code coverage

### Collect test coverage

```
dotnet test .\Workshop.App.Tests\ /p:CollectCoverage=true /p:Exclude="[xunit.*]*"
```

[Integration tests in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-2.2)

### Nuget packages:
+ [coverlet.msbuild](https://www.nuget.org/packages/coverlet.msbuild/) / [GitHub](https://github.com/tonerdo/coverlet)
+ [Microsoft.AspNetCore.Mvc.Testing](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Testing/)
