using System.Collections.Generic;
using Sitecore.Data.Items;

namespace Sug.Ut.Navigation
{
	public interface INavigationService
	{
		IEnumerable<INavigationNode> GetPrimaryNavigation(Item contextItem);
	}
}
