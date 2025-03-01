using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
//using static System.Collections.Specialized.BitVector32;
//using static System.Net.WebRequestMethods;

namespace Client
{
    internal class RestClient
    {
        private string _baseUrl;
        private RestSharp.RestClient _restClient;
        private RestSharp.RestClientOptions _restClientOptions;

        public RestClient()
        {
            _baseUrl = "https://api-pub.bitfinex.com/v2/";
            _restClient = new RestSharp.RestClient(new RestClientOptions(_baseUrl));
        }

        public async Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount)
        {
            string endpoint = $"trades/t{pair}/hist?limit={maxCount}";
            var request = new RestRequest(endpoint);
            var response = await _restClient.GetAsync(request);

            if (response.IsSuccessful != true || response.Content == null)
                return new List<Trade>();

            return JsonParser.Parse<Trade>(response.Content);
        }

        public async Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, int periodInSec, DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0)
        {
            string startParameter = "start";
            string endParameter = "end";
            string countParameter = "count";

            string endpoint = $"candles/trade:{periodInSec}:t{pair}/hist";
            var request = new RestRequest(endpoint);
            
            if (from.HasValue)
                request.AddQueryParameter(startParameter, GetUnixTime(from));

            if (to.HasValue)
                request.AddQueryParameter(endParameter, GetUnixTime(to));

            if (count.HasValue && count > 0)
                request.AddQueryParameter(countParameter, count.ToString());

            var response = await _restClient.GetAsync(request);

            if (response.IsSuccessful != true || response.Content == null)
                return new List<Candle>();

            return JsonParser.Parse<Candle>(response.Content);
        }

        private string GetUnixTime(DateTimeOffset? time) => time?.ToUnixTimeMilliseconds().ToString();
    }
}
