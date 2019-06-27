using Commons;
using CryptographicCreator.Models;
using EventAggregator;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CryptographicCreator.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Fields

        private readonly IEventAggregator eventAggregator;
        private readonly IStatusBarMessages statusBarMessages;

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

        private const string sha512FileFilterExtension =
            "SHA512 hash (*.sha512)|*.sha512";

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
            this.eventAggregator.GetEvent<MD5MessageSentEvent>().Subscribe(ExecuteMD5Message);
            this.eventAggregator.GetEvent<SHA512MessageSentEvent>().Subscribe(ExecuteSHA512Message);
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

        #endregion//AES

        #region MD5

        private string selectedMD5Path;
        public string SelectedMD5Path
        {
            get { return selectedMD5Path; }
            set { SetProperty(ref selectedMD5Path, value); }
        }

        private bool isActiveMD5checksum;
        public bool IsActiveMD5Checksum
        {
            get { return isActiveMD5checksum; }
            set { SetProperty(ref isActiveMD5checksum, value); }
        }

        private bool isActiveMD5ChecksumToCompare;
        public bool IsActiveMD5ChecksumToCompare
        {
            get { return isActiveMD5ChecksumToCompare; }
            set { SetProperty(ref isActiveMD5ChecksumToCompare, value); }
        }

        private bool acceptMD5Event;
        public bool AcceptMD5Event
        {
            get { return acceptMD5Event; }
            set { SetProperty(ref acceptMD5Event, value); }
        }

        private bool isSavedMD5Checksum;
        public bool IsSavedMD5Checksum
        {
            get { return isSavedMD5Checksum; }
            set { SetProperty(ref isSavedMD5Checksum, value); }
        }

        #endregion//MD5

        #region SHA512


        private string selectedSHA512Path;
        public string SelectedSHA512Path
        {
            get { return selectedSHA512Path; }
            set { SetProperty(ref selectedSHA512Path, value); }
        }

        private bool isActiveSHA512checksum;
        public bool IsActiveSHA512Checksum
        {
            get { return isActiveSHA512checksum; }
            set { SetProperty(ref isActiveSHA512checksum, value); }
        }

        private bool isActiveSHA512ChecksumToCompare;
        public bool IsActiveSHA512ChecksumToCompare
        {
            get { return isActiveSHA512ChecksumToCompare; }
            set { SetProperty(ref isActiveSHA512ChecksumToCompare, value); }
        }

        private bool acceptSHA512Event;
        public bool AcceptSHA512Event
        {
            get { return acceptSHA512Event; }
            set { SetProperty(ref acceptSHA512Event, value); }
        }

        private bool isSavedSHA512Checksum;
        public bool IsSavedSHA512Checksum
        {
            get { return isSavedSHA512Checksum; }
            set { SetProperty(ref isSavedSHA512Checksum, value); }
        }

        #endregion//SHA512

        private string statusBarLog;
        public string StatusBarLog
        {
            get { return statusBarLog; }
            set { SetProperty(ref statusBarLog, value); }
        }

        private ChecksumAction checksumAction;
        public ChecksumAction ChecksumAction
        {
            get { return checksumAction; }
            set { SetProperty(ref checksumAction, value); }
        }

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

        private ICommand openMD5ChecksumCommand;
        public ICommand OpenMD5ChecksumCommand
        {
            get
            {
                if (openMD5ChecksumCommand == null)
                    openMD5ChecksumCommand = new DelegateCommand(OpenMD5CommandExecute);
                return openMD5ChecksumCommand;
            }
        }

        private ICommand saveMD5ChecksumCommand;
        public ICommand SaveMD5ChecksumCommand
        {
            get
            {
                if (saveMD5ChecksumCommand == null)
                    saveMD5ChecksumCommand = new DelegateCommand(SaveMD5CommandExecute);
                return saveMD5ChecksumCommand;
            }
        }

        #endregion//MD5

        #region SHA512

        private ICommand openSHA512ChecksumCommand;
        public ICommand OpenSHA512ChecksumCommand
        {
            get
            {
                if (openSHA512ChecksumCommand == null)
                    openSHA512ChecksumCommand = new DelegateCommand(OpenSHA512CommandExecute);
                return openSHA512ChecksumCommand;
            }
        }

        private ICommand saveSHA512ChecksumCommand;
        public ICommand SaveSHA512ChecksumCommand
        {
            get
            {
                if (saveSHA512ChecksumCommand == null)
                    saveSHA512ChecksumCommand = new DelegateCommand(SaveSHA512CommandExecute);
                return saveSHA512ChecksumCommand;
            }
        }

        #endregion//SHA512

        #region Commons

        private ICommand closingCommnad;
        public ICommand ClosingCommand
        {
            get
            {
                if (closingCommnad == null)
                    closingCommnad = new DelegateCommand<CancelEventArgs>(ClosingCommandExecute);
                return closingCommnad;
            }
        }

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

        #endregion//Commons

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
            if (AcceptMD5Event)
            {
                eventAggregator.GetEvent<MD5MessageSentEvent>()
                    .Publish(new MD5Message { ChecksumAction = ChecksumAction.Open, Path = SelectedMD5Path });
                IsActiveMD5ChecksumToCompare = true;
                StatusBarLog = statusBarMessages[StatusBarMessage.MD5ChecksumOpened];
            }
        }

        private void SaveMD5CommandExecute()
        {
            if (AcceptMD5Event)
            {
                eventAggregator.GetEvent<MD5MessageSentEvent>()
                .Publish(new MD5Message { ChecksumAction = ChecksumAction, Path = SelectedMD5Path });
                IsSavedMD5Checksum = true;
                StatusBarLog = statusBarMessages[StatusBarMessage.MD5ChecksumhSaved];
            }
        }

        private void ExecuteMD5Message(MD5Message message)
        {
            if (message.ChecksumAction == ChecksumAction.Generate)
            {
                IsActiveMD5Checksum = true;
                StatusBarLog = statusBarMessages[StatusBarMessage.MD5ChecksumGenerated];
            }
        }

        #endregion

        #region SHA512

        private void OpenSHA512CommandExecute()
        {
            if (AcceptSHA512Event)
            {
                eventAggregator.GetEvent<SHA512MessageSentEvent>()
                    .Publish(new SHA512Message { ChecksumAction = ChecksumAction.Open, Path = SelectedSHA512Path });
                IsActiveSHA512ChecksumToCompare = true;
                StatusBarLog = statusBarMessages[StatusBarMessage.SHA512ChecksumOpened];
            }
        }

        private void SaveSHA512CommandExecute()
        {
            if (AcceptSHA512Event)
            {
                eventAggregator.GetEvent<SHA512MessageSentEvent>()
                .Publish(new SHA512Message { ChecksumAction = ChecksumAction, Path = SelectedSHA512Path });
                IsSavedSHA512Checksum = true;
                StatusBarLog = statusBarMessages[StatusBarMessage.SHA512ChecksumhSaved];
            }
        }        

        private void ExecuteSHA512Message(SHA512Message message)
        {
            if (message.ChecksumAction == ChecksumAction.Generate)
            {
                IsActiveSHA512Checksum = true;
                StatusBarLog = statusBarMessages[StatusBarMessage.SHA512ChecksumGenerated];
            }
        }

        #endregion//SHA512

        #region Commons

        private void ClosingCommandExecute(CancelEventArgs eventArgs)
        {
            if (IsActiveRSAPrivateKey && !IsSavedRSAPrivateKey ||
                 IsActiveRSAPublicKey && !IsSavedRSAPublicKey ||
                 AreActiveRSAEncryptedData && !AreSavedRSAEncryptedData ||
                 IsActiveAESKey && !IsSavedAESKey ||
                 AreActiveAESEncryptedData && !AreSavedAESEncryptedData ||
                 IsActiveMD5Checksum && !IsSavedMD5Checksum ||
                 IsActiveSHA512Checksum && !IsSavedSHA512Checksum)
            {
                var messageBoxResult = MessageBox.Show(
                    "Some data aren't saved. Do you want to save any date before exit? Yes" +
                    " - Save data and close application, No - Close without saving, Cancel - cancel exit.",
                    "Attention",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question);
                switch (messageBoxResult)
                {
                    case MessageBoxResult.Yes:
                        var saveFile = new SaveFileDialog();
                        if (IsActiveRSAPrivateKey && !IsSavedRSAPrivateKey)
                            SaveSequenceRSAPrivateKey();

                        if (IsActiveRSAPublicKey && !IsSavedRSAPublicKey)
                            SaveSequenceRSAPublicKey();

                        if (AreActiveRSAEncryptedData && !AreSavedRSAEncryptedData)
                            SaveSequenceRSAEncryptedData();

                        if (IsActiveAESKey && !IsSavedAESKey)
                            SaveSequenceAESKey();

                        if (AreActiveAESEncryptedData && !AreSavedAESEncryptedData)
                            SaveSequenceAESEncryptedData();

                        if (IsActiveMD5Checksum && !IsSavedMD5Checksum)
                            SaveSequenceMD5Checksum();

                        if (IsActiveSHA512Checksum && !IsSavedSHA512Checksum)
                            SaveSequenceSHA512Checksum();
                        break;
                    case MessageBoxResult.Cancel:
                        eventArgs.Cancel = true;
                        break;
                }
            }
        }

        private void ExitCommandExecute()
            => App.Current.MainWindow.Close();

        private void SaveSequenceRSAPrivateKey()
        {
            var saveFile = new SaveFileDialog();
            saveFile.Filter = privateRSAKeyFilterExtension;
            if (saveFile.ShowDialog().Value)
            {
                eventAggregator.GetEvent<RSAMessageSentEvent>().Publish(new RSAMessage
                {
                    RSAAction = RSAAction.SavePrivateAndPublicKey,
                    Path = saveFile.FileName
                });
            }
        }

        private void SaveSequenceRSAPublicKey()
        {
            var saveFile = new SaveFileDialog();
            saveFile.Filter = publicRSAKeyFilterExtension;
            if (saveFile.ShowDialog().Value)
            {
                eventAggregator.GetEvent<RSAMessageSentEvent>().Publish(new RSAMessage
                {
                    RSAAction = RSAAction.SavePublicKey,
                    Path = saveFile.FileName
                });
            }
        }

        private void SaveSequenceRSAEncryptedData()
        {
            var saveFile = new SaveFileDialog();
            saveFile.Filter = rsaEncryptedDataFilterExtension;
            if (saveFile.ShowDialog().Value)
            {
                eventAggregator.GetEvent<RSAMessageSentEvent>().Publish(new RSAMessage
                {
                    RSAAction = RSAAction.SaveEncryptedData,
                    Path = saveFile.FileName
                });
            }
        }

        private void SaveSequenceAESKey()
        {
            var saveFile = new SaveFileDialog();
            saveFile.Filter = aesKeyFileExtension;
            if (saveFile.ShowDialog().Value)
            {
                eventAggregator.GetEvent<AESMessageSentEvent>().Publish(new AESMessage
                {
                    AESAction = AESAction.SaveKey,
                    Path = saveFile.FileName
                });
            }
        }

        private void SaveSequenceAESEncryptedData()
        {
            var saveFile = new SaveFileDialog();
            saveFile.Filter = aesEncryptedDataFilterExtension;
            if (saveFile.ShowDialog().Value)
            {
                eventAggregator.GetEvent<AESMessageSentEvent>().Publish(new AESMessage
                {
                    AESAction = AESAction.SaveEncryptedData,
                    Path = saveFile.FileName
                });
            }
        }

        private void SaveSequenceMD5Checksum()
        {
            var saveFile = new SaveFileDialog();
            saveFile.Filter = md5FileFilterExtension;
            if (saveFile.ShowDialog().Value)
            {
                eventAggregator.GetEvent<MD5MessageSentEvent>().Publish(new MD5Message
                {
                    ChecksumAction = ChecksumAction.Save,
                    Path = saveFile.FileName
                });
            }
        }

        private void SaveSequenceSHA512Checksum()
        {
            var saveFile = new SaveFileDialog();
            saveFile.Filter = sha512FileFilterExtension;
            if (saveFile.ShowDialog().Value)
            {
                eventAggregator.GetEvent<SHA512MessageSentEvent>().Publish(new SHA512Message
                {
                    ChecksumAction = ChecksumAction.Save,
                    Path = saveFile.FileName
                });
            }
        }

        #endregion//Commons

        #endregion//Methods
    }
}
