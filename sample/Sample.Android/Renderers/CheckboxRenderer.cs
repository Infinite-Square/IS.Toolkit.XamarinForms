using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using IS.Toolkit.XamarinForms.Controls;
using Sample.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ISCheckbox), typeof(CheckboxRenderer))]
namespace Sample.Droid.Renderers
{
    public class CheckboxRenderer : ViewRenderer<ISCheckbox, AppCompatCheckBox>
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
                    AppCompatCheckBox checkBox = new AppCompatCheckBox(Context);
                    checkBox.Checked = RenderedCheckBox.IsChecked;
                    checkBox.CheckedChange += CheckBox_CheckedChange;
                    checkBox.Text = RenderedCheckBox.Text;
                    SetNativeControl(checkBox);
                    UpdateTextColor(checkBox);
                    UpdateAccentColor(checkBox);
                }
            }
        }

        private void CheckBox_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Element.IsChecked = e.IsChecked;
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
                UpdateAccentColor(Control as AppCompatCheckBox);
            }
            else if (e.PropertyName.Equals(nameof(ISCheckbox.Text)))
            {
                Control.Text = RenderedCheckBox.Text;
            }
            else if (e.PropertyName.Equals(nameof(ISCheckbox.TextColor)))
            {
                UpdateTextColor(Control as AppCompatCheckBox);
            }
        }

        private void UpdateAccentColor(AppCompatCheckBox checkBox)
        {
            checkBox?.SetBackgroundColor(RenderedCheckBox.AccentColor.ToAndroid());
        }

        private void UpdateTextColor(AppCompatCheckBox checkBox)
        {
            checkBox?.SetTextColor(RenderedCheckBox.TextColor.ToAndroid());
        }
    }
}