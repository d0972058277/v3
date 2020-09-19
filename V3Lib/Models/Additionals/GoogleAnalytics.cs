using MessagePack;

namespace V3Lib.Models.Additionals
{
    [MessagePackObject(true)]
    public class GoogleAnalytics
    {
        public string Value { get; set; }
    }
}