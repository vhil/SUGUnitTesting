using System.Collections.Generic;
using Sitecore.Data;

namespace Sug.Ut.Navigation
{
	public interface INavigationNode
	{
		ID ItemId { get; }
		string Title { get; }
		string Url { get; }
		bool IsActive { get; }
		ICollection<INavigationNode> Children { get; }
	}
}