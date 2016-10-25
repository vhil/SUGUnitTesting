using Sitecore.Data.Items;

namespace Sug.Ut.Metadata.Services
{
    public interface IMetadataService
    {
        IPageMetadata GetPageMetadata(Item renderingItem);
    }
}