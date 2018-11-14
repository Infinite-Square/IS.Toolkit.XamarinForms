using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS.Toolkit.XamarinForms.Behavior
{
    public class InfiniteScrollBehavior : BaseBehavior<ListView>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                nameof(Command),
                typeof(ICommand),
                typeof(InfiniteScrollBehavior),
                null);

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public static readonly BindableProperty NumberOfItemsBeforeLoadMoreProperty =
            BindableProperty.Create(
                nameof(NumberOfItemsBeforeLoadMore),
                typeof(uint),
                typeof(InfiniteScrollBehavior),
                1U,
                BindingMode.OneWay);

        public uint NumberOfItemsBeforeLoadMore
        {
            get
            {
                return (uint)GetValue(NumberOfItemsBeforeLoadMoreProperty);
            }
            set
            {
                SetValue(NumberOfItemsBeforeLoadMoreProperty, value);
            }
        }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.ItemAppearing += InfiniteListView_ItemAppearing;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.ItemAppearing -= InfiniteListView_ItemAppearing;
        }

        private void InfiniteListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var items = AssociatedObject.ItemsSource as IList;
            if (Command != null &&
                items != null &&
                items.IndexOf(e.Item) >= items.Count - NumberOfItemsBeforeLoadMore)
            {
                if (Command.CanExecute(null))
                {
                    Command.Execute(null);
                }
            }
        }
    }
}