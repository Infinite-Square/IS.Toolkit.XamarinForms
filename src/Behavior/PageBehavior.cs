using IS.Toolkit.XamarinForms.Core;
using System;
using Xamarin.Forms;

namespace IS.Toolkit.XamarinForms.Behavior
{
    public class PageBehavior : BaseBehavior<Page>
    {
        protected override void OnAttachedTo(Page bindable)
        {
            bindable.Appearing += AssociatedObject_Appearing;
            bindable.Disappearing += AssociatedObject_Disappearing;
            base.OnAttachedTo(bindable);
        }

        private void AssociatedObject_Appearing(object sender, EventArgs e)
        {
            (AssociatedObject?.BindingContext as IAppearingViewModel)?.OnAppearing();
        }

        private void AssociatedObject_Disappearing(object sender, EventArgs e)
        {
            (AssociatedObject?.BindingContext as IAppearingViewModel)?.OnDisappearing();
        }

        protected override void OnDetachingFrom(Page bindable)
        {
            bindable.Appearing -= AssociatedObject_Appearing;
            bindable.Disappearing -= AssociatedObject_Disappearing;
            base.OnDetachingFrom(bindable);
        }
    }
}
