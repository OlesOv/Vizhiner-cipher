using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace Vizhiner_cipher
{
    public class ViewModel : BindableBase
    {
        private string _originalText;
        private string _keywordText;
        private string _encryptedText;
        private string _decryptedText;
        private int _languageCBSelectedIndex;

        public string OriginalText
        {
            get => _originalText;
            set
            {
                _originalText = value;
                RaisePropertyChanged("OriginalText");
            }
        }

        public string KeywordText
        {
            get => _keywordText;
            set
            {
                _keywordText = value;
                RaisePropertyChanged("KeywordText");
            }
        }

        public string EncryptedText
        {
            get => _encryptedText;
            set
            {
                _encryptedText = value;
                RaisePropertyChanged("EncryptedText");
            }
        }

        public string DecryptedText
        {
            get => _decryptedText;
            set
            {
                _decryptedText = value;
                RaisePropertyChanged("DecryptedText");
            }
        }

        public int LanguageCBSelectedIndex
        {
            get => _languageCBSelectedIndex;
            set
            {
                _languageCBSelectedIndex = value;
                RaisePropertyChanged("LabguageCBSelectedIndex");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
