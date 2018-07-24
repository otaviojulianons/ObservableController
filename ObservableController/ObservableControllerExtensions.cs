using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using ObservableController.WebSockets;

namespace ObservableController
{
    public static class ObservableControllerExtensions
    {

        public static string GetChannelId<T>(this IObservableController<T> controller)
        {
            return controller.GetType().FullName;
        }

        public static IServiceCollection UseObservablesControllers(this IServiceCollection services)
        {
            services.AddSingleton<WebSocketCollection>();
            return services;
        }

        public static IServiceCollection AddObservableController<TController,TDataObject>(this IServiceCollection services) where TController : Controller, IObservableController<TDataObject>
        {
            services.AddTransient<TController>();
            return services;
        }

        public static IApplicationBuilder UseObservableControllerMiddleware<TController,TDataObject>(this IApplicationBuilder app) where TController : Controller, IObservableController<TDataObject>
        {
            app.UseMiddleware<ObservableControllerMiddleware<TController, TDataObject>>();

            app.Use(async (context, next) =>
            {
                WebSocketCollection webSocketContainer = (WebSocketCollection)context.RequestServices.GetService(typeof(WebSocketCollection));

                if (webSocketContainer.RoutesInscription.ContainsKey(context.Request.Path))
                {
                    var type = webSocketContainer.RoutesInscription[context.Request.Path];
                    dynamic controllerWebSocket = context.RequestServices.GetService(type);
                    var initialData = controllerWebSocket.GetData();
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        await new WebSocketHandler(webSocketContainer, webSocket, context.Request.Path).Invoke(context, initialData);
                    }
                    else
                        context.Response.StatusCode = 200;
                }
                else
                    await next();
            });
            return app;
        }

    }
}
