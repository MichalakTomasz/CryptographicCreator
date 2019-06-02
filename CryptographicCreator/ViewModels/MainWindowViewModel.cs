using Commons;
using EventAggregator;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Input;

namespace CryptographicCreator.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Fields

        private readonly IEventAggregator eventAggregator;

        #endregion//Fiels

        #region Constructor

        public MainWindowViewModel(IEventAggregator eventAggregator)
            => this.eventAggregator = eventAggregator;

        #endregion

        #region Properties

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
            eventAggregator.GetEvent<RSAMessageSentEvnt>()
                .Publish(new RsaMessage { RSAAction = RSAAction.Generate });
            SetRSADataAcrivity(RSAAction.Generate);
        }

        private void OpenCommandExecute()
        {
            if (AcceptEvent) eventAggregator.GetEvent<RSAMessageSentEvnt>()
                .Publish(new RsaMessage{ RSAAction = RSAAction, Path = SelectedPath });
            SetRSADataAcrivity(RSAAction);
        }

        private void SaveCommandExecute()
        {
            eventAggregator.GetEvent<RSAMessageSentEvnt>()
                  .Publish(new RsaMessage { RSAAction = RSAAction, Path = SelectedPath });
            
        }

        private void SetRSADataAcrivity(RSAAction rsaAction)
        {
            switch (rsaAction)
            {
                case RSAAction.Generate:
                    IsActivePublicKey = true;
                    IsActivePrivateKey = true;
                    break;
                case RSAAction.OpenPublicKey:
                    IsActivePublicKey = true;
                    break;
                case RSAAction.OpenPrivateKey:
                    IsActivePrivateKey = true;
                    break;
                case RSAAction.OpenEncryptedData:
                    AreActiveEncryptedData = true;
                    break;
            }
        }
        #endregion//Methods
    }
}
