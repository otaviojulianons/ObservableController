using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace ObservableController.WebSockets
{
    public class WebSocketCollection
    {
        public ConcurrentDictionary<WebSocketKey, WebSocket> _sockets;
        public Dictionary<string,Type> RoutesInscription { get; set; }

        public WebSocketCollection()
        {
            _sockets = new ConcurrentDictionary<WebSocketKey, WebSocket> ();
            RoutesInscription = new Dictionary<string, Type>();
        }

        public async Task Add(WebSocketHandler socket)
        {
            _sockets.TryAdd(socket.GetId(),socket.GetWebSocket());
        }

        public List<WebSocket> GetAll(string channel)
        {
            return _sockets.Where( x => x.Key.Channel == channel).Select(x => x.Value).ToList();
        }

        public async Task Remove(WebSocketKey id)
        {
            WebSocket socket;
            _sockets.TryRemove(id, out socket);

            await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed by the WebSocketCollection",
                                    cancellationToken: CancellationToken.None);
        }




    }
}
