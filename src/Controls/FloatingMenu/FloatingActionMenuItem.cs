using System.Windows.Input;
using Xamarin.Forms;

namespace IS.Toolkit.XamarinForms.Controls
{
    public class FloatingActionMenuItem : BindableObject
    {
        #region Color
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            propertyName: nameof(Color),
            returnType: typeof(Color),
            declaringType: typeof(FloatingActionMenuItem),
            defaultValue: Xamarin.Forms.Color.Accent);

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
        #endregion

        #region BackgroundColor
        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
            propertyName: nameof(BackgroundColor),
            returnType: typeof(Color),
            declaringType: typeof(FloatingActionMenuItem),
            defaultValue: Xamarin.Forms.Color.Transparent);

        public Color BackgroundColor
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

        #region Label
        public static readonly BindableProperty LabelProperty = BindableProperty.Create(
            propertyName: nameof(Label),
            returnType: typeof(string),
            declaringType: typeof(FloatingActionMenuItem),
            defaultValue: default(string));

        public string Label
        {
            get
            {
                return (string)GetValue(LabelProperty);
            }
            set
            {
                SetValue(LabelProperty, value);
            }
        }
        #endregion

        #region Icon
        public static readonly BindableProperty IconProperty = BindableProperty.Create(
            propertyName: nameof(Icon),
            returnType: typeof(ImageSource),
            declaringType: typeof(FloatingActionMenuItem),
            defaultValue: default(ImageSource));

        public ImageSource Icon
        {
            get
            {
                return (ImageSource)GetValue(IconProperty);
            }
            set
            {
                SetValue(IconProperty, value);
            }
        }
        #endregion

        #region Command
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            propertyName: nameof(Command),
            returnType: typeof(ICommand),
            declaringType: typeof(FloatingActionMenuItem),
            defaultValue: default(ICommand));

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
        #endregion
    }
}
