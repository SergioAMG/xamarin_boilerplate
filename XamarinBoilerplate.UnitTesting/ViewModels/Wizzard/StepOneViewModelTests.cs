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
    public class StepOneViewModelTests : BaseViewModelTest
    {
        private StepOneViewModel viewModel;

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
        public void ShouldNextTutorialCommandSendUserToStepTwoPage()
        {
            //arrange
            viewModel = new StepOneViewModel();
            viewModel.NavigationService.SetRootPage(nameof(StepOnePage), new StepOneViewModel());
            Page targetPage = new StepTwoPage();
            Page currentPage = viewModel.NavigationService.CurrentPage;

            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteNextTutorialCommandAsync();
            }).GetAwaiter().GetResult();
            currentPage = viewModel.NavigationService.CurrentPage;

            //assert
            NUnit.Framework.Assert.AreEqual(currentPage.Title, targetPage.Title);
        }

        [TestMethod]
        public void ShouldLastTutorialCommandSendUserToStepThreePage()
        {
            //arrange
            viewModel = new StepOneViewModel();
            viewModel.NavigationService.SetRootPage(nameof(StepOnePage), new StepOneViewModel());
            Page targetPage = new StepThreePage();
            Page currentPage = viewModel.NavigationService.CurrentPage;

            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteLastTutorialCommandAsync();
            }).GetAwaiter().GetResult();
            currentPage = viewModel.NavigationService.CurrentPage;

            //assert
            NUnit.Framework.Assert.AreEqual(currentPage.Title, targetPage.Title);
        }

        [TestMethod]
        public void ShouldExecuteSkipTutorialCommandSkipTheTutorial()
        {
            // TODO: Test ExecuteSkipTutorialCommand
        }

        [TestMethod]
        public void ShouldRefreshOrientationChangeTheOrientationOfTheMainContainer()
        {
            //arrange
            viewModel = new StepOneViewModel();

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
            viewModel = new StepOneViewModel();

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            viewModel.MainContainerOrientation.ShouldBe(StackOrientation.Horizontal);
        }

        [TestMethod]
        public void ShouldMainContainerBeVerticalWhenDeviceOrientationIsInPortraitMode()
        {
            //arrange
            viewModel = new StepOneViewModel();

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            viewModel.MainContainerOrientation.ShouldBe(StackOrientation.Vertical);
        }
    }
}
