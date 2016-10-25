using Sitecore.Data.Items;
using Sug.Ut.SitecoreExtensions.Services;

namespace Sug.Ut.Sites.Services
{
    public class SitesService : ISitesService
    {
        protected readonly IItemExtensionsService ItemExtensions;

        public SitesService(IItemExtensionsService itemExtensions)
        {
            this.ItemExtensions = itemExtensions;
        }

        public Item GetSiteLogoItem(Item renderingItem)
        {
            return this.ItemExtensions.IsDerived(renderingItem, Templates.SiteLogo.TemplateId)
                ? renderingItem
                : this.ItemExtensions.GetAncestorOrSelfOfTemplate(renderingItem, Templates.SiteLogo.TemplateId);
        }
    }
}