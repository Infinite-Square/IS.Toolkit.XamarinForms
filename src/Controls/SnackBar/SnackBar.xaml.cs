using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS.Toolkit.XamarinForms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SnackBar : TemplatedView
    {
        public static readonly BindableProperty MessageProperty = BindableProperty.Create("Message", typeof(string), typeof(SnackBar), default(string));
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly BindableProperty CloseButtonTextProperty = BindableProperty.Create("CloseButtonText", typeof(string), typeof(SnackBar), "Close");
        public string CloseButtonText
        {
            get { return (string)GetValue(CloseButtonTextProperty); }
            set { SetValue(CloseButtonTextProperty, value); }
        }

        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create("TapCommand", typeof(ICommand), typeof(SnackBar), default(ICommand));
        public ICommand TapCommand
        {
            get { return (ICommand)GetValue(TapCommandProperty); }
            set { SetValue(TapCommandProperty, value); }
        }

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create("FontSize", typeof(float), typeof(SnackBar), default(float));
        public float FontSize
        {
            get { return (float)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create("TextColor", typeof(Color), typeof(SnackBar), default(Color));
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static readonly BindableProperty CloseButtonBackGroundColorProperty = BindableProperty.Create("CloseButtonBackGroundColor", typeof(Color), typeof(SnackBar), default(Color));
        public Color CloseButtonBackGroundColor
        {
            get { return (Color)GetValue(CloseButtonBackGroundColorProperty); }
            set { SetValue(CloseButtonBackGroundColorProperty, value); }
        }

        public float FadeOutDuration { get; set; }

        public SnackBar()
        {
            InitializeComponent();
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            IsVisible = false;
        }

        public void Open(string message)
        {
            Message = message;
            IsVisible = true;
        }
    }
}