using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sug.Ut.Navigation.Services;
using Sug.Ut.SitecoreExtensions.Extensions;

namespace Sug.Ut.Navigation
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            serviceCollection.AddMvcControllersInCurrentAssembly();
        }
    }
}