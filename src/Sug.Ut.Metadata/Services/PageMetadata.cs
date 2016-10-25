namespace Sug.Ut.Metadata.Services
{
    public class PageMetadata : IPageMetadata
    {
        public PageMetadata()
        {
            this.SiteTitle = string.Empty;
            this.MetaTitle = string.Empty;
            this.MetaDescription = string.Empty;
            this.MetaKeywords = string.Empty;
        }

        public string SiteTitle { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
    }
}