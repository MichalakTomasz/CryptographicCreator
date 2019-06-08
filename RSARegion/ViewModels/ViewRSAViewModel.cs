using Commons;
using EventAggregator;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;

namespace RSARegion.ViewModels
{
    public class ViewRSAViewModel : BindableBase
    {
        #region Fields

        private readonly IEventAggregator eventAggregator;
        private readonly IRSACryptographicService cryptographicService;
        private readonly IRSASerializationService rsaSerializationService;
        private readonly RSAPairKeyParameters rsaPairKeyParameters;

        private RSAParameters privateAndPublicKeyParameters;
        private RSAParameters publicKeyParameters;
        private byte[] encryptedData;

        #endregion Fields

        #region Constructor

        public ViewRSAViewModel(
            IEventAggregator eventAggregator,
            IRSACryptographicService cryptographicService,
            IRSASerializationService rsaSerializationService)
        {
            this.eventAggregator = eventAggregator;
            this.cryptographicService = cryptographicService;
            this.rsaSerializationService = rsaSerializationService;
            rsaPairKeyParameters = this.cryptographicService.GenerateKeyParameters();
            eventAggregator.GetEvent<RSAMessageSentEvent>().Subscribe(ExecuteMessage);
            PrivateKeyForEncryptionDecryption = true;
        }

        #endregion//Constructor

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

        private string decryptedText;
        public string DecryptedText
        {
            get { return decryptedText; }
            set { SetProperty(ref decryptedText, value); }
        }

        private bool isActivePublicKey;
        public bool IsActivePublicKey
        {
            get { return isActivePublicKey; }
            set { SetProperty(ref isActivePublicKey, value); }
        }

        private bool isActivePrivateKey;
        public bool IsActivePrivateKey
        {
            get { return isActivePrivateKey; }
            set { SetProperty(ref isActivePrivateKey, value); }
        }

        private bool areActiveEncrypreptedData;
        public bool AreActiveEncryptedData
        {
            get { return areActiveEncrypreptedData; }
            set { SetProperty(ref areActiveEncrypreptedData, value); }
        }

        private bool areKeysFromTheSameBase;
        public bool AreKeysFromTheSameBase
        {
            get { return areKeysFromTheSameBase; }
            set { SetProperty(ref areKeysFromTheSameBase, value); }
        }

        private bool privateKeyForEncryptionDecryption;
        public bool PrivateKeyForEncryptionDecryption
        {
            get { return privateKeyForEncryptionDecryption; }
            set { SetProperty(ref privateKeyForEncryptionDecryption, value); }
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
                        .ObservesProperty(() => PrivateKeyForEncryptionDecryption)
                        .ObservesProperty(() => IsActivePublicKey)
                        .ObservesProperty(() => IsActivePrivateKey)
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
                        .ObservesProperty(() => IsActivePrivateKey)
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

        #region Private Methods

        private void ExecuteMessage(RsaMessage message)
        {
            switch (message.RSAAction)
            {
                case RSAAction.Generate:
                    var bothKeyParameters = cryptographicService.GenerateKeyParameters();
                    privateAndPublicKeyParameters = bothKeyParameters.PrivateKeyParameters;
                    publicKeyParameters = bothKeyParameters.PublicKeyParameters;
                    IsActivePrivateKey = true;
                    IsActivePublicKey = true;
                    SetKeyParametersBase();
                    message.RSAAction = RSAAction.None;
                    break;
                case RSAAction.OpenPrivateKey:
                    privateAndPublicKeyParameters = rsaSerializationService.DeserializeKey(message.Path);
                    IsActivePrivateKey = true;
                    SetKeyParametersBase();
                    message.RSAAction = RSAAction.None;
                    break;
                case RSAAction.OpenPublicKey:
                    publicKeyParameters = rsaSerializationService.DeserializeKey(message.Path);
                    IsActivePublicKey = true;
                    SetKeyParametersBase();
                    message.RSAAction = RSAAction.None;
                    break;
                case RSAAction.OpenEncryptedData:
                    encryptedData = rsaSerializationService.DeserializeEncryptedData(message.Path);
                    AreActiveEncryptedData = true;
                    SetKeyParametersBase();
                    message.RSAAction = RSAAction.None;
                    break;
                case RSAAction.SavePublicKey:
                    rsaSerializationService.SerializeKey(publicKeyParameters, message.Path);
                    message.RSAAction = RSAAction.None;
                    break;
                case RSAAction.SavePrivateAndPublicKey:
                    rsaSerializationService.SerializeKey(privateAndPublicKeyParameters, message.Path);
                    message.RSAAction = RSAAction.None;
                    break;
                case RSAAction.SaveEncryptedData:
                    rsaSerializationService.SerializeEncryptedData(encryptedData, message.Path);
                    message.RSAAction = RSAAction.None;
                    break;
            }
        }

        private void SetKeyParametersBase()
        {
            if (IsActivePrivateKey && IsActivePublicKey) AreKeysFromTheSameBase = 
                    cryptographicService.CompareKeyBases(privateAndPublicKeyParameters, publicKeyParameters);
            else AreKeysFromTheSameBase = false;
        }

        private void EncryptCommandExecute()
        {
            var byteArrayText = Encoding.Unicode.GetBytes(Text);
            var keyToEncrypt = PrivateKeyForEncryptionDecryption ? 
                privateAndPublicKeyParameters : publicKeyParameters; 
            encryptedData = cryptographicService.Encrypt(byteArrayText, keyToEncrypt);
            EncryptedText = Encoding.Unicode.GetString(encryptedData);
            AreActiveEncryptedData = true;
            eventAggregator.GetEvent<RSAMessageSentEvent>().Publish(new RsaMessage { RSAAction = RSAAction.Encrypt });
        }

        private void DecryptCommandExecute()
        {
            var decryptedData = cryptographicService.Decrypt(encryptedData, privateAndPublicKeyParameters);
            DecryptedText = Encoding.Unicode.GetString(decryptedData);
            eventAggregator.GetEvent<RSAMessageSentEvent>().Publish(new RsaMessage { RSAAction = RSAAction.Decrypt });
        }

        private bool EncryptCommandCanExecute()
        {
            if (PrivateKeyForEncryptionDecryption)
            {
                return IsActivePrivateKey &&
                    Text?.Length > 0 &&
                    !AreActiveEncryptedData;
            }
            else return IsActivePublicKey &&
                    Text?.Length > 0 &&
                    !AreActiveEncryptedData;
        }
        
        private bool DecryptCommandCanExecute()
            => AreActiveEncryptedData && IsActivePrivateKey;
        
        private void ClearTextCommandExecute()
            => Text = "";

        private bool ClearTextCanCommandExecute()
            => Text?.Length > 0;

        #endregion//Private Methods
    }
}
