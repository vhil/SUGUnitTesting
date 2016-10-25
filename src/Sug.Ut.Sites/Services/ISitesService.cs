using Sitecore.Data.Items;

namespace Sug.Ut.Sites.Services
{
    public interface ISitesService
    {
        Item GetSiteLogoItem(Item renderingItem);
    }
}