using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;

namespace V3Lib
{
    public class V3DistributedCacheOptions : RedisCacheOptions
    {
        public V3DistributedCacheOptions() : base()
        {
            string environmentName = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")) ? throw new ArgumentNullException("ASPNETCORE_ENVIRONMENT") : Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            base.InstanceName = $"V3.{environmentName}.";
        }

        public new string InstanceName => base.InstanceName;
    }

    public interface IV3DistributedCache : IDistributedCache { }

    public class V3DistributedCache : RedisCache, IV3DistributedCache
    {
        public V3DistributedCache(IOptions<V3DistributedCacheOptions> optionsAccessor) : base(optionsAccessor) { }
    }
}