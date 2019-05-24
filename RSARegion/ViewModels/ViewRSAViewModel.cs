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
        private readonly IEventAggregator eventAggregator;
        private readonly IRSACryptographicService cryptographicService;
        private readonly ISerializationService serializationService;
        private readonly RSAPairKeyParameters rsaPairKeyParameters;
        private RSAParameters privateAndPublicKeyParameeters;
        private RSAParameters publicKeyParameters;
        
        public ViewRSAViewModel(IEventAggregator eventAggregator, 
            IRSACryptographicService cryptographicService,
            ISerializationService serializationService)
        {
            this.eventAggregator = eventAggregator;
            this.cryptographicService = cryptographicService;
            this.serializationService = serializationService;
            rsaPairKeyParameters = this.cryptographicService.GenerateKeyParameters();
            eventAggregator.GetEvent<RSAMessageSentEvnt>().Subscribe(ExecuteMessage);
        }

        private void ExecuteMessage(RsaMessage message)
        {
            switch (message.RSAAction)
            {
                case RSAAction.Generate:
                    var bothKeyParameters = cryptographicService.GenerateKeyParameters();
                    privateAndPublicKeyParameeters = bothKeyParameters.PrivateKeyParameters;
                    publicKeyParameters = bothKeyParameters.PublicKeyParameters;
                    break;
                case RSAAction.Open:
                    break;
                case RSAAction.SavePublicKey:
                    serializationService.Serialize(publicKeyParameters, message.Path);
                    break;
                case RSAAction.SavePrivateAndPublicKey:
                    serializationService.Serialize(privateAndPublicKeyParameeters, message.Path);
                    break;
            }
        }

        private string publicKey;
        public string PublicKey
        {
            get { return publicKey; }
            set { SetProperty(ref publicKey, value); }
        }

        private string privatAndPublicKey;
        public string PrivateAndPublicKey
        {
            get { return privatAndPublicKey; }
            set { SetProperty(ref privatAndPublicKey, value); }
        }

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

        private int? bitsKeySize;
        public int? BitsKeySize
        {
            get { return bitsKeySize; }
            set { SetProperty(ref bitsKeySize, value); }
        }

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

        private void EncryptCommandExecute()
        {
            privateAndPublicKeyParameeters = rsaPairKeyParameters.PrivateKeyParameters;
            publicKeyParameters = rsaPairKeyParameters.PublicKeyParameters;
            PrivateAndPublicKey = rsaPairKeyParameters.PrivateKeyParameters.ToString();
            PublicKey = rsaPairKeyParameters.PublicKeyParameters.ToString();
            var byteArrayText = Encoding.UTF8.GetBytes(Text);
            var encrypredByteArray = cryptographicService.Encrypt(byteArrayText, rsaPairKeyParameters.PrivateKeyParameters);
            EncryptedText = Encoding.Unicode.GetString(encrypredByteArray);
        }
    }
}
