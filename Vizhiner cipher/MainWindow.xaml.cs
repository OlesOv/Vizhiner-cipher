using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

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
            DataContext = new ViewModel();
            Animations animations = new()
            {
                Window = this
            };
            Thread KeywordTextBoxAnimation = new(new ParameterizedThreadStart(animations.TextBoxHeightAnimate));
            HeightAnimationParams KeywordTextBoxAnimationHeightParams = new()
            {
                fromHeight = 20,
                toHeight = 36,
                animatedTextBox = KeywordTextBox,
                duration = TimeSpan.FromSeconds(2)
            };
            KeywordTextBoxAnimation.Start(KeywordTextBoxAnimationHeightParams);

            Thread OriginalTextBoxAnimation = new(new ParameterizedThreadStart(animations.TextBoxMarginAnimate));
            MarginAnimationParams originalTextBoxAnimationMarginParams = new()
            {
                fromMargin = new Thickness(-1000, 7, 1000, 7),
                duration = TimeSpan.FromSeconds(2),
                animatedTextBox = OriginalTextBox
            };
            OriginalTextBoxAnimation.Start(originalTextBoxAnimationMarginParams);

            Thread EncryptedTextBoxAnimation = new(new ParameterizedThreadStart(animations.TextBoxMarginAnimate));
            MarginAnimationParams encryptedTextBoxAnimationMarginParams = new()
            {
                fromMargin = new Thickness(7, 1000, 7, -1000),
                duration = TimeSpan.FromSeconds(2),
                animatedTextBox = EncryptedTextBox
            };
            EncryptedTextBoxAnimation.Start(encryptedTextBoxAnimationMarginParams);

            Thread DecryptedTextBoxAnimation = new(new ParameterizedThreadStart(animations.TextBoxMarginAnimate));
            MarginAnimationParams decryptedTextBoxAnimationMarginParams = new()
            {
                fromMargin = new Thickness(1000, 7, -1000, 7),
                duration = TimeSpan.FromSeconds(2),
                animatedTextBox = DecryptedTextBox
            };
            DecryptedTextBoxAnimation.Start(decryptedTextBoxAnimationMarginParams);
        }

        private void KeywordTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (LanguageComboBox.SelectedIndex == (int)Alphabets.LanguageIndexes.Russian)
            {
                e.Handled = !Alphabets.Russian.Contains(e.Text);
            }
            else if (LanguageComboBox.SelectedIndex == (int)Alphabets.LanguageIndexes.English)
            {
                e.Handled = !Alphabets.English.Contains(e.Text);
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
                MessageBox.Show("Ensure that you are writing using selected language and that you are writing in a lowercase");
            }
        }

        private void DisableCipherTextBoxes()
        {
            EncryptedTextBox.IsReadOnly = true;
            OriginalTextBox.IsReadOnly = true;
            EncryptedTextBox.Background = Brushes.LightGray;
            OriginalTextBox.Background = Brushes.LightGray;
            EncryptedTextBox.Text = "";
            OriginalTextBox.Text = "";
            DecryptedTextBox.Text = "";
        }

        private void RussianEncryptAndDecrypt()
        {
            EncryptedTextBox.Text = Cipher.Encrypt(OriginalTextBox.Text, KeywordTextBox.Text, Alphabets.Russian);
            DecryptedTextBox.Text = Cipher.Decrypt(EncryptedTextBox.Text, KeywordTextBox.Text, Alphabets.Russian);
        }

        private void EnglishEncryptAndDecrypt()
        {
            EncryptedTextBox.Text = Cipher.Encrypt(OriginalTextBox.Text, KeywordTextBox.Text, Alphabets.English);
            DecryptedTextBox.Text = Cipher.Decrypt(EncryptedTextBox.Text, KeywordTextBox.Text, Alphabets.English);
        }

        private void KeywordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (KeywordTextBox.Text.Length == 0)
            {
                DisableCipherTextBoxes();
            }

            if (LanguageComboBox.SelectedIndex == (int)Alphabets.LanguageIndexes.Russian)
            {
                RussianEncryptAndDecrypt();
            }
            else if (LanguageComboBox.SelectedIndex == (int)Alphabets.LanguageIndexes.English)
            {
                EnglishEncryptAndDecrypt();
            }
        }

        private void EraseAndSetReadOnlyTextBox(TextBox textBox)
        {
            if (textBox != null)
            {
                textBox.Text = "";
                textBox.IsReadOnly = true;
            }
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EraseAndSetReadOnlyTextBox(OriginalTextBox);
            EraseAndSetReadOnlyTextBox(KeywordTextBox);
            EraseAndSetReadOnlyTextBox(EncryptedTextBox);
            EraseAndSetReadOnlyTextBox(DecryptedTextBox);
        }

        private void OriginalTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LanguageComboBox.SelectedIndex == (int)Alphabets.LanguageIndexes.Russian)
            {
                EncryptedTextBox.Text = Cipher.Encrypt(OriginalTextBox.Text, KeywordTextBox.Text, Alphabets.Russian);
            }
            else if (LanguageComboBox.SelectedIndex == (int)Alphabets.LanguageIndexes.English)
            {
                EncryptedTextBox.Text = Cipher.Encrypt(OriginalTextBox.Text, KeywordTextBox.Text, Alphabets.English);
            }
        }

        private void EncryptedTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LanguageComboBox.SelectedIndex == (int)Alphabets.LanguageIndexes.Russian)
            {
                DecryptedTextBox.Text = Cipher.Decrypt(EncryptedTextBox.Text, KeywordTextBox.Text, Alphabets.Russian);
            }
            else if (LanguageComboBox.SelectedIndex == (int)Alphabets.LanguageIndexes.English)
            {
                DecryptedTextBox.Text = Cipher.Decrypt(EncryptedTextBox.Text, KeywordTextBox.Text, Alphabets.English);
            }
        }
    }
}