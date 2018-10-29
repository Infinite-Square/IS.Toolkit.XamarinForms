#if UWP
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(IS.Toolkit.XamarinForms.Controls.BorderLessEntry), typeof(IS.Toolkit.XamarinForms.Controls.BorderLessEntryRenderer))]
namespace IS.Toolkit.XamarinForms.Controls
{
    public class BorderLessEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.BorderThickness = new Windows.UI.Xaml.Thickness(0);
            }
        }
    }
}
#endif