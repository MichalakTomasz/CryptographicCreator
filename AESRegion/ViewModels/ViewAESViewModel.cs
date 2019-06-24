using Commons;
using EventAggregator;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Text;
using System.Windows.Input;

namespace AESRegion.ViewModels
{
    public class ViewAESViewModel : BindableBase
    {
        #region Fields

        private readonly IEventAggregator eventAggregator;
        private readonly IAESCryptographicService aesCryptographicService;
        private readonly IAESSerializationService aesSerializaionService;

        private AESKey aesKey;
        private byte[] encryptedBuffer;

        #endregion//Fields

        #region Constructor

        public ViewAESViewModel(
            IEventAggregator eventAggregator,
            IAESCryptographicService aesCryptographicService,
            IAESSerializationService aesSerializaionService)
        {
            this.eventAggregator = eventAggregator;
            this.aesCryptographicService = aesCryptographicService;
            this.aesSerializaionService = aesSerializaionService;
            eventAggregator.GetEvent<AESMessageSentEvent>()
                .Subscribe(ExecuteMessage);
        }
        
        #endregion//Region

        #region Properties

        private string text;
        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }

        private string encryptedText;
        public string EncryptedText
        {
            get { return encryptedText; }
            set { SetProperty(ref encryptedText, value); }
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
                        .ObservesProperty(() => AreActiveEncryptedData);
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

        private void ExecuteMessage(AESMessage message)
        {
            switch (message.AESAction)
            {
                case AESAction.Generate:
                    aesKey = aesCryptographicService.GenerateKey();
                    IsActiveKey = true;
                    message.AESAction = AESAction.None;
                    break;
                case AESAction.OpenKey:
                    aesKey = aesSerializaionService.DeserializeKey(message.Path);
                    IsActiveKey = true;
                    message.AESAction = AESAction.None;
                    break;
                case AESAction.OpenEncryptedData:
                    encryptedBuffer = aesSerializaionService.Deserialize(message.Path);   
                    EncryptedText = Encoding.Unicode.GetString(encryptedBuffer);
                    Text = string.Empty;
                    AreActiveEncryptedData = true;
                    message.AESAction = AESAction.None;
                    break;
                case AESAction.SaveKey:
                    aesSerializaionService.SerializeKey(aesKey, message.Path);
                    message.AESAction = AESAction.None;
                    break;
                case AESAction.SaveEncryptedData:
                    aesSerializaionService.Serialize(encryptedBuffer, message.Path);
                    message.AESAction = AESAction.None;
                    break;
            }
        }

        private bool EncryptCommandCanExecute()
            => IsActiveKey && Text?.Length > 0;

        private void EncryptCommandExecute()
        {
            var byteArrayText = Encoding.UTF8.GetBytes(Text);
            encryptedBuffer = aesCryptographicService.Encrypt(byteArrayText, aesKey);
            EncryptedText = Encoding.UTF8.GetString(encryptedBuffer);
            AreActiveEncryptedData = true;
            eventAggregator.GetEvent<AESMessageSentEvent>()
                .Publish(new AESMessage { AESAction = AESAction.Encrypt });
        }

        private bool DecryptCommandCanExecute()
            => AreActiveEncryptedData;

        private void DecryptCommandExecute()
        {
            var decryptedData = aesCryptographicService.Decrypt(encryptedBuffer, aesKey);
            DecryptedText = Encoding.Unicode.GetString(decryptedData);
            eventAggregator.GetEvent<AESMessageSentEvent>()
                .Publish(new AESMessage { AESAction = AESAction.Decrypt });
        }

        private bool ClearTextCanCommandExecute()
            => Text?.Length > 0;

        private void ClearTextCommandExecute()
            => Text = "";

        #endregion//Methods
    }
}
