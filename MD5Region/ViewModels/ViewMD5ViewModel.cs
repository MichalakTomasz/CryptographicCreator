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

        private string md5Checksum;
        public string MD5Checksum
        {
            get { return md5Checksum; }
            set { SetProperty(ref md5Checksum, value); }
        }

        private string checksumToCompareText;
        public string ChecksumToCompareText
        {
            get { return checksumToCompareText; }
            set { SetProperty(ref checksumToCompareText, value); }
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
                        .ObservesProperty(() => MD5Checksum)
                        .ObservesProperty(() => checksumBufferToCompare);
                return compareCommand;
            }
        }

        #endregion//Commands

        #region Methods

        private void GenerateMD5ChecksumCommandExecute()
        {
            var buffer = Encoding.UTF8.GetBytes(Text);
            checkSumBuffer = md5HashService.GetHash(buffer);
            MD5Checksum = GetChecksum(checkSumBuffer);
            eventAggregator.GetEvent<MD5MessageSentEvent>()
                .Publish(new MD5Message { ChecksumAction = ChecksumAction.Generate });
        }

        private bool GnerateMD5ChecksumCommandCanExecute()
            => Text?.Length > 0;

        private bool CompareCommandCanExecute()
            => MD5Checksum?.Length > 0 && ChecksumToCompareText?.Length > 0;

        private void CompareCommandExecute()
        {
            bool areTheSameChecksum = false;
            if (checkSumBuffer != null && checksumBufferToCompare != null &&
                checkSumBuffer.Length == checksumBufferToCompare.Length)
            {
                var i = 0;
                while (i < checkSumBuffer.Length && checkSumBuffer[i] != checksumBufferToCompare[i])
                    i++;
                areTheSameChecksum = i < checkSumBuffer.Length;
            }
            ChecksumsCompareText =  areTheSameChecksum ? 
                "Checksums are the same" :
                "Checksums are different";
        }

        private void ExecuteMessage(MD5Message message)
        {
            switch (message.ChecksumAction)
            {
                case ChecksumAction.Open:
                    checksumBufferToCompare = serializationService.Deserialize(message.Path);
                    ChecksumToCompareText = GetChecksum(checksumBufferToCompare);
                    break;
                case ChecksumAction.Save:
                    serializationService.Serialize(checkSumBuffer, message.Path);
                    message.ChecksumAction = ChecksumAction.None;
                    break;
            }
        }

        private string GetChecksum(byte[] buffer)
        {
            var stringBuilder = new StringBuilder();
            foreach (var item in buffer)
                stringBuilder.Append(item.ToString("x2"));
            return stringBuilder.ToString();
        }

        #endregion//Methods

        #region Fields

        private readonly IHashService md5HashService;
        private readonly IEventAggregator eventAggregator;
        private readonly ISerializationService serializationService;

        private byte[] checkSumBuffer;
        private byte[] checksumBufferToCompare;

        #endregion//Fields
    }
}
