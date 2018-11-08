using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS.Toolkit.XamarinForms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Picker : ContentView
    {
        public event EventHandler<object> SelectedItemChanged;

        private void RaiseSelectedItemChanged(object value)
        {
            SelectedItemChanged?.Invoke(this, value);
        }

        public Picker()
        {
            InitializeComponent();

            var tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) => { _picker.Focus(); };
            _grid.GestureRecognizers.Add(tap);
        }

        #region Title

            #region Title
            public static readonly BindableProperty TitleProperty = BindableProperty.Create(
                    propertyName: nameof(Title),
                    returnType: typeof(string),
                    declaringType: typeof(Picker),
                    defaultValue: default(string),
                    propertyChanged: TitlePropertyChanged);

            private static void TitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
            {
                if (bindable != null)
                {
                    var control = (Picker)bindable;
                    control.OnPropertyChanged(nameof(control.HasTitle));
                }
            }

            public string Title
            {
                get
                {
                    return (string)GetValue(TitleProperty);
                }
                set
                {
                    SetValue(TitleProperty, value);
                }
            }

            public bool HasTitle => !string.IsNullOrEmpty(Title);
            #endregion

            #region TitleTextColor
            public static readonly BindableProperty TitleTextColorProperty = BindableProperty.Create(
                propertyName: nameof(TitleTextColor),
                returnType: typeof(Color),
                declaringType: typeof(Picker),
                defaultValue: Label.TextColorProperty.DefaultValue);

            public Color TitleTextColor
            {
                get
                {
                    return (Color)GetValue(TitleTextColorProperty);
                }
                set
                {
                    SetValue(TitleTextColorProperty, value);
                }
            }
            #endregion

            #region TitleFontSize
            public static readonly BindableProperty TitleFontSizeProperty = BindableProperty.Create(
                propertyName: nameof(TitleFontSize),
                returnType: typeof(double),
                declaringType: typeof(Picker),
                defaultValue: Label.FontSizeProperty.DefaultValue);

            public double TitleFontSize
            {
                get
                {
                    return (double)GetValue(TitleFontSizeProperty);
                }
                set
                {
                    SetValue(TitleFontSizeProperty, value);
                }
            }
            #endregion

            #region TitleFontFamily
            public static readonly BindableProperty TitleFontFamilyProperty = BindableProperty.Create(
                propertyName: nameof(TitleFontFamily),
                returnType: typeof(string),
                declaringType: typeof(Picker),
                defaultValue: Label.FontFamilyProperty.DefaultValue);

            public string TitleFontFamily
            {
                get
                {
                    return (string)GetValue(TitleFontFamilyProperty);
                }
                set
                {
                    SetValue(TitleFontFamilyProperty, value);
                }
            }
            #endregion

            #region TitleFontAttributes
        public static readonly BindableProperty TitleFontAttributesProperty = BindableProperty.Create(
            propertyName: nameof(TitleFontAttributes),
            returnType: typeof(FontAttributes),
            declaringType: typeof(Picker),
            defaultValue: Label.FontAttributesProperty.DefaultValue);

        public FontAttributes TitleFontAttributes
        {
            get
            {
                return (FontAttributes)GetValue(TitleFontAttributesProperty);
            }
            set
            {
                SetValue(TitleFontAttributesProperty, value);
            }
        }
        #endregion

        #endregion

        #region BorderColor
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
            propertyName: nameof(BorderColor),
            returnType: typeof(Color),
            declaringType: typeof(Picker),
            defaultValue: Color.Gray);

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

        #region CornerRadius
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            propertyName: nameof(CornerRadius),
            returnType: typeof(double),
            declaringType: typeof(Picker),
            defaultValue: 3.0);

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

        #region BackgroundColor
        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
            propertyName: nameof(BackgroundColor),
            returnType: typeof(Color),
            declaringType: typeof(Picker),
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

        #region SelectedValue

            #region SelectedItem
            public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
                propertyName: nameof(SelectedItem),
                returnType: typeof(AvailableValue),
                declaringType: typeof(Picker),
                defaultValue: default(AvailableValue),
                    propertyChanged: SelectedItemPropertyChanged);

            private static void SelectedItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
            {
                if (bindable != null)
                {
                    var control = (Picker)bindable;
                    control.OnPropertyChanged(nameof(control.SelectedText));
                    control.OnPropertyChanged(nameof(control.ShowPlaceholder));
                    control.OnPropertyChanged(nameof(control.IsClearableAndHasValue));
                }
            }

            public AvailableValue SelectedItem
            {
                get
                {
                    return (AvailableValue)GetValue(SelectedItemProperty);
                }
                set
                {
                    SetValue(SelectedItemProperty, value);
                }
            }
            #endregion

            #region SelectedValue
            public string SelectedText => SelectedItem?.Label;
            #endregion

            #region TextColor
            public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
                propertyName: nameof(TextColor),
                returnType: typeof(Color),
                declaringType: typeof(Picker),
                defaultValue: Label.TextColorProperty.DefaultValue);

            public Color TextColor
            {
                get
                {
                    return (Color)GetValue(TextColorProperty);
                }
                set
                {
                    SetValue(TextColorProperty, value);
                }
            }
            #endregion

            #region FontSize
            public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
                propertyName: nameof(FontSize),
                returnType: typeof(double),
                declaringType: typeof(Picker),
                defaultValue: Label.FontSizeProperty.DefaultValue);

            public double FontSize
            {
                get
                {
                    return (double)GetValue(FontSizeProperty);
                }
                set
                {
                    SetValue(FontSizeProperty, value);
                }
            }
            #endregion

            #region FontFamily
            public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
                propertyName: nameof(FontFamily),
                returnType: typeof(string),
                declaringType: typeof(Picker),
                defaultValue: Label.FontFamilyProperty.DefaultValue);

            public string FontFamily
            {
                get
                {
                    return (string)GetValue(FontFamilyProperty);
                }
                set
                {
                    SetValue(FontFamilyProperty, value);
                }
            }
            #endregion

            #region FontAttributes
            public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(
                propertyName: nameof(FontAttributes),
                returnType: typeof(FontAttributes),
                declaringType: typeof(Picker),
                defaultValue: Label.FontAttributesProperty.DefaultValue);

            public FontAttributes FontAttributes
            {
                get
                {
                    return (FontAttributes)GetValue(FontAttributesProperty);
                }
                set
                {
                    SetValue(FontAttributesProperty, value);
                }
            }
            #endregion

        #endregion

        #region Placeholder

            #region Placeholder
            public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
                propertyName: nameof(Placeholder),
                returnType: typeof(string),
                declaringType: typeof(Picker),
                defaultValue: default(string));

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

            public bool ShowPlaceholder => string.IsNullOrEmpty(SelectedText);
            #endregion

            #region PlaceholderTextColor
        public static readonly BindableProperty PlaceholderTextColorProperty = BindableProperty.Create(
                propertyName: nameof(PlaceholderTextColor),
                returnType: typeof(Color),
                declaringType: typeof(Picker),
                defaultValue: Entry.PlaceholderColorProperty.DefaultValue);

            public Color PlaceholderTextColor
            {
                get
                {
                    return (Color)GetValue(PlaceholderTextColorProperty);
                }
                set
                {
                    SetValue(PlaceholderTextColorProperty, value);
                }
            }
            #endregion

            #region PlaceholderFontSize
            public static readonly BindableProperty PlaceholderFontSizeProperty = BindableProperty.Create(
                propertyName: nameof(PlaceholderFontSize),
                returnType: typeof(double),
                declaringType: typeof(Picker),
                defaultValue: Label.FontSizeProperty.DefaultValue);

            public double PlaceholderFontSize
            {
                get
                {
                    return (double)GetValue(PlaceholderFontSizeProperty);
                }
                set
                {
                    SetValue(PlaceholderFontSizeProperty, value);
                }
            }
            #endregion

            #region PlaceholderFontFamily
            public static readonly BindableProperty PlaceholderFontFamilyProperty = BindableProperty.Create(
                propertyName: nameof(PlaceholderFontFamily),
                returnType: typeof(string),
                declaringType: typeof(Picker),
                defaultValue: Label.FontFamilyProperty.DefaultValue);

            public string PlaceholderFontFamily
            {
                get
                {
                    return (string)GetValue(PlaceholderFontFamilyProperty);
                }
                set
                {
                    SetValue(PlaceholderFontFamilyProperty, value);
                }
            }
            #endregion

            #region PlaceholderFontAttributes
        public static readonly BindableProperty PlaceholderFontAttributesProperty = BindableProperty.Create(
            propertyName: nameof(PlaceholderFontAttributes),
            returnType: typeof(FontAttributes),
            declaringType: typeof(Picker),
            defaultValue: Label.FontAttributesProperty.DefaultValue);

        public FontAttributes PlaceholderFontAttributes
        {
            get
            {
                return (FontAttributes)GetValue(PlaceholderFontAttributesProperty);
            }
            set
            {
                SetValue(PlaceholderFontAttributesProperty, value);
            }
        }
        #endregion

        #endregion

        #region IconSource
        public static readonly BindableProperty IconSourceProperty = BindableProperty.Create(
            propertyName: nameof(IconSource),
            returnType: typeof(ImageSource),
            declaringType: typeof(Picker),
            defaultValue: ImageSource.FromResource("IS.Toolkit.XamarinForms.Controls.Expander.dropdown.png", typeof(Expander).GetTypeInfo().Assembly));

        public ImageSource IconSource
        {
            get
            {
                return (ImageSource)GetValue(IconSourceProperty);
            }
            set
            {
                SetValue(IconSourceProperty, value);
            }
        }
        #endregion

        #region ClearIcon
        public ImageSource ClearIconSource => ImageSource.FromResource("IS.Toolkit.XamarinForms.Controls.Picker.close.png", typeof(Picker).GetTypeInfo().Assembly);
        #endregion

        #region IsClearable
        public static readonly BindableProperty IsClearableProperty = BindableProperty.Create(
            propertyName: nameof(IsClearable),
            returnType: typeof(bool),
            declaringType: typeof(Picker),
            defaultValue: default(bool),
                propertyChanged: IsClearablePropertyChanged);

        private static void IsClearablePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null)
            {
                var control = (Picker)bindable;
                control.OnPropertyChanged(nameof(control.IsClearableAndHasValue));
            }
        }

        public bool IsClearable
        {
            get
            {
                return (bool)GetValue(IsClearableProperty);
            }
            set
            {
                SetValue(IsClearableProperty, value);
            }
        }

        public bool IsClearableAndHasValue => IsClearable && !ShowPlaceholder;
        #endregion

        #region ItemsSource
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            propertyName: nameof(ItemsSource),
            returnType: typeof(IList<AvailableValue>),
            declaringType: typeof(Picker),
            defaultValue: default(IList<AvailableValue>));

        public IList<AvailableValue> ItemsSource
        {
            get
            {
                return (IList<AvailableValue>)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }
        #endregion

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedItem = (sender as Xamarin.Forms.Picker).SelectedItem as AvailableValue;
            RaiseSelectedItemChanged(SelectedItem.Value);
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            SelectedItem = null;
            RaiseSelectedItemChanged(null);
        }
    }
}