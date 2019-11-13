using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBoilerplate.Interfaces;
using XamarinBoilerplate.Services;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels.Wizzard;
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
            RegisterPages();
            if (!UnitTestingManager.IsRunningFromNUnit)
            {
                IdentifyDevice();
            }
            LaunchApp();
        }

        public void LaunchApp()
        {
            NavigationService.SetRootPage(nameof(StepOnePage), new StepOneViewModel());
        }

        private void RegisterPages()
        {
            NavigationService.Configure(nameof(StepOnePage), typeof(StepOnePage));
            NavigationService.Configure(nameof(StepTwoPage), typeof(StepTwoPage));
            NavigationService.Configure(nameof(StepThreePage), typeof(StepThreePage));

            NavigationService.BindViewModel<StepOneViewModel, StepOnePage>();
            NavigationService.BindViewModel<StepTwoViewModel, StepTwoPage>();
            NavigationService.BindViewModel<StepThreeViewModel, StepThreePage>();

            /*
            NavigationService.Configure(nameof(NoPermissionsPage), typeof(NoPermissionsPage));
            NavigationService.Configure(nameof(DashboardPage), typeof(DashboardPage));
            NavigationService.Configure(nameof(HomePage), typeof(HomePage));
            NavigationService.Configure(nameof(MenuPage), typeof(MenuPage));
            NavigationService.Configure(nameof(DataSimulatorPage), typeof(DataSimulatorPage));
            NavigationService.Configure(nameof(ContactPage), typeof(ContactPage));
            NavigationService.Configure(nameof(CoverageMapPage), typeof(CoverageMapPage));
            NavigationService.Configure(nameof(StepTwoPage), typeof(StepTwoPage));
            NavigationService.Configure(nameof(StepThreePage), typeof(StepThreePage));
            NavigationService.Configure(nameof(LiveChatPage), typeof(LiveChatPage));
            NavigationService.Configure(nameof(NewsReaderPage), typeof(NewsReaderPage));
            NavigationService.Configure(nameof(CustomTabbedPage), typeof(CustomTabbedPage));
            NavigationService.Configure(nameof(SearchPage), typeof(SearchPage));
            NavigationService.Configure(nameof(LoginPage), typeof(LoginPage));

            NavigationService.BindViewModel<NoPermissionsViewModel, NoPermissionsPage>();
            NavigationService.BindViewModel<HomeViewModel, HomePage>();
            NavigationService.BindViewModel<MenuViewModel, MenuPage>();
            NavigationService.BindViewModel<DataSimulatorViewModel, DataSimulatorPage>();
            NavigationService.BindViewModel<ContactViewModel, ContactPage>();
            NavigationService.BindViewModel<CoverageMapViewModel, CoverageMapPage>();
            NavigationService.BindViewModel<StepTwoViewModel, StepTwoPage>();
            NavigationService.BindViewModel<StepThreeViewModel, StepThreePage>();
            NavigationService.BindViewModel<LiveChatViewModel, LiveChatPage>();
            NavigationService.BindViewModel<NewsReaderViewModel, NewsReaderPage>();
            NavigationService.BindViewModel<CustomTabbedViewModel, CustomTabbedPage>();
            NavigationService.BindViewModel<SearchViewModel, SearchPage>();
            NavigationService.BindViewModel<LoginViewModel, LoginPage>();
            */
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
