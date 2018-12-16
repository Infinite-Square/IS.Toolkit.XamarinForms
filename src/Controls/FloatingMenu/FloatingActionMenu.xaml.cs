using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS.Toolkit.XamarinForms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloatingActionMenu : Grid
    {
        private bool _firstClose;

        public FloatingActionMenu()
        {
            InitializeComponent();
        }

        #region Items
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            propertyName: nameof(Items),
            returnType: typeof(IEnumerable<FloatingActionMenuItem>),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: default(IEnumerable<FloatingActionMenuItem>));

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

        #region IsRotateAnimationEnabled
        public static readonly BindableProperty IsRotateAnimationEnabledProperty = BindableProperty.Create(nameof(IsRotateAnimationEnabled), typeof(bool), typeof(FloatingActionButton), default(bool));
        public bool IsRotateAnimationEnabled
        {
            get { return (bool)GetValue(IsRotateAnimationEnabledProperty); }
            set { SetValue(IsRotateAnimationEnabledProperty, value); }
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
            defaultValue: false,
            propertyChanged: IsOpenPropertyChanged);

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

        private static void IsOpenPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null && newValue != null)
            {
                var control = (FloatingActionMenu)bindable;
                var isOpen = (bool)newValue;
                if (!control.IsOpen)
                {
                    control.CloseAnimationOnFab();
                }
                else
                {
                    control.OpenFABAnimation();
                }

                control.OpacityFilter.InputTransparent = !control.IsOpen;
            }
        }

        private async void CloseAnimationOnFab()
        {
            if (IsRotateAnimationEnabled)
            {
                FAB.RestoreRotationAnimation();
            }

            if (ItemsLayout != null)
            {
                var tasks = new List<Task>();
                if (ItemsLayout.ViewItems != null)
                {
                    tasks.Add(OpacityFilter.FadeTo(0, 500));
                    tasks.Add(ItemsLayout.FadeTo(0, 500));

                    for (int i = ItemsLayout.ViewItems.Count - 1; i >= 0; i--)
                    {
                        tasks.Add(ItemsLayout.ViewItems[i].TranslateTo(0, (ItemsLayout.ViewItems[i].Height + 10) * (ItemsLayout.ViewItems.Count - i), 100, Easing.BounceOut));
                    }

                    _firstClose = true;
                    await Task.WhenAll(tasks.ToArray());
                    ItemsLayout.IsVisible = false;
                    OpacityFilter.IsVisible = false;
                }
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (!_firstClose)
            {
                CloseAnimationOnFab();
            }
        }

        private void OpenFABAnimation()
        {
            if (IsRotateAnimationEnabled)
            {
                FAB.RotateAnimation();
            }

            OpacityFilter.FadeTo(0.5, 500);
            ItemsLayout.FadeTo(1, 500);
            OpacityFilter.IsVisible = true;
            ItemsLayout.IsVisible = true;
            foreach (var item in ItemsLayout.ViewItems)
            {
                item.TranslateTo(0, 0);
            }
        }

        #endregion

        #region Size
        public static readonly BindableProperty SizeProperty = BindableProperty.Create(
            propertyName: nameof(Size),
            returnType: typeof(double),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: 70.0d);

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
        #endregion

        #region ItemSize
        public static readonly BindableProperty ItemSizeProperty = BindableProperty.Create(
           propertyName: nameof(ItemSize),
           returnType: typeof(double),
           declaringType: typeof(FloatingActionMenu),
           defaultValue: 50d);
        public double ItemSize
        {
            get
            {
                return (double)GetValue(ItemSizeProperty);
            }
            set
            {
                SetValue(ItemSizeProperty, value);
            }
        }
        #endregion

        private void FloatingActionButton_Clicked(object sender, EventArgs e)
        {
            IsOpen = !IsOpen;
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