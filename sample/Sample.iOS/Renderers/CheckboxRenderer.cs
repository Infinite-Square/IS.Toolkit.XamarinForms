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

[assembly: ExportRenderer(typeof(ISCheckbox), typeof(CheckboxRenderer))]
namespace Sample.IOS.Renderers
{
    public class CheckboxRenderer : ViewRenderer<ISCheckbox, UISwitch>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ISCheckbox> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    UISwitch switchControl = new UISwitch();
                    switchControl.On = Element.IsChecked;
                    switchControl.ValueChanged += CheckBox_AllTouchEvents;
                    SetNativeControl(switchControl);
                }
            }
        }

        private void CheckBox_AllTouchEvents(object sender, EventArgs e)
        {
            Element.IsChecked = Control.On;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(nameof(ISCheckbox.IsChecked)))
            {
                Control.On = Element.IsChecked;
            }
            else if (e.PropertyName.Equals(nameof(ISCheckbox.AccentColor)))
            {
            }
            else if (e.PropertyName.Equals(nameof(ISCheckbox.Text)))
            {
            }
            else if (e.PropertyName.Equals(nameof(ISCheckbox.TextColor)))
            {
            }
        }
    }
}