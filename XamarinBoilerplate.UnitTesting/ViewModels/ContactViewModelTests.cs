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
            viewModel = new ContactViewModel();

            //act

            //assert
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void ShouldCreateOptionsMenuCreateToolbarItems()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            viewModel.CreateOptionsMenu();

            //assert
            Assert.IsNotNull(viewModel.Buttons);
        }

        [TestMethod]
        public void ShouldCreateOptionsMenuCreateAlsoSubMenu()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            viewModel.CreateOptionsMenu();

            //assert
            Assert.IsTrue(viewModel.HasSubMenu);
            Assert.IsNotNull(viewModel.SubMenu);
        }

        [TestMethod]
        public void ShouldInitMethodUpdateSelectedTabIndex()
        {
            //arrange
            viewModel = new ContactViewModel();

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
            viewModel = new ContactViewModel();

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
            var currentPage = viewModel.NavigationService.CurrentPage;
        }

        [TestMethod]
        public void ShouldRefreshOrientationChangeTheOrientationOfTheMainContainer()
        {
            //arrange
            viewModel = new ContactViewModel();

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
            viewModel = new ContactViewModel();

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            viewModel.MainContainerOrientation.ShouldBe(StackOrientation.Horizontal);
        }

        [TestMethod]
        public void ShouldMainContainerBeVerticalWhenDeviceOrientationIsInPortraitMode()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            viewModel.MainContainerOrientation.ShouldBe(StackOrientation.Vertical);
        }

        [TestMethod]
        public void ShouldSetNavBarVisibilityToTrueWhenBindingViewModel()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act

            //assert
            Assert.IsTrue(viewModel.IsNavBarVisible);
        }

        [TestMethod]
        public void ShouldSetOrientationValuesSetDetailsViewWidthForLandscape()
        {
            //arrange
            viewModel = new ContactViewModel();

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
            viewModel = new ContactViewModel();

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
            viewModel = new ContactViewModel();

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
            viewModel = new ContactViewModel();

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
            viewModel = new ContactViewModel();

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
            viewModel = new ContactViewModel();

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();
            viewModel.SetOrientationValues();

            //assert
            viewModel.IsLandscapeButtonVisible.ShouldBeFalse();
        }
    }
}
