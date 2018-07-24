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

## Reference

https://radu-matei.com/blog/aspnet-core-websockets-middleware/