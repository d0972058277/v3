using MessagePack;

namespace V3Lib.Models.Conditions
{
    [MessagePackObject(true)]
    public struct Community : IConditionField
    {
        public string Id { get; set; }

        public static bool operator ==(Community a, Community b) => a.Equals(b);

        public static bool operator !=(Community a, Community b) => !a.Equals(b);
    }
}