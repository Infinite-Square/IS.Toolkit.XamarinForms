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

        public FloatingActionMenu()
        {
            InitializeComponent();
        }

        #region Items
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            propertyName: nameof(Items),
            returnType: typeof(List<Item>),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: default(List<Item>),
            propertyChanged: ItemsPropertyChanged);

        private static void ItemsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null && newValue != null)
            {
                var control = (FloatingActionMenu)bindable;
                control.InitOriginalContentHeight(control.Items.Count * (control.ItemSize + 10));
            }
        }

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
        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create(
            propertyName: nameof(IsOpen),
            returnType: typeof(bool),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: true,
            propertyChanged: IsOpenPropertyChanged);

        private static async void IsOpenPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null && newValue != null)
            {
                var control = (FloatingActionMenu)bindable;
                var isOpen = (bool)newValue;
                await control.IsOpenChangedAsync(isOpen);
            }
        }

        private Task<bool> IsOpenChangedAsync(bool isOpen)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

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
            }

            return taskCompletionSource.Task;
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
                    control.InitOriginalContentHeight(control.Items.Count * (control.ItemSize + 10));
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

        private void FloatingActionButton_Clicked(object sender, EventArgs e)
        {
            IsOpen = !IsOpen;
        }

        private void InitOriginalContentHeight(double size)
        {
            _originalContentHeight = size;

            if (!IsOpen)
            {
                // If not open, need to init container size
                ItemsLayout.HeightRequest = 0;
                ItemsLayout.Opacity = 0;
            }
        }
    }
}