using System.Web.Mvc;
using Sitecore.Mvc.Presentation;
using Sug.Ut.Navigation.Services;

namespace Sug.Ut.Navigation.Controllers
{
	public class NavigationController : Controller
	{
		protected readonly INavigationService NavigationService;

		public NavigationController(INavigationService navigationService)
		{
			this.NavigationService = navigationService;
		}

		public ActionResult PrimaryNavigation()
		{
			var renderingItem = RenderingContext.Current.Rendering.Item;
			var primaryNavigation = this.NavigationService.GetPrimaryNavigation(renderingItem);

			return this.View(primaryNavigation);
		}
	}
}