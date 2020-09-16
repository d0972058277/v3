using MessagePack;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Medias
{
    [MessagePackObject(true)]
    [AddJsonTypeName]
    public class VideoMedia : Media { }
}