using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Shouldly;
using XamarinBoilerplate.Enums;
using XamarinBoilerplate.Utils;

namespace XamarinBoilerplate.UnitTesting.ViewModels.Wizzard
{
    [TestClass]
    public class StepThreeViewModelTests
    {
        XamarinBoilerplate.ViewModels.Wizzard.StepThreeViewModel viewModel;

        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void Tear()
        {
        }

        [TestMethod]
        public void ShouldRefreshOrientationChangeTheOrientationOfTheMainContainer()
        {
            //arrange
            viewModel = new XamarinBoilerplate.ViewModels.Wizzard.StepThreeViewModel();

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            viewModel.MainContainerOrientation.ShouldBe(Xamarin.Forms.StackOrientation.Vertical);
            DeviceManager.Orientation = Devices.Landscape.ToString();
            viewModel.RefreshOrientation();
            viewModel.MainContainerOrientation.ShouldBe(Xamarin.Forms.StackOrientation.Horizontal);
        }

        [TestMethod]
        public void ShouldMainContainerBeHorizontalWhenDeviceOrientationIsInLandscapeMode()
        {
            //arrange
            viewModel = new XamarinBoilerplate.ViewModels.Wizzard.StepThreeViewModel();

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            viewModel.MainContainerOrientation.ShouldBe(Xamarin.Forms.StackOrientation.Horizontal);
        }

        [TestMethod]
        public void ShouldMainContainerBeVerticalWhenDeviceOrientationIsInPortraitMode()
        {
            //arrange
            viewModel = new XamarinBoilerplate.ViewModels.Wizzard.StepThreeViewModel();

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            viewModel.MainContainerOrientation.ShouldBe(Xamarin.Forms.StackOrientation.Vertical);
        }
    }
}
