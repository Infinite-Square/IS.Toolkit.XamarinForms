using System;
using System.Collections.Generic;
using System.Text;

namespace IS.Toolkit.XamarinForms.Controls
{
    public class CheckedChangedEventArgs : EventArgs
    {
        public CheckedChangedEventArgs(bool value)
        {
            Value = value;
        }

        public bool Value { get; private set; }
    }
}
