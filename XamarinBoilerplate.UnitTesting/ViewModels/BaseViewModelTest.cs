using DataManagers.Interfaces;
using NUnit.Framework;
using Xamarin.Forms;
using XamarinBoilerplate.Enums;
using XamarinBoilerplate.UnitTesting.MockData;
using XamarinBoilerplate.UnitTesting.Services;
using XamarinBoilerplate.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace XamarinBoilerplate.UnitTesting.ViewModels
{
    [TestFixture]
    public class BaseViewModelTest
    {
        private IDataService _dataManager;
        private static Application MobileApp;
        private MockBaseViewModel viewModel;

        [TestInitialize]
        public virtual void Initialize()
        {
            if (MobileApp != null)
            {
                return;
            }

            Xamarin.Forms.Mocks.MockForms.Init();
            MobileApp = new App();
            Application.Current = MobileApp;
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            Application.Current = null;
            MobileApp = null;
        }

        #region General Test Cases

        [Test]
        public void ShouldIsTitleCenterFalseWhenInAndroid()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.IsFalse(viewModel.IsTitleCentered);
        }

        [Test]
        public void ShouldIsTitleCenterTrueWhenInIOS()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.IsTrue(viewModel.IsTitleCentered);
        }

        [Test]
        public void ShouldIsAndroidBeTrueWhenInAndroidAndIsIOSShouldBeFalse()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.IsTrue(viewModel.IsAndroid);
            Assert.IsFalse(viewModel.IsIOS);
        }

        [Test]
        public void ShouldIsIOSBeTrueWhenInIOSAndIsAndroidShouldBeFalse()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.IsTrue(viewModel.IsIOS);
            Assert.IsFalse(viewModel.IsAndroid);
        }

        [Test]
        public void ShouldRefreshMainContainerMarginsAdjustGetMainContainerMargin()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Idiom = Devices.Phone.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1334;
            App.ScreenWidth = 750;
            Thickness originalMargin = viewModel.GetMainContainerMargin;
            DeviceManager.Orientation = Devices.Portrait.ToString();
            viewModel.RefreshMainContainerMargins();
            Thickness modifiedMargin = viewModel.GetMainContainerMargin;

            //assert
            Assert.AreNotEqual(modifiedMargin, originalMargin);
        }

        [Test]
        public void ShouldRefreshMainContainerMarginsAdjustLeftIconVisibility()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 960;
            App.ScreenWidth = 640;
            bool originalVisibility = viewModel.IsLeftIconVisible;
            DeviceManager.Orientation = Devices.Portrait.ToString();
            viewModel.RefreshMainContainerMargins();
            bool modifiedVisibility = viewModel.IsLeftIconVisible;

            //assert
            Assert.AreNotEqual(originalVisibility, modifiedVisibility);
        }

        [Test]
        public void ShouldRefreshMainContainerMarginsAdjustBackButtonVisibility()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Idiom = Devices.Tablet.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            bool originalVisibility = viewModel.ShouldBackButtonBeHidden;
            DeviceManager.Orientation = Devices.Portrait.ToString();
            viewModel.RefreshMainContainerMargins();
            bool modifiedVisibility = viewModel.ShouldBackButtonBeHidden;

            //assert
            Assert.AreNotEqual(originalVisibility, modifiedVisibility);
        }

        #endregion

        #region MarginForLeftIconOfActionBar Test Cases

        [Test]
        public void ShouldMarginForLeftIconOfActionBarBeLowerWhenInAndroid()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, 0, 22, 0), viewModel.MarginForLeftIconOfActionBar);
        }

        [Test]
        public void ShouldMarginForLeftIconOfActionBarBeHigherWhenInIOS()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, 0, 27, 0), viewModel.MarginForLeftIconOfActionBar);
        }

        #endregion

        #region MarginForRightIconOfActionBar Test Cases

        [Test]
        public void ShouldMarginForRightIconOfActionBarBeLowerWhenInAndroid()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, 0, 25, 0), viewModel.MarginForRightIconOfActionBar);
        }

        [Test]
        public void ShouldMarginForRightIconOfActionBarBeHigherWhenInIOS()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, 0, 32, 0), viewModel.MarginForRightIconOfActionBar);
        }

        #endregion

        #region MarginForOptionsIconOfActionBar Test Cases

        [Test]
        public void ShouldMarginForOptionsIconOfActionBarBeLowerWhenInAndroid()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, 0, 3, 0), viewModel.MarginForOptionsIconOfActionBar);
        }

        [Test]
        public void ShouldMarginForOptionsIconOfActionBarBeHigherWhenInIOS()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, 0, 5, 0), viewModel.MarginForOptionsIconOfActionBar);
        }

        #endregion region

        #region MarginForSingleIconOfActionBar Test Cases

        [Test]
        public void ShouldMarginForSingleIconOfActionBarBeLowerWhenInAndroid()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, 0, 8, 0), viewModel.MarginForSingleIconOfActionBar);
        }

        [Test]
        public void ShouldMarginForSingleIconOfActionBarBeHigherWhenInIOS()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, 0, 10, 0), viewModel.MarginForSingleIconOfActionBar);
        }

        #endregion

        #region BackButton Test Cases

        [Test]
        public void ShouldBackButtonBeHiddenWhenLandscapeAndTablet()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();
            DeviceManager.Idiom = Devices.Tablet.ToString();

            //assert
            Assert.IsTrue(viewModel.ShouldBackButtonBeHidden);
        }

        [Test]
        public void ShouldBackButtonNotBeHiddenWhenNotInLandscapeAndNotInTablet()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();
            DeviceManager.Idiom = Devices.Phone.ToString();

            //assert
            Assert.IsFalse(viewModel.ShouldBackButtonBeHidden);
        }

        #endregion

        #region IsLeftIconVisible Test Cases

        [Test]
        public void ShouldIsLeftIconVisibleWhenInIphone4InLandscape()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 960;
            App.ScreenWidth = 640;

            //assert
            Assert.IsTrue(viewModel.IsLeftIconVisible);
        }

        [Test]
        public void ShouldIsLeftIconVisibleWhenInIphone5InLandscape()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1136;
            App.ScreenWidth = 640;

            //assert
            Assert.IsTrue(viewModel.IsLeftIconVisible);
        }

        [Test]
        public void ShouldIsLeftIconNotVisibleWhenInIphone4InPortrait()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 960;
            App.ScreenWidth = 640;

            //assert
            Assert.IsFalse(viewModel.IsLeftIconVisible);
        }

        [Test]
        public void ShouldIsLeftIconNotVisibleWhenInIphone5InPortrait()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Portrait.ToString();
            App.ScreenHeight = 1136;
            App.ScreenWidth = 640;

            //assert
            Assert.IsFalse(viewModel.IsLeftIconVisible);
        }

        [Test]
        public void ShouldIsLeftIconVisibleWhenIphoneNewestThan5()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.iOS.ToString();
            App.ScreenHeight = 1334;
            App.ScreenWidth = 750;

            //assert
            Assert.IsTrue(viewModel.IsLeftIconVisible);
        }
        
        [Test]
        public void ShouldIsLeftIconVisibleWhenInAndroid()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.IsTrue(viewModel.IsLeftIconVisible);
        }

        #endregion

        #region GetMainContainerMargin Test Cases

        [Test]
        public void ShouldGetMainContainerMarginBeZeroWhenAndroidAndTabletAndLandscape()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();
            DeviceManager.Idiom = Devices.Tablet.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeZeroWhenAndroidAndTabletAndPortrait()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();
            DeviceManager.Idiom = Devices.Tablet.ToString();
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeZeroWhenAndroidAndPhonePortrait()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();
            DeviceManager.Idiom = Devices.Phone.ToString();
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeZeroWhenAndroidAndPhoneLandscape()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Platform = Devices.Android.ToString();
            DeviceManager.Idiom = Devices.Phone.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeZeroWhenInLowerVersionsOfIOSAndTabletAndLandscape()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "10.0";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Idiom = Devices.Tablet.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeZeroWhenInLowerVersionsOfIOSAndTabletAndPortrait()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "10.0";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Idiom = Devices.Tablet.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeNegativeWhenInLowerVersionsOfIOSAndInPhoneAndLandscape()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "10.0";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Idiom = Devices.Phone.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, -20, 0, 0), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeZeroWhenInLowerVersionsOfIOSAndInPhoneAndPortrait()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "10.0";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Idiom = Devices.Phone.ToString();
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeNegativeWhenInIOSSupportedVersionAndInPortraitAndPhone()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Idiom = Devices.Phone.ToString();
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, -35), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeNegativeWhenInIOSSupportedVersionAndInPortraitAndTablet()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Idiom = Devices.Tablet.ToString();
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, -35), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeZeroWhenInIOSSupportedVersionAndiPhone4AndLandscape()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 960;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeZeroWhenInIOSSupportedVersionAndiPhoneSE_5AndLandscape()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1136;
            App.ScreenWidth = 640;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeZeroWhenInIOSSupportedVersionAndiPhone8_7_6AndLandscape()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1334;
            App.ScreenWidth = 750;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeNegativeWhenInIOSSupportedVersionAndiPhone8Plus_7Plus_6SPlus_6PlusAndLandscape()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1242;
            App.ScreenWidth = 2208;

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, -35), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeNegativeWhenInIOSSupportedVersionAndiPhoneX_XS_11ProAndLandscape()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1125;
            App.ScreenWidth = 2436;

            //assert
            Assert.AreEqual(new Thickness(-45, 0, -45, -35), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeNegativeWhenInIOSSupportedVersionAndiPhone11_XRAndLandscape()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1792;
            App.ScreenWidth = 828;

            //assert
            Assert.AreEqual(new Thickness(-45, 0, -45, -35), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeNegativeWhenInIOSSupportedVersionAndiPhone11ProMax_XSMaxAndLandscape()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2688;
            App.ScreenWidth = 1242;

            //assert
            Assert.AreEqual(new Thickness(-45, 0, -45, -35), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeZeroWhenInIOSSupportedVersionAndiPad_2_MiniAndLandscape()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 1024;
            App.ScreenWidth = 768;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeZeroWhenInIOSSupportedVersionAndiPad3_4_Air_Mini2_Mini3_Air2_Mini4_Pro_2017_2018AndLandscape()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2048;
            App.ScreenWidth = 1536;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeZeroWhenInIOSSupportedVersionAndiPadProSec10AndLandscape()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2224;
            App.ScreenWidth = 1668;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.GetMainContainerMargin);
        }

        [Test]
        public void ShouldGetMainContainerMarginBeZeroWhenInIOSSupportedVersionAndiPadProSec12AndLandscape()
        {
            //arrange
            viewModel = new MockBaseViewModel();

            //act
            DeviceManager.Version = "13.3";
            DeviceManager.Platform = Devices.iOS.ToString();
            DeviceManager.Orientation = Devices.Landscape.ToString();
            App.ScreenHeight = 2732;
            App.ScreenWidth = 2048;

            //assert
            Assert.AreEqual(new Thickness(0), viewModel.GetMainContainerMargin);
        }

        #endregion

        public IDataService DataManager
        {
            get 
            { 
                return _dataManager ?? (_dataManager = new MockDataWrapperService());
            }
            set
            { 
                _dataManager = value;
            }
        }
    }
}
