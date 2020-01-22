using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using XamarinBoilerplate.ViewModels;
using XamarinBoilerplate.Views;
using Shouldly;

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
            viewModel = new HomeViewModel(DataManager);

            //act

            //assert
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void ShouldOpenDrawerCommandOpenTheDrawerMenu()
        {
            //arrange
            viewModel = new HomeViewModel(DataManager);

            //act
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());
            Task.Run(async () =>
            {
                await viewModel.ExecuteOpenDrawerCommandAsync();
            }).GetAwaiter().GetResult();

            //assert
            Assert.IsTrue(viewModel.NavigationService.IsDrawerOpen());
        }

        [TestMethod]
        public void ShouldLoadDataMethodRetrieveNewsList()
        {
            //arrange
            viewModel = new HomeViewModel(DataManager);

            //act

            //assert
            viewModel.NewsItems.Count.ShouldBeGreaterThan(0);
        }
    }
}
