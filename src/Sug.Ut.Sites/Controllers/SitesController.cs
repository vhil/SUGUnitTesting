using System.Web.Mvc;
using Helpfulcore.RenderingExceptions;
using Sitecore.Mvc.Presentation;
using Sug.Ut.Sites.Services;

namespace Sug.Ut.Sites.Controllers
{
    public class SitesController : Controller
    {
        protected readonly ISitesService SitesService;

        public SitesController(ISitesService sitesService)
        {
            this.SitesService = sitesService;
        }

        public ActionResult SiteLogo()
        {
            var renderingItem = RenderingContext.Current.Rendering.Item;
            var siteLogoItem = this.SitesService.GetSiteLogoItem(renderingItem);

            if (siteLogoItem == null)
            {
                throw new RenderingParametersException(
                    "Data Source",
                    "field is not specified or can't find the parent item inherited from the Site Logo template.");
            }

            return this.View(siteLogoItem);
        }
    }
}