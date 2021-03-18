using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Vizhiner_cipher
{
    public class Animations
    {
        private Window window;

        public Window Window
        {
            set
            {
                window = value;
            }
        }

        public void TextBoxMarginAnimate(object marginParams)
        {
            MarginAnimationParams mparams = (MarginAnimationParams)marginParams;
            double frames = 60 * mparams.duration.TotalSeconds;
            double sleepMilliseconds = mparams.duration.TotalMilliseconds / frames;
            Thickness toMargin;
            window.Dispatcher.Invoke(() =>
            {
                toMargin = mparams.animatedTextBox.Margin;
                mparams.animatedTextBox.Margin = mparams.fromMargin;
            }, DispatcherPriority.Background);
            for (int i = 0; i < frames; i++)
            {
                window.Dispatcher.Invoke(() =>
                {
                    Thickness updateMargin = mparams.animatedTextBox.Margin;
                    updateMargin.Left += (toMargin.Left - mparams.fromMargin.Left) / frames;
                    updateMargin.Right += (toMargin.Right - mparams.fromMargin.Right) / frames;
                    updateMargin.Bottom += (toMargin.Bottom - mparams.fromMargin.Bottom) / frames;
                    updateMargin.Top += (toMargin.Top - mparams.fromMargin.Top) / frames;
                    mparams.animatedTextBox.Margin = updateMargin;
                }, DispatcherPriority.Background);
                Thread.Sleep((int)sleepMilliseconds);
            }
        }

        public void TextBoxHeightAnimate(object heightParams)
        {
            HeightAnimationParams hparams = (HeightAnimationParams)heightParams;
            double frames = hparams.duration.TotalSeconds * 60;
            double sleepMilliseconds = hparams.duration.TotalMilliseconds / frames;
            double step = (hparams.toHeight - hparams.fromHeight) / frames;
            window.Dispatcher.Invoke(() =>
            {
                hparams.animatedTextBox.Height = hparams.fromHeight;
            }, DispatcherPriority.Background);
            for (int i = 0; i < frames; i++)
            {
                window.Dispatcher.Invoke(() =>
                {
                    hparams.animatedTextBox.Height += step;
                }, DispatcherPriority.Background);
                Thread.Sleep((int)sleepMilliseconds);
            }
        }
    }
}
