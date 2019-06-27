using Commons;
using CryptographicCreator.Models;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Windows.Interactivity;

namespace CryptographicCreator.Behaviors
{
    class RibbonButtonOpenSaveSHA256HashsumBehavior : Behavior<RibbonButton>
    {
        #region Fields

        private const string fileFilterExtension =
            "SHA256 hash (*.sha256)|*.sha256";

        #endregion//Fields

        #region Dependency Properties

        public string SelectedPath
        {
            get { return (string)GetValue(SelectedPathProperty); }
            set { SetValue(SelectedPathProperty, value); }
        }

        public static readonly DependencyProperty SelectedPathProperty =
            DependencyProperty.Register(
                "SelectedPath",
                typeof(string),
                typeof(RibbonButtonOpenSaveSHA256HashsumBehavior),
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
                typeof(RibbonButtonOpenSaveSHA256HashsumBehavior));

        public ChecksumAction ChecksumAction
        {
            get { return (ChecksumAction)GetValue(ChecksumActionProperty); }
            set { SetValue(ChecksumActionProperty, value); }
        }

        public static readonly DependencyProperty ChecksumActionProperty =
            DependencyProperty.Register(
                "ChecksumAction",
                typeof(ChecksumAction),
                typeof(RibbonButtonOpenSaveSHA256HashsumBehavior),
                new PropertyMetadata(ChecksumAction.None));

        public bool IsActiveChecksum
        {
            get { return (bool)GetValue(IsActiveChecksumProperty); }
            set { SetValue(IsActiveChecksumProperty, value); }
        }

        public static readonly DependencyProperty IsActiveChecksumProperty =
            DependencyProperty.Register(
                "IsActiveChecksum",
                typeof(bool),
                typeof(RibbonButtonOpenSaveSHA256HashsumBehavior),
                new PropertyMetadata(null));

        public bool IsSavedChecksum
        {
            get { return (bool)GetValue(IsSavedChecksumProperty); }
            set { SetValue(IsSavedChecksumProperty, value); }
        }

        public static readonly DependencyProperty IsSavedChecksumProperty =
            DependencyProperty.Register(
                "IsSavedChecksum",
                typeof(bool),
                typeof(RibbonButtonOpenSaveSHA256HashsumBehavior),
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
                typeof(RibbonButtonOpenSaveSHA256HashsumBehavior),
                new PropertyMetadata(false));

        #endregion//Dependency Properties

        #region Methods

        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject is RibbonButton button)
                button.Click += Button_Click;
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
                        SelectedPath = openFileDialog.FileName;
                        ChecksumAction = ChecksumAction.Open;
                        AcceptEvent = true;
                    }
                    break;
                case FileAction.Save:
                    if (IsActiveChecksum)
                    {
                        var saveFileDialog = new SaveFileDialog
                        {
                            Filter = fileFilterExtension
                        };
                        if (saveFileDialog.ShowDialog().Value)
                        {
                            var selectedPath = saveFileDialog.FileName;
                            SelectedPath = selectedPath;
                            AcceptEvent = true;
                            ChecksumAction = ChecksumAction.Save;
                        }
                    }
                    else AcceptEvent = false;
                    break;
            }
        }

        #endregion//Methods
    }
}
