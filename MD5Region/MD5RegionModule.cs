using Commons;
using MD5Region.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MD5Region
{
    public class MD5RegionModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IHashService, MD5Service>();
            containerRegistry.RegisterForNavigation<ViewMD5>();
        }
    }
}