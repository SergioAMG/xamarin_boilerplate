using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBoilerplate.Enums;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels;
using XamarinBoilerplate.Views;

namespace XamarinBoilerplate.UnitTesting.ViewModels
{
    [TestClass]
    public class ContactViewModelTests : BaseViewModelTest
    {
        private ContactViewModel viewModel;

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

        [TestMethod]
        public void ShouldViewModelBeInitializedAndAssociated()
        {
            //arrange
            viewModel = new ContactViewModel(DataManager);

            //act

            //assert
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void ShouldInitMethodUpdateSelectedTabIndex()
        {
            //arrange
            viewModel = new ContactViewModel(DataManager);

            //act
            int selectedIndex = 1;
            viewModel.Init(selectedIndex);

            //asert
            Assert.AreEqual(viewModel.SelectedTabIndex, selectedIndex);
        }

        [TestMethod]
        public void ShouldBackFromDetailsCommandTakeYouBackToSelectedTabActive()
        {
            //arrange
            viewModel = new ContactViewModel(DataManager);

            //act
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());
            viewModel.NavigationService.NavigateAsync(nameof(CustomTabbedPage), null, false);
            int selectedTab = 1;
            viewModel.Init(selectedTab);
            Task.Run(async () =>
            {
                await viewModel.ExecuteBackFromDetailsCommandAsync();
            }).GetAwaiter().GetResult();

            //assert
            int currentTabIndex = viewModel.NavigationService.GetCurrentSelectedTabIndexOverMasterDetailPageWithTabbedPage();
            Assert.AreEqual(selectedTab, currentTabIndex);
        }

        [TestMethod]
        public void ShouldRefreshOrientationChangeTheOrientationOfTheMainContainer()
        {
            //arrange
            viewModel = new ContactViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            viewModel.MainContainerOrientation.ShouldBe(StackOrientation.Vertical);
            DeviceManager.Orientation = Devices.Landscape.ToString();
            viewModel.SetOrientationValues();
            viewModel.MainContainerOrientation.ShouldBe(StackOrientation.Horizontal);
        }

        [TestMethod]
        public void ShouldMainContainerBeHorizontalWhenDeviceOrientationIsInLandscapeMode()
        {
            //arrange
            viewModel = new ContactViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            viewModel.MainContainerOrientation.ShouldBe(StackOrientation.Horizontal);
        }

        [TestMethod]
        public void ShouldMainContainerBeVerticalWhenDeviceOrientationIsInPortraitMode()
        {
            //arrange
            viewModel = new ContactViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            viewModel.MainContainerOrientation.ShouldBe(StackOrientation.Vertical);
        }

        [TestMethod]
        public void ShouldSetNavBarVisibilityToTrueWhenBindingViewModel()
        {
            //arrange
            viewModel = new ContactViewModel(DataManager);

            //act

            //assert
            Assert.IsTrue(viewModel.IsNavBarVisible);
        }

        [TestMethod]
        public void ShouldSetOrientationValuesSetDetailsViewWidthForLandscape()
        {
            //arrange
            viewModel = new ContactViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();
            viewModel.SetOrientationValues();

            //assert
            viewModel.DetailsViewWidth.ShouldBe(App.ScreenWidth * Constants.ContactPageDetailsViewWidthFactor);
        }

        [TestMethod]
        public void ShouldSetOrientationValuesSetDetailsViewWidthForPortrait()
        {
            //arrange
            viewModel = new ContactViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();
            viewModel.SetOrientationValues();

            //assert
            viewModel.DetailsViewWidth.ShouldBe(App.ScreenWidth);
        }

        [TestMethod]
        public void ShouldBottomButtonVisibleWhenInPortraitMode()
        {
            //arrange
            viewModel = new ContactViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();
            viewModel.SetOrientationValues();

            //assert
            viewModel.IsBottomButtonVisible.ShouldBeTrue();
        }

        [TestMethod]
        public void ShouldBottomButtonNotVisibleWhenInLandscapeMode()
        {
            //arrange
            viewModel = new ContactViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();
            viewModel.SetOrientationValues();

            //assert
            viewModel.IsBottomButtonVisible.ShouldBeFalse();
        }

        [TestMethod]
        public void ShouldLandscapeBottomButtonVisibleWhenInLandscapeMode()
        {
            //arrange
            viewModel = new ContactViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();
            viewModel.SetOrientationValues();

            //assert
            viewModel.IsLandscapeButtonVisible.ShouldBeTrue();
        }

        [TestMethod]
        public void ShouldLandscapeBottomButtonNotVisibleWhenInPortraitMode()
        {
            //arrange
            viewModel = new ContactViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();
            viewModel.SetOrientationValues();

            //assert
            viewModel.IsLandscapeButtonVisible.ShouldBeFalse();
        }

        [TestMethod]
        public void ShouldSetNavBarVisibilityTrueSetsCorrectBarVisibility()
        {
            //arrange
            viewModel = new ContactViewModel(DataManager);

            //act
            bool visibility = true;
            viewModel.SetNavBarVisibility(visibility);

            //assert
            viewModel.IsNavBarVisible.ShouldBeTrue();
        }

        [TestMethod]
        public void ShouldSetNavBarVisibilityFalseSetsCorrectBarVisibility()
        {
            //arrange
            viewModel = new ContactViewModel(DataManager);

            //act
            bool visibility = false;
            viewModel.SetNavBarVisibility(visibility);

            //assert
            viewModel.IsNavBarVisible.ShouldBeFalse();
        }

        #region BottomMarginForSubmitButton Test Cases

        [TestMethod]
        public void ShouldBottomMarginForSubmitButtonBeCustomWhenInAndroid()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, 20), viewModel.BottomMarginForSubmitButton);
        }

        [TestMethod]
        public void ShouldBottomMarginForSubmitButtonBeCustomWhenInLowerVersionsOfIOS()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            DeviceManager.Version = "10.0";
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, 20), viewModel.BottomMarginForSubmitButton);
        }

        [TestMethod]
        public void ShouldBottomMarginForSubmitButtonBeCustomWhenInIOSSupportedVersionAndiPhone4()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 960;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, 40), viewModel.BottomMarginForSubmitButton);
        }

        [TestMethod]
        public void ShouldBottomMarginForSubmitButtonBeCustomWhenInIOSSupportedVersionAndiPhoneSE_5()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1136;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, 40), viewModel.BottomMarginForSubmitButton);
        }

        [TestMethod]
        public void ShouldBottomMarginForSubmitButtonBeCustomWhenInIOSSupportedVersionAndiPhone8_7_6()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1334;
            App.ScreenWidth = 750;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, 40), viewModel.BottomMarginForSubmitButton);
        }

        [TestMethod]
        public void ShouldBottomMarginForSubmitButtonBeCustomWhenInIOSSupportedVersionAndiPhone8Plus_7Plus_6SPlus_6Plus()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1242;
            App.ScreenWidth = 2208;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, 40), viewModel.BottomMarginForSubmitButton);
        }

        [TestMethod]
        public void ShouldBottomMarginForSubmitButtonBeCustomWhenInIOSSupportedVersionAndiPhoneX_XS_11Pro()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1125;
            App.ScreenWidth = 2436;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, 20), viewModel.BottomMarginForSubmitButton);
        }

        [TestMethod]
        public void ShouldBottomMarginForSubmitButtonBeCustomWhenInIOSSupportedVersionAndiPhone11_XR()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1792;
            App.ScreenWidth = 828;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, 20), viewModel.BottomMarginForSubmitButton);
        }

        [TestMethod]
        public void ShouldBottomMarginForSubmitButtonBeCustomWhenInIOSSupportedVersionAndiPhone11ProMax_XSMax()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 2688;
            App.ScreenWidth = 1242;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, 20), viewModel.BottomMarginForSubmitButton);
        }

        [TestMethod]
        public void ShouldBottomMarginForSubmitButtonBePositiveWhenInIOSSupportedVersionAndiPad_2_Mini()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1024;
            App.ScreenWidth = 768;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, 20), viewModel.BottomMarginForSubmitButton);
        }

        [TestMethod]
        public void ShouldBottomMarginForSubmitButtonBePositiveWhenInIOSSupportedVersionAndiPad3_4_Air_Mini2_Mini3_Air2_Mini4_Pro_2017_2018()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 2048;
            App.ScreenWidth = 1536;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, 20), viewModel.BottomMarginForSubmitButton);
        }

        [TestMethod]
        public void ShouldBottomMarginForSubmitButtonBePositiveWhenInIOSSupportedVersionAndiPadProSec10()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 2224;
            App.ScreenWidth = 1668;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, 20), viewModel.BottomMarginForSubmitButton);
        }

        [TestMethod]
        public void ShouldBottomMarginForSubmitButtonBePositiveWhenInIOSSupportedVersionAndiPadProSec12()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 2732;
            App.ScreenWidth = 2048;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, 20), viewModel.BottomMarginForSubmitButton);
        }

        #endregion
    }
}
