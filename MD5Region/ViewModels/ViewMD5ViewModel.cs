using Commons;
using Prism.Commands;
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

        public ViewMD5ViewModel(IHashService md5HashService)
            => this.md5HashService = md5HashService;

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

        #endregion//Commands

        #region Methods

        private void GenerateMD5ChecksumCommandExecute()
        {
            var buffer = Encoding.UTF8.GetBytes(Text);
            var hashedBuffer = md5HashService.GetHash(buffer);
            var stringBuilder = new StringBuilder();
            foreach (var item in hashedBuffer)
            {
                stringBuilder.Append(item.ToString("x2"));
            }
            MD5Hash = stringBuilder.ToString();
        }

        private bool GnerateMD5ChecksumCommandCanExecute()
            => Text?.Length > 0;

        #endregion//Methods

        #region Fields

        private readonly IHashService md5HashService;

        #endregion//Fields
    }
}
