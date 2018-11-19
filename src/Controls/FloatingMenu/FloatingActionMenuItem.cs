using System.Windows.Input;
using Xamarin.Forms;

namespace IS.Toolkit.XamarinForms.Controls
{
    public class FloatingActionMenuItem
    {
        public Color Color { get; set; } = Color.Accent;
        public Color BackgroundColor { get; set; } = Color.Transparent;
        public string Label { get; set; }
        public ImageSource Icon { get; set; }
        public ICommand Command { get; set; }
    }
}
