namespace MyBookshelf.Core.Models
{
    public class BookInfo
    {
        public string Kind { get; set; }
        public string Id { get; set; }
        public string Etag { get; set; }
        public string SelfLink { get; set; }
        public Book VolumeInfo { get; set; }
    }
}

