using Rg.Plugins.Popup.Pages;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBoilerplate.ViewModels;
using XamarinBoilerplate.Views;

namespace XamarinBoilerplate.Interfaces
{
    public interface INavigationService
    {
        string CurrentPageKey { get; }
        BaseContentPage CurrentPage { get; }

        void BindViewModel<TVM, TPage>()
        where TVM : BaseViewModel
           where TPage : Page;

        void Configure(string pageKey, Type pageType);
        void SetRootPage(string rootPageKey, BaseViewModel ViewModel = null);
        Task GoBackAsync();
        Task OpenDrawer();
        Task CloseDrawer();
        bool IsDrawerOpen();
        Task ShowLoadingIndicator();
        Task HideLoadingIndicator();
        Task OpenPopUp(PopupPage pageKey);
        Task ClosePopUp();
        void NavigateDetails(string pageKey, object parameter = null);
        Task NavigateModalAsync(string pageKey, bool animated = true);
        Task NavigateModalAsync(string pageKey, object parameter, bool animated = true);
        Task NavigateModalAsync(string pageKey, Action<object> actionParameter, object parameter = null, bool animated = true);
        Task NavigateAsync(string pageKey, object parameter = null, bool animated = true);
        Task NavigateAsync(string pageKey, Action<object> actionParameter, object parameter = null, bool animated = true);
        Page GetPage(string pageKey, object parameter = null, Action<object> actionParameter = null);
        Type ViewModelByKey(string pageKey);
        // TODO: Work around for known Apple issue as described in the following url:
        // https://stackoverflow.com/questions/55372093/uialertcontrollers-actionsheet-gives-constraint-error-on-ios-12-2-12-3
        Task<string> DisplayAlertController(string[] values = null);
    }

}
