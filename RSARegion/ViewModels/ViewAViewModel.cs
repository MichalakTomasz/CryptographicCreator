using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleREARegion.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
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
    }
}
