using Microsoft.VisualStudio.TestTools.UnitTesting;
using XamarinBoilerplate.ViewModels;
using Shouldly;
using System.Threading.Tasks;
using XamarinBoilerplate.Views;

namespace XamarinBoilerplate.UnitTesting.ViewModels
{
    [TestClass]
    public class MenuViewModelTests : BaseViewModelTest
    {
        private MenuViewModel viewModel;

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
        public void ShouldAppVersionNotBeNull()
        {
            //arrange
            viewModel = new MenuViewModel();

            //act

            //assert
            viewModel.AppVersion.ShouldNotBeNullOrEmpty();
        }

        [TestMethod]
        public void ShouldCloseIconCloseNavigationDrawer()
        {
            //arrange
            viewModel = new MenuViewModel();
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());
            viewModel.NavigationService.OpenDrawer();

            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteCloseCommandAsync();
            }).GetAwaiter().GetResult();

            //assert
            viewModel.NavigationService.IsDrawerOpen().ShouldBeFalse();
        }

        [TestMethod]
        [Ignore]
        public void ShouldGoToContactCommandSendUserToContactPage()
        {
            //arrange
            viewModel = new MenuViewModel();
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());
            
            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteGoToContactAsync();
            }).GetAwaiter().GetResult();

            //assert
        }

        [TestMethod]
        [Ignore]
        public void ShouldGoToWizzardStep1CommandSendUserToWizzardStep1Page()
        {
            //arrange
            viewModel = new MenuViewModel();
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());

            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteGoToWizzardStep1CommandAsync();
            }).GetAwaiter().GetResult();

            //assert
        }

        [TestMethod]
        [Ignore]
        public void ShouldGoToSearchCommandSendUserToSearchPage()
        {
            //arrange
            viewModel = new MenuViewModel();
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());

            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteGoToSearchCommandAsync();
            }).GetAwaiter().GetResult();

            //assert
        }

        [TestMethod]
        [Ignore]
        public void ShouldGoToLiveHelpCommandSendUserToLiveHelpPage()
        {
            //arrange
            viewModel = new MenuViewModel();
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());

            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteGoToLiveHelpCommandAsync();
            }).GetAwaiter().GetResult();

            //assert
        }
    }
}
