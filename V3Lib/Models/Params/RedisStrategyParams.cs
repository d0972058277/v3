namespace V3Lib.Models.Params
{
    public class RedisStrategyParams : IStrategyParams
    {
        public string Cachekey { get; set; }
        public string TargetKey { get; set; }
    }
}