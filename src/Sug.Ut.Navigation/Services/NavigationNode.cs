using System.Collections.Generic;
using Sitecore.Data;

namespace Sug.Ut.Navigation.Services
{
	public class NavigationNode : INavigationNode
	{
		public NavigationNode(ID itemId)
		{
			this.ItemId = itemId;
			this.Children = new List<INavigationNode>();
			this.Title = string.Empty;
			this.Url = string.Empty;
		}

		public ID ItemId { get; }
		public string Title { get; set; }
		public string Url { get; set; }
		public bool IsActive { get; set; }
		public ICollection<INavigationNode> Children { get; }
	}
}