using Microsoft.Win32;
using System.Windows;
using System.Windows.Interactivity;

namespace CryptographicCreator.Behaviors
{

    public class ClosingMainWindowBehavior : Behavior<Window>
    {
        #region Fields

        private const string privateKeyFilterExtension =
            "Private Key (*.prk)|*.prk";
        private const string publicKeyFilterExtension =
            "Public Key (*.pbk)|*.pbk";
        private const string encryptedDataFilterExtension =
            "Encrypred data (*.enc)|*.enc";

        #endregion//Fields

        #region Dependency properties

        public bool IsSavedPublicKey
        {
            get { return (bool)GetValue(IsSavedPublicKeyProperty); }
            set { SetValue(IsSavedPublicKeyProperty, value); }
        }
        
        public static readonly DependencyProperty IsSavedPublicKeyProperty =
            DependencyProperty.Register(
                "IsSavedPublicKey", 
                typeof(bool), 
                typeof(ClosingMainWindowBehavior), 
                new PropertyMetadata(false));

        public bool IsSavedPrivateKey
        {
            get { return (bool)GetValue(IsSavedPrivateKeyProperty); }
            set { SetValue(IsSavedPrivateKeyProperty, value); }
        }
        
        public static readonly DependencyProperty IsSavedPrivateKeyProperty =
            DependencyProperty.Register(
                "IsSavedPrivateKey", 
                typeof(bool),
                typeof(ClosingMainWindowBehavior), 
                new PropertyMetadata(false));

        public bool AreSavedEncryptedData
        {
            get { return (bool)GetValue(AreSavedEncryptedDataProperty); }
            set { SetValue(AreSavedEncryptedDataProperty, value); }
        }
        
        public static readonly DependencyProperty AreSavedEncryptedDataProperty =
            DependencyProperty.Register(
                "AreSavedEncryptedData", 
                typeof(bool), 
                typeof(ClosingMainWindowBehavior), 
                new PropertyMetadata(false));

        public bool AreActiveEncryptedData
        {
            get { return (bool)GetValue(AreActiveEncryptedDataProperty); }
            set { SetValue(AreActiveEncryptedDataProperty, value); }
        }

        public static readonly DependencyProperty AreActiveEncryptedDataProperty =
            DependencyProperty.Register(
                "AreActiveEncryptedData", 
                typeof(bool), 
                typeof(ClosingMainWindowBehavior), 
                new PropertyMetadata(false));

        public bool IsActivePrivateKey
        {
            get { return (bool)GetValue(IsActivePrivateKeyProperty); }
            set { SetValue(IsActivePrivateKeyProperty, value); }
        }

        public static readonly DependencyProperty IsActivePrivateKeyProperty =
            DependencyProperty.Register(
                "IsActivePrivateKey", 
                typeof(bool), 
                typeof(ClosingMainWindowBehavior), 
                new PropertyMetadata(false));
        
        public bool IsActivePublicKey
        {
            get { return (bool)GetValue(IsActivePublicKeyProperty); }
            set { SetValue(IsActivePublicKeyProperty, value); }
        }

        public static readonly DependencyProperty IsActivePublicKeyProperty =
            DependencyProperty.Register(
                "IsActivePublicKey", 
                typeof(bool), 
                typeof(ClosingMainWindowBehavior), 
                new PropertyMetadata(false));

        #endregion//Dependency properties

        #region Methods

        protected override void OnAttached()
        {
            base.OnAttached();
            var window = AssociatedObject as Window;
            if (window != null)
            {
                window.Closing += Window_Closing;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ((IsActivePrivateKey && !IsSavedPrivateKey) || 
                 (IsActivePublicKey && !IsSavedPublicKey) ||
                 (AreActiveEncryptedData && !AreSavedEncryptedData))
            {
                var messageBoxResult = MessageBox.Show(
                    "Some data aren't saved. Do you want close application without saving? Yes - Close without saving, No - Save data and close application, Cancel - cancel exit.",
                    "Attention",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question);
                switch (messageBoxResult)
                {
                    case MessageBoxResult.No:
                        var saveFile = new SaveFileDialog();
                        if (IsActivePrivateKey && !IsSavedPrivateKey)
                        {

                            saveFile.Filter = privateKeyFilterExtension;
                            saveFile.ShowDialog();
                        }

                        if (IsActivePublicKey && !IsSavedPublicKey)
                        {
                            saveFile.Filter = publicKeyFilterExtension;
                            saveFile.ShowDialog();
                        }

                        if (AreActiveEncryptedData && !AreSavedEncryptedData)
                        {
                            saveFile.Filter = encryptedDataFilterExtension;
                            saveFile.ShowDialog();
                        }
                        break;
                   case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        #endregion//Methods
    }
}
