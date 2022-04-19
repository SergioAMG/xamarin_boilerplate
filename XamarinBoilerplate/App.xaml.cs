using DataManagers;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBoilerplate.Interfaces;
using XamarinBoilerplate.Services;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels;
using XamarinBoilerplate.ViewModels.Samples;
using XamarinBoilerplate.ViewModels.Wizzard;
using XamarinBoilerplate.Views;
using XamarinBoilerplate.Views.Samples;
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
            else
            {
                bool mockUsingIOS = true;
                MockIdentifyDevice(mockUsingIOS);
            }
        }

        public void LaunchApp()
        {
            bool isWizzardCompleted = Preferences.Get(Constants.WizzardComplete, false);
            if (isWizzardCompleted)
            {
                bool isLoggedIn = Preferences.Get(Constants.LoggedIn, false);
                if (isLoggedIn)
                {
                    NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());
                }
                else
                {
                    NavigationService.SetRootPage(nameof(LoginPage), new LoginViewModel());
                }
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
            NavigationService.Configure(nameof(LoginPage), typeof(LoginPage));
            NavigationService.Configure(nameof(NewsReaderPage), typeof(NewsReaderPage));
            NavigationService.Configure(nameof(SamplesMenuPage), typeof(SamplesMenuPage));
            NavigationService.Configure(nameof(CarouselSamplePage), typeof(CarouselSamplePage));
            NavigationService.Configure(nameof(CollectionViewSamplePage), typeof(CollectionViewSamplePage));

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
            NavigationService.BindViewModel<LoginViewModel, LoginPage>();
            NavigationService.BindViewModel<NewsReaderViewModel, NewsReaderPage>();
            NavigationService.BindViewModel<SamplesMenuViewModel, SamplesMenuPage>();
            NavigationService.BindViewModel<CarouselSampleViewModel, CarouselSamplePage>();
            NavigationService.BindViewModel<CollectionViewSampleViewModel, CollectionViewSamplePage>();
        }

        public void IdentifyDevice()
        {
            DeviceManager.Platform = DeviceInfo.Platform.ToString();
            DeviceManager.Manufacturer = DeviceInfo.Manufacturer;
            DeviceManager.Version = (DeviceInfo.VersionString == null) ? "0" : DeviceInfo.VersionString;
            DeviceManager.Idiom = DeviceInfo.Idiom.ToString();
            DeviceManager.Device = DeviceInfo.Model;
            DeviceManager.Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
        }

        public void MockIdentifyDevice(bool useIOS)
        {
            if (useIOS)
            {
                DeviceManager.Platform = "iOS";
                DeviceManager.Manufacturer = "Apple";
                DeviceManager.Version = "13.3";
                DeviceManager.Idiom = "Phone";
                DeviceManager.Device = "x86_64";
                DeviceManager.Orientation = "Portrait";
            }
            else
            {
                DeviceManager.Platform = "Android";
                DeviceManager.Manufacturer = "Google";
                DeviceManager.Version = "10";
                DeviceManager.Idiom = "Phone";
                DeviceManager.Device = "Android SDK built for x86_64";
                DeviceManager.Orientation = "Portrait";
            }
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

        protected override async void OnStart()
        {
            await OpenOrCreateLocalDatabaseAndConnectToIt();            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private async Task OpenOrCreateLocalDatabaseAndConnectToIt()
        {
            string folderPath = string.Empty, databaseName = string.Empty, databasePath = string.Empty;

            try
            {
                folderPath = DependencyService.Get<IFileManager>().PersonalFolderPath;
                databaseName = "XamarinBoilerplate.db";
                databasePath = Path.Combine(folderPath, databaseName);
            }
            catch (Exception dbDirectoryEx)
            {
                System.Diagnostics.Debug.WriteLine("Database folder Exception. InnerException: " + dbDirectoryEx.InnerException.ToString());
                System.Diagnostics.Debug.WriteLine("Database folder Exception. StackTrace: " + dbDirectoryEx.InnerException.StackTrace.ToString());
            }

            if (File.Exists(databasePath))
            {
                try
                {
                    DataManager.Instance.ConnectToLocalDatabase(databasePath);
                }
                catch (Exception dbConnectionEx)
                {
                    System.Diagnostics.Debug.WriteLine("Database connection Exception. InnerException: " + dbConnectionEx.InnerException.ToString());
                    System.Diagnostics.Debug.WriteLine("Database connection Exception. StackTrace: " + dbConnectionEx.InnerException.StackTrace.ToString());
                }
            }
            else
            {
                try
                {
                    Stream initialDatabaseStream = DependencyService.Get<IFileManager>().GetAssetOrResourceFile(databaseName);
                    await DependencyService.Get<IFileManager>().WriteNewFileFromStream(initialDatabaseStream, databasePath);
                    DataManager.Instance.ConnectToLocalDatabase(databasePath);
                }
                catch (Exception dbCreationEx)
                {
                    System.Diagnostics.Debug.WriteLine("Database creation Exception. InnerException: " + dbCreationEx.InnerException.ToString());
                    System.Diagnostics.Debug.WriteLine("Database creation Exception. StackTrace: " + dbCreationEx.InnerException.StackTrace.ToString());
                }
            }
        }
    }
}
