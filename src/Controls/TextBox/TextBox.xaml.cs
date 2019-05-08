using IS.Toolkit.XamarinForms.Controls.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS.Toolkit.XamarinForms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextBox : ContentView
    {
        private Label _placeholder;
        private Label _errorMessage;
        private BoxView _border;
        private BorderLessEntry _entry;
        public TextBox()
        {
            InitializeComponent();
            InvalidateBorderColor();
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(TextBox), null);

        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(TextBox), Color.Gray);

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(TextBox), propertyChanged: TextChanged);

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(TextBox), Color.Accent, propertyChanged: BorderColorChanged);

        public static readonly BindableProperty KeyboardProperty =
            BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(TextBox), Keyboard.Default);

        public static readonly BindableProperty ErrorColorProperty =
            BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(TextBox), Color.Red, propertyChanged: ErrorColorChanged);

        public static readonly BindableProperty HasErrorProperty =
            BindableProperty.Create(nameof(HasError), typeof(bool), typeof(TextBox), false, propertyChanged: HasErrorChanged);

        public static readonly BindableProperty ErrorTextProperty =
           BindableProperty.Create(nameof(ErrorText), typeof(string), typeof(TextBox), null);

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(TextBox), null);

        public Keyboard Keyboard
        {
            get
            {
                return (Keyboard)GetValue(KeyboardProperty);
            }
            set
            {
                SetValue(KeyboardProperty, value);
            }
        }

        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }
            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }

        public Color PlaceholderColor
        {
            get
            {
                return (Color)GetValue(PlaceholderColorProperty);
            }
            set
            {
                SetValue(PlaceholderColorProperty, value);
            }
        }

        public Color ErrorColor
        {
            get
            {
                return (Color)GetValue(ErrorColorProperty);
            }
            set
            {
                SetValue(ErrorColorProperty, value);
            }
        }

        public Color BorderColor
        {
            get
            {
                return (Color)GetValue(BorderColorProperty);
            }
            set
            {
                SetValue(BorderColorProperty, value);
            }
        }

        public bool HasError
        {
            get
            {
                return (bool)GetValue(HasErrorProperty);
            }
            set
            {
                SetValue(HasErrorProperty, value);
            }
        }

        public string ErrorText
        {
            get
            {
                return (string)GetValue(ErrorTextProperty);
            }
            set
            {
                SetValue(ErrorTextProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public bool IsPassword
        {
            get
            {
                return (bool)GetValue(IsPasswordProperty);
            }
            set
            {
                SetValue(IsPasswordProperty, value);
            }
        }

        private static void TextChanged(BindableObject bindableObject, object oldColor, object newColor)
        {
            (bindableObject as TextBox)?.InvalidatePlaceholderPosition(false);
        }

        private static void HasErrorChanged(BindableObject bindableObject, object oldColor, object newColor)
        {
            (bindableObject as TextBox)?.InvalidateBorderColor();
            (bindableObject as TextBox)?.InvalidateErrorMessageVisibility();
        }

        private static void BorderColorChanged(BindableObject bindableObject, object oldColor, object newColor)
        {
            (bindableObject as TextBox)?.InvalidateBorderColor();
        }

        private static void ErrorColorChanged(BindableObject bindableObject, object oldColor, object newColor)
        {
            (bindableObject as TextBox)?.InvalidateBorderColor();
        }

        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            InvalidateErrorMessageVisibility();
            InvalidatePlaceholderPosition(true);
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            InvalidateErrorMessageVisibility();
            InvalidatePlaceholderPosition(true);
        }

        private async void InvalidatePlaceholderPosition(bool withAnimation)
        {
            if (string.IsNullOrEmpty(Text) && EntryView.IsFocused == false)
            {
                if (PlaceholderView.Y != 0)
                {
                    PlaceholderView.FontSize = 16;
                    if (withAnimation)
                    {
                        await ViewExtensions.TranslateTo(PlaceholderView, 0, 0, (uint)250, Easing.CubicOut);
                    }
                    else
                    {
                        PlaceholderView.TranslationY = 0;
                    }
                }
            }
            else
            {
                PlaceholderView.FontSize = 10;
                if (PlaceholderView.Y != -20)
                {
                    if (withAnimation)
                    {
                        await ViewExtensions.TranslateTo(PlaceholderView, 0, -20, (uint)250, Easing.CubicOut);
                    }
                    else
                    {
                        PlaceholderView.TranslationY = -20;
                    }
                }
            }
        }

        private void InvalidateErrorMessageVisibility()
        {
            ErrorMessageView.IsVisible = HasError;
        }

        internal Label PlaceholderView
        {
            get
            {
                if (_placeholder == null)
                {
                    _placeholder = this.GetTemplateChild<Label>("_placeholder");
                }

                return _placeholder;
            }
        }

        internal Label ErrorMessageView
        {
            get
            {
                if (_errorMessage == null)
                {
                    _errorMessage = this.GetTemplateChild<Label>("_errorMessage");
                }

                return _errorMessage;
            }
        }

        internal BoxView BorderView
        {
            get
            {
                if (_border == null)
                {
                    _border = this.GetTemplateChild<BoxView>("_border");
                }

                return _border;
            }
        }

        internal BorderLessEntry EntryView
        {
            get
            {
                if (_entry == null)
                {
                    _entry = this.GetTemplateChild<BorderLessEntry>("_entry");
                }

                return _entry;
            }
        }

        private void InvalidateBorderColor()
        {
            BorderView.Color = HasError ? ErrorColor : BorderColor;
        }
    }
}