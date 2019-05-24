using Commons;
using EventAggregator;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptographicCreator.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IEventAggregator eventAggregator;

        public MainWindowViewModel(IEventAggregator eventAggregator)
            => this.eventAggregator = eventAggregator;

        private bool hasPublicParameters = true;
        public bool HasPublicParameters
        {
            get { return hasPublicParameters; }
            set { SetProperty(ref hasPublicParameters, value); }
        }

        private bool hasPrivateAndPublicParameters = true;
        public bool HasPrivateAndPublicParameters
        {
            get { return hasPrivateAndPublicParameters; }
            set { SetProperty(ref hasPrivateAndPublicParameters, value); }
        }

        private string selectedPath;
        public string SelectedPath
        {
            get { return selectedPath; }
            set { SetProperty(ref selectedPath, value); }
        }

        private ICommand generateCommand;
        public ICommand GenerateCommand
        {
            get
            {
                if (generateCommand == null)
                {
                    generateCommand = new DelegateCommand(GenerateCommandExecute);
                }
                return generateCommand;
            }
        }

        private ICommand loadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (loadCommand == null)
                {
                    loadCommand = new DelegateCommand(LoadCommandExecute);
                }
                return loadCommand;
            }
        }

        private ICommand savePublicKeyCommnad;
        public ICommand SavePublicKeyCommand
        {
            get
            {
                if (savePublicKeyCommnad == null)
                    savePublicKeyCommnad = new DelegateCommand(SavePublicKeyCommandExecute);
                return savePublicKeyCommnad;
            }
        }

        private ICommand savePrivatAndPulblicKeyCommand;
        public ICommand SavePrivateAndPublicKeyCommand
        {
            get
            {
                if (savePrivatAndPulblicKeyCommand == null)
                    savePrivatAndPulblicKeyCommand = new DelegateCommand(SavePrivateAndPublicCommandExecute);
                return savePrivatAndPulblicKeyCommand;
            }
        }

        private void GenerateCommandExecute()
            => eventAggregator.GetEvent<RSAMessageSentEvnt>().Publish(new RsaMessage { RSAAction = RSAAction.Generate});

        private void SavePublicKeyCommandExecute()
             => eventAggregator.GetEvent<RSAMessageSentEvnt>().Publish(new RsaMessage { RSAAction = RSAAction.SavePublicKey, Path = SelectedPath });

        private void SavePrivateAndPublicCommandExecute()
            => eventAggregator.GetEvent<RSAMessageSentEvnt>().Publish(new RsaMessage { RSAAction = RSAAction.SavePrivateAndPublicKey, Path = SelectedPath });

        private void LoadCommandExecute()
            => eventAggregator.GetEvent<RSAMessageSentEvnt>().Publish(new RsaMessage { RSAAction = RSAAction.Open, Path = SelectedPath });
    }
    
}
