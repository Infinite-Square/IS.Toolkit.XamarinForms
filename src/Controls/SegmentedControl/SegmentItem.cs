using System.Windows.Input;
using Xamarin.Forms;

namespace IS.Toolkit.XamarinForms.Controls
{
    public class SegmentItem : Grid
    {
        private BoxView _boxView;
        private Label _label;
        private Button _button;

        public void Build(
            Color selectedItemBackgroundColor,
            Color selectedItemTextColor,
            CornerRadius cornerRadius,
            Color textColor,
            string selectedItem,
            string value,
            ICommand itemCommand)
        {
            _boxView = new BoxView
            {
                CornerRadius = cornerRadius,
            };

            _label = new Label
            {
                Text = value,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            _button = new Button
            {
                BackgroundColor = Color.Transparent,
                Command = itemCommand,
                CommandParameter = value
            };

            Children.Add(_boxView);
            Children.Add(_label);
            Children.Add(_button);

            InvalidateSelection(selectedItem, textColor, selectedItemBackgroundColor, selectedItemTextColor);
        }

        public void InvalidateSelection(
            string selectedItem,
            Color textColor,
            Color selectedItemBackgroundColor,
            Color selectedItemTextColor)
        {
            var value = BindingContext as string;
            _label.TextColor = selectedItem == value ? selectedItemTextColor : textColor;
            _boxView.BackgroundColor = selectedItem == value ? selectedItemBackgroundColor : Color.Transparent;
        }
    }
}