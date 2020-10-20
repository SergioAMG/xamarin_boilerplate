using Microsoft.VisualStudio.TestTools.UnitTesting;
using XamarinBoilerplate.ViewModels;
using Shouldly;
using System.Threading.Tasks;
using XamarinBoilerplate.Views;
using Xamarin.Forms;
using XamarinBoilerplate.Views.Wizzard;
using XamarinBoilerplate.Views.Samples;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.Enums;

namespace XamarinBoilerplate.UnitTesting.ViewModels
{
    [TestClass]
    public class MenuViewModelTests : BaseViewModelTest
    {
        private MenuViewModel viewModel;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        #region General Tests

        [TestMethod]
        public void ShouldAppVersionNotBeNull()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act

            //assert
            viewModel.AppVersion.ShouldNotBeNullOrEmpty();
        }

        [TestMethod]
        public void ShouldRefreshMainMarginsAdjustLeftMainMargin()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();
            Thickness originalMargin = viewModel.LeftMainMargin;
            DeviceManager.Orientation = Devices.Landscape.ToString();
            viewModel.RefreshMainMargins();
            Thickness modifiedMargin = viewModel.LeftMainMargin;

            //assert
            Assert.AreNotEqual(modifiedMargin, originalMargin);
        }

        [TestMethod]
        public void ShouldRefreshMainMarginsAdjustFooterMargin()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 960;
            App.ScreenWidth = 640;
            Thickness originalMargin = viewModel.FooterMargin;
            App.ScreenHeight = 2688;
            App.ScreenWidth = 1242;
            viewModel.RefreshMainMargins();
            Thickness modifiedMargin = viewModel.FooterMargin;

            //assert
            Assert.AreNotEqual(modifiedMargin, originalMargin);
        }

        [TestMethod]
        public void ShouldRefreshMainMarginsAdjustBottomPanelHeight()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1334;
            App.ScreenWidth = 750;
            DeviceManager.Orientation = Devices.Portrait.ToString();
            int originalHeight = viewModel.BottomPanelHeight;
            DeviceManager.Orientation = Devices.Landscape.ToString();
            viewModel.RefreshMainMargins();
            int modifiedHeight = viewModel.BottomPanelHeight;

            //assert
            Assert.AreNotEqual(modifiedHeight, originalHeight);
        }

        [TestMethod]
        public void ShouldRefreshMainMarginsAdjustCloseButtonMargin()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();
            DeviceManager.Device = Devices.Android.ToString();
            Thickness originalMargin = viewModel.CloseButtonMargin;
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Device = Devices.iOS.ToString();
            viewModel.RefreshMainMargins();
            Thickness modifiedMargin = viewModel.CloseButtonMargin;

            //assert
            Assert.AreNotEqual(modifiedMargin, originalMargin);
        }

        [TestMethod]
        public void ShouldRefreshMainMarginsAdjustBottomTextSpacing()
        {
            //arrange
            viewModel = new MenuViewModel();
            int originalBottomTextSpacing = viewModel.BottomTextSpacing;

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 1334;
            App.ScreenWidth = 750;
            DeviceManager.Orientation = Devices.Landscape.ToString();
            viewModel.RefreshMainMargins();
            int modifiedBottomTextSpacing = viewModel.BottomTextSpacing;

            //assert
            Assert.AreNotEqual(modifiedBottomTextSpacing, originalBottomTextSpacing);
        }

        [TestMethod]
        public void ShouldRefreshMainMarginsAdjustBottomPadding()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 2224;
            App.ScreenWidth = 1668;
            Thickness originalMargin = viewModel.BottomPadding;
            App.ScreenHeight = 2436;
            App.ScreenWidth = 1125;
            viewModel.RefreshMainMargins();
            Thickness modifiedMargin = viewModel.BottomPadding;

            //assert
            Assert.AreNotEqual(modifiedMargin, originalMargin);
        }

        [TestMethod]
        public void ShouldRefreshMainMarginsChangeVisibilityOfCloseButton()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();
            DeviceManager.Idiom = Devices.Tablet.ToString();
            bool originalVisibility = viewModel.IsCloseButtonVisible;
            DeviceManager.Orientation = Devices.Portrait.ToString();
            DeviceManager.Idiom = Devices.Phone.ToString();
            viewModel.RefreshMainMargins();
            bool modifiedVisibility = viewModel.IsCloseButtonVisible;

            //assert
            Assert.AreNotEqual(modifiedVisibility, originalVisibility);
        }

        #endregion

        #region Navigation Tests

[TestMethod]
        public void ShouldCloseIconCloseNavigationDrawer()
        {
            //arrange
            viewModel = new MenuViewModel();
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());
            viewModel.NavigationService.OpenDrawer();

            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteCloseCommandAsync();
            }).GetAwaiter().GetResult();

            //assert
            NUnit.Framework.Assert.IsFalse(viewModel.NavigationService.IsDrawerOpen());
        }

        [TestMethod]
        public void ShouldGoToContactCommandSendUserToContactPage()
        {
            //arrange
            viewModel = new MenuViewModel();
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());
            
            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteGoToContactAsync();
            }).GetAwaiter().GetResult();
            Page currentPage  = viewModel.NavigationService.GetCurrentDetailsPage();
            Page targetPage = new ContactPage();

            //assert
            NUnit.Framework.Assert.AreEqual(currentPage.Title, targetPage.Title);
        }

        [TestMethod]
        public void ShouldGoToWizzardStep1CommandSendUserToWizzardStep1Page()
        {
            //arrange
            viewModel = new MenuViewModel();
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());
            
            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteGoToWizzardStep1CommandAsync();
            }).GetAwaiter().GetResult();
            Page currentPage = viewModel.NavigationService.CurrentPage;
            Page targetPage = new StepOnePage();

            //assert
            NUnit.Framework.Assert.AreEqual(currentPage.Title, targetPage.Title);
        }

        [TestMethod]
        public void ShouldGoToSamplesCommandSendUserToSamplesPage()
        {
            //arrange
            viewModel = new MenuViewModel();
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());

            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteGoToSamplesCommandAsync();
            }).GetAwaiter().GetResult();

            Page currentMasterPage = viewModel.NavigationService.CurrentMasterDetailPage;
            var currentDetailsPage = viewModel.NavigationService.CurrentMasterDetailPage.Detail;
            int indexOfDetailsPage = 0;
            Page targetPage = new SamplesMenuPage(indexOfDetailsPage);

            //assert
            NUnit.Framework.Assert.AreEqual(currentDetailsPage.Title, targetPage.Title);
        }

        [TestMethod]
        public void ShouldGoToLogoutCommandSendUserToLoginPage()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteGoToLogoutCommandAsync();
            }).GetAwaiter().GetResult();

            Page currentPage = viewModel.NavigationService.CurrentPage;
            Page targetPage = new LoginPage();

            //assert
            Assert.AreEqual(currentPage.Title, targetPage.Title);
        }

        #endregion

        #region Close Button Tests

        [TestMethod]
        public void ShouldCloseButtonHaveCustomMarginWhenAndroid()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, 0, 15, 0), viewModel.CloseButtonMargin);
        }

        [TestMethod]
        public void ShouldCloseButtonHaveCustomMarginWhenIOSSupportedVersion()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, 1, 15, 0), viewModel.CloseButtonMargin);
        }

        [TestMethod]
        public void ShouldCloseButtonHaveCustomMarginWhenIOSNotSupportedVersionInPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "10.0";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Portrait.ToString(); 
            
            //assert
            Assert.AreEqual(new Thickness(0, 20, 15, 0), viewModel.CloseButtonMargin);
        }

        [TestMethod]
        public void ShouldCloseButtonHaveCustomMarginWhenIOSNotSupportedVersionInLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "10.0";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, 0, 15, 0), viewModel.CloseButtonMargin);
        }

        [TestMethod]
        public void ShouldCloseButtonNotBeVisibleWhenInTabletAndInLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Idiom = Devices.Tablet.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            Assert.IsFalse(viewModel.IsCloseButtonVisible);
        }

        [TestMethod]
        public void ShouldCloseButtonBeVisibleWhenNotInTabletAndNotInLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Idiom = Devices.Phone.ToString();
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            Assert.IsTrue(viewModel.IsCloseButtonVisible);
        }

        #endregion

        #region Footer Margin Tests

        [TestMethod]
        public void ShouldFooterMarginBeZeroWhenInAndroid()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.FooterMargin);
        }

        [TestMethod]
        public void ShouldFooterMarginBeZeroWhenInLowerVersionsOfIOS()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "10.0";
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.FooterMargin);
        }

        [TestMethod]
        public void ShouldFooterMarginBeZeroWhenInIOSSupportedVersionAndiPhone4()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 960;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.FooterMargin);
        }

        [TestMethod]
        public void ShouldFooterMarginBeZeroWhenInIOSSupportedVersionAndiPhoneSE_5()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1136;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.FooterMargin);
        }

        [TestMethod]
        public void ShouldFooterMarginBeZeroWhenInIOSSupportedVersionAndiPhone8_7_6()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1334;
            App.ScreenWidth = 750;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.FooterMargin);
        }

        [TestMethod]
        public void ShouldFooterMarginBeZeroWhenInIOSSupportedVersionAndiPhone8Plus_7Plus_6SPlus_6Plus()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1242;
            App.ScreenWidth = 2208;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.FooterMargin);
        }

        [TestMethod]
        public void ShouldFooterMarginBeNegativeWhenInIOSSupportedVersionAndiPhoneX_XS_11Pro()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1125;
            App.ScreenWidth = 2436;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, -35), viewModel.FooterMargin);
        }

        [TestMethod]
        public void ShouldFooterMarginBeNegativeWhenInIOSSupportedVersionAndiPhone11_XR()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1792;
            App.ScreenWidth = 828;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, -35), viewModel.FooterMargin);
        }

        [TestMethod]
        public void ShouldFooterMarginBeNegativeWhenInIOSSupportedVersionAndiPhone11ProMax_XSMax()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 2688;
            App.ScreenWidth = 1242;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, -35), viewModel.FooterMargin);
        }

        [TestMethod]
        public void ShouldFooterMarginBeNegativeWhenInIOSSupportedVersionAndiPad_2_Mini()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1024;
            App.ScreenWidth = 768;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, -35), viewModel.FooterMargin);
        }

        [TestMethod]
        public void ShouldFooterMarginBeNegativeWhenInIOSSupportedVersionAndiPad3_4_Air_Mini2_Mini3_Air2_Mini4_Pro_2017_2018()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 2048;
            App.ScreenWidth = 1536;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, -35), viewModel.FooterMargin);
        }

        [TestMethod]
        public void ShouldFooterMarginBeNegativeWhenInIOSSupportedVersionAndiPadProSec10()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 2224;
            App.ScreenWidth = 1668;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, -35), viewModel.FooterMargin);
        }

        [TestMethod]
        public void ShouldFooterMarginBeNegativeWhenInIOSSupportedVersionAndiPadProSec12()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 2732;
            App.ScreenWidth = 2048;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, -35), viewModel.FooterMargin);
        }

        #endregion

        #region Left Main Margin Tests

        [TestMethod]
        public void ShouldLeftMainMarginBeZeroWhenInPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.LeftMainMargin);
        }

        [TestMethod]
        public void ShouldLeftMainMarginBeZeroWhenInAndroid()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.LeftMainMargin);
        }

        [TestMethod]
        public void ShouldLeftMainMarginBeZeroWhenInLandscapeAndIOSVersionIsLowerThanSupportedOne()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.LeftMainMargin);
        }

        [TestMethod]
        public void ShouldLeftMainMarginBeZeroWhenInIOSSupportedVersionAndiPhone4()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 960;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.LeftMainMargin);
        }

        [TestMethod]
        public void ShouldLeftMainMarginBeZeroWhenInIOSSupportedVersionAndiPhoneSE_5()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1136;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.LeftMainMargin);
        }

        [TestMethod]
        public void ShouldLeftMainMarginBeZeroWhenInIOSSupportedVersionAndiPhone8_7_6()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1334;
            App.ScreenWidth = 750;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.LeftMainMargin);
        }

        [TestMethod]
        public void ShouldLeftMainMarginBeZeroWhenInIOSSupportedVersionAndiPhone8Plus_7Plus_6SPlus_6Plus()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1242;
            App.ScreenWidth = 2208;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.LeftMainMargin);
        }

        [TestMethod]
        public void ShouldLeftMainMarginBeNegativeWhenInIOSSupportedVersionAndiPhoneX_XS_11Pro()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2436;
            App.ScreenWidth = 1125;

            //assert
            Assert.AreEqual(new Thickness(-45, 0, 0, 0), viewModel.LeftMainMargin);
        }

        [TestMethod]
        public void ShouldLeftMainMarginBeNegativeWhenInIOSSupportedVersionAndiPhone11_XR()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1792;
            App.ScreenWidth = 828;

            //assert
            Assert.AreEqual(new Thickness(-45, 0, 0, 0), viewModel.LeftMainMargin);
        }

        [TestMethod]
        public void ShouldLeftMainMarginBeNegativeWhenInIOSSupportedVersionAndiPhone11ProMax_XSMax()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2688;
            App.ScreenWidth = 1242;

            //assert
            Assert.AreEqual(new Thickness(-45, 0, 0, 0), viewModel.LeftMainMargin);
        }

        [TestMethod]
        public void ShouldLeftMainMarginBeZeroWhenInIOSSupportedVersionAndiPad_2_Mini()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1024;
            App.ScreenWidth = 768;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.LeftMainMargin);
        }

        [TestMethod]
        public void ShouldLeftMainMarginBeZeroWhenInIOSSupportedVersionAndiPad3_4_Air_Mini2_Mini3_Air2_Mini4_Pro_2017_2018()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2048;
            App.ScreenWidth = 1536;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.LeftMainMargin);
        }

        [TestMethod]
        public void ShouldLeftMainMarginBeZeroWhenInIOSSupportedVersionAndiPadProSec10()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2224;
            App.ScreenWidth = 1668;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.LeftMainMargin);
        }

        [TestMethod]
        public void ShouldLeftMainMarginBeZeroWhenInIOSSupportedVersionAndiPadProSec12()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2732;
            App.ScreenWidth = 2048;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.LeftMainMargin);
        }

        #endregion

        #region Bottom Padding Tests

        [TestMethod]
        public void ShouldBottomPaddingBeCustomWhenInAndroid()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.AreEqual(new Thickness(20, 0, 0, 0), viewModel.BottomPadding);
        }

        [TestMethod]
        public void ShouldBottomPaddingBeCustomWhenInLowerVersionsOfIOS()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "10.0";
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.AreEqual(new Thickness(20, 0, 0, 0), viewModel.BottomPadding);
        }

        [TestMethod]
        public void ShouldBottomPaddingBeCustomWhenInIOSSupportedVersionAndiPhone4()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 960;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(new Thickness(20, 0, 0, 0), viewModel.BottomPadding);
        }

        [TestMethod]
        public void ShouldBottomPaddingBeCustomWhenInIOSSupportedVersionAndiPhoneSE_5()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1136;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(new Thickness(20, 0, 0, 0), viewModel.BottomPadding);
        }

        [TestMethod]
        public void ShouldBottomPaddingBeCustomWhenInIOSSupportedVersionAndiPhone8_7_6()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1334;
            App.ScreenWidth = 750;

            //assert
            Assert.AreEqual(new Thickness(20, 0, 0, 0), viewModel.BottomPadding);
        }

        [TestMethod]
        public void ShouldBottomPaddingBeCustomWhenInIOSSupportedVersionAndiPhone8Plus_7Plus_6SPlus_6Plus()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1242;
            App.ScreenWidth = 2208;

            //assert
            Assert.AreEqual(new Thickness(20, 0, 0, 0), viewModel.BottomPadding);
        }

        [TestMethod]
        public void ShouldBottomPaddingBeCustomWhenInIOSSupportedVersionAndiPhoneX_XS_11Pro()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1125;
            App.ScreenWidth = 2436;

            //assert
            Assert.AreEqual(new Thickness(20, 0, 0, 0), viewModel.BottomPadding);
        }

        [TestMethod]
        public void ShouldBottomPaddingBeCustomWhenInIOSSupportedVersionAndiPhone11_XR()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1792;
            App.ScreenWidth = 828;

            //assert
            Assert.AreEqual(new Thickness(20, 0, 0, 0), viewModel.BottomPadding);
        }

        [TestMethod]
        public void ShouldBottomPaddingBeCustomWhenInIOSSupportedVersionAndiPhone11ProMax_XSMax()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 2688;
            App.ScreenWidth = 1242;

            //assert
            Assert.AreEqual(new Thickness(20, 0, 0, 0), viewModel.BottomPadding);
        }

        [TestMethod]
        public void ShouldBottomPaddingBeCustomWhenInIOSSupportedVersionAndiPad_2_Mini()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1024;
            App.ScreenWidth = 768;

            //assert
            Assert.AreEqual(new Thickness(20, -25, 0, 0), viewModel.BottomPadding);
        }

        [TestMethod]
        public void ShouldBottomPaddingBeCustomWhenInIOSSupportedVersionAndiPad3_4_Air_Mini2_Mini3_Air2_Mini4_Pro_2017_2018()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 2048;
            App.ScreenWidth = 1536;

            //assert
            Assert.AreEqual(new Thickness(20, -25, 0, 0), viewModel.BottomPadding);
        }

        [TestMethod]
        public void ShouldBottomPaddingBeCustomWhenInIOSSupportedVersionAndiPadProSec10()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 2224;
            App.ScreenWidth = 1668;

            //assert
            Assert.AreEqual(new Thickness(20, -25, 0, 0), viewModel.BottomPadding);
        }

        [TestMethod]
        public void ShouldBottomPaddingBeCustomWhenInIOSSupportedVersionAndiPadProSec12()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 2732;
            App.ScreenWidth = 2048;

            //assert
            Assert.AreEqual(new Thickness(20, -25, 0, 0), viewModel.BottomPadding);
        }

        #endregion

        #region Bottom Text Spacing Tests

        [TestMethod]
        public void ShouldBottomTextSpacingBeZeroWhenInAndroid()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.AreEqual(0, viewModel.BottomTextSpacing);
        }

        [TestMethod]
        public void ShouldBottomTextSpacingBeCustomWhenInLowerVersionsOfIOSAndInLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.AreEqual(-5, viewModel.BottomTextSpacing);
        }

        [TestMethod]
        public void ShouldBottomTextSpacingBeZeroWhenInLowerVersionsOfIOSAndInPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.AreEqual(0, viewModel.BottomTextSpacing);
        }

        [TestMethod]
        public void ShouldBottomTextSpacingBeCustomWhenInIOSSupportedVersionAndiPhone4InPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 960;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(0, viewModel.BottomTextSpacing);
        }

        [TestMethod]
        public void ShouldBottomTextSpacingBeCustomWhenInIOSSupportedVersionAndiPhone4InLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 960;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(-5, viewModel.BottomTextSpacing);
        }

        [TestMethod]
        public void ShouldBottomTextSpacingBeCustomWhenInIOSSupportedVersionAndiPhoneSE_5InPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 1136;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(0, viewModel.BottomTextSpacing);
        }

        [TestMethod]
        public void ShouldBottomTextSpacingBeCustomWhenInIOSSupportedVersionAndiPhoneSE_5InLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1136;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(-5, viewModel.BottomTextSpacing);
        }

        [TestMethod]
        public void ShouldBottomTextSpacingBeCustomWhenInIOSSupportedVersionAndiPhone8_7_6InPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 1334;
            App.ScreenWidth = 750;

            //assert
            Assert.AreEqual(0, viewModel.BottomTextSpacing);
        }

        [TestMethod]
        public void ShouldBottomTextSpacingBeCustomWhenInIOSSupportedVersionAndiPhone8_7_6InLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1334;
            App.ScreenWidth = 750;

            //assert
            Assert.AreEqual(-5, viewModel.BottomTextSpacing);
        }

        [TestMethod]
        public void ShouldBottomTextSpacingBeZeroWhenInIOSSupportedVersionAndiPhone8Plus_7Plus_6SPlus_6Plus()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1242;
            App.ScreenWidth = 2208;

            //assert
            Assert.AreEqual(0, viewModel.BottomTextSpacing);
        }

        [TestMethod]
        public void ShouldBottomTextSpacingBeZeroWhenInIOSSupportedVersionAndiPhoneX_XS_11Pro()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1125;
            App.ScreenWidth = 2436;

            //assert
            Assert.AreEqual(0, viewModel.BottomTextSpacing);
        }

        [TestMethod]
        public void ShouldBottomTextSpacingBeZeroWhenInIOSSupportedVersionAndiPhone11_XR()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1792;
            App.ScreenWidth = 828;

            //assert
            Assert.AreEqual(0, viewModel.BottomTextSpacing);
        }

        [TestMethod]
        public void ShouldBottomTextSpacingBeZeroWhenInIOSSupportedVersionAndiPhone11ProMax_XSMax()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 2688;
            App.ScreenWidth = 1242;

            //assert
            Assert.AreEqual(0, viewModel.BottomTextSpacing);
        }

        [TestMethod]
        public void ShouldBottomTextSpacingBeZeroWhenInIOSSupportedVersionAndiPad_2_Mini()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1024;
            App.ScreenWidth = 768;

            //assert
            Assert.AreEqual(0, viewModel.BottomTextSpacing);
        }

        [TestMethod]
        public void ShouldBottomTextSpacingBeZeroWhenInIOSSupportedVersionAndiPad3_4_Air_Mini2_Mini3_Air2_Mini4_Pro_2017_2018()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 2048;
            App.ScreenWidth = 1536;

            //assert
            Assert.AreEqual(0, viewModel.BottomTextSpacing);
        }

        [TestMethod]
        public void ShouldBottomTextSpacingBeZeroWhenInIOSSupportedVersionAndiPadProSec10()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 2224;
            App.ScreenWidth = 1668;

            //assert
            Assert.AreEqual(0, viewModel.BottomTextSpacing);
        }

        [TestMethod]
        public void ShouldBottomTextSpacingBeZeroWhenInIOSSupportedVersionAndiPadProSec12()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 2732;
            App.ScreenWidth = 2048;

            //assert
            Assert.AreEqual(0, viewModel.BottomTextSpacing);
        }

        #endregion

        #region Bottom Panel Height

        [TestMethod]
        public void ShouldBottomPanelHeightbeCustomWhenInAndroidAndInPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            Assert.AreEqual(57, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightbeCustomWhenInAndroidAndInLandscapeAndPhone()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            DeviceManager.Idiom = Devices.Phone.ToString();

            //assert
            Assert.AreEqual(60, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightbeCustomWhenInAndroidAndInLandscapeAndTablet()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            DeviceManager.Idiom = Devices.Tablet.ToString();

            //assert
            Assert.AreEqual(56, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPhone4AndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 960;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(51, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPhone4AndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 960;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(33, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPhoneSE_5AndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 1136;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(51, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPhoneSE_5AndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1136;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(33, viewModel.BottomPanelHeight);
        }
        
        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPhone8_7_6AndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 1334;
            App.ScreenWidth = 750;

            //assert
            Assert.AreEqual(50, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPhone8_7_6AndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1334;
            App.ScreenWidth = 750;

            //assert
            Assert.AreEqual(33, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPhone8Plus_7Plus_6SPlus_6Plus()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            App.ScreenHeight = 2208;
            App.ScreenWidth = 1242;

            //assert
            Assert.AreEqual(50, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPhoneX_XS_11ProAndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 2436;
            App.ScreenWidth = 1125;

            //assert
            Assert.AreEqual(85, viewModel.BottomPanelHeight);
        }
        
        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPhoneX_XS_11ProAndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2436;
            App.ScreenWidth = 1125;

            //assert
            Assert.AreEqual(75, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPhone11_XRAndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 1792;
            App.ScreenWidth = 828;

            //assert
            Assert.AreEqual(85, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPhone11_XRAndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1792;
            App.ScreenWidth = 828;

            //assert
            Assert.AreEqual(91, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPhone11ProMax_XSMaxAndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 2688;
            App.ScreenWidth = 1242;

            //assert
            Assert.AreEqual(85, viewModel.BottomPanelHeight);
        }
        
        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPhone11ProMax_XSMaxAndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2688;
            App.ScreenWidth = 1242;

            //assert
            Assert.AreEqual(91, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPad_2_MiniAndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 1024;
            App.ScreenWidth = 768;

            //assert
            Assert.AreEqual(84, viewModel.BottomPanelHeight);
        }
        
        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPad_2_MiniAndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1024;
            App.ScreenWidth = 768;

            //assert
            Assert.AreEqual(86, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPad3_4_Air_Mini2_Mini3_Air2_Mini4_Pro_2017_2018AndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 2048;
            App.ScreenWidth = 1536;

            //assert
            Assert.AreEqual(84, viewModel.BottomPanelHeight);
        }
        
        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPad3_4_Air_Mini2_Mini3_Air2_Mini4_Pro_2017_2018AndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2048;
            App.ScreenWidth = 1536;

            //assert
            Assert.AreEqual(86, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPadProSec10AndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 2224;
            App.ScreenWidth = 1668;

            //assert
            Assert.AreEqual(84, viewModel.BottomPanelHeight);
        }
        
        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPadProSec10AndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2224;
            App.ScreenWidth = 1668;

            //assert
            Assert.AreEqual(86, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPadProSec12AndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 2732;
            App.ScreenWidth = 2048;

            //assert
            Assert.AreEqual(84, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInIOSSupportedVersionAndiPadProSec12AndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "13.3";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2732;
            App.ScreenWidth = 2048;

            //assert
            Assert.AreEqual(86, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInLowerVersionsOfIOSAndiPhone4AndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 960;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(51, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInLowerVersionsOfIOSAndiPhone4AndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 960;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(75, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInLowerVersionsOfIOSAndiPhoneSE_5AndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 1136;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(51, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInLowerVersionsOfIOSAndiPhoneSE_5AndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1136;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(75, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInLowerVersionsOfIOSAndiPhone8_7_6AndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 1334;
            App.ScreenWidth = 750;

            //assert
            Assert.AreEqual(50, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInLowerVersionsOfIOSAndiPhone8_7_6AndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1334;
            App.ScreenWidth = 750;

            //assert
            Assert.AreEqual(53, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInLowerVersionsOfIOSAndiPhone8Plus_7Plus_6SPlus_6PlusAndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 2208;
            App.ScreenWidth = 1242;

            //assert
            Assert.AreEqual(50, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInLowerVersionsOfIOSAndiPhone8Plus_7Plus_6SPlus_6PlusAndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2208;
            App.ScreenWidth = 1242;

            //assert
            Assert.AreEqual(51, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInLowerVersionsOfIOSAndiPad_2_MiniAndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 1024;
            App.ScreenWidth = 768;

            //assert
            Assert.AreEqual(49, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInLowerVersionsOfIOSAndiPad_2_MiniAndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1024;
            App.ScreenWidth = 768;

            //assert
            Assert.AreEqual(50, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInLowerVersionsOfIOSAndiPad3_4_Air_Mini2_Mini3_Air2_Mini4_Pro_2017_2018AndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 2048;
            App.ScreenWidth = 1536;

            //assert
            Assert.AreEqual(49, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInLowerVersionsOfIOSAndiPad3_4_Air_Mini2_Mini3_Air2_Mini4_Pro_2017_2018AndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2048;
            App.ScreenWidth = 1536;

            //assert
            Assert.AreEqual(50, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInLowerVersionsOfIOSAndiPadProSec10AndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 2224;
            App.ScreenWidth = 1668;

            //assert
            Assert.AreEqual(49, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInLowerVersionsOfIOSAndiPadProSec10AndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2224;
            App.ScreenWidth = 1668;

            //assert
            Assert.AreEqual(51, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInLowerVersionsOfIOSAndiPadProSec12AndPortrait()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 2732;
            App.ScreenWidth = 2048;

            //assert
            Assert.AreEqual(49, viewModel.BottomPanelHeight);
        }

        [TestMethod]
        public void ShouldBottomPanelHeightBeCustomWhenInLowerVersionsOfIOSAndiPadProSec12AndLandscape()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Version = "10.0";
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2732;
            App.ScreenWidth = 2048;

            //assert
            Assert.AreEqual(51, viewModel.BottomPanelHeight);
        }

        #endregion
    }
}