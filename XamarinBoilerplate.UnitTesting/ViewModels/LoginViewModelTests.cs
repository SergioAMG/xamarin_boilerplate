using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBoilerplate.Enums;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.Views;
using XamarinBoilerplate.ViewModels;

namespace XamarinBoilerplate.UnitTesting.ViewModels
{
    [TestClass]
    public class LoginViewModelTests : BaseViewModelTest
    {
        private LoginViewModel viewModel;

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
            viewModel = new LoginViewModel(DataManager);

            //act

            //assert
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void ShouldRefreshOrientationChangeTheOrientationOfTheContainer()
        {
            //arrange
            viewModel = new LoginViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            viewModel.ContainerOrientation.ShouldBe(StackOrientation.Vertical);
            DeviceManager.Orientation = Devices.Landscape.ToString();
            viewModel.RefreshOrientation();
            viewModel.ContainerOrientation.ShouldBe(StackOrientation.Horizontal);
        }

        [TestMethod]
        public void ShouldContainerBeHorizontalWhenDeviceOrientationIsInLandscapeMode()
        {
            //arrange
            viewModel = new LoginViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            viewModel.ContainerOrientation.ShouldBe(StackOrientation.Horizontal);
        }

        [TestMethod]
        public void ShouldContainerBeVerticalWhenDeviceOrientationIsInPortraitMode()
        {
            //arrange
            viewModel = new LoginViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            viewModel.ContainerOrientation.ShouldBe(StackOrientation.Vertical);
        }

        [TestMethod]
        public void ShouldLoginSuccessfullyTakesUserToDashboardPage()
        {
            //arrange
            viewModel = new LoginViewModel(DataManager);
            viewModel.Login = "John Doe";
            viewModel.Password = "*****";

            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteLoginCommandAsync();
            }).GetAwaiter().GetResult();

            MasterDetailPage currentMasterDetailPage = viewModel.NavigationService.CurrentMasterDetailPage;
            MasterDetailPage targetPage = new DashboardPage();

            //assert
            Assert.AreEqual(targetPage.GetType(), currentMasterDetailPage.GetType());
            Assert.IsTrue(viewModel.IsLoggedIn);
        }

        [TestMethod]
        public void ShouldLoginErrorShouldStayInSamePage()
        {
            //arrange
            viewModel = new LoginViewModel(DataManager);

            viewModel.NavigationService.SetRootPage(nameof(LoginPage), new LoginViewModel());
            Page targetPage = new LoginPage();

            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteLoginCommandAsync();
            }).GetAwaiter().GetResult();
            Page currentPage = viewModel.NavigationService.CurrentPage;

            //assert
            Assert.AreEqual(targetPage.GetType(), currentPage.GetType());
            Assert.IsFalse(viewModel.IsLoggedIn);
        }
    }
}
