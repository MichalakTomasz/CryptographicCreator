using Prism.Ioc;
using Prism.Unity;
using Prism.Modularity;
using System.Windows;
using CryptographicCreator.Views;
using RSARegion;
using Commons;
using CryptographicCreator.Models;
using AESRegion;
using MD5Region;
using SHA512Region;

namespace CryptographicCreator
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell() 
            =>  Container.Resolve<MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ISerializationService, SerializationService>();
            containerRegistry.Register<IGZipCompressionService, GZipCompressionService>();
            containerRegistry.Register<ISimpleMaskService, SimpleMaskService>();
            containerRegistry.Register<IStatusBarMessages, StatusBarMessages>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<RSARegionModule>();
            moduleCatalog.AddModule<AESRegionModule>();
            moduleCatalog.AddModule<MD5RegionModule>();
            moduleCatalog.AddModule<SHA512RegionModule>();
        }
    }
}
