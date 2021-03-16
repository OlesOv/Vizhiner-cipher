using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Vizhiner_cipher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    static class Chars
    {
        public static string RussianAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя",
            EnglishAlphabet = "abcdefghijklmnopqrstuvwxyz";

    }
    public partial class MainWindow : Window
    {
        public const int RussianLanguageIndex = 0,
            EnglishLanguageIndex = 1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private static string Encrypt(string text, string keyword, string alphabet)
        {
            string result = "";
            int lastKeyChar = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (alphabet.Contains(text[i]))
                {
                    result += alphabet[(alphabet.IndexOf(text[i]) + 
                        alphabet.IndexOf(keyword[lastKeyChar % keyword.Length])) % alphabet.Length];
                    lastKeyChar++;
                }
                else
                {
                    result += text[i];
                }
            }
            return result;
        }

        private static string Decrypt(string text, string keyword, string alphabet)
        {
            string result = "";
            int lastKeyChar = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (alphabet.Contains(text[i]))
                {
                    result += alphabet[(alphabet.IndexOf(text[i]) + alphabet.Length -
                        alphabet.IndexOf(keyword[lastKeyChar % keyword.Length])) % alphabet.Length];
                    lastKeyChar++;
                }
                else
                {
                    result += text[i];
                }
            }
            return result;
        }

        private void KeywordTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (LanguageComboBox.SelectedIndex == RussianLanguageIndex)
            {
                e.Handled = !Chars.RussianAlphabet.Contains(e.Text);
            }
            else if (LanguageComboBox.SelectedIndex == EnglishLanguageIndex)
            {
                e.Handled = !Chars.EnglishAlphabet.Contains(e.Text);
            }

            if (!e.Handled)
            {
                EncryptedTextBox.IsReadOnly = false;
                OriginalTextBox.IsReadOnly = false;
            }
            else
            {
                MessageBox.Show("Ensure that you selected a correct language and you are writing in a lower-case");
            }
        }

        private void KeywordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(KeywordTextBox.Text.Length == 0)
            {
                EncryptedTextBox.IsReadOnly = true;
                OriginalTextBox.IsReadOnly = true;
                EncryptedTextBox.Text = "";
                OriginalTextBox.Text = "";
                DecryptedTextBox.Text = "";
            }

            if (LanguageComboBox.SelectedIndex == RussianLanguageIndex)
            {
                EncryptedTextBox.Text = Encrypt(OriginalTextBox.Text, KeywordTextBox.Text, Chars.RussianAlphabet);
                DecryptedTextBox.Text = Decrypt(EncryptedTextBox.Text, KeywordTextBox.Text, Chars.RussianAlphabet);
            }
            else if (LanguageComboBox.SelectedIndex == EnglishLanguageIndex)
            {
                EncryptedTextBox.Text = Encrypt(OriginalTextBox.Text, KeywordTextBox.Text, Chars.EnglishAlphabet);
                DecryptedTextBox.Text = Decrypt(EncryptedTextBox.Text, KeywordTextBox.Text, Chars.EnglishAlphabet);
            }
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EncryptedTextBox != null)
            {
                EncryptedTextBox.Text = "";
                EncryptedTextBox.IsReadOnly = true;
            }
            if (KeywordTextBox != null)
            {
                KeywordTextBox.Text = "";
            }

            if (DecryptedTextBox != null)
            {
                DecryptedTextBox.Text = "";
            }

            if (OriginalTextBox != null)
            {
                OriginalTextBox.Text = "";
                OriginalTextBox.IsReadOnly = true;
            }
        }

        private void OriginalTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(LanguageComboBox.SelectedIndex == RussianLanguageIndex)
            {
                EncryptedTextBox.Text = Encrypt(OriginalTextBox.Text, KeywordTextBox.Text, Chars.RussianAlphabet);
            }
            else if(LanguageComboBox.SelectedIndex == EnglishLanguageIndex)
            {
                EncryptedTextBox.Text = Encrypt(OriginalTextBox.Text, KeywordTextBox.Text, Chars.EnglishAlphabet);
            }
        }

        private void EncryptedTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LanguageComboBox.SelectedIndex == RussianLanguageIndex)
            {
                DecryptedTextBox.Text = Decrypt(EncryptedTextBox.Text, KeywordTextBox.Text, Chars.RussianAlphabet);
            }
            else if (LanguageComboBox.SelectedIndex == EnglishLanguageIndex)
            {
                DecryptedTextBox.Text = Decrypt(EncryptedTextBox.Text, KeywordTextBox.Text, Chars.EnglishAlphabet);
            }
        }
    }
}