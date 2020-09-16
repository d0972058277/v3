using MessagePack;

namespace V3Lib.Models.Extensions
{
    [MessagePackObject(true)]
    public struct Number
    {
        public decimal Value { get; set; }
    }
}