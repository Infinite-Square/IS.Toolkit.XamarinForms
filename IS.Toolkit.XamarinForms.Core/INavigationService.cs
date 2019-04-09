using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IS.Toolkit.XamarinForms.Core
{
    public interface INavigationService
    {
        Task NavigateToPageAsync<TViewModel>(TViewModel vm)
         where TViewModel : IAppearingViewModel;
        Task<TResult> NavigateToModalPageAsync<TViewModel, TResult>(TViewModel vm, bool inMainThread = false)
            where TViewModel : IModalViewModel<TResult>;
        Task PopAsync();
        Task PopModalAsync();
    }

    public interface IAppearingViewModel
    {
        void OnAppearing();
        void OnDisappearing();
        Task OnBackAsync();
    }

    public interface IModalViewModel<TResult> : IAppearingViewModel
    {
        Task OnNavigationFinishedAsync();
        ICommand ValidateCommand { get; }
        ICommand CancelCommand { get; }
        Task<TResult> Task { get; }
    }
}
