using ModuleREARegion.Views;
using Prism.Ioc;
using Prism.Regions;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;

namespace CryptographicCreator.Views
{
    public partial class MainWindow : RibbonWindow
    {
        private readonly IContainerExtension container;
        private readonly IRegionManager regionManager;

        public MainWindow(IContainerExtension container, IRegionManager regionManager)
        {
            InitializeComponent();
            this.container = container;
            this.regionManager = regionManager;
            ribbonMenu.SelectionChanged += RibbonMenu_SelectionChanged;
        }

        private void RibbonMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTab = e.AddedItems[0] as RibbonTab;
            switch (selectedTab.Header)
            {
                case "RSA":
                    var region = regionManager.Regions["RSARegion"];
                    var rsaView = container.Resolve<ViewRSA>();
                    region.Add(rsaView);
                    break;
            }
        }
    }
}
