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
    public partial class SnackBar : ContentView
    {
        public static readonly BindableProperty SnackBarTappedProperty = BindableProperty.Create(nameof(SnackBarTappedCommand), typeof(ICommand), typeof(SnackBar), default(ICommand));
        public ICommand SnackBarTappedCommand
        {
            get { return (ICommand)GetValue(SnackBarTappedProperty); }
            set { SetValue(SnackBarTappedProperty, value); }
        }

        public static readonly BindableProperty CloseOptionTappedProperty = BindableProperty.Create(nameof(CloseOptionTappedCommand), typeof(ICommand), typeof(SnackBar), default(ICommand));
        public ICommand CloseOptionTappedCommand
        {
            get { return (ICommand)GetValue(CloseOptionTappedProperty); }
            set { SetValue(CloseOptionTappedProperty, value); }
        }

        public static readonly BindableProperty MessageProperty = BindableProperty.Create(nameof(Message), typeof(string), typeof(SnackBar), default(string));
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly BindableProperty CloseTextProperty = BindableProperty.Create(nameof(CloseText), typeof(string), typeof(SnackBar), default(string));
        public string CloseText
        {
            get { return (string)GetValue(CloseTextProperty); }
            set { SetValue(CloseTextProperty, value); }
        }

        public static readonly BindableProperty FadeOutDurationProperty = BindableProperty.Create(nameof(FadeOutDuration), typeof(long), typeof(SnackBar), default(double));
        public long FadeOutDuration
        {
            get { return (long)GetValue(FadeOutDurationProperty); }
            set { SetValue(FadeOutDurationProperty, value); }
        }

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(float), typeof(SnackBar), default(float));
        public float FontSize
        {
            get { return (float)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(SnackBar), default(Color));
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public SnackBar()
        {
            InitializeComponent();
        }
    }
}