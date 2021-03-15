using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vizhiner_cipher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string[] Alphabets = { "абвгдеёжзийклмнопрстуфхцшщъыьэюя", "abcdefghijklmnopqrstuvwxyz",
            "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ", "ABCDEFGHIJKLMOPQRSTUVWXYZ", " ?!., \n\r" };

        private string Encrypt(string text)
        {
            string result = "", key = "";
            int j = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if(Alphabets[4].IndexOf(text[i]) < 0)
                {
                    key += KeywordTextBox.Text.ToLower()[j % KeywordTextBox.Text.Length];
                    j++;
                }
                else
                {
                    key += '_';
                }
            }
            //Russian
            for (int i = 0; i < text.Length; i++)
            {
                if (LanguageComboBox.SelectedIndex == 0)
                {
                    if (Alphabets[0].IndexOf(text[i]) >= 0)  //Lower Case
                    {
                        result += Alphabets[0][(Alphabets[0].IndexOf(text[i]) +
                            Alphabets[0].IndexOf(key[i])) % Alphabets[0].Length];
                    }
                    else if (Alphabets[2].IndexOf(text[i]) >= 0) //Upper Case
                    {
                        result += Alphabets[2][(Alphabets[2].IndexOf(text[i]) +
                            Alphabets[0].IndexOf(key[i])) % Alphabets[2].Length];
                    }
                    else
                    {
                        result += text[i];
                    }
                }
                //English
                else
                {
                    if (Alphabets[1].IndexOf(text[i]) >= 0)  //Lower Case
                    {
                        result += Alphabets[1][(Alphabets[1].IndexOf(text[i]) +
                            Alphabets[1].IndexOf(key[i])) % Alphabets[1].Length];
                    }
                    else if (Alphabets[3].IndexOf(text[i]) >= 0) //Upper Case
                    {
                        result += Alphabets[3][(Alphabets[3].IndexOf(text[i]) +
                            Alphabets[1].IndexOf(key[i])) % Alphabets[3].Length];
                    }
                    else
                    {
                        result += text[i];
                    }
                }
            }
            return result;
        }

        private string Decrypt(string text)
        {
            string result = "", key = "";
            int j = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (Alphabets[4].IndexOf(text[i]) < 0)
                {
                    key += KeywordTextBox.Text.ToLower()[j % KeywordTextBox.Text.Length];
                    j++;
                }
                else
                {
                    key += '_';
                }
            }
            //Russian
            for (int i = 0; i < text.Length; i++)
            {
                if (LanguageComboBox.SelectedIndex == 0)
                {
                    if (Alphabets[0].IndexOf(text[i]) >= 0)  //Lower Case
                    {
                        result += Alphabets[0][(Alphabets[0].IndexOf(text[i]) + Alphabets[0].Length -
                            Alphabets[0].IndexOf(key[i])) % Alphabets[0].Length];
                    }
                    else if (Alphabets[2].IndexOf(text[i]) >= 0) //Upper Case
                    {
                        result += Alphabets[2][(Alphabets[2].IndexOf(text[i]) + Alphabets[2].Length -
                            Alphabets[0].IndexOf(key[i])) % Alphabets[2].Length];
                    }
                    else
                    {
                        result += text[i];
                    }
                }
                //English
                else
                {
                    if (Alphabets[1].IndexOf(text[i]) >= 0)  //Lower Case
                    {
                        result += Alphabets[1][(Alphabets[1].IndexOf(text[i]) + Alphabets[1].Length -
                            Alphabets[1].IndexOf(key[i])) % Alphabets[1].Length];
                    }
                    else if (Alphabets[3].IndexOf(text[i]) >= 0) //Upper Case
                    {
                        result += Alphabets[3][(Alphabets[3].IndexOf(text[i]) + Alphabets[3].Length -
                            Alphabets[1].IndexOf(key[i])) % Alphabets[3].Length];
                    }
                    else
                    {
                        result += text[i];
                    }
                }
            }
            return result;
        }

        private Regex[] Regexes = { new Regex(@"^[?!,.а-яА-ЯёЁ0-9\s]+$"), new Regex(@"^[?!,.a-zA-Z0-9\s]+$") };

        private void OriginalTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regexes[LanguageComboBox.SelectedIndex].IsMatch(e.Text);
            if (!e.Handled)
            {
                EncryptedTextBox.Text = Encrypt(OriginalTextBox.Text + e.Text);
            }
        }

        private void EncryptedTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regexes[LanguageComboBox.SelectedIndex].IsMatch(e.Text);
            if (!e.Handled)
            {
                DecryptedTextBox.Text = Decrypt(EncryptedTextBox.Text + e.Text);
            }
        }

        private void KeywordTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regexes[LanguageComboBox.SelectedIndex].IsMatch(e.Text);
            if (!e.Handled)
            {
                EncryptedTextBox.IsReadOnly = false;
                OriginalTextBox.IsReadOnly = false;
            }
            EncryptedTextBox.Text = Encrypt(OriginalTextBox.Text);
            DecryptedTextBox.Text = Decrypt(EncryptedTextBox.Text);
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(EncryptedTextBox != null)
            {
                EncryptedTextBox.Text = "";
                EncryptedTextBox.IsReadOnly = true;
            }
            if (KeywordTextBox != null) KeywordTextBox.Text = "";
            if (DecryptedTextBox != null) DecryptedTextBox.Text = "";
            if (OriginalTextBox != null) OriginalTextBox.IsReadOnly = true;
        }

        //private void KeywordTextBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if(e.Key == Key.Back && KeywordTextBox.Text.Length == 1)
        //    {
        //        EncryptedTextBox.Text = "";
        //        OriginalTextBox.Text = "";
        //        DecryptedTextBox.Text = "";
        //        EncryptedTextBox.IsReadOnly = true;
        //        OriginalTextBox.IsReadOnly = true;
        //    }
        //}
    }
}