using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sug.Ut.Metadata.Services;
using Sug.Ut.SitecoreExtensions.Extensions;

namespace Sug.Ut.Metadata
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMetadataService, MetadataService>();
            serviceCollection.AddMvcControllersInCurrentAssembly();
        }
    }
}