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
        ViewModel _viewModel = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
            _viewModel.LanguageCBSelectedIndex = 0;
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

        public void DisableCipherTextBoxes(TextBox originalTextBox, TextBox encryptedTextBox, TextBox decryptedTextBox)
        {
            encryptedTextBox.IsReadOnly = true;
            originalTextBox.IsReadOnly = true;
            encryptedTextBox.Background = Brushes.LightGray;
            originalTextBox.Background = Brushes.LightGray;
            encryptedTextBox.Text = "";
            originalTextBox.Text = "";
            decryptedTextBox.Text = "";
        }
    }
}