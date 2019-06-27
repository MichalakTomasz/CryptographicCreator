using Commons;
using CryptographicCreator.Models;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Windows.Interactivity;

namespace CryptographicCreator.Behaviors
{
    public class RibbonButtonOpenSaveAESKeyBehavior : Behavior<RibbonButton>
    {
        #region Fields

        private const string fileFilterExtension =
            "Key (*.ask)|*.ask|Encrypred data (*.aed)|*.aed";

        #endregion//Fields

        #region Dependency Properties

        public string SelectedPath
        {
            get { return (string)GetValue(SelectedPathProperty); }
            set { SetValue(SelectedPathProperty, value); }
        }

        public static readonly DependencyProperty SelectedPathProperty =
            DependencyProperty.Register(
                "SelectedPath", typeof(string),
                typeof(RibbonButtonOpenSaveAESKeyBehavior),
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
                typeof(RibbonButtonOpenSaveAESKeyBehavior));

        public bool IsActiveKey
        {
            get { return (bool)GetValue(IsActiveKeyProperty); }
            set { SetValue(IsActiveKeyProperty, value); }
        }

        public static readonly DependencyProperty IsActiveKeyProperty =
            DependencyProperty.Register(
                "IsActiveKey",
                typeof(bool),
                typeof(RibbonButtonOpenSaveAESKeyBehavior),
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
                typeof(RibbonButtonOpenSaveAESKeyBehavior),
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
                typeof(RibbonButtonOpenSaveAESKeyBehavior),
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
                typeof(RibbonButtonOpenSaveAESKeyBehavior),
                new PropertyMetadata(AESAction.None));

        #endregion//Dependency Properties

        #region Methods

        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject is RibbonButton button)
            {
                button.Click += Button_Click;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (FileAction)
            {
                case FileAction.Open:
                    var openFileDialog = new OpenFileDialog
                    {
                        Filter = fileFilterExtension
                    };
                    if (openFileDialog.ShowDialog().Value)
                    {
                        var selectedPath = openFileDialog.FileName;
                        var extension = Path.GetExtension(selectedPath);
                        switch (extension)
                        {
                            case ".ask":
                                OpenKey(selectedPath);
                                break;
                            case ".aed":
                                OpenEncryptedData(selectedPath);
                                break;
                        }
                    }
                    break;
                case FileAction.Save:
                    if (IsActiveKey ||
                        AreActiveEncryptedData)
                    {
                        var saveFileDialog = new SaveFileDialog
                        {
                            Filter = fileFilterExtension
                        };
                        if (saveFileDialog.ShowDialog().Value)
                        {
                            var selectedPath = saveFileDialog.FileName;
                            var extension = Path.GetExtension(selectedPath);
                            switch (extension)
                            {
                                case ".ask":
                                    if (IsActiveKey)
                                        AESAction = AESAction.SaveKey;
                                    break;
                                case ".aed":
                                    if (AreActiveEncryptedData)
                                        AESAction = AESAction.SaveEncryptedData;
                                    break;
                            }
                            SelectedPath = saveFileDialog.FileName;
                            AcceptEvent = true;
                        }
                    }
                    else AcceptEvent = false;
                    break;
            }
        }

        private void OpenKey(string selectedPath)
        {
            if (IsActiveKey)
            {
                if (MessageBox.Show(
                    "Key is opened now, override this key?",
                    "Attention",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    AcceptEvent = false;
                    return;
                }
                else OpenKeySequence(selectedPath);
            }
            else OpenKeySequence(selectedPath);
        }

        private void OpenKeySequence(string selectedPath)
        {
            SelectedPath = selectedPath;
            AESAction = AESAction.OpenKey;
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
            AESAction = AESAction.OpenEncryptedData;
            AcceptEvent = true;
        }

        #endregion//Methods
    }
}
