using Prism.Ioc;
using Prism.Unity;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CryptographicCreator.Views;
using ModuleREARegion;
using Commons;

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
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<RSARegionModule>();
        }
    }

}
