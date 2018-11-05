using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Xamarin.Forms;

namespace IS.Toolkit.XamarinForms.Controls
{
    public class Checkbox : ContentView
    {
        public Checkbox()
        {
            Content = new Image()
            {
                Source = CheckboxEmptyIcon,
                Aspect = Aspect.AspectFit
            };

            var tapped = new TapGestureRecognizer();
            tapped.Tapped += Tapped_Tapped;
            GestureRecognizers.Add(tapped);
        }

        private void Tapped_Tapped(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;
        }

        #region CheckboxIcon
        public static readonly BindableProperty CheckboxIconProperty = BindableProperty.Create(
            propertyName: nameof(CheckboxIcon),
            returnType: typeof(ImageSource),
            declaringType: typeof(Checkbox),
            defaultValue: ImageSource.FromResource("IS.Toolkit.XamarinForms.Controls.Checkbox.checkbox_fill.png", typeof(Expander).GetTypeInfo().Assembly));

        public ImageSource CheckboxIcon
        {
            get
            {
                return (ImageSource)GetValue(CheckboxIconProperty);
            }
            set
            {
                SetValue(CheckboxIconProperty, value);
            }
        }
        #endregion

        #region CheckboxEmptyIcon
        public static readonly BindableProperty CheckboxEmptyIconProperty = BindableProperty.Create(
            propertyName: nameof(CheckboxEmptyIcon),
            returnType: typeof(ImageSource),
            declaringType: typeof(Checkbox),
            defaultValue: ImageSource.FromResource("IS.Toolkit.XamarinForms.Controls.Checkbox.checkbox_empty.png", typeof(Expander).GetTypeInfo().Assembly));

        public ImageSource CheckboxEmptyIcon
        {
            get
            {
                return (ImageSource)GetValue(CheckboxEmptyIconProperty);
            }
            set
            {
                SetValue(CheckboxEmptyIconProperty, value);
            }
        }
        #endregion

        #region IsChecked
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
            propertyName: nameof(IsChecked),
            returnType: typeof(bool),
            declaringType: typeof(Checkbox),
            defaultValue: false,
            propertyChanged: IsCheckedPropertyChanged);

        private static void IsCheckedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null && newValue != null)
            {
                var control = (Checkbox)bindable;
                var isOpen = (bool)newValue;
                (control.Content as Image).Source = isOpen ? control.CheckboxIcon : control.CheckboxEmptyIcon;
            }
        }

        public bool IsChecked
        {
            get
            {
                return (bool)GetValue(IsCheckedProperty);
            }
            set
            {
                SetValue(IsCheckedProperty, value);
            }
        }
        #endregion
    }
}