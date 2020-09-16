using MessagePack;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Styles
{
    [Union(0, typeof(BannerStyle))]
    [Union(1, typeof(RecommendationStyle))]
    [MessagePackObject(true)]
    [AddJsonTypeName]
    public abstract class Style : Additional<Style> { }
}