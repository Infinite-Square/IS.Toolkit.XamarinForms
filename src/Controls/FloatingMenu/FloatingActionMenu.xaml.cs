using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS.Toolkit.XamarinForms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloatingActionMenu : Grid
    {
        private int _animationSpeed = 500;
        private bool _canClose = true;
        private bool _firstClose = false;

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

        public static readonly BindableProperty IsRotateAnimationEnabledProperty = BindableProperty.Create(nameof(IsRotateAnimationEnabled), typeof(bool), typeof(FloatingActionButton), default(bool));
        public bool IsRotateAnimationEnabled
        {
            get { return (bool)GetValue(IsRotateAnimationEnabledProperty); }
            set { SetValue(IsRotateAnimationEnabledProperty, value); }
        }

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

        private void CloseAnimationOnFab()
        {
            if (IsRotateAnimationEnabled)
            {
                FAB.RestoreRotationAnimation();
            }

            if (ItemsLayout != null)
            {
                if (ItemsLayout.ViewItems != null)
                {
                    OpacityFilter.FadeTo(0, 500);
                    int nAnimationCount = 0;
                    for (int i = ItemsLayout.ViewItems.Count - 1; i >= 0; i--)
                    {
                        for (int j = ItemsLayout.ViewItems.Count - i; j > 0; j--)
                        {
                            nAnimationCount++;
                        }

                        ItemsLayout.ViewItems[i].TranslateTo(0, (ItemsLayout.ViewItems[i].Height + 10) * (ItemsLayout.ViewItems.Count - i));
                    }

                    _firstClose = true;
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
            OpacityFilter.IsVisible = true;
            ItemsLayout.IsVisible = true;
            foreach (var item in ItemsLayout.ViewItems)
            {
                item.TranslateTo(0, 0);
            }
        }

        private async void IsOpenChanged(bool isOpen)
        {
            if (isOpen && IsRotateAnimationEnabled)
            {
                FAB.RotateAnimation();
            }
            else if (!isOpen && IsRotateAnimationEnabled)
            {
                FAB.RestoreRotationAnimation();
            }

            if (ItemsLayout != null)
            {
                if (ItemsLayout.ViewItems != null)
                {
                    if (!isOpen)
                    {
                        Task animations = Task.Run(() =>
                        {
                            OpacityFilter.FadeTo(0, 500);
                            int nAnimationCount = 0;
                            for (int i = ItemsLayout.ViewItems.Count - 1; i >= 0; i--)
                            {
                                for (int j = ItemsLayout.ViewItems.Count - i; j > 0; j--)
                                {
                                    nAnimationCount++;
                                }

                                Debug.WriteLine($"{(ItemsLayout.ViewItems[i].Height + 10) * (ItemsLayout.ViewItems.Count - i)}");
                                ItemsLayout.ViewItems[i].TranslateTo(0, (ItemsLayout.ViewItems[i].Height + 10) * (ItemsLayout.ViewItems.Count - i));
                            }
                        }).ContinueWith(
                            (t) =>
                            OpacityFilter.IsVisible = false);
                        Device.BeginInvokeOnMainThread(async () =>
                                                        await animations);
                        OpacityFilter.IsVisible = false;
                    }
                    else
                    {
                        await OpacityFilter.FadeTo(0.5, 500);
                        OpacityFilter.IsVisible = true;
                        ItemsLayout.IsVisible = true;
                        foreach (var item in ItemsLayout.ViewItems)
                        {
                            item.TranslateTo(0, 0);
                        }
                    }
                }
            }

            OpacityFilter.InputTransparent = !isOpen;

            ////if (Items != null && _originalContentHeight != default)
            ////{
            ////    var animate = new Animation(
            ////        callback: d => ItemsLayout.HeightRequest = d,
            ////        start: isOpen ? 0 : _originalContentHeight,
            ////        end: isOpen ? _originalContentHeight : 0);
            ////    animate.Commit(
            ////        owner: ItemsLayout,
            ////        name: "ExpanderAnimation",
            ////        length: 150u);

            ////    var animate2 = new Animation(
            ////        callback: d => ItemsLayout.Opacity = d,
            ////        start: isOpen ? 0 : 1,
            ////        end: isOpen ? 1 : 0);
            ////    animate2.Commit(
            ////        owner: ItemsLayout,
            ////        name: "ExpanderFadeAnimation",
            ////        length: 150u);

            ////    var animate3 = new Animation(
            ////        callback: d => OpacityFilter.Opacity = d,
            ////        start: isOpen ? 0 : 1,
            ////        end: isOpen ? 1 : 0);
            ////    animate3.Commit(
            ////        owner: OpacityFilter,
            ////        name: "ExpanderOpacityFilterAnimation",
            ////        length: 150u,
            ////        finished: (arg, value) =>
            ////        {
            ////            OpacityFilter.InputTransparent = !isOpen;
            ////        });
            ////}
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
            defaultValue: 70.0);

        ////private static new void SizeChanged(BindableObject bindable, object oldValue, object newValue)
        ////{
        ////    if (bindable != null && newValue != null)
        ////    {
        ////        var control = (FloatingActionMenu)bindable;
        ////        control._originalContentHeight = default;
        ////        control.ItemSize = 2 * control.Size / 3;
        ////        control.ItemsMargin = new Thickness(0, 0, (control.Size / 2) - (control.ItemSize / 2), 0);
        ////        if (control.Items != default)
        ////        {
        ////            control.InitOriginalContentHeight(control.Items.Count() * (control.ItemSize + 10));
        ////        }
        ////    }
        ////}

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

        #region MainButtonToItemMargin

        public static readonly BindableProperty MainButtonToItemMarginProperty = BindableProperty.Create(nameof(MainButtonToItemMargin), typeof(Thickness), typeof(FloatingActionMenu), new Thickness(0, 0, 0, 0));

        public Thickness MainButtonToItemMargin
        {
            get { return (Thickness)GetValue(MainButtonToItemMarginProperty); }
            set { SetValue(MainButtonToItemMarginProperty, value); }
        }

        #endregion

        private void FloatingActionButton_Clicked(object sender, EventArgs e)
        {
            IsOpen = !IsOpen;
        }

        ////private void InitOriginalContentHeight(double size)
        ////{
        ////    _originalContentHeight = size;

        ////    if (!IsOpen)
        ////    {
        ////        ItemsLayout.HeightRequest = 0;
        ////        OpacityFilter.HeightRequest = 0;
        ////        OpacityFilter.Opacity = 0;
        ////        OpacityFilter.InputTransparent = true;
        ////        ItemsLayout.Opacity = 0;
        ////    }
        ////}

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