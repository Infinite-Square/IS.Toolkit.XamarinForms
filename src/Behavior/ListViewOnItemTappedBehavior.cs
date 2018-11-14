using System.Windows.Input;
using Xamarin.Forms;

namespace IS.Toolkit.XamarinForms.Behavior
{
    public class ListViewOnItemTappedBehavior : BaseBehavior<ListView>
    {
        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.ItemTapped += Bindable_ItemTapped;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.ItemTapped -= Bindable_ItemTapped;
        }

        private void Bindable_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var cmd = Command;

            if (cmd != null && cmd.CanExecute(null))
            {
               cmd.Execute(e.Item);
            }
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.CreateAttached(
                nameof(Command),
                typeof(ICommand),
                typeof(ListViewOnItemTappedBehavior),
                null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
    }
}
