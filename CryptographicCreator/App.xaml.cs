using Prism.Ioc;
using Prism.Unity;
using Prism.Modularity;
using System.Windows;
using CryptographicCreator.Views;
using RSARegion;
using Commons;
using CryptographicCreator.Models;
using AESRegion;
using RSARegion.Views;
using AESRegion.Views;

namespace CryptographicCreator
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell() 
            =>  Container.Resolve<MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ICompressionService, RSAGZipCompressionService>();
            containerRegistry.Register<ISerializationService, SerializationService>();
            containerRegistry.Register<IRSASerializationService, RSASerializationService>();
            containerRegistry.Register<IStatusBarMessages, StatusBarMessages>();
            containerRegistry.RegisterForNavigation<ViewRSA>("RSARegion");
            containerRegistry.RegisterForNavigation<ViewAES>("AESRegion");
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<RSARegionModule>();
            moduleCatalog.AddModule<AESRegionModule>();
        }
    }
}
