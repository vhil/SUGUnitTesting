using Sitecore.Data.Items;
using Sug.Ut.SitecoreExtensions.Services;

namespace Sug.Ut.Metadata.Services
{
    public class MetadataService : IMetadataService
    {
        protected readonly IItemExtensionsService ItemExtensions;

        public MetadataService(IItemExtensionsService itemExtensions)
        {
            this.ItemExtensions = itemExtensions;
        }

        public IPageMetadata GetPageMetadata(Item renderingItem)
        {
            var websiteMetadataItem = this.GetWebsiteMetadataItem(renderingItem);

            var pageMetadata = new PageMetadata();

            if (websiteMetadataItem != null)
            {
                pageMetadata.SiteTitle = websiteMetadataItem[Templates.WebsiteMetadata.FieldNames.MetaTitle];
            }

            var pageMetadataItem = this.ItemExtensions.IsDerived(renderingItem, Templates.Metadata.TemplateId)
                ? renderingItem
                : null;

            if (pageMetadataItem != null)
            {
                pageMetadata.MetaTitle = pageMetadataItem[Templates.Metadata.FieldNames.MetaTitle];
                pageMetadata.MetaDescription = pageMetadataItem[Templates.Metadata.FieldNames.MetaDescription];
                pageMetadata.MetaKeywords = pageMetadataItem[Templates.Metadata.FieldNames.MetaKeywords];
            }

            return pageMetadata;
        }

        protected virtual Item GetWebsiteMetadataItem(Item renderingItem)
        {
            return this.ItemExtensions.GetAncestorOrSelfOfTemplate(renderingItem, Templates.WebsiteMetadata.TemplateId);
        }
    }
}