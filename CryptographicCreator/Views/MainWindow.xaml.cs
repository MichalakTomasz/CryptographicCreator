using AESRegion.Views;
using RSARegion.Views;
using Prism.Ioc;
using Prism.Regions;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;

namespace CryptographicCreator.Views
{
    public partial class MainWindow : RibbonWindow
    {
        private readonly IRegionManager regionManager;

        public MainWindow(IContainerExtension container, IRegionManager regionManager)
        {
            InitializeComponent();
            this.regionManager = regionManager;
            ribbonMenu.SelectionChanged += RibbonMenu_SelectionChanged;
        }

        private void RibbonMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTab = e.AddedItems[0] as RibbonTab;
            switch (selectedTab.Header)
            {
                case "RSA":
                    regionManager.RequestNavigate("ContentRegion", "ViewRSA");
                    break;
                case "AES":
                    regionManager.RequestNavigate("ContentRegion", "ViewAES");
                    break;
                case "MD5":
                    regionManager.RequestNavigate("ContentRegion", "ViewMD5");
                    break;
            }
        }
    }
}
