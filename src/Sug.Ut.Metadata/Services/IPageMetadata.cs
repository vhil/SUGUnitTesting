namespace Sug.Ut.Metadata.Services
{
    public interface IPageMetadata
    {
        string SiteTitle { get; }
        string MetaTitle { get; }
        string MetaDescription { get; }
        string MetaKeywords { get; }
    }
}