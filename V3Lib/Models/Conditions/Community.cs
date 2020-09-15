namespace V3Lib.Models.Conditions
{
    public struct Community : IConditionField
    {
        public string Id { get; set; }

        public static bool operator ==(Community a, Community b) => a.Equals(b);

        public static bool operator !=(Community a, Community b) => !a.Equals(b);
    }
}