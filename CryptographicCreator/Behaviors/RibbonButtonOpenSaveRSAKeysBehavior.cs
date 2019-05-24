using Commons;
using CryptographicCreator.Models;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Windows.Interactivity;

namespace CryptographicCreator.Behaviors
{
    public class RibbonButtonOpenSaveRSAKeysBehavior : Behavior<RibbonButton>
    {
        public string SelectedFilePath
        {
            get { return (string)GetValue(SelectedFilePathProperty); }
            set { SetValue(SelectedFilePathProperty, value); }
        }

        public static readonly DependencyProperty SelectedFilePathProperty =
            DependencyProperty.Register(
                "SelectedFilePath", typeof(string),
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
                    if (openFileDialog.ShowDialog().Value)
                        SelectedFilePath = openFileDialog.FileName;
                    break;
                case FileAction.Save:
                    var saveFileDialog = new SaveFileDialog();
                    if (saveFileDialog.ShowDialog().Value)
                        SelectedFilePath =saveFileDialog.FileName;
                    break;
                default:
                    break;
            }
        }
    }
}
