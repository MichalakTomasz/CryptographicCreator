using Commons;
using CryptographicCreator.Models;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Windows.Interactivity;

namespace CryptographicCreator.Behaviors
{
    public class RibbonButtonOpenSaveRSAKeysBehavior : Behavior<RibbonButton>
    {
        private const string fileFilterExtension = 
            "Private Key (*.prk)|*.prk|Public Key (*.pbk)|*.pbk|Encrypred data (*.enc)|*.enc";

        public string SelectedPath
        {
            get { return (string)GetValue(SelectedPathProperty); }
            set { SetValue(SelectedPathProperty, value); }
        }

        public static readonly DependencyProperty SelectedPathProperty =
            DependencyProperty.Register(
                "SelectedPath", typeof(string),
                typeof(RibbonButtonOpenSaveRSAKeysBehavior),
                new PropertyMetadata(string.Empty));

        public FileAction FileAction
        {
            get { return (FileAction)GetValue(FileActionProperty); }
            set { SetValue(FileActionProperty, value); }
        }
        
        public static readonly DependencyProperty FileActionProperty =
            DependencyProperty.Register(
                "FileAction", 
                typeof(FileAction), 
                typeof(RibbonButtonOpenSaveRSAKeysBehavior));

        public bool IsActivePrivateKey
        {
            get { return (bool)GetValue(IsActivePrivateKeyProperty); }
            set { SetValue(IsActivePrivateKeyProperty, value); }
        }

        public static readonly DependencyProperty IsActivePrivateKeyProperty =
            DependencyProperty.Register(
                "IsActivePrivateKey", 
                typeof(bool), 
                typeof(RibbonButtonOpenSaveRSAKeysBehavior), 
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
                typeof(RibbonButtonOpenSaveRSAKeysBehavior), 
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
                typeof(RibbonButtonOpenSaveRSAKeysBehavior), 
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
                typeof(RibbonButtonOpenSaveRSAKeysBehavior), 
                new PropertyMetadata(false));

        public RSAAction RSAAction
        {
            get { return (RSAAction)GetValue(RSAActionProperty); }
            set { SetValue(RSAActionProperty, value); }
        }
        
        public static readonly DependencyProperty RSAActionProperty =
            DependencyProperty.Register(
                "RSAAction",
                typeof(RSAAction), 
                typeof(RibbonButtonOpenSaveRSAKeysBehavior),
                new PropertyMetadata(RSAAction.None));

        protected override void OnAttached()
        {
            base.OnAttached();
            var button = AssociatedObject as RibbonButton;
            if (button != null)
            {
                button.Click += Button_Click;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (FileAction)
            {
                case FileAction.Open:
                    var openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = fileFilterExtension;
                    if (openFileDialog.ShowDialog().Value)
                    {
                        var selectedPath = openFileDialog.FileName;
                        var extension = Path.GetExtension(selectedPath);
                        switch (extension)
                        {
                            case ".prk":
                                OpenPrivateKey(selectedPath);
                                break;
                            case ".pbk":
                                OpenPublicKey(selectedPath);
                                break;
                            case ".enc":
                                OpenEncryptedData(selectedPath);
                                break;
                        }
                    }
                    break;
                case FileAction.Save:
                    if (IsActivePrivateKey || 
                        IsActivePublicKey || 
                        AreActiveEncryptedData)
                    {
                        var saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = fileFilterExtension;
                        if (saveFileDialog.ShowDialog().Value)
                        {
                            var selectedPath = saveFileDialog.FileName;
                            var extension = Path.GetExtension(selectedPath);
                            switch (extension)
                            {
                                case ".prk":
                                    if (IsActivePrivateKey)
                                        RSAAction = RSAAction.SavePrivateAndPublicKey;
                                    break;
                                case ".pbk":
                                    if (IsActivePublicKey)
                                        RSAAction = RSAAction.SavePublicKey;
                                    break;
                                case ".enc":
                                    if (AreActiveEncryptedData)
                                        RSAAction = RSAAction.SaveEncryptedData;
                                    break;
                            }
                            SelectedPath = saveFileDialog.FileName;
                        }    
                    }
                    else
                    {
                        AcceptEvent = false;
                    }
                    break;
            }
        }

        private void OpenPrivateKey(string selectedPath)
        {
            if (IsActivePrivateKey)
            {
                if (MessageBox.Show(
                    "Private key is opened now, override this key?",
                    "Attention",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    AcceptEvent = false;
                    return;
                }
                else OpenPrivateKeySequence(selectedPath);
            }  
            else OpenPrivateKeySequence(selectedPath);
        }

        private void OpenPrivateKeySequence(string selectedPath)
        {
            SelectedPath = selectedPath;
            RSAAction = RSAAction.OpenPrivateKey;
            AcceptEvent = true;
        }

        private void OpenPublicKey(string selectedPath)
        {
            if (IsActivePublicKey)
            {
                if (MessageBox.Show(
                    "Public key is opened now, override this key?",
                    "Attention",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    AcceptEvent = false;
                    return;
                }
                else OpenPublicKeySequence(selectedPath);
            }
            else OpenPublicKeySequence(selectedPath);
        }

        private void OpenPublicKeySequence(string selectedPath)
        {
            SelectedPath = selectedPath;
            RSAAction = RSAAction.OpenPublicKey;
            AcceptEvent = true;
        }

        private void OpenEncryptedData(string selectedPath)
        {
            if (AreActiveEncryptedData)
            {
                if (MessageBox.Show(
                    "Encrypted data are opened now, override these data?",
                    "Attention",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    AcceptEvent = false;
                    return;
                }
                else OpenEncryptedDataSequence(selectedPath);
            }
            else OpenEncryptedDataSequence(selectedPath);
        }

        private void OpenEncryptedDataSequence(string selectedPath)
        {
            SelectedPath = selectedPath;
            RSAAction = RSAAction.OpenEncryptedData;
            AcceptEvent = true;
        }
    }
}
