using Xamarin.Forms;

// From : https://github.com/andreinitescu/XFItemsControl/blob/master/XFItemsControl/XFItemsControl/ItemsControl.cs
namespace IS.Toolkit.XamarinForms.Controls
{
    internal static class LayoutExtension
    {
        public static readonly BindableProperty IsItemsHostProperty =
            BindableProperty.CreateAttached("IsItemsHost", typeof(bool), typeof(Layout<View>), false);

        public static bool GetIsItemsHost(this Layout<View> layout)
        {
            return (bool)layout.GetValue(IsItemsHostProperty);
        }

        public static void SetIsItemsHost(this Layout<View> layout, bool value)
        {
            layout.SetValue(IsItemsHostProperty, value);
        }
    }
}
