using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Foundation;
using IS.Toolkit.XamarinForms.Controls;
using Sample.IOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CheckBox), typeof(CheckboxRenderer))]
namespace Sample.IOS.Renderers
{
    public class CheckboxRenderer : ViewRenderer<CheckBox, UICheckBox>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CheckBox> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    UICheckBox uICheckBox = new UICheckBox();
                    uICheckBox.TouchUpInside += UICheckBox_TouchUpInside;
                    uICheckBox.TextColor = e.NewElement.TextColor.ToUIColor();
                    uICheckBox.Text = e.NewElement.Text;
                    uICheckBox.IsChecked = e.NewElement.IsChecked;
                    uICheckBox.AccentColor = e.NewElement.AccentColor.ToUIColor();
                    uICheckBox.FontSize = e.NewElement.FontSize;
                    SetNativeControl(uICheckBox);
                }
            }
        }

        private void UICheckBox_TouchUpInside(object sender, EventArgs e)
        {
            Element.IsChecked = Control.IsChecked;
            Element.InvokeCheckChanged(Control.IsChecked);
            Element.CheckedCommand?.Execute(Element.CheckedCommandArguement);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(nameof(IS.Toolkit.XamarinForms.Controls.CheckBox.IsChecked)))
            {
                Control.IsChecked = Element.IsChecked;
            }
            else if (e.PropertyName.Equals(nameof(IS.Toolkit.XamarinForms.Controls.CheckBox.AccentColor)))
            {
                Control.AccentColor = Element.AccentColor.ToUIColor();
            }
            else if (e.PropertyName.Equals(nameof(IS.Toolkit.XamarinForms.Controls.CheckBox.Text)))
            {
                Control.Text = Element.Text;
            }
            else if (e.PropertyName.Equals(nameof(IS.Toolkit.XamarinForms.Controls.CheckBox.TextColor)))
            {
                Control.TextColor = Element.TextColor.ToUIColor();
            }
            else if (e.PropertyName.Equals(nameof(Element.FontSize)))
            {
                Control.FontSize = Element.FontSize;
            }
        }
    }
}