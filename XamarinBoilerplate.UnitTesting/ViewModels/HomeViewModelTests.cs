using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XamarinBoilerplate.ViewModels;
using XamarinBoilerplate.Views;

namespace XamarinBoilerplate.UnitTesting.ViewModels
{
    [TestClass]
    public class HomeViewModelTests : BaseViewModelTest
    {
        private HomeViewModel viewModel;

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
            viewModel = new HomeViewModel();

            //act

            //assert
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void ShouldCreateOptionsMenuCreateToolbarItems()
        {
            //arrange
            viewModel = new HomeViewModel();

            //act
            viewModel.CreateOptionsMenu();

            //assert
            Assert.IsNotNull(viewModel.Buttons);
        }

        [TestMethod]
        public void ShouldCreateOptionsMenuCreateAlsoSubMenu()
        {
            //arrange
            viewModel = new HomeViewModel();

            //act
            viewModel.CreateOptionsMenu();

            //assert
            Assert.IsTrue(viewModel.HasSubMenu);
            Assert.IsNotNull(viewModel.SubMenu);
        }

        [TestMethod]
        public void ShouldOpenDrawerCommandOpenTheDrawerMenu()
        {
            //arrange
            viewModel = new HomeViewModel();

            //act
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());
            Task.Run(async () =>
            {
                await viewModel.ExecuteOpenDrawerCommandAsync();
            }).GetAwaiter().GetResult();

            //assert
            Assert.IsTrue(viewModel.NavigationService.IsDrawerOpen());
        }
    }
}
