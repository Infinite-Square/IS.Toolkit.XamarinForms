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
        public class CheckChangedEventArg : EventArgs
        {
            public bool IsChecked { get; set; }
        }

        public event EventHandler OnCheckChange;

        public static readonly BindableProperty IsCheckedBindableProperty = BindableProperty.Create(
                                                        nameof(IsChecked),
                                                        typeof(bool),
                                                        typeof(CheckBox),
                                                        false,
                                                        BindingMode.TwoWay);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CheckBox), default(string));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(float), typeof(CheckBox), default(float));
        public float FontSize
        {
            get { return (float)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CheckBox), Color.Black);
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedBindableProperty);
            set => SetValue(IsCheckedBindableProperty, value);
        }

        public static readonly BindableProperty AccentColorProperty = BindableProperty.Create(nameof(AccentColor), typeof(Color), typeof(CheckBox), Color.Transparent);
        public Color AccentColor
        {
            get { return (Color)GetValue(AccentColorProperty); }
            set { SetValue(AccentColorProperty, value); }
        }

        public static readonly BindableProperty CheckedCommandProperty = BindableProperty.Create(nameof(CheckedCommand), typeof(ICommand), typeof(CheckBox), null);
        public ICommand CheckedCommand
        {
            get => (ICommand)GetValue(CheckedCommandProperty);
            set => SetValue(CheckedCommandProperty, value);
        }

        public static readonly BindableProperty CheckedCommandArguementProperty = BindableProperty.Create(nameof(CheckedCommandArguement), typeof(object), typeof(CheckBox), default(object));
        public object CheckedCommandArguement
        {
            get => (object)GetValue(CheckedCommandArguementProperty);
            set => SetValue(CheckedCommandArguementProperty, value);
        }

        public void InvokeCheckChanged(bool isChecked)
        {
            OnCheckChange?.Invoke(this, new CheckChangedEventArg { IsChecked = isChecked });
        }
    }
}
