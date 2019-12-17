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
                SetScreenDimentions();
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
            NavigationService.Configure(nameof(MapPage), typeof(MapPage));
            NavigationService.Configure(nameof(DataUsagePage), typeof(DataUsagePage));
            NavigationService.Configure(nameof(CustomTabbedPage), typeof(CustomTabbedPage));
            NavigationService.Configure(nameof(ContactPage), typeof(ContactPage));

            NavigationService.BindViewModel<StepOneViewModel, StepOnePage>();
            NavigationService.BindViewModel<StepTwoViewModel, StepTwoPage>();
            NavigationService.BindViewModel<StepThreeViewModel, StepThreePage>();
            NavigationService.BindViewModel<MenuViewModel, MenuPage>();
            NavigationService.BindViewModel<DashboardViewModel, DashboardPage>();
            NavigationService.BindViewModel<HomeViewModel, HomePage>();
            NavigationService.BindViewModel<MapViewModel, MapPage>();
            NavigationService.BindViewModel<DataUsageViewModel, DataUsagePage>();
            NavigationService.BindViewModel<CustomTabbedViewModel, CustomTabbedPage>();
            NavigationService.BindViewModel<ContactViewModel, ContactPage>();
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

        public void SetScreenDimentions()
        {
            ScreenWidth = DeviceDisplay.MainDisplayInfo.Width;
            ScreenHeight = DeviceDisplay.MainDisplayInfo.Height;
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
