using IS.Toolkit.XamarinForms.Controls;
using Sample.UWP.Renderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(IS.Toolkit.XamarinForms.Controls.CheckBox), typeof(CheckboxRenderer))]
namespace Sample.UWP.Renderers
{
    public class CheckboxRenderer : ViewRenderer<IS.Toolkit.XamarinForms.Controls.CheckBox, Windows.UI.Xaml.Controls.CheckBox>
    {
        private TextBlock _contentText;

        protected override void OnElementChanged(ElementChangedEventArgs<IS.Toolkit.XamarinForms.Controls.CheckBox> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    Windows.UI.Xaml.Controls.CheckBox checkBox = new Windows.UI.Xaml.Controls.CheckBox();
                    _contentText = new TextBlock();

                    checkBox.IsChecked = Element.IsChecked;
                    checkBox.Checked += CheckBox_Checked;
                    checkBox.Unchecked += CheckBox_Unchecked;
                    _contentText.FontSize = Element.FontSize;
                    _contentText.Text = Element.Text;
                    UpdateTextColor();
                    checkBox.Content = _contentText;
                    SetNativeControl(checkBox);
                    UpdateAccentColor(checkBox);
                }
            }
        }

        private void CheckBox_Unchecked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Element.IsChecked = (bool)Control.IsChecked;
            Element.InvokeCheckChanged((bool)Control.IsChecked);
            Element.CheckedCommand?.Execute(Element.CheckedCommandArguement);
        }

        private void CheckBox_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Element.IsChecked = (bool)Control.IsChecked;
            Element.InvokeCheckChanged((bool)Control.IsChecked);
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
                UpdateAccentColor(Control as Windows.UI.Xaml.Controls.CheckBox);
            }
            else if (e.PropertyName.Equals(nameof(IS.Toolkit.XamarinForms.Controls.CheckBox.Text)))
            {
                Control.Content = Element.Text;
            }
            else if (e.PropertyName.Equals(nameof(IS.Toolkit.XamarinForms.Controls.CheckBox.TextColor)))
            {
                UpdateTextColor();
            }
            else if (e.PropertyName.Equals(nameof(Element.FontSize)))
            {
                _contentText.FontSize = Element.FontSize;
            }
        }

        private void UpdateAccentColor(Windows.UI.Xaml.Controls.CheckBox checkBox) => checkBox.Foreground = new SolidColorBrush(Element.AccentColor.ToUwp());

        private void UpdateTextColor() => _contentText.Foreground = new SolidColorBrush(Element.TextColor.ToUwp());
    }
}
