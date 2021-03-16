using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Vizhiner_cipher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    static class Alphabets
    {
        public const string RussianAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя",
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
            char[] result = new char[text.Length];
            string originalText = text;
            int lastKeyChar = 0;
            text = text.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                if (alphabet.Contains(text[i]))
                {
                    char temp = alphabet[(alphabet.IndexOf(text[i]) +
                        alphabet.IndexOf(keyword[lastKeyChar % keyword.Length])) % alphabet.Length];

                    if (originalText[i] == char.ToUpper(text[i]))
                    {
                        result[i] = char.ToUpper(temp);
                    }
                    else
                    {
                        result[i] = temp;
                    }
                    lastKeyChar++;
                }
                else
                {
                    result[i] = text[i];
                }
            }
            return new string(result);
        }

        private static string Decrypt(string text, string keyword, string alphabet)
        {
            char[] result = new char[text.Length];
            string originalText = text;
            int lastKeyChar = 0;
            text = text.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                if (alphabet.Contains(text[i]))
                {
                    char temp = alphabet[(alphabet.IndexOf(text[i]) + alphabet.Length -
                        alphabet.IndexOf(keyword[lastKeyChar % keyword.Length])) % alphabet.Length];

                    if (originalText[i] == char.ToUpper(text[i]))
                    {
                        result[i] = char.ToUpper(temp);
                    }
                    else
                    {
                        result[i] = temp;
                    }
                    lastKeyChar++;
                }
                else
                {
                    result[i] = text[i];
                }
            }
            return new string(result);
        }

        private void KeywordTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (LanguageComboBox.SelectedIndex == RussianLanguageIndex)
            {
                e.Handled = !Alphabets.RussianAlphabet.Contains(e.Text);
            }
            else if (LanguageComboBox.SelectedIndex == EnglishLanguageIndex)
            {
                e.Handled = !Alphabets.EnglishAlphabet.Contains(e.Text);
            }

            if (!e.Handled)
            {
                EncryptedTextBox.IsReadOnly = false;
                OriginalTextBox.IsReadOnly = false;
                EncryptedTextBox.Background = Brushes.White;
                OriginalTextBox.Background = Brushes.White;
            }
            else
            {
                MessageBox.Show("Ensure that you selected a correct language and that you are writing in a lowercase");
            }
        }

        private void KeywordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (KeywordTextBox.Text.Length == 0)
            {
                EncryptedTextBox.IsReadOnly = true;
                OriginalTextBox.IsReadOnly = true;
                EncryptedTextBox.Background = Brushes.LightGray;
                OriginalTextBox.Background = Brushes.LightGray;
                EncryptedTextBox.Text = "";
                OriginalTextBox.Text = "";
                DecryptedTextBox.Text = "";
            }

            if (LanguageComboBox.SelectedIndex == RussianLanguageIndex)
            {
                EncryptedTextBox.Text = Encrypt(OriginalTextBox.Text, KeywordTextBox.Text, Alphabets.RussianAlphabet);
                DecryptedTextBox.Text = Decrypt(EncryptedTextBox.Text, KeywordTextBox.Text, Alphabets.RussianAlphabet);
            }
            else if (LanguageComboBox.SelectedIndex == EnglishLanguageIndex)
            {
                EncryptedTextBox.Text = Encrypt(OriginalTextBox.Text, KeywordTextBox.Text, Alphabets.EnglishAlphabet);
                DecryptedTextBox.Text = Decrypt(EncryptedTextBox.Text, KeywordTextBox.Text, Alphabets.EnglishAlphabet);
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
            if (LanguageComboBox.SelectedIndex == RussianLanguageIndex)
            {
                EncryptedTextBox.Text = Encrypt(OriginalTextBox.Text, KeywordTextBox.Text, Alphabets.RussianAlphabet);
            }
            else if (LanguageComboBox.SelectedIndex == EnglishLanguageIndex)
            {
                EncryptedTextBox.Text = Encrypt(OriginalTextBox.Text, KeywordTextBox.Text, Alphabets.EnglishAlphabet);
            }
        }

        private void EncryptedTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LanguageComboBox.SelectedIndex == RussianLanguageIndex)
            {
                DecryptedTextBox.Text = Decrypt(EncryptedTextBox.Text, KeywordTextBox.Text, Alphabets.RussianAlphabet);
            }
            else if (LanguageComboBox.SelectedIndex == EnglishLanguageIndex)
            {
                DecryptedTextBox.Text = Decrypt(EncryptedTextBox.Text, KeywordTextBox.Text, Alphabets.EnglishAlphabet);
            }
        }
    }
}