using System.Threading.Tasks;
using MessagePack;

namespace Microsoft.Extensions.Caching.Distributed
{
    public static class DistributedCacheExtensions
    {
        public static async Task SetObjectAsync<T>(this IDistributedCache distributedCache, string key, T value)
        {
            // 這邊使用 MsgPack 來做序列化，可以使用其他序列化方法，如 Json.Net, System.Text.Json
            byte[] bytes = MessagePackSerializer.Serialize(value);
            await distributedCache.SetAsync(key, bytes);
        }

        public static async Task<T> GetObjectAsync<T>(this IDistributedCache distributedCache, string key)
        {
            // 這邊使用 MsgPack 來做反序列化，可以使用其他反序列化方法，如 Json.Net, System.Text.Json
            var bytes = await distributedCache.GetAsync(key);

            if (bytes is null) return default(T);

            var obj = MessagePackSerializer.Deserialize<T>(bytes);
            return obj;
        }
    }
}