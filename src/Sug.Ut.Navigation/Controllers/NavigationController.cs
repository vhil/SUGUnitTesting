using System.Web.Mvc;

namespace Sug.Ut.Navigation.Controllers
{
	public class NavigationController : Controller
	{
		public ActionResult PrimaryNavigation()
		{
			return this.View();
		}
	}
}