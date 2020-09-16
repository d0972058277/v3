using MessagePack;

namespace V3Lib.Models.Extensions
{
    [MessagePackObject(true)]
    public struct GoogleAnalytics
    {
        public string Value { get; set; }
    }
}