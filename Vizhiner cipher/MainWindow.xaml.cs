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
        public static string RussianAlphabet = "абвгдеёжзийклмнопрстуфхцшщъыьэюя",
            EnglishAlphabet = "abcdefghijklmnopqrstuvwxyz";

    }
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string Encrypt(string text, string keyword, string alphabet)
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
            e.Handled = !Chars.EnglishAlphabet.Contains(e.Text);
            if (!e.Handled)
            {
                EncryptedTextBox.IsReadOnly = false;
                OriginalTextBox.IsReadOnly = false;
            }
            EncryptedTextBox.Text = Encrypt(OriginalTextBox.Text, KeywordTextBox.Text, Chars.EnglishAlphabet);
            DecryptedTextBox.Text = Decrypt(EncryptedTextBox.Text, KeywordTextBox.Text, Chars.EnglishAlphabet);
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
    }
}