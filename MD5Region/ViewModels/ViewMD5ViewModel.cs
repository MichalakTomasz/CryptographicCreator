using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD5Region.ViewModels
{
    public class ViewMD5ViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ViewMD5ViewModel()
        {
            Message = "View A from your Prism Module";
        }
    }
}
