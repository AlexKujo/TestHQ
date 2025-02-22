using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class WebsocketClient
    {
        public event Action<Trade> NewBuyTrade;
        public event Action<Trade> NewSellTrade;
        event Action<Candle> CandleSeriesProcessing;

        public void SubscribeTrades(string pair, int maxCount = 100)
        {

        }

        public void UnsubscribeTrades(string pair)
        {

        }

        public void SubscribeCandles(string pair, int periodInSec, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = 0)
        {

        }

        public void UnsubscribeCandles(string pair)
        {

        }
    }
}
