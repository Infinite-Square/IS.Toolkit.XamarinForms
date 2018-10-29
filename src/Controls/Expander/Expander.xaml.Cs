using System;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS.Toolkit.XamarinForms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Expander : ContentView
    {
        private double _originalContentHeight;

        public Expander()
        {
            InitializeComponent();
        }

        #region BackgroundColor
        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
            propertyName: nameof(BackgroundColor),
            returnType: typeof(Color),
            declaringType: typeof(Expander),
            defaultValue: Color.White);

        public new Color BackgroundColor
        {
            get
            {
                return (Color)GetValue(BackgroundColorProperty);
            }
            set
            {
                SetValue(BackgroundColorProperty, value);
            }
        }
        #endregion

        #region CornerRadius
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            propertyName: nameof(CornerRadius),
            returnType: typeof(double),
            declaringType: typeof(Expander),
            defaultValue: default(double));

        public double CornerRadius
        {
            get
            {
                return (double)GetValue(CornerRadiusProperty);
            }
            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }
        #endregion

        #region BorderColor
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
            propertyName: nameof(BorderColor),
            returnType: typeof(Color),
            declaringType: typeof(Expander),
            defaultValue: Color.DarkGray);

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
        #endregion

        #region BorderWidth
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(
            propertyName: nameof(BorderWidth),
            returnType: typeof(Thickness),
            declaringType: typeof(Expander),
            defaultValue: new Thickness(1));

        public Thickness BorderWidth
        {
            get
            {
                return (Thickness)GetValue(BorderWidthProperty);
            }
            set
            {
                SetValue(BorderWidthProperty, value);
            }
        }
        #endregion

        #region LeftIcon
        public static readonly BindableProperty LeftIconProperty = BindableProperty.Create(
            propertyName: nameof(LeftIcon),
            returnType: typeof(ImageSource),
            declaringType: typeof(Expander),
            defaultValue: default(ImageSource));

        public ImageSource LeftIcon
        {
            get
            {
                return (ImageSource)GetValue(LeftIconProperty);
            }
            set
            {
                SetValue(LeftIconProperty, value);
            }
        }
        #endregion

        #region RightIcon
        public static readonly BindableProperty RightIconProperty = BindableProperty.Create(
            propertyName: nameof(RightIcon),
            returnType: typeof(ImageSource),
            declaringType: typeof(Expander),
            defaultValue: ImageSource.FromResource("IS.Toolkit.XamarinForms.Controls.Expander.dropdown.png", typeof(Expander).GetTypeInfo().Assembly));

        public ImageSource RightIcon
        {
            get
            {
                return (ImageSource)GetValue(RightIconProperty);
            }
            set
            {
                SetValue(RightIconProperty, value);
            }
        }
        #endregion

        #region HeaderText
        public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create(
            propertyName: nameof(HeaderText),
            returnType: typeof(string),
            declaringType: typeof(Expander),
            defaultValue: default(string));

        public string HeaderText
        {
            get
            {
                return (string)GetValue(HeaderTextProperty);
            }
            set
            {
                SetValue(HeaderTextProperty, value);
            }
        }
        #endregion

        #region HeaderTextColor
        public static readonly BindableProperty HeaderTextColorProperty = BindableProperty.Create(
            propertyName: nameof(HeaderTextColor),
            returnType: typeof(Color),
            declaringType: typeof(Expander),
            defaultValue: Color.Accent);

        public Color HeaderTextColor
        {
            get
            {
                return (Color)GetValue(HeaderTextColorProperty);
            }
            set
            {
                SetValue(HeaderTextColorProperty, value);
            }
        }
        #endregion

        #region HeaderFontFamily
        public static readonly BindableProperty HeaderFontFamilyProperty = BindableProperty.Create(
            propertyName: nameof(HeaderFontFamily),
            returnType: typeof(string),
            declaringType: typeof(Expander),
            defaultValue: default(string));

        public string HeaderFontFamily
        {
            get
            {
                return (string)GetValue(HeaderFontFamilyProperty);
            }
            set
            {
                SetValue(HeaderFontFamilyProperty, value);
            }
        }
        #endregion

        #region HeaderFontAttributes
        public static readonly BindableProperty HeaderFontAttributesProperty = BindableProperty.Create(
            propertyName: nameof(HeaderFontAttributes),
            returnType: typeof(FontAttributes),
            declaringType: typeof(Expander),
            defaultValue: default(FontAttributes));

        public FontAttributes HeaderFontAttributes
        {
            get
            {
                return (FontAttributes)GetValue(HeaderFontAttributesProperty);
            }
            set
            {
                SetValue(HeaderFontAttributesProperty, value);
            }
        }
        #endregion

        #region Content
        public static readonly BindableProperty ExtendedContentProperty = BindableProperty.Create(
            propertyName: nameof(ExtendedContent),
            returnType: typeof(View),
            declaringType: typeof(Expander),
            propertyChanged: ContentPropertyChanged);

        private static void ContentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (Expander)bindable;
            var value = (View)newValue;

            if (control == null || control.ExpanderContent == null || ReferenceEquals(newValue, control))
            {
                // View is null or not changed
                return;
            }

            // Update content
            control.ExpanderContent.Content = (View)newValue;
            if (control._originalContentHeight == default)
            {
                control.ExpanderContainer.SizeChanged += ExpanderContent_SizeChanged;
            }
        }

        private static void ExpanderContent_SizeChanged(object sender, EventArgs e)
        {
            var view = sender as View;
            if (!(view.Parent.Parent.Parent.Parent is Expander control))
            {
                return;
            }

            // Only at initialization
            if (control._originalContentHeight == default)
            {
                control._originalContentHeight = control.ExpanderContainer.Height;
                if (!control.IsOpen)
                {
                    // If not open, need to init container size
                    control.ExpanderContainer.HeightRequest = 0;
                }
            }
        }

        public View ExtendedContent
        {
            get { return (View)GetValue(ExtendedContentProperty); }
            set { SetValue(ExtendedContentProperty, value); }
        }
        #endregion

        #region IsOpen
        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create(
            propertyName: nameof(IsOpen),
            returnType: typeof(bool),
            declaringType: typeof(Expander),
            defaultValue: true,
            propertyChanged: IsOpenPropertyChanged);

        private static void IsOpenPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null && newValue != null)
            {
                var control = (Expander)bindable;
                var isOpen = (bool)newValue;

                control.RightIconImage.RotateTo(isOpen ? 180 : 0, length: 150);
                if (control.ExtendedContent != null)
                {
                    var animate = new Animation(
                        callback: d => control.ExpanderContainer.HeightRequest = d,
                        start: isOpen ? 0 : control._originalContentHeight,
                        end: isOpen ? control._originalContentHeight : 0);
                    animate.Commit(
                        owner: control.ExpanderContainer,
                        name: "ExpanderAnimation",
                        length: control.Animated ? 150u : 0u);
                }
            }
        }

        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(IsOpenProperty);
            }
            set
            {
                SetValue(IsOpenProperty, value);
            }
        }
        #endregion

        #region Animated
        public static readonly BindableProperty AnimatedProperty = BindableProperty.Create(
            propertyName: nameof(Animated),
            returnType: typeof(bool),
            declaringType: typeof(Expander),
            defaultValue: true);

        public bool Animated
        {
            get
            {
                return (bool)GetValue(AnimatedProperty);
            }
            set
            {
                SetValue(AnimatedProperty, value);
            }
        }
        #endregion

        #region SeparatorColor
        public static readonly BindableProperty SeparatorColorProperty = BindableProperty.Create(
            propertyName: nameof(SeparatorColor),
            returnType: typeof(Color),
            declaringType: typeof(Expander),
            defaultValue: Color.DarkGray);

        public Color SeparatorColor
        {
            get
            {
                return (Color)GetValue(SeparatorColorProperty);
            }
            set
            {
                SetValue(SeparatorColorProperty, value);
            }
        }
        #endregion

        #region SeparatorHeight
        public static readonly BindableProperty SeparatorHeightProperty = BindableProperty.Create(
            propertyName: nameof(SeparatorHeight),
            returnType: typeof(double),
            declaringType: typeof(Expander),
            defaultValue: 1.0);

        public double SeparatorHeight
        {
            get
            {
                return (double)GetValue(SeparatorHeightProperty);
            }
            set
            {
                SetValue(SeparatorHeightProperty, value);
            }
        }
        #endregion

        #region HasSeparator
        public static readonly BindableProperty HasSeparatorProperty = BindableProperty.Create(
            propertyName: nameof(HasSeparator),
            returnType: typeof(bool),
            declaringType: typeof(Expander),
            defaultValue: true);

        public bool HasSeparator
        {
            get
            {
                return (bool)GetValue(HasSeparatorProperty);
            }
            set
            {
                SetValue(HasSeparatorProperty, value);
            }
        }
        #endregion

        private void Expander_Clicked(object sender, EventArgs e)
        {
            IsOpen = !IsOpen;
        }
    }
}