using SHA512Region.Views;
using Prism.Ioc;
using Prism.Modularity;
using Commons;

namespace SHA512Region
{
    public class SHA512RegionModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IHashService, SHA512Service>("SHA512");
            containerRegistry.RegisterForNavigation<ViewSHA512>();
        }
    }
}