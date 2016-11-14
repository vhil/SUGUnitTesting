using System.Collections.Generic;
using System.Linq;
using Sitecore.Abstractions;
using Sitecore.Data.Items;
using Sug.Ut.SitecoreExtensions.Extensions;
using Sug.Ut.SitecoreExtensions.Services;

namespace Sug.Ut.Navigation.Services
{
	public class NavigationService : INavigationService
	{
        protected readonly BaseLinkManager LinkManager;
        protected readonly IItemExtensionsService ItemExtensions;

	    public NavigationService(
            BaseLinkManager linkManager,
            IItemExtensionsService itemExtensions)
	    {
	        this.LinkManager = linkManager;
	        this.ItemExtensions = itemExtensions;
	    }

	    public IEnumerable<INavigationNode> GetPrimaryNavigation(Item currentItem)
	    {
	        var navigationRoot = this.GetNavigationRoot(currentItem);
            var navigationItems = this.GetNavigableChildren(navigationRoot);

	        return navigationItems.Select(navItem => this.CreateNavigationNode(navItem, currentItem));
	    }

	    protected virtual INavigationNode CreateNavigationNode(Item navigationItem, Item currentItem)
	    {
            var navigationNode = new NavigationNode(navigationItem.ID)
            {
                IsActive = navigationItem.ID == currentItem.ID,
                Title = navigationItem[Templates.Navigation.FieldNames.NavigationTitle],
                Url = this.LinkManager.GetItemUrl(navigationItem),
            };

	        var children = this.GetNavigableChildren(navigationItem).Select(x => this.CreateNavigationNode(x, currentItem));

	        foreach (var child in children)
	        {
                navigationNode.Children.Add(child);
            }

	        return navigationNode;
	    }

	    protected virtual IEnumerable<Item> GetNavigableChildren(Item item)
	    {
            return item.Children
                .Where(x => this.ItemExtensions.IsDerived(x, Templates.Navigation.TemplateId))
                .Where(x => x[Templates.Navigation.FieldNames.HideFromNavigation] != "1");
        }

	    protected virtual Item GetNavigationRoot(Item contextItem)
	    {
	        return this.ItemExtensions.GetAncestorOrSelfOfTemplate(contextItem, Templates.NavigationRoot.TemplateId);
	    }
	}
}