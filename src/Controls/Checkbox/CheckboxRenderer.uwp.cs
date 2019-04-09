#if UWP
using IS.Toolkit.XamarinForms.Controls;
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
namespace IS.Toolkit.XamarinForms.Controls
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
                    checkBox.Unchecked += CheckBox_Checked;
                    checkBox.Content = _contentText;
                    SetNativeControl(checkBox);
                }
            }
        }

        private void CheckBox_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Element.IsChecked = (bool)Control.IsChecked;
            Element.InvokeCheckChanged((bool)Control.IsChecked);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(nameof(CheckBox.IsChecked)))
            {
                Control.IsChecked = Element.IsChecked;
            }
        }
    }
}
#endif
