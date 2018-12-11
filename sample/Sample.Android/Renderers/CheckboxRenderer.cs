using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using IS.Toolkit.XamarinForms.Controls;
using Sample.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(IS.Toolkit.XamarinForms.Controls.CheckBox), typeof(CheckboxRenderer))]
namespace Sample.Droid.Renderers
{
    public class CheckboxRenderer : ViewRenderer<IS.Toolkit.XamarinForms.Controls.CheckBox, AppCompatCheckBox>
    {
        private IS.Toolkit.XamarinForms.Controls.CheckBox RenderedCheckBox => Element as IS.Toolkit.XamarinForms.Controls.CheckBox;

        public CheckboxRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<IS.Toolkit.XamarinForms.Controls.CheckBox> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    AppCompatCheckBox checkBox = new AppCompatCheckBox(Context);
                    checkBox.Checked = RenderedCheckBox.IsChecked;
                    checkBox.CheckedChange += CheckBox_CheckedChange;
                    checkBox.TextSize = Element.FontSize;
                    checkBox.BackgroundTintList = UpdateAccentColor(Element.AccentColor);
                    checkBox.Text = RenderedCheckBox.Text;
                    SetNativeControl(checkBox);
                    UpdateTextColor(checkBox);
                }
            }
        }

        private void CheckBox_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Element.IsChecked = e.IsChecked;
            Element.InvokeCheckChanged(e.IsChecked);
            Element.CheckedCommand?.Execute(Element.CheckedCommandArguement);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(nameof(IS.Toolkit.XamarinForms.Controls.CheckBox.IsChecked)))
            {
                Control.Checked = Element.IsChecked;
            }
            else if (e.PropertyName.Equals(nameof(IS.Toolkit.XamarinForms.Controls.CheckBox.AccentColor)))
            {
                Control.BackgroundTintList = UpdateAccentColor(Element.AccentColor);
            }
            else if (e.PropertyName.Equals(nameof(IS.Toolkit.XamarinForms.Controls.CheckBox.Text)))
            {
                Control.Text = RenderedCheckBox.Text;
            }
            else if (e.PropertyName.Equals(nameof(IS.Toolkit.XamarinForms.Controls.CheckBox.TextColor)))
            {
                UpdateTextColor(Control as AppCompatCheckBox);
            }
            else if (e.PropertyName.Equals(nameof(Element.FontSize)))
            {
                Control.TextSize = Element.FontSize;
            }
        }

        private ColorStateList UpdateAccentColor(Color color)
        {
            return new ColorStateList(
                new[]
                {
                    new[] { -global::Android.Resource.Attribute.StateEnabled },
                    new[] { -global::Android.Resource.Attribute.StateChecked },
                    new[] { global::Android.Resource.Attribute.StateChecked }
                },
                new int[]
                {
                    color.WithSaturation(0.1).ToAndroid(),
                    color.ToAndroid(),
                    color.ToAndroid()
                });
        }

        private void UpdateTextColor(AppCompatCheckBox checkBox)
        {
            checkBox?.SetTextColor(RenderedCheckBox.TextColor.ToAndroid());
        }
    }
}