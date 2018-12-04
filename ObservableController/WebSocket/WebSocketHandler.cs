using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace ObservableController.WebSockets
{
    public class WebSocketHandler
    {
        private WebSocketManager _manager;
        private WebSocket _webSocket;
        private WebSocketKey _id;

        public WebSocketHandler(WebSocketManager manager, WebSocket webSocket,string channel)
        {
            _manager = manager;
            _webSocket = webSocket;
            _id = new WebSocketKey(channel);
        }

        public async Task Invoke<TDataObject>(HttpContext context,List<TDataObject> initialData)
        {
            await _manager.AddWebSocketHandler(this);
            await _webSocket.SendData(initialData);

            var data = await _webSocket.ReceiveData();
            while (!data.result.CloseStatus.HasValue)
            {
                await _manager.SendAll(_id.Channel,data.GetValue());
                data = await _webSocket.ReceiveData();
            }
            await _webSocket.CloseAsync(data.result.CloseStatus.Value, data.result.CloseStatusDescription, CancellationToken.None);
        }

        public WebSocket GetWebSocket()
        {
            return _webSocket;
        }

        public WebSocketKey GetId()
        {
            return _id;
        }

    }
}
