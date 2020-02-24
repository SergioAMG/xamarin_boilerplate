using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Threading.Tasks;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels.Samples;

namespace XamarinBoilerplate.UnitTesting.ViewModels.Samples
{
    [TestClass]
    public class CarouselSampleViewModelTests : BaseViewModelTest
    {
        private CarouselSampleViewModel viewModel;

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
            viewModel = new CarouselSampleViewModel(DataManager);

            //act

            //assert
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void ShouldExtraInfoVisibleWhenPortraitModeIsSelected()
        {
            //arrange
            viewModel = new CarouselSampleViewModel(DataManager);

            //act
            DeviceManager.Orientation = Constants.PortraitMode;

            //assert
            Assert.IsTrue(viewModel.ExtraInfoVisible);
        }

        [TestMethod]
        public void ShouldExtraInfoNotVisibleWhenLandscapeModeIsSelected()
        {
            //arrange
            viewModel = new CarouselSampleViewModel(DataManager);

            //act
            DeviceManager.Orientation = Constants.LandscapeMode;

            //assert
            Assert.IsFalse(viewModel.ExtraInfoVisible);
        }

        [TestMethod]
        public void ShouldLoadDataMethodRetrieveFlightsList()
        {
            //arrange
            viewModel = new CarouselSampleViewModel(DataManager);

            //act
            Task.Run(async () =>
            {
                await viewModel.LoadData();
            }).GetAwaiter().GetResult();

            //assert
            viewModel.Flights.Count.ShouldBeGreaterThan(0);
        }
    }
}
