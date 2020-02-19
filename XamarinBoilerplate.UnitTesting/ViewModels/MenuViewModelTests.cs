using Microsoft.VisualStudio.TestTools.UnitTesting;
using XamarinBoilerplate.ViewModels;
using Shouldly;
using System.Threading.Tasks;
using XamarinBoilerplate.Views;
using Xamarin.Forms;
using XamarinBoilerplate.Views.Wizzard;
using XamarinBoilerplate.Views.Samples;

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
            NUnit.Framework.Assert.IsFalse(viewModel.NavigationService.IsDrawerOpen());
        }

        [TestMethod]
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
            Page currentPage  = viewModel.NavigationService.GetCurrentDetailsPage();
            Page targetPage = new ContactPage();

            //assert
            NUnit.Framework.Assert.AreEqual(currentPage.Title, targetPage.Title);
        }

        [TestMethod]
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
            Page currentPage = viewModel.NavigationService.CurrentPage;
            Page targetPage = new StepOnePage();

            //assert
            NUnit.Framework.Assert.AreEqual(currentPage.Title, targetPage.Title);
        }

        [TestMethod]
        public void ShouldGoToSamplesCommandSendUserToSamplesPage()
        {
            //arrange
            viewModel = new MenuViewModel();
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());

            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteGoToSamplesCommandAsync();
            }).GetAwaiter().GetResult();

            Page currentMasterPage = viewModel.NavigationService.CurrentMasterDetailPage;
            var currentDetailsPage = viewModel.NavigationService.CurrentMasterDetailPage.Detail;
            int indexOfDetailsPage = 0;
            Page targetPage = new SamplesMenuPage(indexOfDetailsPage);

            //assert
            NUnit.Framework.Assert.AreEqual(currentDetailsPage.Title, targetPage.Title);
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
