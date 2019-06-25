using Commons;
using CryptographicCreator.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Windows.Interactivity;

namespace CryptographicCreator.Behaviors
{
    public class RibbonButtonOpenSaveMD5HashsumBehavior : Behavior<RibbonButton>
    {
        #region Fields

        private const string fileFilterExtension =
            "MD5 hash (*.md5)|*.md5";

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
                typeof(RibbonButtonOpenSaveMD5HashsumBehavior), 
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
                typeof(RibbonButtonOpenSaveMD5HashsumBehavior));

        public ChecksumAction ChecksumAction
        {
            get { return (ChecksumAction)GetValue(ChecksumActionProperty); }
            set { SetValue(ChecksumActionProperty, value); }
        }

        public static readonly DependencyProperty ChecksumActionProperty =
            DependencyProperty.Register(
                "ChecksumAction", 
                typeof(ChecksumAction), 
                typeof(RibbonButtonOpenSaveMD5HashsumBehavior), 
                new PropertyMetadata(ChecksumAction.None));

        public bool IsActiveHash
        {
            get { return (bool)GetValue(IsActiveHashProperty); }
            set { SetValue(IsActiveHashProperty, value); }
        }

        public static readonly DependencyProperty IsActiveHashProperty =
            DependencyProperty.Register(
                "IsActiveHash", 
                typeof(bool), 
                typeof(RibbonButtonOpenSaveMD5HashsumBehavior), 
                new PropertyMetadata(null));

        public bool AcceptEvent
        {
            get { return (bool)GetValue(AcceptEventProperty); }
            set { SetValue(AcceptEventProperty, value); }
        }

        public static readonly DependencyProperty AcceptEventProperty =
            DependencyProperty.Register(
                "AcceptEvent", 
                typeof(bool), 
                typeof(RibbonButtonOpenSaveMD5HashsumBehavior), 
                new PropertyMetadata(false));
        
        #endregion//Dependency Properties

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
            switch (FileAction)
            {
                case FileAction.Open:
                    var openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = fileFilterExtension;
                    if (openFileDialog.ShowDialog().Value)
                    {
                        var selectedPath = openFileDialog.FileName;
                        if (IsActiveHash)
                        {
                            if (MessageBox.Show(
                                "MD5 checksum is opened, overide this checksum?",
                                "Attention",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question) == MessageBoxResult.No)
                            {
                                AcceptEvent = false;
                            }
                            else OpenMD5HashsumSequence(selectedPath);
                        }
                        else OpenMD5HashsumSequence(selectedPath);
                    }
                    break;
                case FileAction.Save:
                    break;
            }
        }

        private void OpenMD5HashsumSequence(string selectedPath)
        {
            SelectedPath = selectedPath;
            ChecksumAction = ChecksumAction.Open;
            AcceptEvent = true;
        }

        #endregion//Methods
    }
}
