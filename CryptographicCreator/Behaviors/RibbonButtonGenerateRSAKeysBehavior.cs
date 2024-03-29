﻿using Commons;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Windows.Interactivity;

namespace CryptographicCreator.Behaviors
{
    public class RibbonButtonGenerateRSAKeysBehavior : Behavior<RibbonButton>
    {
        #region Dependency properties

        public bool IsActivePrivateKey
        {
            get { return (bool)GetValue(IsActivePrivateKeyProperty); }
            set { SetValue(IsActivePrivateKeyProperty, value); }
        }

        public static readonly DependencyProperty IsActivePrivateKeyProperty =
            DependencyProperty.Register(
                "IsActivePrivateKey",
                typeof(bool),
                typeof(RibbonButtonGenerateRSAKeysBehavior),
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
                typeof(RibbonButtonGenerateRSAKeysBehavior),
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
                typeof(RibbonButtonGenerateRSAKeysBehavior),
                new PropertyMetadata(false));

        public bool IsSavedPublicKey
        {
            get { return (bool)GetValue(IsSavedPublicKeyProperty); }
            set { SetValue(IsSavedPublicKeyProperty, value); }
        }

        public static readonly DependencyProperty IsSavedPublicKeyProperty =
            DependencyProperty.Register(
                "IsSavedPublicKey",
                typeof(bool),
                typeof(RibbonButtonGenerateRSAKeysBehavior),
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
                typeof(RibbonButtonGenerateRSAKeysBehavior),
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
                typeof(RibbonButtonGenerateRSAKeysBehavior),
                new PropertyMetadata(RSAAction.None));

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
            if ((IsActivePrivateKey && !IsSavedPrivateKey) ||
                (IsActivePublicKey && !IsSavedPublicKey))
            {
                if (MessageBox.Show(
                    "One or both keys are not saved and will be lost. Do you still generate keys?",
                    "Attention",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning)
                    == MessageBoxResult.Yes)
                {
                    AcceptEvent = true;
                    RSAAction = RSAAction.Generate;
                }


                else AcceptEvent = false;
            }
            else
            {
                AcceptEvent = true;
                RSAAction = RSAAction.Generate;
            }
        }

        #endregion//Methods
    }
}
