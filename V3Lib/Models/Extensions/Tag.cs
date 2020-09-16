using MessagePack;

namespace V3Lib.Models.Extensions
{
    [MessagePackObject(true)]
    public struct Tag
    {
        /// <summary>
        /// Tag 值
        /// </summary>
        public string Value { get; set; }
    }
}