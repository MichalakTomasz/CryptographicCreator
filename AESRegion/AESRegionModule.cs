using AESRegion.Views;
using Commons.Services;
using Prism.Ioc;
using Prism.Modularity;

namespace AESRegion
{
    public class AESRegionModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IAESCryptographicService, AESCryptographicService>();
            containerRegistry.RegisterForNavigation<ViewAES>();
        }
    }
}