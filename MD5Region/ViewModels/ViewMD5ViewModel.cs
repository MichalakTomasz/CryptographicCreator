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
        #region Commands

        private ICommand generateMD5ChecksumCommand;
        public ICommand GenerateMD5ChecksumCommand
        {
            get
            {
                if (generateMD5ChecksumCommand == null)
                    generateMD5ChecksumCommand = new DelegateCommand(GenerateMD5ChecksumCommandExecute);
                return generateMD5ChecksumCommand;
            }
        }

        #endregion//Commands

        #region Methods

        private void GenerateMD5ChecksumCommandExecute()
        {
            throw new NotImplementedException();
        }

        #endregion//Methods
    }
}
