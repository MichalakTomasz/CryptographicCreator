using Commons;
using RSARegion.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace RSARegion
{
    public class RSARegionModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IRSACryptographicService, RSACryptographicService>();
            containerRegistry.Register<IRSASerializationService, RSASerializationService>();
            containerRegistry.Register<IRSAMaskService, RSAMaskService>();
            containerRegistry.RegisterForNavigation<ViewRSA>();
        }
    }
}