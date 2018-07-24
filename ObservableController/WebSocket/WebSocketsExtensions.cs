using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObservableController.WebSockets
{
    public static class WebSocketsExtensions
    {

        public static async Task SendData(this WebSocket webSocket, object data)
        {
            var json = JsonConvert.SerializeObject(data);
            if (!webSocket.CloseStatus.HasValue)
            {
                var bytes = Encoding.UTF8.GetBytes(json);
                await webSocket.SendAsync(new ArraySegment<byte>(bytes, 0, bytes.Length ), WebSocketMessageType.Text, true, CancellationToken.None);
            }

               
        }

        public static async Task<(WebSocketReceiveResult result,byte[] buffer)> ReceiveData(this WebSocket webSocket)
        {
            var buffer = new byte[1024];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            return (result, buffer);
        }

        public static string GetValue(this (WebSocketReceiveResult result, byte[] buffer) data)
        {
            return Encoding.UTF8.GetString(data.buffer, 0, data.result.Count);
        }

        public static async Task SendAll(this WebSocketCollection webSocketManager,string channel, object data)
        {
            foreach (var webSocket in webSocketManager.GetAll(channel))
                await webSocket.SendData(data);
        }

    }
}
