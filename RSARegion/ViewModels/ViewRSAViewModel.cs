using Commons;
using EventAggregator;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModuleREARegion.ViewModels
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
            eventAggregator.GetEvent<RSAMessageSentEvnt>().Subscribe(ExecuteMessage);
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

        private bool areActiveEncrypryptedData;
        public bool AreActiveEncryptedData
        {
            get { return areActiveEncrypryptedData; }
            set { SetProperty(ref areActiveEncrypryptedData, value); }
        }

        #endregion//Properties

        #region Commands
       
        private ICommand encryptCommand;
        public ICommand EncryptCommand
        {
            get
            {
                if (encryptCommand == null)
                    encryptCommand = new DelegateCommand(EncryptCommandExecute);
                return encryptCommand;
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
                    message.RSAAction = RSAAction.None;
                    break;
                case RSAAction.OpenPrivateKey:
                    privateAndPublicKeyParameters = rsaSerializationService.DeserializeKey(message.Path);
                    IsActivePrivateKey = true;
                    message.RSAAction = RSAAction.None;
                    break;
                case RSAAction.OpenPublicKey:
                    publicKeyParameters = rsaSerializationService.DeserializeKey(message.Path);
                    IsActivePublicKey = true;
                    message.RSAAction = RSAAction.None;
                    break;
                case RSAAction.OpenEncryptedData:
                    rsaSerializationService.DeserializeEncryptedData(message.Path);
                    IsActivePublicKey = true;
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
                    message.RSAAction = RSAAction.None;
                    break;
            }
        }

        private void EncryptCommandExecute()
        {
            privateAndPublicKeyParameters = rsaPairKeyParameters.PrivateKeyParameters;
            IsActivePrivateKey = !privateAndPublicKeyParameters.Equals(default(RSAParameters));
            publicKeyParameters = rsaPairKeyParameters.PublicKeyParameters;
            IsActivePublicKey = !publicKeyParameters.Equals(default(RSAParameters));
            var byteArrayText = Encoding.UTF8.GetBytes(Text);
            var encrypredByteArray = cryptographicService.Encrypt(byteArrayText, rsaPairKeyParameters.PrivateKeyParameters);
            EncryptedText = Encoding.Unicode.GetString(encrypredByteArray);
        }

        #endregion//Private Methods
    }
}
