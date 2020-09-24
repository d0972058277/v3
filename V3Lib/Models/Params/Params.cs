namespace V3Lib.Models.Params
{
    public class Params<T> : IParams
    {
        public T Value { get; set; }
    }
}