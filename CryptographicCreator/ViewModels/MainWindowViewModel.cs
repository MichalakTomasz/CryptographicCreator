using Commons;
using CryptographicCreator.Models;
using EventAggregator;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace CryptographicCreator.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Fields

        private readonly IEventAggregator eventAggregator;
        private readonly IStatusBarMessages statusBarMessages;

        #endregion//Fiels

        #region Constructor

        public MainWindowViewModel(
            IEventAggregator eventAggregator, 
            IStatusBarMessages statusBarMessages)
        {
            this.eventAggregator = eventAggregator;
            this.statusBarMessages = statusBarMessages;
            this.eventAggregator.GetEvent<RSAMessageSentEvent>().Subscribe(ExecuteMessage);
        }

        #endregion//Constructor

        #region Properties

        private bool isActiveRSAPublicKey;
        public bool IsActiveRSAPublicKey
        {
            get { return isActiveRSAPublicKey; }
            set { SetProperty(ref isActiveRSAPublicKey, value); }
        }

        private bool isSavedRSAPublicKey;
        public bool IsSavedRSAPublicKey
        {
            get { return isSavedRSAPublicKey; }
            set { SetProperty(ref isSavedRSAPublicKey, value); }
        }

        private bool isActiveRSAPrivateKey;
        public bool IsActiveRSAPrivateKey
        {
            get { return isActiveRSAPrivateKey; }
            set { SetProperty(ref isActiveRSAPrivateKey, value); }
        }

        private bool isSavedRSAPrivateKey;
        public bool IsSavedRSAPrivateKey
        {
            get { return isSavedRSAPrivateKey; }
            set { SetProperty(ref isSavedRSAPrivateKey, value); }
        }

        private bool areActiveRSAEncrypryptedData;
        public bool AreActiveRSAEncryptedData
        {
            get { return areActiveRSAEncrypryptedData; }
            set { SetProperty(ref areActiveRSAEncrypryptedData, value); }
        }

        private bool areSavedEncryptedData;
        public bool AreSavedEncryptedData
        {
            get { return areSavedEncryptedData; }
            set { SetProperty(ref areSavedEncryptedData, value); }
        }

        private bool acceptEvent;
        public bool AcceptEvent
        {
            get { return acceptEvent; }
            set { SetProperty(ref acceptEvent, value); }
        }

        private RSAAction rsaAction;
        public RSAAction RSAAction
        {
            get { return rsaAction; }
            set { SetProperty(ref rsaAction, value); }
        }

        private string selectedPath;
        public string SelectedPath
        {
            get { return selectedPath; }
            set { SetProperty(ref selectedPath, value); }
        }

        private string statusBarLog;
        public string StatusBarLog
        {
            get { return statusBarLog; }
            set { SetProperty(ref statusBarLog, value); }
        }

        #endregion//Properties

        #region Commands

        private ICommand generateCommand;
        public ICommand GenerateCommand
        {
            get
            {
                if (generateCommand == null)
                    generateCommand = new DelegateCommand(GenerateCommandExecute);
                return generateCommand;
            }
        }

        private ICommand openCommand;
        public ICommand OpenCommand
        {
            get
            {
                if (openCommand == null)
                    openCommand = new DelegateCommand(OpenCommandExecute);
                return openCommand;
            }
        }

        private ICommand saveCommnad;
        public ICommand SaveCommand
        {
            get
            {
                if (saveCommnad == null)
                    saveCommnad = new DelegateCommand(SaveCommandExecute);
                return saveCommnad;
            }
        }

        #endregion//Commands

        #region Methods

        private void GenerateCommandExecute()
        {
            if (AcceptEvent)
            {
                eventAggregator.GetEvent<RSAMessageSentEvent>()
                    .Publish(new RsaMessage { RSAAction = RSAAction.Generate });
                SetRSADataAcrivity(RSAAction.Generate);
                IsSavedRSAPrivateKey = false;
                IsSavedRSAPublicKey = false;
                StatusBarLog = statusBarMessages[StatusBarMessage.RSAKeysGenerated];
            }
            else StatusBarLog = statusBarMessages[StatusBarMessage.Canceled];
        }

        private void OpenCommandExecute()
        {
            if (AcceptEvent)
            {
                eventAggregator.GetEvent<RSAMessageSentEvent>()
                    .Publish(new RsaMessage { RSAAction = RSAAction, Path = SelectedPath });
                SetRSADataAcrivity(RSAAction);
                switch (RSAAction)
                {
                    case RSAAction.OpenPublicKey:
                        StatusBarLog = statusBarMessages[StatusBarMessage.RSAPublicKeyOpened];
                        break;
                    case RSAAction.OpenPrivateKey:
                        StatusBarLog = statusBarMessages[StatusBarMessage.RSAPrivateKeyOpened];
                        break;
                    case RSAAction.OpenEncryptedData:
                        break;
                }
            }
        }

        private void SaveCommandExecute()
        {
            if (AcceptEvent)
            {
                eventAggregator.GetEvent<RSAMessageSentEvent>()
                  .Publish(new RsaMessage { RSAAction = RSAAction, Path = SelectedPath });
                switch (RSAAction)
                {
                    case RSAAction.SavePublicKey:
                        IsSavedRSAPublicKey = true;
                        StatusBarLog = statusBarMessages[StatusBarMessage.RSAPublicKeySaved];
                        break;
                    case RSAAction.SavePrivateAndPublicKey:
                        IsSavedRSAPrivateKey = true;
                        StatusBarLog = statusBarMessages[StatusBarMessage.RSAPrivateKeySaved];
                        break;
                    case RSAAction.SaveEncryptedData:
                        AreSavedEncryptedData = true;
                        StatusBarLog = statusBarMessages[StatusBarMessage.RSAEncryptedDataSaved];
                        break;
                }
            }
        }

        private void SetRSADataAcrivity(RSAAction rsaAction)
        {
            switch (rsaAction)
            {
                case RSAAction.Generate:
                    IsActiveRSAPublicKey = true;
                    IsActiveRSAPrivateKey = true;
                    break;
                case RSAAction.OpenPublicKey:
                    IsActiveRSAPublicKey = true;
                    break;
                case RSAAction.OpenPrivateKey:
                    IsActiveRSAPrivateKey = true;
                    break;
                case RSAAction.OpenEncryptedData:
                    AreActiveRSAEncryptedData = true;
                    break;
            }
        }

        private void ExecuteMessage(RsaMessage message)
        {
            switch (message.RSAAction)
            {
                case RSAAction.Encrypt:
                    AreActiveRSAEncryptedData = true;
                    statusBarLog = statusBarMessages[StatusBarMessage.RSADataEncrypted];
                    break;
                case RSAAction.Decrypt:
                    StatusBarLog = statusBarMessages[StatusBarMessage.RSADataDecrypted];
                    break;
            }
        }

        #endregion//Methods
    }
}
