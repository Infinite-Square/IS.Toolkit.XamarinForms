using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using IS.Toolkit.XamarinForms.Controls;
using Sample.IOS.CustomControl;
using Sample.IOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CheckBox), typeof(CheckBoxRenderer))]
namespace Sample.IOS.Renderers
{
    public class CheckBoxRenderer : ViewRenderer<CheckBox, UICheckBox>
    {
        internal const float DefaultSize = 30.0f;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control.CheckedChanged -= OnElementCheckedChanged;
            }

            base.Dispose(disposing);
        }

        public override CGSize SizeThatFits(CGSize size)
        {
            var result = base.SizeThatFits(size);

            var height = result.Height;
            var width = result.Width;

            if (height < DefaultSize)
            {
                height = DefaultSize;
            }

            if (width < DefaultSize)
            {
                width = DefaultSize;
            }

            var final = (nfloat)Math.Min(width, height);
            result.Width = final;
            result.Height = final;

            return result;
        }

        public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            var sizeConstraint = base.GetDesiredSize(widthConstraint, heightConstraint);

            var set = false;

            var width = widthConstraint;
            var height = heightConstraint;
            if (sizeConstraint.Request.Width == 0)
            {
                if (widthConstraint <= 0 || double.IsInfinity(widthConstraint))
                {
                    width = DefaultSize;
                    set = true;
                }
            }

            if (sizeConstraint.Request.Height == 0)
            {
                if (heightConstraint <= 0 || double.IsInfinity(heightConstraint))
                {
                    height = DefaultSize;
                    set = true;
                }
            }

            if (set)
            {
                sizeConstraint = new SizeRequest(new Size(width, height), new Size(DefaultSize, DefaultSize));
            }

            return sizeConstraint;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CheckBox> e)
        {
            if (e.OldElement != null)
            {
                e.OldElement.CheckedChanged -= OnElementCheckedChanged;
            }

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new UICheckBox());
                }

                Control.IsVisible = Element.IsVisible;
                Control.IsChecked = Element.IsChecked;
                Control.IsEnabled = Element.IsEnabled;

                Control.CheckedChanged += OnElementCheckedChanged;
                UpdateCheckedColor();
                UpdateUncheckedColor();
            }

            base.OnElementChanged(e);
        }

        private void UpdateCheckedColor()
        {
            if (Element == null)
            {
                return;
            }

            Control.CheckedColor = Element.CheckedColor;
        }

        private void UpdateUncheckedColor()
        {
            if (Element == null)
            {
                return;
            }

            Control.UncheckedColor = Element.UncheckedColor;
        }

        private void OnElementCheckedChanged(object sender, EventArgs e)
        {
            ((IElementController)Element).SetValueFromRenderer(CheckBox.IsCheckedProperty, Control.IsChecked);
        }

        private void OnElementChecked(object sender, EventArgs e)
        {
            ////Element.IsChecked = e;
            Element.InvokeCheckChanged(Element.IsChecked);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CheckBox.CheckedColorProperty.PropertyName)
            {
                UpdateCheckedColor();
            }
            else if (e.PropertyName == CheckBox.UncheckedColorProperty.PropertyName)
            {
                UpdateUncheckedColor();
            }
            else if (e.PropertyName == CheckBox.IsEnabledProperty.PropertyName)
            {
                Control.IsEnabled = Element.IsEnabled;
            }
            else if (e.PropertyName == CheckBox.IsCheckedProperty.PropertyName)
            {
                Control.IsChecked = Element.IsChecked;
            }
            else if (e.PropertyName == CheckBox.IsVisibleProperty.PropertyName)
            {
                Control.IsVisible = Element.IsVisible;
            }
        }
    }
}