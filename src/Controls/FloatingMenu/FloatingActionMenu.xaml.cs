using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS.Toolkit.XamarinForms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloatingActionMenu : Grid
    {
        private double _originalContentHeight;

        public FloatingActionMenu()
        {
            InitializeComponent();
        }

        #region Items
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            propertyName: nameof(Items),
            returnType: typeof(IEnumerable<FloatingActionMenuItem>),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: default(IEnumerable<FloatingActionMenuItem>),
            propertyChanged: ItemsPropertyChanged);

        private static void ItemsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null && newValue != null)
            {
                var control = (FloatingActionMenu)bindable;
                control.InitOriginalContentHeight(control.Items.Count() * (control.ItemSize + 10));
            }
        }

        public IEnumerable<FloatingActionMenuItem> Items
        {
            get
            {
                return (IEnumerable<FloatingActionMenuItem>)GetValue(ItemsProperty);
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

        #region FilterBackgroundColor
        public static readonly BindableProperty FilterBackgroundColorProperty = BindableProperty.Create(
            propertyName: nameof(FilterBackgroundColor),
            returnType: typeof(Color),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: Color.FromHex("#B3ffffff"));

        public Color FilterBackgroundColor
        {
            get
            {
                return (Color)GetValue(FilterBackgroundColorProperty);
            }
            set
            {
                SetValue(FilterBackgroundColorProperty, value);
            }
        }
        #endregion

        #region Padding
        public static new readonly BindableProperty PaddingProperty = BindableProperty.Create(
            propertyName: nameof(Padding),
            returnType: typeof(Thickness),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: default(Thickness));

        public new Thickness Padding
        {
            get
            {
                return (Thickness)GetValue(PaddingProperty);
            }
            set
            {
                SetValue(PaddingProperty, value);
            }
        }
        #endregion

        #region IsOpen
        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create(
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
                control.IsOpenChanged(isOpen);
            }
        }

        private void IsOpenChanged(bool isOpen)
        {
            // RightIconImage.RotateTo(isOpen ? 180 : 0, length: 150);
            if (Items != null && _originalContentHeight != default)
            {
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

                var animate3 = new Animation(
                    callback: d => OpacityFilter.Opacity = d,
                    start: isOpen ? 0 : 1,
                    end: isOpen ? 1 : 0);
                animate3.Commit(
                    owner: OpacityFilter,
                    name: "ExpanderOpacityFilterAnimation",
                    length: 150u,
                    finished: (arg, value) =>
                    {
                        OpacityFilter.InputTransparent = !isOpen;
                    });
            }
        }

        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(IsOpenProperty);
            }
            set
            {
                SetValue(IsOpenProperty, value);
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
                control.ItemsMargin = new Thickness(0, 0, (control.Size / 2) - (control.ItemSize / 2), 0);
                if (control.Items != default)
                {
                    control.InitOriginalContentHeight(control.Items.Count() * (control.ItemSize + 10));
                }
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
        public Thickness ItemsMargin { get; set; } = new Thickness(0, 0, 10, 0);
        #endregion

        #region ItemsPadding
        public static readonly BindableProperty ItemsPaddingProperty = BindableProperty.Create(
            propertyName: nameof(ItemsPadding),
            returnType: typeof(Thickness),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: default(Thickness));

        public Thickness ItemsPadding
        {
            get
            {
                return (Thickness)GetValue(ItemsPaddingProperty);
            }
            set
            {
                SetValue(ItemsPaddingProperty, value);
            }
        }
        #endregion

        private void FloatingActionButton_Clicked(object sender, EventArgs e)
        {
            IsOpen = !IsOpen;
        }

        private void InitOriginalContentHeight(double size)
        {
            _originalContentHeight = size;

            if (!IsOpen)
            {
                ItemsLayout.HeightRequest = 0;
                OpacityFilter.HeightRequest = 0;
                OpacityFilter.Opacity = 0;
                OpacityFilter.InputTransparent = true;
                ItemsLayout.Opacity = 0;
            }
        }

        private void OpacityFilter_Tapped(object sender, EventArgs e)
        {
            IsOpen = false;
        }

        private void ItemClicked(object sender, EventArgs e)
        {
            IsOpen = false;
        }
    }
}