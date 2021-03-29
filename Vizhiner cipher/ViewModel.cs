using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Prism.Commands;
using Prism.Mvvm;

namespace Vizhiner_cipher
{
    public class ViewModel : BindableBase
    {
        private readonly Cipher _cipher = new Cipher();

        public string OriginalText
        {
            get => _cipher.OriginalText;
            set
            {
                _cipher.OriginalText = value;
                RaisePropertyChanged("OriginalText");
                EncryptedText = _cipher.EncryptedText;
            }
        }

        public string KeywordText
        {
            get => _cipher.KeywordText;
            set
            {
                _cipher.KeywordText = value;
                RaisePropertyChanged("KeywordText");
                EncryptedText = _cipher.EncryptedText;
            }
        }

        public string EncryptedText
        {
            get => _cipher.EncryptedText;
            set
            {
                _cipher.EncryptedText = value;
                RaisePropertyChanged("EncryptedText");
                DecryptedText = _cipher.DecryptedText;
            }
        }

        public string DecryptedText
        {
            get => _cipher.DecryptedText;
            set
            {
                _cipher.DecryptedText = value;
                RaisePropertyChanged("DecryptedText");
            }
        }

        public int LanguageCBSelectedIndex
        {
            get => _cipher.LanguageCBSelectedIndex;
            set
            {
                _cipher.LanguageCBSelectedIndex = value;
                RaisePropertyChanged("LanguageCBSelectedIndex");
                KeywordText = "";
                RaisePropertyChanged("KeywordText");
                RaisePropertyChanged("OriginalText");
                RaisePropertyChanged("EncryptedText");
                RaisePropertyChanged("DecryptedText");
            }
        }

        public void DisableCipherTextBoxes(TextBox textBox)
        {
            textBox.IsReadOnly = true;
            textBox.Background = Brushes.LightGray;
            textBox.Text = "";
        }
    }
}
