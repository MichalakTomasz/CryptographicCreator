using SHA256Region.Views;
using Prism.Ioc;
using Prism.Modularity;
using Commons;

namespace SHA256Region
{
    public class SHA256RegionModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IHashService, SHA256Service>("SHA256");
            containerRegistry.RegisterForNavigation<ViewSHA256>();
        }
    }
}