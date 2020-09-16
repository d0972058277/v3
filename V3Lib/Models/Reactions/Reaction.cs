namespace V3Lib.Models.Reactions
{
    public abstract class Reaction : Additional<Reaction>
    {
        public string Path { get; set; }
    }
}