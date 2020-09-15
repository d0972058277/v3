namespace V3Lib.Models.Conditions
{
    public struct Location : IConditionField
    {
        public string City { get; set; }
        public string Area { get; set; }
        public string Village { get; set; }

        public static bool operator ==(Location a, Location b) => a.Equals(b);

        public static bool operator !=(Location a, Location b) => !a.Equals(b);
    }
}