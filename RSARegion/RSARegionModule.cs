using ModuleREARegion.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleREARegion
{
    public class RSARegionModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("RSARegion", typeof(ViewA));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}