using DataManagers.Interfaces;
using Plugin.Fingerprint;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.Views;

namespace XamarinBoilerplate.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _login;
        private string _password;
        private ICommand _loginCommand;
        private ICommand _useFingerprintCommand;

        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                if (_login != value)
                {
                    _login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public StackOrientation ContainerOrientation
        {
            get
            {
                return (DeviceManager.IsLandscape) ? StackOrientation.Horizontal : StackOrientation.Vertical;
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new CommandExtended(ExecuteLoginCommandAsync));

            }
        }

        public ICommand UseFingerprintCommand
        {
            get
            {
                return _useFingerprintCommand ?? (_useFingerprintCommand = new CommandExtended(ExecuteUseFingerprintCommandAsync));

            }
        }

        public LoginViewModel(IDataService dataManager = null) : base(dataManager)
        {
            if (dataManager != null)
            {
                DataManager = dataManager;
            }
        }

        public void RefreshOrientation()
        {
            OnPropertyChanged(nameof(ContainerOrientation));
        }

        public async Task ExecuteUseFingerprintCommandAsync()
        {
            bool allowAlternativeAuthentication = IsIOS;
            bool isFingerPrintAvailable = await CrossFingerprint.Current.IsAvailableAsync(allowAlternativeAuthentication);
            if (isFingerPrintAvailable)
            {
                var authentication = await CrossFingerprint.Current.AuthenticateAsync(Localization.AppResources.FingerprintRequest);
                if (authentication.Authenticated)
                {
                    await NavigationService.ShowLoadingIndicator();
                    NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());
                }
                else
                {
                    await NavigationService.CurrentPage.DisplayAlert(
                        Localization.AppResources.Error,
                        Localization.AppResources.FingerprintError,
                        Localization.AppResources.Okay);

                }
            }
            else
            {
                await NavigationService.CurrentPage.DisplayAlert(
                        Localization.AppResources.Error,
                        Localization.AppResources.FingerprintNotAvailable,
                        Localization.AppResources.Okay);
            }
        }

        public async Task ExecuteLoginCommandAsync()
        {
            if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password))
            {
                await NavigationService.ShowLoadingIndicator();
                NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());
            }
            else
            {
                await NavigationService.CurrentPage.DisplayAlert(
                    Localization.AppResources.Error,
                    Localization.AppResources.LoginError,
                    Localization.AppResources.Okay);
            }
        }
    }
}
