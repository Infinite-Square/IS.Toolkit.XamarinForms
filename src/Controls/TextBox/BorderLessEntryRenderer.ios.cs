#if IOS
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(IS.Toolkit.XamarinForms.Controls.BorderLessEntry), typeof(IS.Toolkit.XamarinForms.Controls.BorderlessEntryRenderer))]
namespace IS.Toolkit.XamarinForms.Controls
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}
#endif