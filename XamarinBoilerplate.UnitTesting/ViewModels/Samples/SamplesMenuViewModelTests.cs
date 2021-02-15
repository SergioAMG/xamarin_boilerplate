using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Forms;
using Shouldly;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels.Samples;
using XamarinBoilerplate.Views;
using XamarinBoilerplate.ViewModels;
using XamarinBoilerplate.Enums;
using System.Threading.Tasks;
using XamarinBoilerplate.ViewModels.DataObjects;
using XamarinBoilerplate.Views.Samples;

namespace XamarinBoilerplate.UnitTesting.ViewModels.Samples
{
    [TestClass]
    public class SamplesMenuViewModelTest : BaseViewModelTest
    {
        private SamplesMenuViewModel viewModel;

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
            viewModel = new SamplesMenuViewModel(DataManager);

            //act

            //assert
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void ShouldSampleMenuItemSelectedTakeUserToSelectedPage()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());

            Page targetPage = viewModel.NavigationService.GetPage(nameof(Views.Samples.CarouselSamplePage), null, null);
            Page currentPage = viewModel.NavigationService.CurrentPage;

            viewModel.SampleMenuItemSelected = new XamarinBoilerplate.ViewModels.DataObjects.SampleMenuItemViewModel()
            {
                SampleMenuItem = "CarouselSamplePage",
                SampleMenuImage = "baseline_arrow_back_black_24"
            };

            currentPage = viewModel.NavigationService.CurrentPage;
            
            //assert
            Assert.AreEqual(targetPage.Title, currentPage.Title);
        }

        [TestMethod]
        public void ShouldSampleMenuItemsContainsMoreThanZeroItems()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act

            //assert
            viewModel.SampleMenu.Count.ShouldBeGreaterThan(0);
        }
        [TestMethod]
        public void ShouldSelectedTabIndexChangeAfterInitMethod()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);
            int selectedIndex = 0;
            
            //act
            viewModel.Init(selectedIndex);

            //assert
            Assert.AreEqual(selectedIndex, viewModel.SelectedTabIndex);
        }

        [TestMethod]
        public void ShouldCustomTextAlignmentBeCenterWhenUsingiOS()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.AreEqual(viewModel.CustomTextAlignment, TextAlignment.Center);
        }

        [TestMethod]
        public void ShouldCustomTextAlignmentBeStartWhenUsingAndroid()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.AreEqual(viewModel.CustomTextAlignment, TextAlignment.Start);
        }

        [TestMethod]
        public void ShouldCustomTextLayoutBeCenteredWhenUsingiOS()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.AreEqual(viewModel.CustomTextLayout, LayoutOptions.Center);
        }

        [TestMethod]
        public void ShouldCustomTextLayoutBeStartPositionWhenUsingAndroid()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.AreEqual(viewModel.CustomTextLayout, LayoutOptions.Start);
        }

        [TestMethod]
        public void ShouldFirstColumnWidthBeGreaterWhenInLandscapeMode()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            Assert.AreEqual(viewModel.FirstColumnWidth, new GridLength(9, GridUnitType.Star));
        }

        [TestMethod]
        public void ShouldFirstColumnWidthBeLowerWhenInPortraitMode()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            Assert.AreEqual(viewModel.FirstColumnWidth, new GridLength(16, GridUnitType.Star));
        }

        [TestMethod]
        public void ShouldSecondColumnWidthBeGreaterWhenInLandscapeMode()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            Assert.AreEqual(viewModel.SecondColumnWidth, new GridLength(80, GridUnitType.Star));
        }

        [TestMethod]
        public void ShouldSecondColumnWidthBeLowerWhenInPortraitMode()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            Assert.AreEqual(viewModel.SecondColumnWidth, new GridLength(70, GridUnitType.Star));
        }

        [TestMethod]
        public void ShouldThirdColumnWidthBeGreaterWhenInLandscapeMode()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            Assert.AreEqual(viewModel.ThirdColumnWidth, new GridLength(10, GridUnitType.Star));
        }

        [TestMethod]
        public void ShouldThirdColumnWidthBeLowerWhenInPortraitMode()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            Assert.AreEqual(viewModel.ThirdColumnWidth, new GridLength(20, GridUnitType.Star));
        }

        [TestMethod]
        public void ShouldBackFromDetailsTakeYouToActiveTabInPreviousCustomTabbedPage()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            int targetTabIndex = 2;
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());
            viewModel.NavigationService.NavigateAsync(nameof(CustomTabbedPage), null, false);
            viewModel.Init(targetTabIndex);

            Task.Run(async () =>
            {
                await viewModel.ExecuteBackFromDetailsCommandAsync();
            }).GetAwaiter().GetResult();

            //assert
            int currentTabIndex = viewModel.NavigationService.GetCurrentSelectedTabIndexOverMasterDetailPageWithTabbedPage();
            Assert.AreEqual(targetTabIndex, currentTabIndex);
        }

        [TestMethod]
        public void ShouldGenerateSamplesMenuCreateTheSamplesMenu()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act

            //assert
            viewModel.SampleMenu.Count.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void ShouldNavigateToSampleTakeYouToGivenPage()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());
            viewModel.NavigationService.NavigateAsync(nameof(SamplesMenuPage), null, false);
            
            //given page
            Page targetPage = new CollectionViewSamplePage();
            
            SampleMenuItemViewModel menuItemViewModel = new SampleMenuItemViewModel()
            {
                SampleMenuImage = "baseline_view_comfy_black_24.png",
                SampleMenuItem = Constants.CollectionViewMenu
            };

            //act
            Task.Run(async () =>
            {
                await viewModel.NavigateToSample(menuItemViewModel);
            }).GetAwaiter().GetResult();
            Page currentPage = viewModel.NavigationService.CurrentPage;

            //assert
            NUnit.Framework.Assert.AreEqual(currentPage.Title, targetPage.Title);
        }

        [TestMethod]
        public void ShouldSetOrientationValuesSetFirstColumnWidthToLowerValueWhenInLandscape()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();
            viewModel.SetOrientationValues();

            //assert
            Assert.AreEqual(new GridLength(9, GridUnitType.Star), viewModel.FirstColumnWidth);
        }

        [TestMethod]
        public void ShouldSetOrientationValuesSetFirstColumnWidthToGreaterValueWhenInPortrait()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();
            viewModel.SetOrientationValues();

            //assert
            Assert.AreEqual(new GridLength(16, GridUnitType.Star), viewModel.FirstColumnWidth);
        }

        [TestMethod]
        public void ShouldSetOrientationValuesSetSecondColumnWidthToGreaterValueWhenInLandscape()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();
            viewModel.SetOrientationValues();

            //assert
            Assert.AreEqual(new GridLength(80, GridUnitType.Star), viewModel.SecondColumnWidth);
        }

        [TestMethod]
        public void ShouldSetOrientationValuesSetSecondColumnWidthToLowerValueWhenInPortrait()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();
            viewModel.SetOrientationValues();

            //assert
            Assert.AreEqual(new GridLength(70, GridUnitType.Star), viewModel.SecondColumnWidth);
        }

        [TestMethod]
        public void ShouldSetOrientationValuesSetThirdColumnWidthToLowerValueWhenInLandscape()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();
            viewModel.SetOrientationValues();

            //assert
            Assert.AreEqual(new GridLength(10, GridUnitType.Star), viewModel.ThirdColumnWidth);
        }

        [TestMethod]
        public void ShouldSetOrientationValuesSetThirdColumnWidthToGreaterValueWhenInPortrait()
        {
            //arrange
            viewModel = new SamplesMenuViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();
            viewModel.SetOrientationValues();

            //assert
            Assert.AreEqual(new GridLength(20, GridUnitType.Star), viewModel.ThirdColumnWidth);
        }
    }
}
