using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sug.Ut.SitecoreExtensions.Services;

namespace Sug.Ut.SitecoreExtensions
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IItemExtensionsService, ItemExtensionsService>();
        }
    }
}