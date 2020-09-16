using MessagePack;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Styles
{
    [MessagePackObject(true)]
    [AddJsonTypeName]
    public class RecommendationStyle : Style { }
}