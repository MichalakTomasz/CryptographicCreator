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

namespace MD5Region.ViewModels
{
    public class ViewMD5ViewModel : BindableBase
    {
        #region Constructor

        public ViewMD5ViewModel(
            IHashService md5HashService,
            IEventAggregator eventAggregator,
            ISerializationService serializationService)
        {
            this.md5HashService = md5HashService;
            this.eventAggregator = eventAggregator;
            this.serializationService = serializationService;

            eventAggregator.GetEvent<MD5MessageSentEvent>()
                .Subscribe(ExecuteMessage);
        }
        
        #endregion//Constructor

        #region Properties

        private string text;
        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }

        private string md5Hash;
        public string MD5Hash
        {
            get { return md5Hash; }
            set { SetProperty(ref md5Hash, value); }
        }

        private string checksumToCompare;
        public string ChecksumToCompare
        {
            get { return checksumToCompare; }
            set { SetProperty(ref checksumToCompare, value); }
        }

        private string checksumsCompareText;
        public string ChecksumsCompareText
        {
            get { return checksumsCompareText; }
            set { SetProperty(ref checksumsCompareText, value); }
        }

        #endregion//Properties

        #region Commands

        private ICommand generateMD5ChecksumCommand;
        public ICommand GenerateMD5ChecksumCommand
        {
            get
            {
                if (generateMD5ChecksumCommand == null)
                    generateMD5ChecksumCommand = new DelegateCommand(GenerateMD5ChecksumCommandExecute, 
                        GnerateMD5ChecksumCommandCanExecute)
                        .ObservesProperty(() => Text);
                return generateMD5ChecksumCommand;
            }
        }

        private ICommand compareCommand;
        public ICommand CompareCommand
        {
            get
            {
                if (compareCommand == null)
                    compareCommand = new DelegateCommand(CompareCommandExecute,
                        CompareCommandCanExecute)
                        .ObservesProperty(() => Text)
                        .ObservesProperty(() => ChecksumToCompare);
                return generateMD5ChecksumCommand;
            }
        }

        #endregion//Commands

        #region Methods

        private void GenerateMD5ChecksumCommandExecute()
        {
            var buffer = Encoding.UTF8.GetBytes(Text);
            hashsumBuffer = md5HashService.GetHash(buffer);
            var stringBuilder = new StringBuilder();
            foreach (var item in hashsumBuffer)
                stringBuilder.Append(item.ToString("x2"));
            MD5Hash = stringBuilder.ToString();
        }

        private bool GnerateMD5ChecksumCommandCanExecute()
            => Text?.Length > 0;

        private bool CompareCommandCanExecute()
            => Text?.Length > 0 && ChecksumToCompare.Length > 0;

        private void CompareCommandExecute()
        {
            bool areTheSameChecksum = false;
            if (hashsumBuffer != null && hashsumBufferToCompare != null &&
                hashsumBuffer.Length == hashsumBufferToCompare.Length)
            {
                var i = 0;
                while (i < hashsumBuffer.Length && hashsumBuffer[i] != hashsumBufferToCompare[i])
                    i++;
                areTheSameChecksum = i < hashsumBuffer.Length;
            }
            ChecksumsCompareText =  areTheSameChecksum ? 
                "Checksums are the same" :
                "Checksums are different";

        }

        private void ExecuteMessage(MD5Message message)
        {
            switch (message.HahshsumAction)
            {
                case ChecksumAction.Open:
                    hashsumBufferToCompare = serializationService.Deserialize(message.Path);
                    
                    break;
                case ChecksumAction.Save:
                    serializationService.Serialize(hashsumBuffer, message.Path);
                    message.HahshsumAction = ChecksumAction.None;
                    break;
            }
        }

        #endregion//Methods

        #region Fields

        private readonly IHashService md5HashService;
        private readonly IEventAggregator eventAggregator;
        private readonly ISerializationService serializationService;

        private byte[] hashsumBuffer;
        private byte[] hashsumBufferToCompare;

        #endregion//Fields
    }
}
