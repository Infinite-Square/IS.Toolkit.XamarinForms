using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IS.Toolkit.XamarinForms.Controls;
using Sample.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ISCheckbox), typeof(CheckboxRenderer))]
namespace Sample.Droid.Renderers
{
    public class CheckboxRenderer : ViewRenderer<ISCheckbox, CheckBox>
    {
        private ISCheckbox RenderedCheckBox => Element as ISCheckbox;

        public CheckboxRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ISCheckbox> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    UpdateAccentColor();
                }

                Control.Checked = RenderedCheckBox.IsChecked;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(nameof(ISCheckbox.IsChecked)))
            {
                Control.Checked = Element.IsChecked;
            }
            else if (e.PropertyName.Equals(nameof(ISCheckbox.AccentColor)))
            {
                UpdateAccentColor();
            }
        }

        private void UpdateAccentColor()
        {
            Control?.Background?.SetColorFilter(RenderedCheckBox.AccentColor.ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcAtop);
        }
    }
}