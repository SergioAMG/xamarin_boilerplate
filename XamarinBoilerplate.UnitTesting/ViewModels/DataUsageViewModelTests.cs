using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using XamarinBoilerplate.Enums;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels;
using XamarinBoilerplate.Views;

namespace XamarinBoilerplate.UnitTesting.ViewModels
{
    [TestClass]
    public class DataUsageViewModelTests : BaseViewModelTest
    {
        private DataUsageViewModel viewModel;

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
            viewModel = new DataUsageViewModel(DataManager);

            //act

            //assert
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void ShouldIsAndroidMenuVisibleTrueWhenInAndroid()
        {
            //arrange
            viewModel = new DataUsageViewModel(DataManager);

            //act
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.IsTrue(viewModel.IsAndroidMenuVisible);
        }

        [TestMethod]
        public void ShouldIsAndroidMenuVisibleFalseWhenInIOS()
        {
            //arrange
            viewModel = new DataUsageViewModel(DataManager);

            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.IsFalse(viewModel.IsAndroidMenuVisible);
        }

        [TestMethod]
        public void ShouldIsIOSMenuVisibleTrueWhenInIOS()
        {
            //arrange
            viewModel = new DataUsageViewModel(DataManager);

            //act
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.IsTrue(viewModel.IsIOSMenuVisible);
        }

        [TestMethod]
        public void ShouldIsIOSMenuVisibleFalseWhenInAndroid()
        {
            //arrange
            viewModel = new DataUsageViewModel(DataManager);

            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.IsFalse(viewModel.IsIOSMenuVisible);
        }

        [TestMethod]
        public void ShouldOpenIconOpenNavigationDrawer()
        {
            //arrange
            viewModel = new DataUsageViewModel(DataManager);

            //act
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());
            viewModel.NavigationService.NavigateAsync(nameof(DataUsagePage), null, false);

            Task.Run(async () =>
            {
                await viewModel.ExecuteOpenDrawerCommandAsync();
            }).GetAwaiter().GetResult();

            //assert
            Assert.IsTrue(viewModel.NavigationService.IsDrawerOpen());
        }
    }
}
