using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Sample.IOS.Renderers
{
    public class UICheckBox : UIButton
    {
        public string Text
        {
            set => SetTitle(value, UIControlState.Normal);
        }

        public bool IsChecked
        {
            get => Selected;
            set => Selected = value;
        }

        public UIColor TextColor
        {
            set => SetTitleColor(value, UIControlState.Normal);
        }

        public UIColor AccentColor
        {
            set => BackgroundColor = value;
        }

        public float FontSize
        {
            set => Font = UIFont.SystemFontOfSize(value);
        }

        public UICheckBox()
        {
            SetImage(UIImage.FromBundle("checkbox_empty.png"), UIControlState.Normal);
            SetImage(UIImage.FromBundle("checkbox_fill.png"), UIControlState.Selected);
        }
    }
}