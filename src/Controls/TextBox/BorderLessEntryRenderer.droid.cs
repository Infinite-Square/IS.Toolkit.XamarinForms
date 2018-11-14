#if ANDROID
using Android.Content;
using Android.Graphics.Drawables;
using IS.Toolkit.XamarinForms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderLessEntry), typeof(BorderLessEntryRenderer))]
namespace IS.Toolkit.XamarinForms.Controls
{
    public class BorderLessEntryRenderer : EntryRenderer
    {
        public BorderLessEntryRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}
#endif