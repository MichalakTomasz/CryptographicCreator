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

namespace SHA512Region.ViewModels
{
    public class ViewSHA512ViewModel : BindableBase
    {
        #region Constructor

        public ViewSHA512ViewModel(
            IHashService sha512HashService,
            IEventAggregator eventAggregator,
            ISerializationService serializationService)
        {
            this.sha512HashService = sha512HashService;
            this.eventAggregator = eventAggregator;
            this.serializationService = serializationService;

            eventAggregator.GetEvent<SHA512MessageSentEvent>()
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

        private string sha512Checksum;
        public string SHA512Checksum
        {
            get { return sha512Checksum; }
            set { SetProperty(ref sha512Checksum, value); }
        }

        private string checksumToCompareText;
        public string ChecksumToCompareText
        {
            get { return checksumToCompareText; }
            set { SetProperty(ref checksumToCompareText, value); }
        }

        private string checksumsCompareResultText;
        public string ChecksumsCompareResultText
        {
            get { return checksumsCompareResultText; }
            set { SetProperty(ref checksumsCompareResultText, value); }
        }

        #endregion//Properties

        #region Commands

        private ICommand generateSHA512ChecksumCommand;
        public ICommand GenerateSHA512ChecksumCommand
        {
            get
            {
                if (generateSHA512ChecksumCommand == null)
                    generateSHA512ChecksumCommand = new DelegateCommand(GenerateSHA512ChecksumCommandExecute,
                        GenerateSHA512ChecksumCommandCanExecute)
                        .ObservesProperty(() => Text);
                return generateSHA512ChecksumCommand;
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
                        .ObservesProperty(() => SHA512Checksum)
                        .ObservesProperty(() => checksumBufferToCompare);
                return compareCommand;
            }
        }

        #endregion//Commands

        #region Methods

        private void GenerateSHA512ChecksumCommandExecute()
        {
            var buffer = Encoding.UTF8.GetBytes(Text);
            checkSumBuffer = sha512HashService.GetHash(buffer);
            SHA512Checksum = GetChecksum(checkSumBuffer);
            eventAggregator.GetEvent<SHA512MessageSentEvent>()
                .Publish(new SHA512Message { ChecksumAction = ChecksumAction.Generate });
        }

        private bool GenerateSHA512ChecksumCommandCanExecute()
            => Text?.Length > 0;

        private bool CompareCommandCanExecute()
            => SHA512Checksum?.Length > 0 && ChecksumToCompareText?.Length > 0;

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
            ChecksumsCompareResultText = areTheSameChecksum ?
                "Checksums are the same" :
                "Checksums are different";
        }

        private void ExecuteMessage(SHA512Message message)
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

        private readonly IHashService sha512HashService;
        private readonly IEventAggregator eventAggregator;
        private readonly ISerializationService serializationService;

        private byte[] checkSumBuffer;
        private byte[] checksumBufferToCompare;

        #endregion//Fields
    }
}
