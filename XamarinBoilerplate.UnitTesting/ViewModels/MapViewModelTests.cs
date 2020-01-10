using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using XamarinBoilerplate.ViewModels;
using XamarinBoilerplate.Views;

namespace XamarinBoilerplate.UnitTesting.ViewModels
{
    [TestClass]
    public class MapViewModelTests : BaseViewModelTest
    {
        private MapViewModel viewModel;

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
        public void ShouldViewModelbeInitializedAndAssociated()
        {
            //arrange
            viewModel = new MapViewModel(DataManager);

            //act

            //assert
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void ShouldCreateOptionsMenuCreateToolbarItems()
        {
            //arrange
            viewModel = new MapViewModel(DataManager);

            //act
            viewModel.CreateOptionsMenu();

            //assert
            Assert.IsNotNull(viewModel.Buttons);
        }

        [TestMethod]
        public void ShouldCreateOptionsMenuCreateAlsoSubMenu()
        {
            //arrange
            viewModel = new MapViewModel(DataManager);

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
            viewModel = new MapViewModel(DataManager);

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
