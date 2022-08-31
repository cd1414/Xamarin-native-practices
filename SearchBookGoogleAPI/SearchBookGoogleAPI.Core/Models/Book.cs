namespace MyBookshelf.Core.Models
{
    public class Book
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string[] Authors { get; set; }
        public string Publisher { get; set; }
        public string PublishedDate { get; set; }
        public string Description { get; set; }
        public IndustryIdentifier[] IndustryIdentifier { get; set; }
        public int PageCount { get; set; }
        public int PrintedPageCount { get; set; }
        public string PrintType { get; set; }
        public string[] Categories { get; set; }
        public ImageLinks ImageLinks { get; set; }
        public int Progress { get; set; }
        public int Status { get; set; }
    }
}

