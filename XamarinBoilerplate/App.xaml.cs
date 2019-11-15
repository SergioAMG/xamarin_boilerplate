using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBoilerplate.Interfaces;
using XamarinBoilerplate.Services;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels;
using XamarinBoilerplate.ViewModels.Wizzard;
using XamarinBoilerplate.Views;
using XamarinBoilerplate.Views.Wizzard;

namespace XamarinBoilerplate
{
    public partial class App : Application
    {
        public static INavigationService NavigationService { get; } = new ViewNavigationService();
        public static double ScreenHeight;
        public static double ScreenWidth;

        public App()
        {
            InitializeComponent();
            //SetLocalization();
            RegisterPages();
            if (!UnitTestingManager.IsRunningFromNUnit)
            {
                IdentifyDevice();
                LaunchApp();
            }
        }

        public void LaunchApp()
        {
            bool isWizzardCompleted = Preferences.Get(Constants.WizzardComplete, false);
            if (isWizzardCompleted)
            {
                NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());
            }
            else
            {
                NavigationService.SetRootPage(nameof(StepOnePage), new StepOneViewModel());
            }
        }

        private void RegisterPages()
        {
            NavigationService.Configure(nameof(StepOnePage), typeof(StepOnePage));
            NavigationService.Configure(nameof(StepTwoPage), typeof(StepTwoPage));
            NavigationService.Configure(nameof(StepThreePage), typeof(StepThreePage));
            NavigationService.Configure(nameof(DashboardPage), typeof(DashboardPage));
            NavigationService.Configure(nameof(MenuPage), typeof(MenuPage));
            NavigationService.Configure(nameof(HomePage), typeof(HomePage));

            NavigationService.BindViewModel<StepOneViewModel, StepOnePage>();
            NavigationService.BindViewModel<StepTwoViewModel, StepTwoPage>();
            NavigationService.BindViewModel<StepThreeViewModel, StepThreePage>();
            NavigationService.BindViewModel<MenuViewModel, MenuPage>();
            NavigationService.BindViewModel<DashboardViewModel, DashboardPage>();
            NavigationService.BindViewModel<HomeViewModel, HomePage>();
        }

        public void IdentifyDevice()
        {
            DeviceManager.Platform = DeviceInfo.Platform.ToString();
            DeviceManager.Manufacturer = DeviceInfo.Manufacturer;
            DeviceManager.Version = DeviceInfo.VersionString;
            DeviceManager.Idiom = DeviceInfo.Idiom.ToString();
            DeviceManager.Device = DeviceInfo.Model;
            DeviceManager.Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
        }

        public void SetLocalization()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
            //CultureInfo.CurrentCulture = new CultureInfo("es-MX", false);
            Localization.AppResources.Culture = CultureInfo.CurrentCulture;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
