using System.Collections.Generic;
using Sitecore.Data.Items;

namespace Sug.Ut.Navigation
{
	public class NavigationService : INavigationService
	{
		public IEnumerable<INavigationNode> GetPrimaryNavigation(Item contextItem)
		{
			yield break;
		}
	}
}