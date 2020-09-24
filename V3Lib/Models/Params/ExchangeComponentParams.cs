using V3Lib.Models.Components;

namespace V3Lib.Models.Params
{
    public class ExchangeComponentParams : IParams
    {
        public Component TargetComponent { get; set; }
        public Component ExchangeComponent { get; set; }
    }
}