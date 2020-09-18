using MessagePack;

namespace V3Lib.Models.Conditions
{
    [MessagePackObject(true)]
    public class Village
    {
        public string Name { get; set; }
    }
}