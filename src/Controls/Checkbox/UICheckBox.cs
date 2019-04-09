#if IOS
using CoreGraphics;
using IS.Toolkit.XamarinForms.Controls;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CheckBox), typeof(CheckBoxRenderer))]
namespace IS.Toolkit.XamarinForms.Controls
{
    public class UICheckBox : UIControl
    {
        public UICheckBox()
        {
            BackgroundColor = UIColor.Clear;
        }

        public EventHandler CheckedChanged { get; set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (value == _isChecked)
                {
                    return;
                }

                _isChecked = value;
                SetNeedsDisplay();
            }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (value == _isEnabled)
                {
                    return;
                }

                _isEnabled = value;

                UserInteractionEnabled = IsEnabled;

                SetNeedsDisplay();
            }
        }

        private Color _checkColor;

        private Color _checkedColor;

        private Color _uncheckedColor;

        private Color _disabledColor;
        public Color DisabledColor
        {
            get => _disabledColor;
            set
            {
                if (_disabledColor == value)
                {
                    return;
                }

                _disabledColor = value;
                SetNeedsDisplay();
            }
        }

        public Color CheckColor
        {
            get => _checkColor;
            set
            {
                if (_checkColor == value)
                {
                    return;
                }

                _checkColor = value;
                SetNeedsDisplay();
            }
        }

        public Color CheckedColor
        {
            get => _checkedColor;
            set
            {
                if (_checkedColor == value)
                {
                    return;
                }

                _checkedColor = value;
                SetNeedsDisplay();
            }
        }

        public Color UncheckedColor
        {
            get => _uncheckedColor;
            set
            {
                if (_uncheckedColor == value)
                {
                    return;
                }

                _uncheckedColor = value;
                SetNeedsDisplay();
            }
        }

        public bool IsVisible
        {
            get
            {
                return !Hidden;
            }
            set
            {
                var hidden = !value;
                if (hidden != Hidden)
                {
                    Hidden = hidden;
                    SetNeedsDisplay();
                }
            }
        }

        public override void Draw(CGRect rect)
        {
            if (IsEnabled)
            {
                var checkedColor = CheckedColor.IsDefault ? TintColor : CheckedColor.ToUIColor();
                checkedColor.SetFill();
                if (IsChecked)
                {
                    checkedColor.SetStroke();
                }
                else
                {
                    (UncheckedColor.IsDefault ? TintColor : UncheckedColor.ToUIColor()).SetStroke();
                }
            }
            else
            {
                (DisabledColor.IsDefault ? UIColor.Black.ColorWithAlpha(.5f) : DisabledColor.ToUIColor()).SetColor();
            }

            var width = Bounds.Size.Width;
            var height = Bounds.Size.Height;

            var outerDiameter = Math.Min(width, height);
            var lineWidth = 2.0 / CheckBoxRenderer.DefaultSize * outerDiameter;
            var diameter = outerDiameter - (3 * lineWidth);
            var radius = diameter / 2;

            var xOffset = diameter + (lineWidth * 2) <= width ? lineWidth * 2 : (width - diameter) / 2;
            var hPadding = xOffset;
            var vPadding = (nfloat)((height - diameter) / 2);

            var backgroundRect = new CGRect(xOffset, vPadding, diameter, diameter);
            var boxPath = UIBezierPath.FromOval(backgroundRect);
            boxPath.LineWidth = (nfloat)lineWidth;
            boxPath.Stroke();
            if (IsChecked)
            {
                boxPath.Fill();
                var checkPath = new UIBezierPath
                {
                    LineWidth = (nfloat)0.077,
                    LineCapStyle = CGLineCap.Round,
                    LineJoinStyle = CGLineJoin.Round
                };
                var context = UIGraphics.GetCurrentContext();
                context.SaveState();
                context.TranslateCTM((nfloat)hPadding + (nfloat)(0.05 * diameter), vPadding + (nfloat)(0.1 * diameter));
                context.ScaleCTM((nfloat)diameter, (nfloat)diameter);
                checkPath.MoveTo(new CGPoint(0.72f, 0.22f));
                checkPath.AddLineTo(new CGPoint(0.33f, 0.6f));
                checkPath.AddLineTo(new CGPoint(0.15f, 0.42f));
                (CheckColor.IsDefault ? UIColor.White : CheckColor.ToUIColor()).SetStroke();
                checkPath.Stroke();
                context.RestoreState();
            }
        }

        public override bool BeginTracking(UITouch uitouch, UIEvent uievent)
        {
            IsChecked = !IsChecked;
            CheckedChanged?.Invoke(this, null);
            return base.BeginTracking(uitouch, uievent);
        }
    }
}
#endif