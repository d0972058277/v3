using Newtonsoft.Json;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models
{
    public static class PrototypeExtensions
    {
        public static T Clone<T>(this T obj)
        {
            var json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new JsonTypeNameContractResolver()
            });

            // 使用自身的 Type
            var clone = (T) JsonConvert.DeserializeObject(json, obj.GetType(), new JsonSerializerSettings
            {
                ContractResolver = new JsonTypeNameContractResolver()
            });

            return clone;
        }
    }
}