using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client
{
    internal static class JsonParser
    {
        public static List<T> Parse<T>(string json) where T : IExchangeData
        {           
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}
