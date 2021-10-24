using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities.Helpers
{
    public class JSONHelper
    {
        public static byte[] Serialize(object item)
        {
            var jsonString = JsonConvert.SerializeObject(item);
            return Encoding.UTF8.GetBytes(jsonString);
        }

        public static T Deserialize<T>(byte[] item)
        {
            if (item == null)
                return default(T);

            var jsonString = Encoding.UTF8.GetString(item);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static T Deserialize<T>(string item)
        {
            if (item == null)
                return default(T);
            return JsonConvert.DeserializeObject<T>(item);
        }

        public static bool IsNullOrEmpty(JToken token)
        {
            return (token == null) ||
                   (token.Type == JTokenType.Array && !token.HasValues) ||
                   (token.Type == JTokenType.Object && !token.HasValues) ||
                   (token.Type == JTokenType.String && token.ToString() == String.Empty) ||
                   (token.Type == JTokenType.Null);
        }
    }
}
