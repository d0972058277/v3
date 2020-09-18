using MessagePack;

namespace V3Lib.Models.Additionals
{
    [MessagePackObject(true)]
    public struct GoogleAnalytics
    {
        public string Value { get; set; }
    }
}