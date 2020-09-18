using MessagePack;

namespace V3Lib.Models.Additionals
{
    [MessagePackObject(true)]
    public struct Number
    {
        public decimal Value { get; set; }
    }
}