using Prism.Mvvm;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Vizhiner_cipher
{
    public class Cipher : BindableBase
    {
        private string _originalText;
        private string _keywordText;
        private string _encryptedText;
        private string _decryptedText;

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
                RaisePropertyChanged("DecryptedText");
            }
        }

        public string Encrypt(string text, string keyword, string alphabet)
        {
            char[] result = text.ToCharArray();
            int lastKeyChar = 0;
            text = text.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                char currentSymbol = alphabet[(alphabet.IndexOf(text[i]) +
                        alphabet.IndexOf(keyword[lastKeyChar % keyword.Length])) % alphabet.Length];
                lastKeyChar++;

                if (char.IsUpper(result[i]))
                {
                    currentSymbol = char.ToUpper(currentSymbol);
                }
                result[i] = currentSymbol;
            }
            return new string(result);
        }

        public string Decrypt(string text, string keyword, string alphabet)
        {
            char[] result = text.ToCharArray();
            int lastKeyChar = 0;
            text = text.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                char currentSymbol = alphabet[(alphabet.IndexOf(text[i]) + alphabet.Length -
                    alphabet.IndexOf(keyword[lastKeyChar % keyword.Length])) % alphabet.Length];
                lastKeyChar++;

                if (char.IsUpper(result[i]))
                {
                    currentSymbol = char.ToUpper(currentSymbol);
                }
                result[i] = currentSymbol;
            }
            return new string(result);
        }
    }
}
