using Commons;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Windows.Interactivity;

namespace CryptographicCreator.Behaviors
{
    public class RibbonButtonGenerateAESKeyBehavior : Behavior<RibbonButton>
    {
        #region Dependency properties

        public bool IsActiveKey
        {
            get { return (bool)GetValue(IsActiveKeyProperty); }
            set { SetValue(IsActiveKeyProperty, value); }
        }

        public static readonly DependencyProperty IsActiveKeyProperty =
            DependencyProperty.Register(
                "IsActiveKey",
                typeof(bool),
                typeof(RibbonButtonGenerateAESKeyBehavior),
                new PropertyMetadata(false));

        public bool IsSavedKey
        {
            get { return (bool)GetValue(IsSavedKeyProperty); }
            set { SetValue(IsSavedKeyProperty, value); }
        }

        public static readonly DependencyProperty IsSavedKeyProperty =
            DependencyProperty.Register(
                "IsSavedKey",
                typeof(bool),
                typeof(RibbonButtonGenerateAESKeyBehavior),
                new PropertyMetadata(false));

        public bool AcceptEvent
        {
            get { return (bool)GetValue(AcceptEventProperty); }
            set { SetValue(AcceptEventProperty, value); }
        }

        public static readonly DependencyProperty AcceptEventProperty =
            DependencyProperty.Register(
                "AcceptEvent",
                typeof(bool),
                typeof(RibbonButtonGenerateAESKeyBehavior),
                new PropertyMetadata(false));


        public AESAction AESAction
        {
            get { return (AESAction)GetValue(AESActionProperty); }
            set { SetValue(AESActionProperty, value); }
        }
        
        public static readonly DependencyProperty AESActionProperty =
            DependencyProperty.Register(
                "AESAction", 
                typeof(AESAction), 
                typeof(RibbonButtonGenerateAESKeyBehavior), 
                new PropertyMetadata(AESAction.None));

        #endregion//Dependency properties

        #region Methods

        protected override void OnAttached()
        {
            base.OnAttached();
            var button = AssociatedObject as RibbonButton;
            if (button != null)
                button.Click += Button_Click;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsActiveKey && !IsSavedKey)
            {
                if (MessageBox.Show(
                    "Key is not saved and will be lost. Do you still generate key?",
                    "Attention",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning)
                    == MessageBoxResult.Yes)
                {
                    AcceptEvent = true;
                    AESAction = AESAction.Generate;
                }
                else AcceptEvent = false;
            }
            else
            {
                AcceptEvent = true;
                AESAction = AESAction.Generate;
            }
        }

        #endregion//Methods
    }
}
