using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS.Toolkit.XamarinForms.Controls
{
    public class ISCheckbox : View
    {
        public static readonly BindableProperty IsCheckedBindableProperty = BindableProperty.Create(
                                                        nameof(IsChecked),
                                                        typeof(bool),
                                                        typeof(ISCheckbox),
                                                        false,
                                                        BindingMode.TwoWay);

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedBindableProperty);
            set => SetValue(IsCheckedBindableProperty, value);
        }

        public static readonly BindableProperty AccentColorProperty = BindableProperty.Create(nameof(AccentColor), typeof(Color), typeof(ISCheckbox), Color.CadetBlue);
        public Color AccentColor
        {
            get { return (Color)GetValue(AccentColorProperty); }
            set { SetValue(AccentColorProperty, value); }
        }

        public static readonly BindableProperty CheckedCommandProperty = BindableProperty.Create(nameof(CheckedCommand), typeof(ICommand), typeof(ISCheckbox), null);
        public ICommand CheckedCommand
        {
            get => (ICommand)GetValue(CheckedCommandProperty);
            set => SetValue(CheckedCommandProperty, value);
        }

        public static readonly BindableProperty CheckedCommandArguementProperty = BindableProperty.Create(nameof(CheckedCommandArguement), typeof(object), typeof(ISCheckbox), default(object));
        public object CheckedCommandArguement
        {
            get => (object)GetValue(CheckedCommandArguementProperty);
            set => SetValue(CheckedCommandArguementProperty, value);
        }
    }
}
