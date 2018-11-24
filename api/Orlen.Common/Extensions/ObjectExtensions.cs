using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Net.Http;
using System.Text;

namespace Orlen.Common.Extensions
{
    public static class ObjectExtensions
    {
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        public static string Stringify(this object obj)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }

        public static StringContent AsJson(this object o)
        {
            return new StringContent(JsonConvert.SerializeObject(o, settings), Encoding.UTF8, "application/json");
        }

        public static JContainer AsJContainer(this object o)
        {
            var serializer = new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            if (o is IEnumerable)
                return JArray.FromObject(o, serializer);

            return JObject.FromObject(o, serializer);
        }
        public static JObject AsJObject(this object o)
        {
            var serializer = new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return JObject.FromObject(o, serializer);
        }
    }
}