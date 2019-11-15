using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBoilerplate.Interfaces;
using XamarinBoilerplate.ViewModels;
using XamarinBoilerplate.Views;
using XamarinBoilerplate.Views.Popups;

namespace XamarinBoilerplate.Services
{
    public class ViewNavigationService : INavigationService
    {
        private readonly object _sync = new object();
        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
        private readonly Dictionary<string, Type> _viewModelByKey = new Dictionary<string, Type>();
        private readonly Stack<NavigationPage> _navigationPageStack = new Stack<NavigationPage>();
        private NavigationPage CurrentNavigationPage => _navigationPageStack.Peek();

        public ViewNavigationService()
        {
        }

        public void BindViewModel<TVM, TPage>()
        where TVM : BaseViewModel
            where TPage : Page
        {
            lock (_sync)
            {

                if (_pagesByKey.ContainsKey(typeof(TPage).Name))
                {
                    _pagesByKey[typeof(TPage).Name] = typeof(TPage);
                }
                else
                {
                    _pagesByKey.Add(typeof(TPage).Name, typeof(TPage));
                }

                if (_viewModelByKey.ContainsKey(typeof(TPage).Name))
                {
                    _viewModelByKey[typeof(TPage).Name] = typeof(TVM);
                }
                else
                {
                    _viewModelByKey.Add(typeof(TPage).Name, typeof(TVM));
                }

                ViewModelContainer.Current.Initialize<TVM, TPage>();
            }
        }

        public void Configure(string pageKey, Type pageType)
        {
            lock (_sync)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    _pagesByKey[pageKey] = pageType;
                }
                else
                {
                    _pagesByKey.Add(pageKey, pageType);
                }
            }
        }

        public void SetRootPage(string rootPageKey, BaseViewModel ViewModel = null)
        {
            ViewModelContainer.Current.BuildContainer();

            var rootPage = GetPage(rootPageKey);
            _navigationPageStack.Clear();

            if (ViewModel != null)
            {
                rootPage.BindingContext = ViewModel;
            }

            var mainPage = new NavigationPage(rootPage);
            _navigationPageStack.Push(mainPage);

            App app = Application.Current as App;

            app.MainPage = mainPage;
        }

        public string CurrentPageKey
        {
            get
            {
                lock (_sync)
                {
                    if (CurrentNavigationPage?.CurrentPage == null)
                    {
                        return null;
                    }

                    var pageType = CurrentNavigationPage.CurrentPage.GetType();

                    return _pagesByKey.ContainsValue(pageType)
                        ? _pagesByKey.First(p => p.Value == pageType).Key
                        : null;
                }
            }
        }

        public BaseContentPage CurrentPage
        {
            get
            {
                lock (_sync)
                {
                    if (CurrentNavigationPage?.CurrentPage == null)
                    {
                        return null;
                    }

                    return (BaseContentPage)CurrentNavigationPage.CurrentPage;
                }
            }
        }

        public async Task GoBackAsync()
        {
            var navigationStack = CurrentNavigationPage.Navigation;
            if (navigationStack.NavigationStack.Count > 1)
            {
                await CurrentNavigationPage.PopAsync();
                return;
            }

            if (_navigationPageStack.Count > 1)
            {
                _navigationPageStack.Pop();
                await CurrentNavigationPage.Navigation.PopModalAsync();
                return;
            }

            await CurrentNavigationPage.PopAsync();
            return;
        }

        public async Task OpenDrawer()
        {
            var navigationStack = CurrentNavigationPage.Navigation;
            if (navigationStack.NavigationStack.Count > 0)
            {
                foreach (var page in navigationStack.NavigationStack)
                {
                    if (page is NavigationPage)
                    {
                        if ((page as NavigationPage).CurrentPage is MasterDetailPage)
                        {
                            ((page as NavigationPage).CurrentPage as MasterDetailPage).IsPresented = true;
                        }
                    }
                    else if (page is MasterDetailPage)
                    {
                        (page as MasterDetailPage).IsPresented = true;
                    }
                }
                return;
            }
            return;
        }

        public async Task CloseDrawer()
        {
            var navigationStack = CurrentNavigationPage.Navigation;
            if (navigationStack.NavigationStack.Count > 0)
            {
                foreach (var page in navigationStack.NavigationStack)
                {
                    if (page is NavigationPage)
                    {
                        if ((page as NavigationPage).CurrentPage is MasterDetailPage)
                        {
                            ((page as NavigationPage).CurrentPage as MasterDetailPage).IsPresented = false;
                        }
                    }
                    else if (page is MasterDetailPage)
                    {
                        (page as MasterDetailPage).IsPresented = false;
                    }
                }
                return;
            }
            return;
        }

        public bool IsDrawerOpen()
        {
            var navigationStack = CurrentNavigationPage.Navigation;
            if (navigationStack.NavigationStack.Count > 0)
            {
                foreach (var page in navigationStack.NavigationStack)
                {
                    if (page is NavigationPage)
                    {
                        if ((page as NavigationPage).CurrentPage is MasterDetailPage)
                        {
                            return ((page as NavigationPage).CurrentPage as MasterDetailPage).IsPresented;
                        }
                    }
                    else if (page is MasterDetailPage)
                    {
                        return (page as MasterDetailPage).IsPresented;
                    }
                }
                return false;
            }
            return false;
        }

        public async Task NavigateAsync(string pageKey, bool animated = true)
        {
            await NavigateAsync(pageKey, null, animated);
        }

        public async Task NavigateAsync(string pageKey, object parameter, bool animated = true)
        {
            var page = GetPage(pageKey, parameter);

            if (page == null)
            {
                return;
            }

            await CurrentNavigationPage.Navigation.PushAsync(page, animated);
        }

        public async Task NavigateAsync(string pageKey, Action<object> actionParameter, object parameter = null, bool animated = true)
        {
            var page = GetPage(pageKey, parameter, actionParameter);

            if (page == null)
            {
                return;
            }

            await CurrentNavigationPage.Navigation.PushAsync(page, animated);
        }

        public async Task NavigateModalAsync(string pageKey, bool animated = true)
        {
            await NavigateModalAsync(pageKey, null, animated);
        }

        public async Task NavigateModalAsync(string pageKey, object parameter, bool animated = true)
        {
            var page = GetPage(pageKey, parameter);
            NavigationPage.SetHasNavigationBar(page, false);
            var modalNavigationPage = new NavigationPage(page);
            await CurrentNavigationPage.Navigation.PushModalAsync(modalNavigationPage, animated);
            _navigationPageStack.Push(modalNavigationPage);
        }

        public async Task NavigateModalAsync(string pageKey, Action<object> actionParameter, object parameter = null, bool animated = true)
        {
            var page = GetPage(pageKey, parameter, actionParameter);
            NavigationPage.SetHasNavigationBar(page, false);
            var modalNavigationPage = new NavigationPage(page);
            await CurrentNavigationPage.Navigation.PushModalAsync(modalNavigationPage, animated);
            _navigationPageStack.Push(modalNavigationPage);
        }

        public void NavigateDetails(string pageKey, object parameter = null)
        {
            var detailsPage = GetPage(pageKey, parameter);
            var navigationStack = CurrentNavigationPage.Navigation;
            if (navigationStack.NavigationStack.Count > 0)
            {
                foreach (var page in navigationStack.NavigationStack)
                {
                    if (page is NavigationPage)
                    {
                        if ((page as NavigationPage).CurrentPage is MasterDetailPage)
                        {
                            ((page as NavigationPage).CurrentPage as MasterDetailPage).Detail = detailsPage;
                        }
                    }
                    else if (page is MasterDetailPage)
                    {
                        (page as MasterDetailPage).Detail = detailsPage;
                    }
                }
                return;
            }
            return;
        }

        public Type ViewModelByKey(string pageKey)
        {
            if (!_viewModelByKey.ContainsKey(pageKey))
            {
                return null;
            }
            var type = _viewModelByKey[pageKey];

            return type;
        }

        public Page GetPage(string pageKey, object parameter = null, Action<object> actionParameter = null)
        {
            try
            {
                lock (_sync)
                {
                    if (!_pagesByKey.ContainsKey(pageKey))
                    {
                        throw new ArgumentException($"NO SUCH PAGE: {pageKey}. DID YOU FORGET TO CALL: NavigationService.Configure?");
                    }

                    var type = _pagesByKey[pageKey];
                    ConstructorInfo constructor;
                    object[] parameters;

                    if (parameter == null)
                    {
                        constructor = type.GetTypeInfo()
                            .DeclaredConstructors
                            .FirstOrDefault(c => !c.GetParameters().Any());

                        parameters = new object[] { };
                    }
                    else
                    {
                        constructor = type.GetTypeInfo()
                            .DeclaredConstructors
                            .FirstOrDefault(c =>
                            {
                                var pageParameters = c.GetParameters();
                                return pageParameters.Length == 1;
                            });

                        parameters = new[]
                        {
                            parameter
                        };
                    }

                    if (constructor == null)
                    {
                        throw new InvalidOperationException("NO SUITABLE CONSTRUCTOR FOUND FOR PAGE " + pageKey);
                    }

                    var page = constructor.Invoke(parameters) as Page;

                    if (page is BaseContentPage && actionParameter != null)
                    {
                        (page as BaseContentPage).CallbackAction = actionParameter;
                    }

                    return page;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task OpenPopUp(PopupPage page)
        {
            await CurrentNavigationPage.Navigation.PushPopupAsync(page, true);
        }

        public async Task ClosePopUp()
        {
            await CurrentNavigationPage.Navigation.PopPopupAsync();
        }

        public async Task ShowLoadingIndicator()
        {
            await OpenPopUp(new LoadingPopup());
        }

        public async Task HideLoadingIndicator()
        {
            await ClosePopUp();
        }

        public async Task<string> DisplayAlertController(string[] values = null)
        {
            string selectionResult = await CurrentNavigationPage.DisplayActionSheet(null, "Cancelar", null, values);
            return selectionResult;
        }
    }

}
