using MessagePack;

namespace V3Lib.Models.Additionals
{
    [MessagePackObject(true)]
    public class Tag
    {
        /// <summary>
        /// Tag 值
        /// </summary>
        public string Value { get; set; }
    }
}