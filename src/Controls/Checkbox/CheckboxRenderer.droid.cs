#if ANDROID
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using IS.Toolkit.XamarinForms;
using IS.Toolkit.XamarinForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(IS.Toolkit.XamarinForms.Controls.CheckBox), typeof(CheckboxRenderer))]
namespace IS.Toolkit.XamarinForms.Controls
{
    public class CheckboxRenderer : ViewRenderer<IS.Toolkit.XamarinForms.Controls.CheckBox, AppCompatCheckBox>,
        CompoundButton.IOnCheckedChangeListener
    {
        private static bool? _isLollipopOrNewer;
        public static bool IsLollipopOrNewer
        {
            get
            {
                if (!_isLollipopOrNewer.HasValue)
                {
                    _isLollipopOrNewer = (int)Build.VERSION.SdkInt >= 21;
                }

                return _isLollipopOrNewer.Value;
            }
        }

        public CheckboxRenderer(Context context)
            : base(context)
        {
        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            ((IElementController)Control).SetValueFromRenderer(IS.Toolkit.XamarinForms.Controls.CheckBox.IsCheckedProperty, isChecked);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == IS.Toolkit.XamarinForms.Controls.CheckBox.CheckedColorProperty.PropertyName ||
                e.PropertyName == IS.Toolkit.XamarinForms.Controls.CheckBox.UncheckedColorProperty.PropertyName)
            {
                UpdateOnColor();
            }
            else if (e.PropertyName == IS.Toolkit.XamarinForms.Controls.CheckBox.IsCheckedProperty.PropertyName)
            {
                Control.Checked = Element.IsChecked;
            }

            base.OnElementPropertyChanged(sender, e);
        }

        private void UpdateOnColor()
        {
            if ((Element as IS.Toolkit.XamarinForms.Controls.CheckBox) == null || Control == null)
            {
                return;
            }

            var mode = PorterDuff.Mode.SrcIn;

            var stateChecked = global::Android.Resource.Attribute.StateChecked;
            var stateEnabled = global::Android.Resource.Attribute.StateEnabled;

            var uncheckedDefault = Android.Graphics.Color.Gray;
            var disabledColor = Android.Graphics.Color.LightGray;

            var list = new ColorStateList(
                    new int[][]
                    {
                        new int[] { -stateEnabled, stateChecked },
                        new int[] { stateEnabled, stateChecked },
                        new int[] { stateEnabled, -stateChecked },
                        new int[] { },
                    },
                    new int[]
                    {
                        disabledColor,
                        Element.CheckedColor == Xamarin.Forms.Color.Default ? Xamarin.Forms.Color.Accent.ToAndroid() : Element.CheckedColor.ToAndroid(),
                        Element.UncheckedColor == Xamarin.Forms.Color.Default ? uncheckedDefault : Element.UncheckedColor.ToAndroid(),
                        disabledColor,
                    });

            if (IsLollipopOrNewer)
            {
                Control.ButtonTintList = list;
                Control.ButtonTintMode = mode;
            }
            else
            {
                CompoundButtonCompat.SetButtonTintList(Control, list);
                CompoundButtonCompat.SetButtonTintMode(Control, mode);
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<IS.Toolkit.XamarinForms.Controls.CheckBox> e)
        {
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new AppCompatCheckBox(Context));
                }

                Control.CheckedChange += Control_CheckedChange;
                Control.Checked = Element.IsChecked;
                Control.Enabled = Element.IsEnabled;
                UpdateOnColor();
            }

            base.OnElementChanged(e);
        }

        private void Control_CheckedChange(object sender, Android.Widget.CompoundButton.CheckedChangeEventArgs e)
        {
            Element.InvokeCheckChanged(e.IsChecked);
            Element.IsChecked = e.IsChecked;
        }
    }
}
#endif