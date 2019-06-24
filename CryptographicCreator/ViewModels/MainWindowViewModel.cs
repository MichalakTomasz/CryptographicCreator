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
            this.eventAggregator.GetEvent<RSAMessageSentEvent>().Subscribe(ExecuteRSAMessage);
            this.eventAggregator.GetEvent<AESMessageSentEvent>().Subscribe(ExecuteAESMessage);
        }

        #endregion//Constructor

        #region Properties

        #region RSA

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

        private bool areSavedRSAEncryptedData;
        public bool AreSavedRSAEncryptedData
        {
            get { return areSavedRSAEncryptedData; }
            set { SetProperty(ref areSavedRSAEncryptedData, value); }
        }

        private bool acceptRSAEvent;
        public bool AcceptRSAEvent
        {
            get { return acceptRSAEvent; }
            set { SetProperty(ref acceptRSAEvent, value); }
        }

        private RSAAction rsaAction;
        public RSAAction RSAAction
        {
            get { return rsaAction; }
            set { SetProperty(ref rsaAction, value); }
        }

        private string selectedRSAPath;
        public string SelectedRSAPath
        {
            get { return selectedRSAPath; }
            set { SetProperty(ref selectedRSAPath, value); }
        }

        #endregion//RSA

        #region AES

        private AESAction aesAction;
        public AESAction AESAction
        {
            get { return aesAction; }
            set { SetProperty(ref aesAction, value); }
        }

        private string selectedAESPath;
        public string SelectedAESPath
        {
            get { return selectedAESPath; }
            set { SetProperty(ref selectedAESPath, value); }
        }

        private bool acceptAESEvent;
        public bool AcceptAESEvent
        {
            get { return acceptAESEvent; }
            set { SetProperty(ref acceptAESEvent, value); }
        }

        private bool isActiveAESKey;
        public bool IsActiveAESKey
        {
            get { return isActiveAESKey; }
            set { SetProperty(ref isActiveAESKey, value); }
        }

        private bool isSavedAESKey;
        public bool IsSavedAESKey
        {
            get { return isSavedAESKey; }
            set { SetProperty(ref isSavedAESKey, value); }
        }

        private bool areActiveAESEncrypryptedData;
        public bool AreActiveAESEncryptedData
        {
            get { return areActiveAESEncrypryptedData; }
            set { SetProperty(ref areActiveAESEncrypryptedData, value); }
        }

        private bool areSavedAESEncryptedData;
        public bool AreSavedAESEncryptedData
        {
            get { return areSavedAESEncryptedData; }
            set { SetProperty(ref areSavedAESEncryptedData, value); }
        }

        private string statusBarLog;
        public string StatusBarLog
        {
            get { return statusBarLog; }
            set { SetProperty(ref statusBarLog, value); }
        }

        #endregion//AES

        #endregion//Properties

        #region Commands

        #region RSA     

        private ICommand generateRSACommand;
        public ICommand GenerateRSACommand
        {
            get
            {
                if (generateRSACommand == null)
                    generateRSACommand = new DelegateCommand(GenerateRSACommandExecute);
                return generateRSACommand;
            }
        }

        private ICommand openRSACommand;
        public ICommand OpenRSACommand
        {
            get
            {
                if (openRSACommand == null)
                    openRSACommand = new DelegateCommand(OpenRSACommandExecute);
                return openRSACommand;
            }
        }

        private ICommand saveRSACommnad;
        public ICommand SaveRSACommand
        {
            get
            {
                if (saveRSACommnad == null)
                    saveRSACommnad = new DelegateCommand(SaveRSACommandExecute);
                return saveRSACommnad;
            }
        }

        #endregion//RSA

        #region AES

        private ICommand generateAESCommand;
        public ICommand GenerateAESCommand
        {
            get
            {
                if (generateAESCommand == null)
                    generateAESCommand = new DelegateCommand(GenerateAESCommandExecute);
                return generateAESCommand;
            }
        }

        private ICommand openAESCommand;
        public ICommand OpenAESCommand
        {
            get
            {
                if (openAESCommand == null)
                    openAESCommand = new DelegateCommand(OpenAESCommandExecute);
                return openAESCommand;
            }
        }

        private ICommand saveAESCommnad;
        public ICommand SaveAESCommand
        {
            get
            {
                if (saveAESCommnad == null)
                    saveAESCommnad = new DelegateCommand(SaveAESCommandExecute);
                return saveAESCommnad;
            }
        }

        #endregion//AES

        #region MD5

        private ICommand openMD5Command;
        public ICommand OpenMD5Command
        {
            get
            {
                if (openMD5Command == null)
                    openMD5Command = new DelegateCommand(OpenMD5CommandExecute);
                return openMD5Command;
            }
        }

        private ICommand saveMD5Command;
        public ICommand SaveMD5Command
        {
            get
            {
                if (saveMD5Command == null)
                    saveMD5Command = new DelegateCommand(SaveMD5CommandExecute);
                return saveMD5Command;
            }
        }

        #endregion
        private ICommand exitCommnad;
        public ICommand ExitCommand
        {
            get
            {
                if (exitCommnad == null)
                    exitCommnad = new DelegateCommand(ExitCommandExecute);
                return exitCommnad;
            }
        }

        #endregion//Commands

        #region Methods

        #region RSA

        private void GenerateRSACommandExecute()
        {
            if (AcceptRSAEvent)
            {
                eventAggregator.GetEvent<RSAMessageSentEvent>()
                    .Publish(new RSAMessage { RSAAction = RSAAction.Generate });
                SetRSADataActivity(RSAAction);
                SetRSAStatus(RSAAction);
            }
            else StatusBarLog = statusBarMessages[StatusBarMessage.Canceled];
        }

        private void OpenRSACommandExecute()
        {
            if (AcceptRSAEvent)
            {
                eventAggregator.GetEvent<RSAMessageSentEvent>()
                    .Publish(new RSAMessage { RSAAction = RSAAction, Path = SelectedRSAPath });
                SetRSADataActivity(RSAAction);
                SetRSAStatus(RSAAction);
            }
        }

        private void SaveRSACommandExecute()
        {
            if (AcceptRSAEvent)
            {
                eventAggregator.GetEvent<RSAMessageSentEvent>()
                  .Publish(new RSAMessage { RSAAction = RSAAction, Path = SelectedRSAPath });
                SetRSADataActivity(RSAAction);
                SetRSAStatus(RSAAction);
            }
        }

        private void SetRSADataActivity(RSAAction rsaAction)
        {
            switch (rsaAction)
            {
                case RSAAction.Generate:
                    IsActiveRSAPublicKey = true;
                    IsActiveRSAPrivateKey = true;
                    IsSavedRSAPrivateKey = false;
                    IsSavedRSAPublicKey = false;
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
                case RSAAction.SavePublicKey:
                    IsSavedRSAPublicKey = true;
                    break;
                case RSAAction.SavePrivateAndPublicKey:
                    IsSavedRSAPrivateKey = true;
                    break;
                case RSAAction.SaveEncryptedData:
                    AreSavedRSAEncryptedData = true;
                    break;
            }
        }

        private void SetRSAStatus(RSAAction rsaAction)
        {
            switch (RSAAction)
            {
                case RSAAction.Generate:
                    StatusBarLog = statusBarMessages[StatusBarMessage.RSAKeysGenerated];
                    break;
                case RSAAction.OpenPublicKey:
                    StatusBarLog = statusBarMessages[StatusBarMessage.RSAPublicKeyOpened];
                    break;
                case RSAAction.OpenPrivateKey:
                    StatusBarLog = statusBarMessages[StatusBarMessage.RSAPrivateKeyOpened];
                    break;
                case RSAAction.OpenEncryptedData:
                    StatusBarLog = statusBarMessages[StatusBarMessage.RSAEncryptedDataOpened];
                    break;
                case RSAAction.SavePublicKey:
                    StatusBarLog = statusBarMessages[StatusBarMessage.RSAPublicKeySaved];
                    break;
                case RSAAction.SavePrivateAndPublicKey:
                    StatusBarLog = statusBarMessages[StatusBarMessage.RSAPrivateKeySaved];
                    break;
                case RSAAction.SaveEncryptedData:
                    StatusBarLog = statusBarMessages[StatusBarMessage.RSAEncryptedDataSaved];
                    break;
                case RSAAction.Encrypt:
                    StatusBarLog = statusBarMessages[StatusBarMessage.RSADataEncrypted];
                    break;
                case RSAAction.Decrypt:
                    StatusBarLog = statusBarMessages[StatusBarMessage.RSADataDecrypted];
                    break;
            }
        }

        private void ExecuteRSAMessage(RSAMessage message)
        {
            if (message.RSAAction == RSAAction.Encrypt)
                AreActiveRSAEncryptedData = true;
            SetRSAStatus(message.RSAAction);
        }

        #endregion//RSA

        #region AES

        private void GenerateAESCommandExecute()
        {
            if (AcceptAESEvent)
            {
                eventAggregator.GetEvent<AESMessageSentEvent>()
                    .Publish(new AESMessage { AESAction = AESAction.Generate });
                SetAESDataActivity(AESAction);
                SetAESStatus(AESAction);
            }
        }

        private void OpenAESCommandExecute()
        {
            if (AcceptAESEvent)
            {
                eventAggregator.GetEvent<AESMessageSentEvent>().
                    Publish(new AESMessage { AESAction = AESAction, Path = SelectedAESPath });
                SetAESDataActivity(AESAction);
                SetAESStatus(AESAction);
            }
        }

        private void SaveAESCommandExecute()
        {
            if (AcceptAESEvent)
            {
                eventAggregator.GetEvent<AESMessageSentEvent>()
                .Publish(new AESMessage { AESAction = AESAction, Path = SelectedAESPath });
                SetAESDataActivity(AESAction);
                SetAESStatus(AESAction);
            }
        }

        private void ExecuteAESMessage(AESMessage message)
        {
            if (message.AESAction == AESAction.Encrypt)
                AreActiveAESEncryptedData = true;
            SetAESStatus(AESAction);
        }

        private void SetAESDataActivity(AESAction aesAction)
        {
            switch (aesAction)
            {
                case AESAction.Generate:
                    IsActiveAESKey = true;
                    isSavedAESKey = false;
                    break;
                case AESAction.OpenKey:
                    IsActiveAESKey = true;
                    break;
                case AESAction.OpenEncryptedData:
                    areActiveAESEncrypryptedData = true;
                    break;
                case AESAction.SaveKey:
                    isSavedAESKey = true;
                    break;
                case AESAction.SaveEncryptedData:
                    AreActiveAESEncryptedData = true;
                    break;
            }
        }

        private void SetAESStatus(AESAction aesAction)
        {
            switch (aesAction)
            {
                case AESAction.Generate:
                    StatusBarLog = statusBarMessages[StatusBarMessage.AESKeyGenerated];
                    break;
                case AESAction.OpenKey:
                    StatusBarLog = statusBarMessages[StatusBarMessage.AESKeyOpened];
                    break;
                case AESAction.OpenEncryptedData:
                    StatusBarLog = statusBarMessages[StatusBarMessage.AESEncryptedDataOpened];
                    break;
                case AESAction.SaveKey:
                    StatusBarLog = statusBarMessages[StatusBarMessage.AESKeySaved];
                    break;
                case AESAction.SaveEncryptedData:
                    StatusBarLog = statusBarMessages[StatusBarMessage.AESEncryptedDataSaved];
                    break;
                case AESAction.Encrypt:
                    StatusBarLog = statusBarMessages[StatusBarMessage.AESDataEncrypted];
                    break;
                case AESAction.Decrypt:
                    StatusBarLog = statusBarMessages[StatusBarMessage.AESDataDecrypted];
                    break;
            }
        }

        #endregion//AES

        #region MD5

        private void OpenMD5CommandExecute()
        {
            throw new NotImplementedException();
        }

        private void SaveMD5CommandExecute()
        {
            throw new NotImplementedException();
        }

        #endregion

        private void ExitCommandExecute()
            => App.Current.MainWindow.Close();

        #endregion//Methods
    }
}
