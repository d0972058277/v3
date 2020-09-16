namespace V3Lib.Models.Medias
{
    public abstract class Media : Additional<Media>
    {
        public string Title { get; set; }
        public string Path { get; set; }
    }
}