using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sug.Ut.SitecoreExtensions.Extensions;
using Sug.Ut.Sites.Services;

namespace Sug.Ut.Sites
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ISitesService, SitesService>();
            serviceCollection.AddMvcControllersInCurrentAssembly();
        }
    }
}