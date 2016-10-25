using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sug.Ut.SitecoreExtensions.Services
{
    public interface IItemExtensionsService
    {
        bool IsDerived(Item item, ID templateId);
        Item GetAncestorOrSelfOfTemplate(Item item, ID templateId);
    }
}