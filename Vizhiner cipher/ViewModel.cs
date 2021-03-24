using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Prism.Commands;
using Prism.Mvvm;

namespace Vizhiner_cipher
{
    public class ViewModel : BindableBase
    {
        private readonly Cipher _cipher;

        private string _originalText;
        private string _keywordText;
        private string _encryptedText;
        private string _decryptedText;
        private int _languageCBSelectedIndex = 0;

        public string OriginalText
        {
            get => _originalText;
            set
            {
                _originalText = value;
                _cipher.OriginalText = value;
                RaisePropertyChanged("OriginalText");
                RaisePropertyChanged("EncryptedText");
                RaisePropertyChanged("DecryptedText");
            }
        }

        public string KeywordText
        {
            get => _keywordText;
            set
            {
                _keywordText = value;
                _cipher.KeywordText = value;
                RaisePropertyChanged("KeywordText");
                RaisePropertyChanged("EncryptedText");
                RaisePropertyChanged("DecryptedText");
            }
        }

        public string EncryptedText
        {
            get => _encryptedText;
            set
            {
                _encryptedText = value;
                _cipher.EncryptedText = value;
                
                RaisePropertyChanged("EncryptedText");
                RaisePropertyChanged("DecryptedText");
            }
        }

        public string DecryptedText
        {
            get => _decryptedText;
            set
            {
                _decryptedText = value;
                _cipher.DecryptedText = value;
                RaisePropertyChanged("DecryptedText");
            }
        }

        public int LanguageCBSelectedIndex
        {
            get => _languageCBSelectedIndex;
            set
            {
                _languageCBSelectedIndex = value;
                _cipher.LanguageCBSelectedIndex = value;
                RaisePropertyChanged("LanguageCBSelectedIndex");
                RaisePropertyChanged("OriginalText");
                RaisePropertyChanged("EncryptedText");
                RaisePropertyChanged("DecryptedText");
            }
        }

        public ViewModel()
        {
            _cipher = new Cipher();

            _cipher.PropertyChanged += (s, e) =>
            {
                RaisePropertyChanged(e.PropertyName);
            };
        }

        public void EraseAndSetReadOnlyTextBox(TextBox textBox)
        {
            if (textBox != null)
            {
                textBox.Text = "";
                textBox.IsReadOnly = true;
            }
        }
    }
}
