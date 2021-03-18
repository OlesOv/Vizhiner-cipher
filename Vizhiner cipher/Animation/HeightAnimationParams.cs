using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Vizhiner_cipher
{
    public class HeightAnimationParams
    {
        public double fromHeight { get; set; }

        public double toHeight { get; set; }

        public TextBox animatedTextBox { get; set; }

        public TimeSpan duration { get; set; }
    }
}
