using Microsoft.Win32;
using System.Windows;
using System.Windows.Interactivity;

namespace CryptographicCreator.Behaviors
{

    public class ClosingMainWindowBehavior : Behavior<Window>
    {
        #region Fields

        private const string privateRSAKeyFilterExtension =
            "RSA Private Key (*.prk)|*.prk";
        private const string publicRSAKeyFilterExtension =
            "RSA Public Key (*.pbk)|*.pbk";
        private const string rsaEncryptedDataFilterExtension =
            "RSA Encrypred data (*.enc)|*.enc";

        private const string aesKeyFileExtension =
            "AES Key (*.ask)|*.ask";
        private const string aesEncryptedDataFilterExtension =
            "AES Encrypred data (*.aed)|*.aed";

        private const string md5FileFilterExtension =
            "MD5 hash (*.md5)|*.md5";

        #endregion//Fields

        #region Dependency properties

        #region RSA

        public bool IsSavedRSAPublicKey
        {
            get { return (bool)GetValue(IsSavedRSAPublicKeyProperty); }
            set { SetValue(IsSavedRSAPublicKeyProperty, value); }
        }

        public static readonly DependencyProperty IsSavedRSAPublicKeyProperty =
            DependencyProperty.Register(
                "IsSavedRSAPublicKey",
                typeof(bool),
                typeof(ClosingMainWindowBehavior),
                new PropertyMetadata(false));

        public bool IsSavedRSAPrivateKey
        {
            get { return (bool)GetValue(IsSavedRSAPrivateKeyProperty); }
            set { SetValue(IsSavedRSAPrivateKeyProperty, value); }
        }

        public static readonly DependencyProperty IsSavedRSAPrivateKeyProperty =
            DependencyProperty.Register(
                "IsSavedRSAPrivateKey",
                typeof(bool),
                typeof(ClosingMainWindowBehavior),
                new PropertyMetadata(false));

        public bool AreSavedRSAEncryptedData
        {
            get { return (bool)GetValue(AreSavedRSAEncryptedDataProperty); }
            set { SetValue(AreSavedRSAEncryptedDataProperty, value); }
        }

        public static readonly DependencyProperty AreSavedRSAEncryptedDataProperty =
            DependencyProperty.Register(
                "AreSavedRSAEncryptedData",
                typeof(bool),
                typeof(ClosingMainWindowBehavior),
                new PropertyMetadata(false));

        public bool AreActiveRSAEncryptedData
        {
            get { return (bool)GetValue(AreActiveRSAEncryptedDataProperty); }
            set { SetValue(AreActiveRSAEncryptedDataProperty, value); }
        }

        public static readonly DependencyProperty AreActiveRSAEncryptedDataProperty =
            DependencyProperty.Register(
                "AreActiveRSAEncryptedData",
                typeof(bool),
                typeof(ClosingMainWindowBehavior),
                new PropertyMetadata(false));

        public bool IsActiveRSAPrivateKey
        {
            get { return (bool)GetValue(IsActiveRSAPrivateKeyProperty); }
            set { SetValue(IsActiveRSAPrivateKeyProperty, value); }
        }

        public static readonly DependencyProperty IsActiveRSAPrivateKeyProperty =
            DependencyProperty.Register(
                "IsActiveRSAPrivateKey",
                typeof(bool),
                typeof(ClosingMainWindowBehavior),
                new PropertyMetadata(false));

        public bool IsActiveRSAPublicKey
        {
            get { return (bool)GetValue(IsActiveRSAPublicKeyProperty); }
            set { SetValue(IsActiveRSAPublicKeyProperty, value); }
        }

        public static readonly DependencyProperty IsActiveRSAPublicKeyProperty =
            DependencyProperty.Register(
                "IsActiveRSAPublicKey",
                typeof(bool),
                typeof(ClosingMainWindowBehavior),
                new PropertyMetadata(false));

        #endregion//RSA

        #region AES

        public bool IsSavedAESKey
        {
            get { return (bool)GetValue(IsSavedAESKeyProperty); }
            set { SetValue(IsSavedAESKeyProperty, value); }
        }

        public static readonly DependencyProperty IsSavedAESKeyProperty =
            DependencyProperty.Register(
                "IsSavedAESKey",
                typeof(bool),
                typeof(ClosingMainWindowBehavior),
                new PropertyMetadata(false));

        public bool AreSavedAESEncryptedData
        {
            get { return (bool)GetValue(AreSavedAESEncryptedDataProperty); }
            set { SetValue(AreSavedAESEncryptedDataProperty, value); }
        }

        public static readonly DependencyProperty AreSavedAESEncryptedDataProperty =
            DependencyProperty.Register(
                "AreSavedAESEncryptedData",
                typeof(bool),
                typeof(ClosingMainWindowBehavior),
                new PropertyMetadata(false));

        public bool AreActiveAESEncryptedData
        {
            get { return (bool)GetValue(AreActiveAESEncryptedDataProperty); }
            set { SetValue(AreActiveAESEncryptedDataProperty, value); }
        }

        public static readonly DependencyProperty AreActiveAESEncryptedDataProperty =
            DependencyProperty.Register(
                "AreActiveAESEncryptedData", 
                typeof(bool), 
                typeof(ClosingMainWindowBehavior), 
                new PropertyMetadata(false));

        public bool IsActiveAESKey
        {
            get { return (bool)GetValue(IsActiveAESKeyProperty); }
            set { SetValue(IsActiveAESKeyProperty, value); }
        }

        public static readonly DependencyProperty IsActiveAESKeyProperty =
            DependencyProperty.Register(
                "IsActiveAESKey",
                typeof(bool),
                typeof(ClosingMainWindowBehavior),
                new PropertyMetadata(false));

        #endregion//AES

        #region MD5

        public bool IsActiveMD5Checksum
        {
            get { return (bool)GetValue(IsActiveMD5ChecksumProperty); }
            set { SetValue(IsActiveMD5ChecksumProperty, value); }
        }

        public static readonly DependencyProperty IsActiveMD5ChecksumProperty =
            DependencyProperty.Register(
                "IsActiveMD5Checksum",
                typeof(bool),
                typeof(ClosingMainWindowBehavior),
                new PropertyMetadata(false));

        public bool IsSavedMD5CheckSum
        {
            get { return (bool)GetValue(IsSavedMD5CheckSumProperty); }
            set { SetValue(IsSavedMD5CheckSumProperty, value); }
        }

        public static readonly DependencyProperty IsSavedMD5CheckSumProperty =
            DependencyProperty.Register(
                "IsSavedMD5CheckSum",
                typeof(bool),
                typeof(ClosingMainWindowBehavior),
                new PropertyMetadata(false));

        #endregion//MD5

        #endregion//Dependency properties

        #region Methods

        protected override void OnAttached()
        {
            base.OnAttached();
            var window = AssociatedObject as Window;
            if (window != null)
                window.Closing += Window_Closing;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ((IsActiveRSAPrivateKey && !IsSavedRSAPrivateKey) ||
                 (IsActiveRSAPublicKey && !IsSavedRSAPublicKey) ||
                 (AreActiveRSAEncryptedData && !AreSavedRSAEncryptedData ||
                 IsActiveAESKey && !IsSavedAESKey ||
                 AreActiveAESEncryptedData && !AreSavedAESEncryptedData ||
                 IsActiveMD5Checksum && !IsSavedMD5CheckSum))
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
                        if (IsActiveRSAPrivateKey && !IsSavedRSAPrivateKey)
                        {
                            saveFile.Filter = privateRSAKeyFilterExtension;
                            saveFile.ShowDialog();
                        }

                        if (IsActiveRSAPublicKey && !IsSavedRSAPublicKey)
                        {
                            saveFile.Filter = publicRSAKeyFilterExtension;
                            saveFile.ShowDialog();
                        }

                        if (AreActiveRSAEncryptedData && !AreSavedRSAEncryptedData)
                        {
                            saveFile.Filter = rsaEncryptedDataFilterExtension;
                            saveFile.ShowDialog();
                        }

                        if (IsActiveAESKey && !IsSavedAESKey)
                        {
                            saveFile.Filter = aesKeyFileExtension;
                            saveFile.ShowDialog();
                        }

                        if (AreActiveAESEncryptedData && !AreSavedAESEncryptedData)
                        {
                            saveFile.Filter = aesEncryptedDataFilterExtension;
                            saveFile.ShowDialog();
                        }

                        if (IsActiveMD5Checksum && !IsSavedMD5CheckSum)
                        {
                            saveFile.Filter = md5FileFilterExtension;
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
