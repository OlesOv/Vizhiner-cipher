using System;
using System.Windows;
using System.Windows.Controls;

namespace Vizhiner_cipher
{
    public class MarginAnimationParams
    {
        public Thickness fromMargin { get; set; }

        public TextBox animatedTextBox { get; set; }

        public TimeSpan duration;
    }
}
