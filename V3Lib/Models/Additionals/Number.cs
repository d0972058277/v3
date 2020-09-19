using MessagePack;

namespace V3Lib.Models.Additionals
{
    [MessagePackObject(true)]
    public class Number
    {
        public decimal Value { get; set; }
    }
}