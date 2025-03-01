using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class BitfinexConnector : ITestConnector
    {
        private RestClient _restClient;
        private WebsocketClient _websocketClient;

        private string _baseUrl;                     //!!!!!!!!!!!!!!

        public BitfinexConnector()
        {
            _restClient = new RestClient();
            _websocketClient = new WebsocketClient();
        }

        #region REST

        public Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount) => _restClient.GetNewTradesAsync(pair, maxCount);

        public Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, int periodInSec, DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0) => _restClient.GetCandleSeriesAsync(pair, periodInSec, from, to, count);

        #endregion

        #region Socket

        public event Action<Trade> NewBuyTrade
        {
            add => _websocketClient.NewBuyTrade += value;
            remove => _websocketClient.NewBuyTrade -= value;
        }

        public event Action<Trade> NewSellTrade
        {
            add => _websocketClient.NewSellTrade += value;
            remove => _websocketClient.NewSellTrade -= value;
        }

        public void SubscribeTrades(string pair, int maxCount = 100) => _websocketClient.SubscribeTrades(pair, maxCount);

        public void UnsubscribeTrades(string pair) => _websocketClient.UnsubscribeTrades(pair);

        public event Action<Candle> CandleSeriesProcessing
        {
            add => _websocketClient.CandleSeriesProcessing += value;
            remove => _websocketClient.CandleSeriesProcessing -= value;
        }

        public void SubscribeCandles(string pair, int periodInSec, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = 0) => _websocketClient.SubscribeCandles(pair, periodInSec, from, to, count);

        public void UnsubscribeCandles(string pair) => _websocketClient.UnsubscribeCandles(pair);

        #endregion
    }
}
