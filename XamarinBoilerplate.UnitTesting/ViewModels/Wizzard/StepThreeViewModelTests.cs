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
    public class StepThreeViewModelTests : BaseViewModelTest
    {
        private StepThreeViewModel viewModel;

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
        public void ShouldBackTutorialCommandSendUserToStepTwoPage()
        {
            //arrange
            viewModel = new StepThreeViewModel();
            Page targetPage = new StepTwoPage();
            viewModel.NavigationService.SetRootPage(nameof(StepOnePage), new StepOneViewModel());
            viewModel.NavigationService.NavigateAsync(nameof(StepTwoPage), null, false);
            viewModel.NavigationService.NavigateAsync(nameof(StepThreePage), null, false);
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
        public void ShouldStartTutorialCommandSendUserToStepOnePage()
        {
            //arrange
            viewModel = new StepThreeViewModel();
            Page targetPage = new StepOnePage();
            viewModel.NavigationService.SetRootPage(nameof(StepOnePage), new StepOneViewModel());
            viewModel.NavigationService.NavigateAsync(nameof(StepTwoPage), null, false);
            viewModel.NavigationService.NavigateAsync(nameof(StepThreePage), null, false);
            Page currentPage = viewModel.NavigationService.CurrentPage;

            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteStartTutorialCommandAsync();
            }).GetAwaiter().GetResult();
            currentPage = viewModel.NavigationService.CurrentPage;

            //assert
            NUnit.Framework.Assert.AreEqual(currentPage.GetType(), targetPage.GetType());
        }

        [TestMethod]
        public void ShouldExecuteDoneTutorialCommandCompleteTheTutorial()
        {
            // TODO: Test ExecuteDoneTutorialCommand
        }

        [TestMethod]
        public void ShouldRefreshOrientationChangeTheOrientationOfTheMainContainer()
        {
            //arrange
            viewModel = new StepThreeViewModel();

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
            viewModel = new StepThreeViewModel();

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            viewModel.MainContainerOrientation.ShouldBe(StackOrientation.Horizontal);
        }

        [TestMethod]
        public void ShouldMainContainerBeVerticalWhenDeviceOrientationIsInPortraitMode()
        {
            //arrange
            viewModel = new StepThreeViewModel();

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            viewModel.MainContainerOrientation.ShouldBe(StackOrientation.Vertical);
        }
    }
}
