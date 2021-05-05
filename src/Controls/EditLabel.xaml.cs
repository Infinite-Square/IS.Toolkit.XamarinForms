using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS.Toolkit.XamarinForms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditLabel : ContentView
    {
        public static readonly BindableProperty IsFocusedModeProperty = BindableProperty.Create(nameof(IsFocusedMode), typeof(bool), typeof(EditLabel), default(bool));
        public bool IsFocusedMode
        {
            get { return (bool)GetValue(IsFocusedModeProperty); }
            set { SetValue(IsFocusedModeProperty, value); }
        }

        public static readonly BindableProperty ContentChangedProperty = BindableProperty.Create(nameof(ContentChanged), typeof(bool), typeof(EditLabel), default(bool), propertyChanged: OnContentChangedPropertyChanged);
        public bool ContentChanged
        {
            get { return (bool)GetValue(ContentChangedProperty); }
            private set { SetValue(ContentChangedProperty, value); }
        }

        private static void OnContentChangedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((bool)newValue == true)
            {
                Thread.Sleep(100);
                Device.BeginInvokeOnMainThread(() =>
                {
                    (bindable as EditLabel).MainEntry.Focus();
                    (bindable as EditLabel).MainEntry.CursorPosition = (bindable as EditLabel).MainEntry.Text?.Length ?? 0;
                });
            }
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(EditLabel), default(string));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(float), typeof(EditLabel), 12f);
        public float FontSize
        {
            get { return (float)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(EditLabel), default(string));
        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(EditLabel), default(string));
        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(EditLabel), default(FontAttributes));
        public FontAttributes FontAttributes
        {
            get { return (FontAttributes)GetValue(FontAttributesProperty); }
            set { SetValue(FontAttributesProperty, value); }
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(EditLabel), default(Color));
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public EditLabel()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            IsFocusedMode = true;
        }

        private void MainLabel_Focused(object sender, FocusEventArgs e)
        {
        }
    }
}