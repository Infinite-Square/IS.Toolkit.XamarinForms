using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS.Toolkit.XamarinForms.Controls
{
    public class CheckBox : View
    {
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
            nameof(IsChecked),
            typeof(bool),
            typeof(CheckBox),
            false,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                ////((CheckBox)bindable).CheckedChanged?.Invoke(bindable, new CheckedChangedEventArgs((bool)newValue));
            },
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty CheckedColorProperty = BindableProperty.Create(nameof(CheckedColor), typeof(Color), typeof(CheckBox), Color.Default);

        public Color CheckedColor
        {
            get { return (Color)GetValue(CheckedColorProperty); }
            set { SetValue(CheckedColorProperty, value); }
        }

        public static readonly BindableProperty UncheckedColorProperty = BindableProperty.Create(nameof(UncheckedColor), typeof(Color), typeof(CheckBox), Color.Default);

        public Color UncheckedColor
        {
            get { return (Color)GetValue(UncheckedColorProperty); }
            set { SetValue(UncheckedColorProperty, value); }
        }

        public CheckBox()
        {
        }

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public event EventHandler<CheckedChangedEventArgs> CheckedChanged;
        public void InvokeCheckChanged(bool isChecked)
        {
            CheckedChanged?.Invoke(this, new CheckedChangedEventArgs(IsChecked = isChecked));
        }
    }
}
