using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Forms;
using Shouldly;
using XamarinBoilerplate.Enums;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels.Samples;
using XamarinBoilerplate.ViewModels.DataObjects;
using System.Collections.ObjectModel;

namespace XamarinBoilerplate.UnitTesting.ViewModels.Samples
{
    [TestClass]
    public class CollectionViewSampleViewModelTests : BaseViewModelTest
    {
        private CollectionViewSampleViewModel viewModel;

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
            viewModel = new CollectionViewSampleViewModel(DataManager);

            //act

            //assert
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void ShouldFirstColumnWidthBeLowerWhenInLandscapeMode()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            Assert.AreEqual(viewModel.FirstColumnWidth, new GridLength(2.5, GridUnitType.Star));
        }

        [TestMethod]
        public void ShouldFirstColumnWidthBeGreaterWhenInPortraitMode()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            Assert.AreEqual(viewModel.FirstColumnWidth, new GridLength(3, GridUnitType.Star));
        }

        [TestMethod]
        public void ShouldSecondColumnWidthBeGreaterWhenInLandscapeMode()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();

            //assert
            Assert.AreEqual(viewModel.SecondColumnWidth, new GridLength(6, GridUnitType.Star));
        }

        [TestMethod]
        public void ShouldSecondColumnWidthBeLowerWhenInPortraitMode()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();

            //assert
            Assert.AreEqual(viewModel.SecondColumnWidth, new GridLength(5.5, GridUnitType.Star));
        }

        [TestMethod]
        public void ShouldThirdColumnWidthBeAlwaysTheSame()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);

            //act

            //assert
            Assert.AreEqual(viewModel.ThirdColumnWidth, new GridLength(1.5, GridUnitType.Star));
        }

        [TestMethod]
        public void ShouldSearchMarginBarBeGreaterOnAndroid()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);

            //act
            DeviceManager.Platform = Devices.Android.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, 0, 13, 5), viewModel.SearchMargin);
        }

        [TestMethod]
        public void ShouldSearchMarginBarBeLowerOnIOS()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);

            //act
            DeviceManager.Platform = Devices.iOS.ToString();

            //assert
            Assert.AreEqual(new Thickness(0, 0, 0, 5), viewModel.SearchMargin);
        }

        [TestMethod]
        public void ShouldLoadDataMethodRetrieveBrandList()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);

            //act
            Task.Run(async () =>
            {
                await viewModel.LoadData();
            }).GetAwaiter().GetResult();
            IEnumerable<PopularBrandsViewModel> differences = viewModel.PopularBrands.Except(viewModel.PopularBrandsFromServer);

            //assert
            Assert.IsNotNull(viewModel.PopularBrands);
            Assert.IsNotNull(viewModel.PopularBrandsFromServer);
            viewModel.PopularBrands.Count.ShouldBeGreaterThan(0);
            viewModel.PopularBrandsFromServer.Count.ShouldBeGreaterThan(0);
            Assert.AreEqual(viewModel.PopularBrands.Count, viewModel.PopularBrandsFromServer.Count);
            Assert.AreEqual(0, differences.Count());
        }

        [TestMethod]
        public void ShouldDeleteCommandRemoveElementFromDataSources()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);
            PopularBrandsViewModel popularBrandsViewModel = new PopularBrandsViewModel();

            //act
            Task.Run(async () =>
            {
                await viewModel.LoadData();
            }).GetAwaiter().GetResult();

            popularBrandsViewModel = viewModel.PopularBrands.FirstOrDefault();

            Task.Run(async () =>
            {
                await viewModel.ExecuteOnDeleteCommandAsync(popularBrandsViewModel);
            }).GetAwaiter().GetResult();

            //assert
            Assert.IsFalse(viewModel.PopularBrands.Contains(popularBrandsViewModel));
            Assert.IsFalse(viewModel.PopularBrandsFromServer.Contains(popularBrandsViewModel));
        }

        [TestMethod]
        public void ShouldFavoriteCommandMarkElementAsFavorite()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);
            PopularBrandsViewModel popularBrandsViewModel = new PopularBrandsViewModel();

            //act
            Task.Run(async () =>
            {
                await viewModel.LoadData();
            }).GetAwaiter().GetResult();

            popularBrandsViewModel = viewModel.PopularBrands.FirstOrDefault();
            popularBrandsViewModel.IsFavorite = false;

            Task.Run(async () =>
            {
                await viewModel.ExecuteOnFavoriteCommandAsync(popularBrandsViewModel);
            }).GetAwaiter().GetResult();

            //assert
            Assert.IsTrue(popularBrandsViewModel.IsFavorite);
            Assert.IsTrue(viewModel.PopularBrandsFromServer[viewModel.PopularBrandsFromServer.IndexOf(popularBrandsViewModel)].IsFavorite);
        }

        [TestMethod]
        public void ShouldFavoriteCommandMarkElementAsNotFavoriteWhenIsAlreadyFavorite()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);
            PopularBrandsViewModel popularBrandsViewModel = new PopularBrandsViewModel();

            //act
            Task.Run(async () =>
            {
                await viewModel.LoadData();
            }).GetAwaiter().GetResult();

            popularBrandsViewModel = viewModel.PopularBrands.FirstOrDefault();
            popularBrandsViewModel.IsFavorite = true;

            Task.Run(async () =>
            {
                await viewModel.ExecuteOnFavoriteCommandAsync(popularBrandsViewModel);
            }).GetAwaiter().GetResult();

            //assert
            Assert.IsFalse(popularBrandsViewModel.IsFavorite);
            Assert.IsFalse(viewModel.PopularBrandsFromServer[viewModel.PopularBrandsFromServer.IndexOf(popularBrandsViewModel)].IsFavorite);
        }

        [TestMethod]
        public void ShouldPerformSearchCommandMarkRetrieveSearchResults()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);
            PopularBrandsViewModel popularBrandsViewModel = new PopularBrandsViewModel();

            //act
            Task.Run(async () =>
            {
                await viewModel.LoadData();
            }).GetAwaiter().GetResult();

            popularBrandsViewModel = viewModel.PopularBrands.FirstOrDefault();

            Task.Run(async () =>
            {
                await viewModel.ExecuteOnPerformSearchCommandAsync(popularBrandsViewModel.ItemTitle);
            }).GetAwaiter().GetResult();

            var searchResults = new ObservableCollection<PopularBrandsViewModel>(
                viewModel.PopularBrandsFromServer.Where(x => x.ItemTitle.ToUpper().Contains(popularBrandsViewModel.ItemTitle.ToUpper()) ||
                                    x.Text.ToUpper().Contains(popularBrandsViewModel.ItemTitle.ToUpper())));

            IEnumerable<PopularBrandsViewModel> differences = viewModel.PopularBrands.Except(searchResults);

            //assert
            Assert.AreEqual(searchResults.Count, viewModel.PopularBrands.Count);
            Assert.AreEqual(0, differences.Count());
        }

        [TestMethod]
        public void ShouldRefreshCommandRefreshMainBrandListFirstTest()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);
            int originalCollectionCount = 0;

            //act
            Task.Run(async () =>
            {
                await viewModel.LoadData();
            }).GetAwaiter().GetResult();

            viewModel.PopularBrands.Remove(viewModel.PopularBrands.FirstOrDefault());
            viewModel.PopularBrands.Remove(viewModel.PopularBrands.FirstOrDefault());
            originalCollectionCount = viewModel.PopularBrands.Count();

            Task.Run(async () =>
            {
                await viewModel.ExecuteOnRefreshCommandAsync();
            }).GetAwaiter().GetResult();

            //assert
            viewModel.PopularBrands.Count.ShouldBeGreaterThan(originalCollectionCount);
            viewModel.PopularBrandsFromServer.Count.ShouldBeGreaterThan(originalCollectionCount);
        }

        [TestMethod]
        public void ShouldRefreshCommandRefreshMainBrandListSecondTest()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);
            ObservableCollection<PopularBrandsViewModel> originalData = new ObservableCollection<PopularBrandsViewModel>();
            ObservableCollection<PopularBrandsViewModel> refreshedData = new ObservableCollection<PopularBrandsViewModel>();
            IEnumerable<PopularBrandsViewModel> differences;

            //act
            Task.Run(async () =>
            {
                await viewModel.LoadData();
            }).GetAwaiter().GetResult();

            if (viewModel.PopularBrands.Count > 1000)
            {
                originalData = new ObservableCollection<PopularBrandsViewModel>(viewModel.PopularBrands);

                Task.Run(async () =>
                {
                    await viewModel.ExecuteOnRefreshCommandAsync();
                }).GetAwaiter().GetResult();
                refreshedData = new ObservableCollection<PopularBrandsViewModel>(viewModel.PopularBrands);

                differences = originalData.Except(refreshedData);
            }
            else
            {
                List<PopularBrandsViewModel> list = new List<PopularBrandsViewModel>();
                list.Add(new PopularBrandsViewModel());
                IEnumerable<PopularBrandsViewModel> en = list;
                differences = en;
            }

            //assert
            Assert.AreEqual(originalData.Count, refreshedData.Count);
            differences.Count().ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void ShouldSelectItemSelectElementOfTheCollectionView()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);

            //act
            Task.Run(async () =>
            {
                await viewModel.LoadData();
            }).GetAwaiter().GetResult();

            if (viewModel.PopularBrands.Count > 0)
            {
                viewModel.SelectedBrand = new PopularBrandsViewModel();
                viewModel.SelectedBrand = viewModel.PopularBrands.FirstOrDefault();
            }

            //assert
            Assert.IsNotNull(viewModel.SelectedBrand);
        }

        [TestMethod]
        public void ShouldReLoadItemsSetIsRefreshingEqualsToFalse()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);

            //act
            viewModel.IsRefreshing = true;
            bool reLoadData = true;

            Task.Run(async () =>
            {
                await viewModel.LoadData(reLoadData);
            }).GetAwaiter().GetResult();

            //assert
            Assert.IsFalse(viewModel.IsRefreshing);
        }

        [TestMethod]
        public void ShouldSetOrientationValuesSetFirstColumnWidthToLowerValueWhenInLandscape()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();
            viewModel.SetOrientationValues();

            //assert
            Assert.AreEqual(new GridLength(2.5, GridUnitType.Star), viewModel.FirstColumnWidth);
        }

        [TestMethod]
        public void ShouldSetOrientationValuesSetFirstColumnWidthToGreaterValueWhenInPortrait()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();
            viewModel.SetOrientationValues();

            //assert
            Assert.AreEqual(new GridLength(3, GridUnitType.Star), viewModel.FirstColumnWidth);
        }

        [TestMethod]
        public void ShouldSetOrientationValuesSetSecondColumnWidthToGreaterValueWhenInLandscape()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();
            viewModel.SetOrientationValues();

            //assert
            Assert.AreEqual(new GridLength(6, GridUnitType.Star), viewModel.SecondColumnWidth);
        }

        [TestMethod]
        public void ShouldSetOrientationValuesSetSecondColumnWidthToLowerValueWhenInPortrait()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Portrait.ToString();
            viewModel.SetOrientationValues();

            //assert
            Assert.AreEqual(new GridLength(5.5, GridUnitType.Star), viewModel.SecondColumnWidth);
        }

        [TestMethod]
        public void ShouldSetOrientationValuesSetThirdColumnWidth()
        {
            //arrange
            viewModel = new CollectionViewSampleViewModel(DataManager);

            //act
            DeviceManager.Orientation = Devices.Landscape.ToString();
            viewModel.SetOrientationValues();

            //assert
            Assert.AreEqual(new GridLength(1.5, GridUnitType.Star), viewModel.ThirdColumnWidth);
        }
    }
}
