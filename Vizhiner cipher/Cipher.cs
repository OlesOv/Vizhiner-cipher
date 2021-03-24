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
        private int _languageCBSelectedIndex;

        public string OriginalText
        {
            get => _originalText;
            set
            {
                _originalText = value;
                if(LanguageCBSelectedIndex == (int)Alphabets.LanguageIndexes.Russian)
                {
                    EncryptedText = Encrypt(value, KeywordText, Alphabets.Russian);
                }
                else if(LanguageCBSelectedIndex == (int)Alphabets.LanguageIndexes.English)
                {
                    EncryptedText = Encrypt(value, KeywordText, Alphabets.English);
                }
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
                if (LanguageCBSelectedIndex == (int)Alphabets.LanguageIndexes.Russian)
                {
                    DecryptedText = Decrypt(value, KeywordText, Alphabets.Russian);
                }
                else if (LanguageCBSelectedIndex == (int)Alphabets.LanguageIndexes.English)
                {
                    DecryptedText = Decrypt(value, KeywordText, Alphabets.English);
                }
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
                RaisePropertyChanged("LanguageCBSelectedIndex");
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
