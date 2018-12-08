# ObservableController

This project aims to abstract
the implementation of observable controllers using web sockets.

## How to implements

### Statup.cs

```csharp
public void ConfigureServices(IServiceCollection services)
{
    .  .  .
    services.UseObservablesControllers();
    services.AddObservableController<DeveloperController, List<Developer>>();
    .  .  .
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    .  .  .
    app.UseWebSockets();
    app.UseObservableControllerMiddleware<DeveloperController, List<Developer>>();
    .  .  .
}        
```

### Controller.cs

Implement the interface *IObservableController*

```csharp
namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("Developers/[action]")]
    public class DeveloperController : Controller, IObservableController<List<Developer>>
    {
        private DeveloperService _service;
        public DeveloperController(DeveloperService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<Developer> GetData() => _service.GetAll();
    }
}
```

## How to consume

```javascript
var socket = new WebSocket(`ws://localhost:5000/Developers/Subscribe`);  
socket.onmessage = function (event) {  
    console.log('message',JSON.parse(event.data));
} 
```
## Sample

http://observable-controller.herokuapp.com/Sample/index.html

## Installing ObservableController

You should install [ObservableController with NuGet](https://www.nuget.org/packages/ObservableController):
```
    Install-Package ObservableController 
```
Or via the .NET Core command line interface:
```
    dotnet add package ObservableController 
```
