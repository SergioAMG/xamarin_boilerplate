using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using XamarinBoilerplate.ViewModels;
using XamarinBoilerplate.Views;
using Shouldly;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.Enums;
using Xamarin.Forms;
using XamarinBoilerplate.ViewModels.DataObjects;

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
            Task.Run(async () =>
            {
                await viewModel.LoadData();
            }).GetAwaiter().GetResult();

            //assert
            viewModel.NewsItems.Count.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void ShouldLoadingIndicatorScaleBeLowerOnAndroid()
        {
            //arrange
            viewModel = new HomeViewModel(DataManager);

            //act
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            viewModel.LoadingIndicatorScale.ShouldBe(1.5);
        }

        [TestMethod]
        public void ShouldLoadingIndicatorScaleBeGreaterOnIOS()
        {
            //arrange
            viewModel = new HomeViewModel(DataManager);

            //act
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            viewModel.LoadingIndicatorScale.ShouldBe(2.0);
        }

        [TestMethod]
        public void ShouldItemSelectedTakeYouToDetailsPageWithTheSelectedItem()
        {
            //arrange
            viewModel = new HomeViewModel(DataManager);
            viewModel.NavigationService.SetRootPage(nameof(HomePage), new HomeViewModel());
            Page currentPage = viewModel.NavigationService.CurrentPage;

            //act
            viewModel.ItemSelected = new NewsViewModel() {
                Text = "Sample Test Text",
                ItemTitle = "Sample Test Item Title",
                Image = "sampleOne.png",
                Date = new System.DateTime().Date
            };
            Page targetPage = new NewsReaderPage(viewModel.ItemSelected);
            currentPage = viewModel.NavigationService.CurrentPage;

            //assert
            NUnit.Framework.Assert.AreEqual(currentPage.Title, targetPage.Title);
        }
    }
}
