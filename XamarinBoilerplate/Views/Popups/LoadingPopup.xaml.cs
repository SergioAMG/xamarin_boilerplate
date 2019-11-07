using Xamarin.Forms.Xaml;
using XamarinBoilerplate.ViewModels.Popups;

namespace XamarinBoilerplate.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public LoadingPopup()
        {
            InitializeComponent();
            BindingContext = new LoadingPopUpViewModel();
        }
    }
}