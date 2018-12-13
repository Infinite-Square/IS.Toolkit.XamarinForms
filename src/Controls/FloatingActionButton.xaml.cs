using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS.Toolkit.XamarinForms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloatingActionButton : ContentView
    {
        public event EventHandler Clicked;

        public FloatingActionButton()
        {
             InitializeComponent();
        }

        public static readonly BindableProperty ColorProperty =
           BindableProperty.Create(nameof(Color), typeof(Color), typeof(FloatingActionButton), Color.Accent);
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(ImageSource), typeof(FloatingActionButton), null);
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FloatingActionButton), null);
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(FloatingActionButton), null);
        public static readonly BindableProperty SizeProperty =
            BindableProperty.Create(nameof(Size), typeof(double), typeof(FloatingActionButton), 50.0);
        public static readonly BindableProperty ItemPaddingProperty =
            BindableProperty.Create(nameof(ItemPadding), typeof(Thickness), typeof(FloatingActionButton), default(Thickness));

        public Color Color
        {
            get
            {
                return (Color)GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }

        public ImageSource Image
        {
            get
            {
                return (ImageSource)GetValue(ImageProperty);
            }
            set
            {
                SetValue(ImageProperty, value);
            }
        }

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public object CommandParameter
        {
            get
            {
                return GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        public double Size
        {
            get
            {
                return (double)GetValue(SizeProperty);
            }
            set
            {
                SetValue(SizeProperty, value);
            }
        }

        public Thickness ItemPadding
        {
            get
            {
                return (Thickness)GetValue(ItemPaddingProperty);
            }
            set
            {
                SetValue(ItemPaddingProperty, value);
            }
        }

        private void TapGestureReconizerTapped(object sender, EventArgs e)
        {
            Clicked?.Invoke(this, EventArgs.Empty);

            if (Command != null)
            {
                if (Command.CanExecute(CommandParameter))
                {
                    Command.Execute(CommandParameter);
                }
            }
        }

        public async void RotateAnimation()
        {
            await this.RotateTo(45, 90);
        }

        public async void RestoreRotationAnimation()
        {
            await this.RotateTo(0, 90);
        }
    }
}