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

[assembly: ExportRenderer(typeof(ISCheckbox), typeof(CheckboxRenderer))]
namespace Sample.UWP.Renderers
{
    public class CheckboxRenderer : ViewRenderer<ISCheckbox, CheckBox>
    {
        private TextBlock _contentText;

        protected override void OnElementChanged(ElementChangedEventArgs<ISCheckbox> e)
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
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(nameof(ISCheckbox.IsChecked)))
            {
                Control.IsChecked = Element.IsChecked;
            }
            else if (e.PropertyName.Equals(nameof(ISCheckbox.AccentColor)))
            {
                UpdateAccentColor(Control as CheckBox);
            }
            else if (e.PropertyName.Equals(nameof(ISCheckbox.Text)))
            {
                Control.Content = Element.Text;
            }
            else if (e.PropertyName.Equals(nameof(ISCheckbox.TextColor)))
            {
                UpdateTextColor();
            }
        }

        private void UpdateAccentColor(CheckBox checkBox) => checkBox.Foreground = new SolidColorBrush(Element.AccentColor.ToUwp());

        private void UpdateTextColor() => _contentText.Foreground = new SolidColorBrush(Element.TextColor.ToUwp());
    }
}
