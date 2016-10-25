using Microsoft.Extensions.DependencyInjection;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sug.Ut.SitecoreExtensions.Services;

namespace Sug.Ut.SitecoreExtensions.Extensions
{
    public static class ItemExtensions
    {
        private static IItemExtensionsService Service
        {
            get { return ServiceLocator.ServiceProvider.GetService<IItemExtensionsService>(); }
        }

        public static bool IsDerived(this Item item, ID templateId)
        {
            return Service.IsDerived(item, templateId);
        }
    }
}