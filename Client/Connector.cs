using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Connector : ITestConnector
    {
        private RestClient _restClient;
        private WebsocketClient _websocketClient;

        public Connector()
        {
            _restClient = new RestClient();
            _websocketClient = new WebsocketClient();
        }

        public Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount) => _restClient.GetNewTradesAsync(pair, maxCount);

        public Task<IEnumerable<Candle>> GetCandlesAsync(string pair, int periodInSec, DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0) => _restClient.GetCandleSeriesAsync(pair, periodInSec, from, to, count);
    }
}
