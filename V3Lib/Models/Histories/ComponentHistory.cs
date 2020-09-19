using MessagePack;
using V3Lib.Models.Components;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Histories
{
    public class ComponentHistory : History
    {
        public Component Component { get; set; }
    }
}