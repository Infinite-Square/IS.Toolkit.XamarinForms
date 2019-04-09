using IS.Toolkit.XamarinForms.Behavior;
using IS.Toolkit.XamarinForms.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IS.Toolkit.XamarinForms.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<Type, Func<Page>> _types;
        private readonly Application _currentApp;
        private bool _isPopingModal;

        public NavigationService(Application currentApp)
        {
            _types = new Dictionary<Type, Func<Page>>();
            _currentApp = currentApp;
        }

        private void Register<TViewModel, TPage>()
                where TViewModel : IAppearingViewModel
                where TPage : Page, new()
        {
            _types.Add(typeof(TViewModel), () => { return new TPage(); });
        }

        public async Task NavigateToPageAsync<TViewModel>(TViewModel vm)
            where TViewModel : IAppearingViewModel
        {
            var mainPage = GetMainPage();
            await mainPage.Navigation.PushAsync(GetNewPage(vm));
            RemoveNoHistoryPages();
        }

        public async Task<TResult> NavigateToModalPageAsync<TViewModel, TResult>(TViewModel vm, bool inMainThread = false)
            where TViewModel : IModalViewModel<TResult>
        {
            var tcs = new TaskCompletionSource<bool>();

            // do not put theses two lines in BeginInvokeOnMainThread => we have an exception... if you do that
            var mainPage = GetMainPage();
            if (!inMainThread)
            {
                await mainPage.Navigation.PushModalAsync(GetNewPage(vm)).ConfigureAwait(false);
            }

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    if (inMainThread)
                    {
                        await mainPage.Navigation.PushModalAsync(GetNewPage(vm)).ConfigureAwait(false);
                    }
                    await vm.OnNavigationFinishedAsync().ConfigureAwait(false);
                    tcs.SetResult(true);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            await tcs.Task.ConfigureAwait(false);
            return await vm.Task;
        }

        public Task PopAsync()
        {
            return GetMainPage().Navigation.PopAsync();
        }

        public async Task PopModalAsync()
        {
            if (!_isPopingModal)
            {
                try
                {
                    _isPopingModal = true;
                    await GetMainPage().Navigation.PopModalAsync();
                }
                finally
                {
                    _isPopingModal = false;
                }
            }
        }

        private void RemoveNoHistoryPages()
        {
            var mainPage = GetMainPage();
            var toRemove = mainPage.Navigation.NavigationStack.OfType<INoHistoryPage>().ToList();
            var lastPage = mainPage.Navigation.NavigationStack.LastOrDefault();

            foreach (var item in toRemove)
            {
                if (item != lastPage)
                {
                    mainPage.Navigation.RemovePage(item as Page);
                }
            }
        }

        private Page GetMainPage()
        {
            return (Page)_currentApp.MainPage;
        }

        private Page GetNewPage<TViewModel>(TViewModel vm)
            where TViewModel : IAppearingViewModel
        {
            var tVM = typeof(TViewModel);
            if (_types.ContainsKey(tVM))
            {
                var func = _types[tVM];
                var page = func.Invoke();
                page.BindingContext = vm;

                page.Behaviors.Add(new PageBehavior());
                return page;
            }
            else
            {
                throw new NotImplementedException($"{tVM.Name} didn't have any Page registered");
            }
        }
    }
}
