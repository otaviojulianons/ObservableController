using System;

namespace ObservableController.WebSockets
{
    public class WebSocketKey
    {
        public WebSocketKey(string channel)
        {
            Id = Guid.NewGuid().ToString();
            Channel = channel;
        }

        public string Id { get; set; }
        public string Channel { get; set; }

        public override string ToString()
        {
            return $"{Channel}{Id}";
        }
    }
}
