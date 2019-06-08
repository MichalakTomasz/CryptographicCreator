using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AESRegion.ViewModels
{
    public class ViewAESViewModel : BindableBase
    {
        #region Properties

        private string text;
        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }

        private bool isActiveKey;
        public bool IsActiveKey
        {
            get { return isActiveKey; }
            set { SetProperty(ref isActiveKey, value); }
        }

        private bool areActiveEncryptedData;
        public bool AreActiveEncryptedData
        {
            get { return areActiveEncryptedData; }
            set { SetProperty(ref areActiveEncryptedData, value); }
        }

        private string decryptedText;
        public string DecryptedText
        {
            get { return decryptedText; }
            set { SetProperty(ref decryptedText, value); }
        }

        #endregion//Properties

        #region Commands

        private ICommand encryptCommand;
        public ICommand EncryptCommand
        {
            get
            {
                if (encryptCommand == null)
                    encryptCommand = new DelegateCommand(EncryptCommandExecute, EncryptCommandCanExecute)
                        .ObservesProperty(() => IsActiveKey)
                        .ObservesProperty(() => AreActiveEncryptedData)
                        .ObservesProperty(() => Text);
                return encryptCommand;
            }
        }

        private ICommand decryptCommand;
        public ICommand DecryptCommand
        {
            get
            {
                if (decryptCommand == null)
                    decryptCommand = new DelegateCommand(DecryptCommandExecute, DecryptCommandCanExecute)
                        .ObservesProperty(() => IsActiveKey)
                        .ObservesProperty(() => AreActiveEncryptedData)
                        .ObservesProperty(() => DecryptedText);
                return decryptCommand;
            }
        }

        private ICommand clearTextCommnad;
        public ICommand ClearTextCommand
        {
            get
            {
                if (clearTextCommnad == null)
                    clearTextCommnad = new DelegateCommand(ClearTextCommandExecute, ClearTextCanCommandExecute)
                        .ObservesProperty(() => Text);
                return clearTextCommnad;
            }
        }

        #endregion//Commands

        #region Methods

        private bool EncryptCommandCanExecute()
            => IsActiveKey && Text?.Length > 0;

        private void EncryptCommandExecute()
        {
            throw new NotImplementedException();
        }

        private bool DecryptCommandCanExecute()
            => AreActiveEncryptedData;

        private void DecryptCommandExecute()
        {
            throw new NotImplementedException();
        }

        private bool ClearTextCanCommandExecute()
            => Text?.Length > 0;

        private void ClearTextCommandExecute()
            => Text = "";

        #endregion//Methods
    }
}
