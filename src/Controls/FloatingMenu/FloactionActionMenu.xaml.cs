using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS.Toolkit.XamarinForms.Controls.FloatingMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloatingActionMenu : ContentView
    {
        private double _originalContentHeight;
        private bool _isAnimating;

        public FloatingActionMenu()
        {
            InitializeComponent();
            ItemsLayout.SizeChanged += ItemsLayout_SizeChanged;
        }

        private void ItemsLayout_SizeChanged(object sender, EventArgs e)
        {
            // Only at initialization
            if (_originalContentHeight == default(double) || !_isAnimating)
            {
                _originalContentHeight = ItemsLayout.Bounds.Height;
                if (!IsOpen)
                {
                    // If not open, need to init container size
                    ItemsLayout.HeightRequest = 0;
                    ItemsLayout.Opacity = 0;
                }
            }
        }

        #region Items
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            propertyName: nameof(Items),
            returnType: typeof(List<Item>),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: default(List<Item>));

        public List<Item> Items
        {
            get
            {
                return (List<Item>)GetValue(ItemsProperty);
            }
            set
            {
                SetValue(ItemsProperty, value);
            }
        }
        #endregion

        #region Image
        public static readonly BindableProperty ImageProperty = BindableProperty.Create(
            propertyName: nameof(Image),
            returnType: typeof(ImageSource),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: default(ImageSource));

        public ImageSource Image
        {
            get
            {
                return (ImageSource)GetValue(ImageProperty);
            }
            set
            {
                SetValue(ImageProperty, value);
            }
        }
        #endregion

        #region Color
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            propertyName: nameof(Color),
            returnType: typeof(Color),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: Color.Accent);

        public Color Color
        {
            get
            {
                return (Color)GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }
        #endregion

        #region IsOpen
        public static readonly BindableProperty OpenedProperty = BindableProperty.Create(
            propertyName: nameof(IsOpen),
            returnType: typeof(bool),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: true,
            propertyChanged: IsOpenPropertyChanged);

        private static void IsOpenPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null && newValue != null)
            {
                var control = (FloatingActionMenu)bindable;
                var isOpen = (bool)newValue;
                control.IsOpenChangedAsync(isOpen);
            }
        }

        private async Task IsOpenChangedAsync(bool isOpen)
        {
            // RightIconImage.RotateTo(isOpen ? 180 : 0, length: 150);
            if (Items != null && _originalContentHeight != default)
            {
                _isAnimating = true;
                var animate = new Animation(
                    callback: d => ItemsLayout.HeightRequest = d,
                    start: isOpen ? 0 : _originalContentHeight,
                    end: isOpen ? _originalContentHeight : 0);
                animate.Commit(
                    owner: ItemsLayout,
                    name: "ExpanderAnimation",
                    length: 150u);

                var animate2 = new Animation(
                    callback: d => ItemsLayout.Opacity = d,
                    start: isOpen ? 0 : 1,
                    end: isOpen ? 1 : 0);
                animate2.Commit(
                    owner: ItemsLayout,
                    name: "ExpanderFadeAnimation",
                    length: 150u);

                await Task.Delay(200);
                _isAnimating = false;
            }
        }

        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(OpenedProperty);
            }
            set
            {
                SetValue(OpenedProperty, value);
            }
        }
        #endregion

        #region Size
        public static readonly BindableProperty SizeProperty = BindableProperty.Create(
            propertyName: nameof(Size),
            returnType: typeof(double),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: 70.0,
            propertyChanged: SizeChanged);

        private static new void SizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null && newValue != null)
            {
                var control = (FloatingActionMenu)bindable;
                control._originalContentHeight = default;
                control.ItemSize = 2 * control.Size / 3;
                control.ItemsMargin = new Thickness(0, 0, control.Size / 4, 0);

                // if (control.ItemsLayout != null && control.ItemsLayout.Bounds.Height != -1)
                // {
                //    control._originalContentHeight = control.ItemsLayout.Bounds.Height;
                // }
                control.ItemsLayout.ForceLayout();
            }
        }

        public double Size
        {
            get
            {
                return (double)GetValue(SizeProperty);
            }
            set
            {
                SetValue(SizeProperty, value);
            }
        }

        public double ItemSize { get; set; } = 50;
        public Thickness ItemsMargin { get; set; } = new Thickness(0, 0, 17.5, 0);
        #endregion

        // #region BackgroundColor
        // public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
        //    propertyName: nameof(BackgroundColor),
        //    returnType: typeof(Color),
        //    declaringType: typeof(FloatingActionMenu),
        //    defaultValue: Color.FromHex("#80ffffff"));

        // public new Color BackgroundColor
        // {
        //    get
        //    {
        //        return (Color)GetValue(BackgroundColorProperty);
        //    }
        //    set
        //    {
        //        SetValue(BackgroundColorProperty, value);
        //    }
        // }
        // #endregion
        private void FloatingActionButton_Clicked(object sender, EventArgs e)
        {
            IsOpen = !IsOpen;
        }
    }
}