using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

// From : https://github.com/andreinitescu/XFItemsControl/blob/master/XFItemsControl/XFItemsControl/ItemsControl.cs
namespace IS.Toolkit.XamarinForms.Controls
{
    public class ItemsControl : ContentView
    {
        private Layout<View> _itemsLayout;
        public IList<View> ViewItems => _itemsLayout?.Children;

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(ItemsControl), propertyChanged: ItemsSourceChanged);

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(ItemsControl), propertyChanged: ItemTemplateChanged);

        public static readonly BindableProperty ItemsLayoutProperty =
            BindableProperty.Create(nameof(ItemsLayout), typeof(DataTemplate), typeof(ItemsControl), propertyChanged: ItemsLayoutChanged);

        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create(nameof(Orientation), typeof(StackOrientation), typeof(ItemsControl), defaultValue: StackOrientation.Vertical, propertyChanged: (s, n, o) => ((ItemsControl)s).OnOrientationPropertyChanged());
        public static readonly BindableProperty SpacingProperty =
            BindableProperty.Create(nameof(Spacing), typeof(double), typeof(ItemsControl), defaultValue: 0.0, propertyChanged: (s, n, o) => ((ItemsControl)s).OnItemsLayoutPropertyChanged());

        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public DataTemplate ItemsLayout
        {
            get => (DataTemplate)GetValue(ItemsLayoutProperty);
            set => SetValue(ItemsLayoutProperty, value);
        }

        public StackOrientation Orientation
        {
            get => (StackOrientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public double Spacing
        {
            get => (double)GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        protected virtual View CreateItem(object item)
        {
            if (ItemTemplate == null)
            {
                return new Label() { Text = item.ToString() };
            }
            else
            {
                var itemView = (View)ItemTemplate.CreateContent();
                itemView.BindingContext = item;
                return itemView;
            }
        }

        protected virtual void CreateItemsLayout()
        {
            Content = ItemsLayout != null ?
                (Layout)ItemsLayout.CreateContent() :
                new StackLayout()
                {
                    Orientation = Orientation,
                    Spacing = Spacing
                };

            if (Content is Layout<View> viewLayout)
            {
                _itemsLayout = viewLayout;
            }
            else
            {
                _itemsLayout = FindItemsHost(Content);
            }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as ItemsControl).OnItemsSourceChanged(oldValue, newValue);
        }

        private void OnItemsSourceChanged(object oldValue, object newValue)
        {
            if (oldValue is INotifyCollectionChanged oldCollection)
            {
                oldCollection.CollectionChanged -= Collection_CollectionChanged;
            }

            if (newValue is INotifyCollectionChanged newCollection)
            {
                newCollection.CollectionChanged += Collection_CollectionChanged;
            }

            OnItemsSourcePropertyChanged();
        }

        private static void ItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as ItemsControl).OnItemTemplatePropertyChanged();
        }

        private static void ItemsLayoutChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as ItemsControl).OnItemsLayoutPropertyChanged();
        }

        private void OnItemsSourcePropertyChanged()
        {
            if (_itemsLayout == null)
            {
                CreateItemsLayout();
            }

            _itemsLayout.Children.Clear();

            if (ItemsSource != null)
            {
                foreach (object item in ItemsSource)
                {
                    _itemsLayout.Children.Add(CreateItem(item));
                }
            }
        }

        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_itemsLayout != null)
            {
                OnItemsSourcePropertyChanged();
            }
        }

        private void OnItemTemplatePropertyChanged()
        {
            if (_itemsLayout == null)
            {
                return;
            }

            OnItemsSourcePropertyChanged();
        }

        private void OnItemsLayoutPropertyChanged()
        {
            CreateItemsLayout();

            OnItemsSourcePropertyChanged();
        }

        private void OnOrientationPropertyChanged()
        {
            CreateItemsLayout();
            (_itemsLayout as StackLayout).Orientation = Orientation;
            OnItemsSourcePropertyChanged();
        }

        private Layout<View> FindItemsHost(View currView)
        {
            if (currView is Layout<View> viewLayout && viewLayout.GetIsItemsHost())
            {
                return viewLayout;
            }
            else
            {
                if (currView is Layout layoutView)
                {
                    foreach (Element e in layoutView.Children)
                    {
                        Layout<View> itemsHost = FindItemsHost((View)e);
                        if (itemsHost != null)
                        {
                            return itemsHost;
                        }
                    }
                }

                return null;
            }
        }
    }
}
