using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class RestClient
    {
        public Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount)
        {

        }

        public Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, int periodInSec, DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0)
        {

        }

    }
}
