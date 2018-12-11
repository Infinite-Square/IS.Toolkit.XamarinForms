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

[assembly: ExportRenderer(typeof(Checkbox), typeof(CheckboxRenderer))]
namespace Sample.UWP.Renderers
{
    public class CheckboxRenderer : ViewRenderer<Checkbox, CheckBox>
    {
        private TextBlock _contentText;

        protected override void OnElementChanged(ElementChangedEventArgs<Checkbox> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    CheckBox checkBox = new CheckBox();
                    _contentText = new TextBlock();

                    checkBox.IsChecked = Element.IsChecked;
                    checkBox.Checked += CheckBox_Checked;
                    _contentText.Text = Element.Text;
                    UpdateTextColor();
                    checkBox.Content = _contentText;
                    SetNativeControl(checkBox);
                    UpdateAccentColor(checkBox);
                }
            }
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

            if (e.PropertyName.Equals(nameof(Checkbox.IsChecked)))
            {
                Control.IsChecked = Element.IsChecked;
            }
            else if (e.PropertyName.Equals(nameof(Checkbox.AccentColor)))
            {
                UpdateAccentColor(Control as CheckBox);
            }
            else if (e.PropertyName.Equals(nameof(Checkbox.Text)))
            {
                Control.Content = Element.Text;
            }
            else if (e.PropertyName.Equals(nameof(Checkbox.TextColor)))
            {
                UpdateTextColor();
            }
        }

        private void UpdateAccentColor(CheckBox checkBox) => checkBox.Foreground = new SolidColorBrush(Element.AccentColor.ToUwp());

        private void UpdateTextColor() => _contentText.Foreground = new SolidColorBrush(Element.TextColor.ToUwp());
    }
}
