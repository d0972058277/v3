using System;
using MessagePack;

namespace V3Lib.Models.Additionals
{
    [MessagePackObject(true)]
    public class EndDateTime
    {
        public DateTime DateTime { get; set; }
    }
}