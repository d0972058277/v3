namespace V3Lib.Models.Params
{
    public class MongoStrategyParams : IStrategyParams
    {
        public string Database { get; set; }
        public string Collection { get; set; }
        public string TargetKey { get; set; }
        public int StackSize { get; set; }
    }
}