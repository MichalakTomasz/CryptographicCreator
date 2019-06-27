using Commons;
using EventAggregator;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Text;
using System.Windows.Input;
using Unity.Attributes;

namespace SHA256Region.ViewModels
{
    public class ViewSHA256ViewModel : BindableBase
    {
        #region Constructor

        public ViewSHA256ViewModel(
            [Dependency("SHA256")]IHashService sha256HashService,
            IEventAggregator eventAggregator,
            ISerializationService serializationService)
        {
            this.sha256HashService = sha256HashService;
            this.eventAggregator = eventAggregator;
            this.serializationService = serializationService;

            eventAggregator.GetEvent<SHA256MessageSentEvent>()
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

        private string sha256Checksum;
        public string SHA256Checksum
        {
            get { return sha256Checksum; }
            set { SetProperty(ref sha256Checksum, value); }
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

        private ICommand generateSHA256ChecksumCommand;
        public ICommand GenerateSHA256ChecksumCommand
        {
            get
            {
                if (generateSHA256ChecksumCommand == null)
                    generateSHA256ChecksumCommand = new DelegateCommand(GenerateSHA256ChecksumCommandExecute,
                        GenerateSHA256ChecksumCommandCanExecute)
                        .ObservesProperty(() => Text);
                return generateSHA256ChecksumCommand;
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
                        .ObservesProperty(() => SHA256Checksum)
                        .ObservesProperty(() => ChecksumToCompareText);
                return compareCommand;
            }
        }

        #endregion//Commands

        #region Methods

        private void GenerateSHA256ChecksumCommandExecute()
        {
            var buffer = Encoding.UTF8.GetBytes(Text);
            checksumBuffer = sha256HashService.GetHash(buffer);
            SHA256Checksum = GetChecksum(checksumBuffer);
            eventAggregator.GetEvent<SHA256MessageSentEvent>()
                .Publish(new SHA256Message { ChecksumAction = ChecksumAction.Generate });
        }

        private bool GenerateSHA256ChecksumCommandCanExecute()
            => Text?.Length > 0;

        private bool CompareCommandCanExecute()
            => SHA256Checksum?.Length > 0 && ChecksumToCompareText?.Length > 0;

        private void CompareCommandExecute()
        {
            bool areTheSameChecksum = false;
            if (checksumBuffer != null && checksumBufferToCompare != null &&
                checksumBuffer.Length == checksumBufferToCompare.Length)
            {
                var i = 0;
                while (i < checksumBuffer.Length && checksumBuffer[i] != checksumBufferToCompare[i])
                    i++;
                areTheSameChecksum = i < checksumBuffer.Length;
            }
            ChecksumsCompareResultText = areTheSameChecksum ?
                "Checksums are the same" :
                "Checksums are different";
        }

        private void ExecuteMessage(SHA256Message message)
        {
            switch (message.ChecksumAction)
            {
                case ChecksumAction.Open:
                    checksumBufferToCompare = serializationService.Deserialize(message.Path);
                    ChecksumToCompareText = GetChecksum(checksumBufferToCompare);
                    break;
                case ChecksumAction.Save:
                    serializationService.Serialize(checksumBuffer, message.Path);
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

        private readonly IHashService sha256HashService;
        private readonly IEventAggregator eventAggregator;
        private readonly ISerializationService serializationService;

        private byte[] checksumBuffer;
        private byte[] checksumBufferToCompare;

        #endregion//Fields
    }
}
