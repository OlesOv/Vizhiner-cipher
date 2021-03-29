using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizhiner_cipher;

namespace VizhinerCipherTests
{
    [TestFixture]
    public class UITests
    {
        ViewModel _viewModel;
        List<string> _recievedEvents;

        [SetUp]
        public void SetUp()
        {
            _viewModel = new ViewModel();
            _recievedEvents = new List<string>();
            _viewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                _recievedEvents.Add(e.PropertyName);
            };
        }

        [Test]
        public void OriginalTextChanged_WhenCalled_RaiseOnPropertyChangedEvent()
        {
            _viewModel.OriginalText = "test";

            Assert.That(_recievedEvents.Contains("OriginalText"));
        }

        [Test]
        public void EncryptedTextChanged_WhenCalled_RaiseOnPropertyChangedEvent()
        {
            _viewModel.EncryptedText = "test";

            Assert.That(_recievedEvents.Contains("EncryptedText"));
        }

        [Test]
        public void DecryptedTextChanged_WhenCalled_RaiseOnPropertyChangedEvent()
        {
            _viewModel.DecryptedText = "test";

            Assert.That(_recievedEvents.Contains("DecryptedText"));
        }

        [Test]
        public void KeywordTextChanged_WhenCalled_RaiseOnPropertyChangedEvent()
        {
            _viewModel.KeywordText = "test";

            Assert.That(_recievedEvents.Contains("KeywordText"));
        }

        [Test]
        public void LanguageCBSelectedIndexChanged_WhenCalled_RaiseOnPropertyChangedEvent()
        {
            _viewModel.LanguageCBSelectedIndex = 1;

            Assert.That(_recievedEvents.Contains("LanguageCBSelectedIndex"));
        }

        [Test]
        public void LanguageCBSelectedIndexChanged_WhenCalled_KeywordTextIsSetToEmpty()
        {
            _viewModel.LanguageCBSelectedIndex = 1;

            Assert.That(_viewModel.KeywordText.Equals(""));
        }
    }
}
