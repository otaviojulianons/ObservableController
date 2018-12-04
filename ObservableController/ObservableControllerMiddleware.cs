using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ObservableController.WebSockets;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObservableController
{
    public class ObservableControllerMiddleware<TController, TDataObject> where TController : Controller, IObservableController<TDataObject>
    {

        private RequestDelegate _next;
        private readonly TController _controller;
        private WebSockets.WebSocketManager _webSocketCollection;
        private List<string> _routesController;
        private string _channel;

        public ObservableControllerMiddleware(RequestDelegate next, IActionDescriptorCollectionProvider routes, TController  controller, WebSockets.WebSocketManager webSocketCollection)
        {
            _next = next;
            _controller = controller;
            _webSocketCollection = webSocketCollection;
            _routesController = routes.ActionDescriptors.Items
                .Where( x => x.DisplayName.StartsWith(controller.GetType().FullName))
                .Select(x => $"/{x.AttributeRouteInfo.Template}").ToList();

            _channel = _routesController.Where(r => r.Contains("GetData")).FirstOrDefault().Replace("GetData", "Subscribe");
            webSocketCollection.Channels.Add(_channel, controller.GetType());

        }

        public Task Invoke(HttpContext httpContext)
        {
            var result = _next(httpContext);
            if (_routesController.Contains(httpContext.Request.Path.Value))
                _webSocketCollection.SendAll(_channel, _controller.GetData());

            return result;
        }



    }
}
