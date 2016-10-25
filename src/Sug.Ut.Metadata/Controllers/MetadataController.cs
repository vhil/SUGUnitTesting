using System.Web.Mvc;
using Sitecore.Mvc.Presentation;
using Sug.Ut.Metadata.Services;

namespace Sug.Ut.Metadata.Controllers
{
    public class MetadataController : Controller
    {
        protected readonly IMetadataService MetadataService;

        public MetadataController(IMetadataService metadataService)
        {
            this.MetadataService = metadataService;
        }

        public ActionResult PageMetadata()
        {
            var renderingItem = RenderingContext.Current.Rendering.Item;
            var pageMetadata = this.MetadataService.GetPageMetadata(renderingItem);

            return this.View(pageMetadata);
        }
    }
}