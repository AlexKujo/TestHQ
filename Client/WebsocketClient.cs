using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Client
{
    internal class WebsocketClient
    {
        public event Action<Trade> NewBuyTrade;
        public event Action<Trade> NewSellTrade;
        public event Action<Candle> CandleSeriesProcessing;

        private string _baseUrl = "wss://api-pub.bitfinex.com/ws/2";

        private WebSocket _webSocket;

        public WebsocketClient()
        {
            _webSocket = new WebSocket(_baseUrl);
            
            _webSocket.OnMessage += HandleMessage;
        }

        public void Connect() => _webSocket.Connect();
        public void Close() => _webSocket.Close();

        private void HandleMessage(object sender, MessageEventArgs e)
        {
            var json = JArray.Parse(e.Data);

            if (json.Count > 1 && json[1] is JArray trades)
            {
                // Обрабатываем все трейды
                foreach (var tradeData in trades)
                {
                    var tradeArray = tradeData as JArray;
                    ProcessTrade(tradeArray); 
                }
            }
        }

        private void ProcessTrade(JArray tradeData)
        {
            var newTrade = new Trade
            {
                Id = tradeData[0].ToString(),
                Time = DateTimeOffset.FromUnixTimeMilliseconds(tradeData[1].ToObject<long>()),
                Amount = Math.Abs(tradeData[2].ToObject<decimal>()),
                Price = tradeData[3].ToObject<decimal>(),
                Side = tradeData[2].ToObject<decimal>() > 0 ? "buy" : "sell"
            };

            if (newTrade.Side == "buy")
                NewBuyTrade?.Invoke(newTrade);
            else
                NewSellTrade?.Invoke(newTrade);
        }

        public void SubscribeTrades(string pair, int maxCount = 100)
        {
            var subscribeeMessage = new
            {
                @event = "subscribe",
                channel = "trades",
                symbol = $"t{pair.ToUpper()}"
            };

            _webSocket.Send(JsonConvert.SerializeObject(subscribeeMessage));

        }

        public void UnsubscribeTrades(string pair)
        {
            if (_webSocket.ReadyState != WebSocketState.Open)
                return;

            var unsubscribeMessage = new
            {
                @event = "unsubscribe",
                channel = "trades",
                symbol = $"t{pair.ToUpper()}"
            };

            _webSocket.Send(JsonConvert.SerializeObject(unsubscribeMessage));
        }

        public void SubscribeCandles(string pair, int periodInSec, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = 0)
        {

        }

        public void UnsubscribeCandles(string pair)
        {

        }
    }
}
