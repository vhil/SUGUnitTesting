using System;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;

namespace Sug.Ut.SitecoreExtensions.Services
{
    public class ItemExtensionsService : IItemExtensionsService
    {
        public bool IsDerived(Item item, ID templateId)
        {
            if (item == null)
            {
                return false;
            }

            return !templateId.IsNull && this.IsDerived(item, item.Database.Templates[templateId]);
        }

        public Item GetAncestorOrSelfOfTemplate(Item item, ID templateId)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return this.IsDerived(item, templateId) 
                ? item 
                : item.Axes.GetAncestors().Reverse().FirstOrDefault(i => this.IsDerived(i, templateId));
        }

        protected virtual bool IsDerived(Item item, Item templateItem)
        {
            if (item == null)
            {
                return false;
            }

            if (templateItem == null)
            {
                return false;
            }

            var itemTemplate = TemplateManager.GetTemplate(item);
            return itemTemplate != null && (itemTemplate.ID == templateItem.ID || itemTemplate.DescendsFrom(templateItem.ID));
        }
    }
}