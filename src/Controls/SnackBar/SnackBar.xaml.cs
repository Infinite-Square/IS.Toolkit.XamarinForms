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

        public static readonly BindableProperty CloseButtonBackGroundColorProperty = BindableProperty.Create("CloseButtonBackGroundColor", typeof(Color), typeof(SnackBar), Color.Transparent);
        public Color CloseButtonBackGroundColor
        {
            get { return (Color)GetValue(CloseButtonBackGroundColorProperty); }
            set { SetValue(CloseButtonBackGroundColorProperty, value); }
        }

        public uint FadeOutDuration { get; set; }
        public uint FadeInDuration { get; set; }

        #region IsOpen
        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create("IsOpen", typeof(bool), typeof(SnackBar), false, propertyChanged: IsOpenChanged);
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        private static async void IsOpenChanged(BindableObject bindable, object oldValue, object newValue)
        {
            bool isOpen;

            if (bindable != null && newValue != null)
            {
                var control = (SnackBar)bindable;
                isOpen = (bool)newValue;

                if (control.IsOpen == false)
                {
                    await control.FadeTo(1, control.FadeOutDuration);
                    control.IsVisible = false;
                }
                else
                {
                    control.IsVisible = true;
                    await control.FadeTo(100, control.FadeInDuration);
                }
            }
        }

        #endregion

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create("FontFamily", typeof(string), typeof(SnackBar), default(string));
        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public SnackBar()
        {
            InitializeComponent();
            FadeOutDuration = 2000;
            FadeInDuration = 1000;
            IsVisible = false;
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            Close();
        }

        public async void Close()
        {
            await this.TranslateTo(0, 50, FadeOutDuration);
            Message = string.Empty;
            IsVisible = false;
        }

        public async void Open(string message)
        {
            IsVisible = true;
            Message = message;
            await this.TranslateTo(0, 0, FadeInDuration);
        }
    }
}