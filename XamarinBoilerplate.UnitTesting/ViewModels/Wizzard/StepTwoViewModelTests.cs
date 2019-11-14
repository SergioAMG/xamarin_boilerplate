using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBoilerplate.Enums;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels.Wizzard;
using XamarinBoilerplate.Views.Wizzard;

namespace XamarinBoilerplate.UnitTesting.ViewModels.Wizzard
{
    [TestClass]
    public class StepTwoViewModelTests : BaseViewModelTest
    {
        private StepTwoViewModel viewModel;

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
        public void ShouldNextTutorialCommandSendUserToStepThreePage()
        {
            //arrange
            viewModel = new StepTwoViewModel();
            viewModel.NavigationService.SetRootPage(nameof(StepOnePage), new StepOneViewModel());
            viewModel.NavigationService.NavigateAsync(nameof(StepTwoPage), null, false);
            Page targetPage = new StepThreePage();
            Page currentPage = viewModel.NavigationService.CurrentPage;

            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteNextTutorialCommandAsync();
            }).GetAwaiter().GetResult();
            currentPage = viewModel.NavigationService.CurrentPage;

            //assert
            NUnit.Framework.Assert.AreEqual(currentPage.GetType(), targetPage.GetType());
        }

        [TestMethod]
        public void ShouldBackTutorialCommandSendUserToStepOnePage()
        {
            //arrange
            viewModel = new StepTwoViewModel();
            viewModel.NavigationService.SetRootPage(nameof(StepOnePage), new StepOneViewModel());
            viewModel.NavigationService.NavigateAsync(nameof(StepTwoPage), null, false);
            Page targetPage = new StepOnePage();
            Page currentPage = viewModel.NavigationService.CurrentPage;

            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteBackTutorialCommandAsync();
            }).GetAwaiter().GetResult();
            currentPage = viewModel.NavigationService.CurrentPage;

            //assert
            NUnit.Framework.Assert.AreEqual(currentPage.GetType(), targetPage.GetType());
        }

        [TestMethod]
        public void ShouldRefreshOrientationChangeTheOrientationOfTheMainContainer()
        {
            //arrange
            viewModel = new StepTwoViewModel();

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            viewModel.MainContainerOrientation.ShouldBe(StackOrientation.Vertical);
            DeviceManager.Orientation = Devices.Landscape.ToString();
            viewModel.RefreshOrientation();
            viewModel.MainContainerOrientation.ShouldBe(StackOrientation.Horizontal);
        }

        [TestMethod]
        public void ShouldMainContainerBeHorizontalWhenDeviceOrientationIsInLandscapeMode()
        {
            //arrange
            viewModel = new StepTwoViewModel();

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            viewModel.MainContainerOrientation.ShouldBe(StackOrientation.Horizontal);
        }

        [TestMethod]
        public void ShouldMainContainerBeVerticalWhenDeviceOrientationIsInPortraitMode()
        {
            //arrange
            viewModel = new StepTwoViewModel();

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            viewModel.MainContainerOrientation.ShouldBe(StackOrientation.Vertical);
        }
    }
}
