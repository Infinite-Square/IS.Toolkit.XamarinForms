using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS.Toolkit.XamarinForms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloatingActionMenuItem : TemplatedView
    {
        public event EventHandler ButtonClicked;

        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(FloatingActionMenuItem), Color.Accent);
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(FloatingActionMenuItem), default(string));
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(FloatingActionMenuItem), default(ImageSource));
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FloatingActionMenuItem), default(ICommand));
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty LabelBackGroundColorProperty = BindableProperty.Create(nameof(LabelBackGroundColor), typeof(Color), typeof(Command), Color.Transparent);
        public Color LabelBackGroundColor
        {
            get { return (Color)GetValue(LabelBackGroundColorProperty); }
            set { SetValue(LabelBackGroundColorProperty, value); }
        }

        public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(float), typeof(FloatingActionMenuItem), default(float));
        public float Size
        {
            get { return (float)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public FloatingActionMenuItem()
        {
            InitializeComponent();
        }

        private void FloatingActionButton_Clicked(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(sender, e);
        }
    }
}