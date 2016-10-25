using System.Collections.Generic;
using Sitecore.Data.Items;

namespace Sug.Ut.Navigation.Services
{
	public interface INavigationService
	{
		IEnumerable<INavigationNode> GetPrimaryNavigation(Item currentItem);
	}
}
