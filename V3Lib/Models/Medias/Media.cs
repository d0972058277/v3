using MessagePack;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Medias
{
    [Union(0, typeof(ImageMedia))]
    [Union(1, typeof(AudioMedia))]
    [Union(2, typeof(VideoMedia))]
    [MessagePackObject(true)]
    [AddJsonTypeName]
    public abstract class Media
    {
        public string Title { get; set; }
        public string Path { get; set; }
    }
}