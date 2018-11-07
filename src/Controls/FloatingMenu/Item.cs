using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IS.Toolkit.XamarinForms.Controls.FloatingMenu
{
    public class Item
    {
        public Color Color { get; set; } = Color.Accent;
        public Color BackgroundColor { get; set; } = Color.Transparent;
        public string Label { get; set; }
        public ImageSource Icon { get; set; }
        public Command Action { get; set; }
    }
}
