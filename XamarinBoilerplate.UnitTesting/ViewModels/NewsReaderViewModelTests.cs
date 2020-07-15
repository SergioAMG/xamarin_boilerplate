using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Threading.Tasks;
using XamarinBoilerplate.ViewModels;
using XamarinBoilerplate.ViewModels.DataObjects;

namespace XamarinBoilerplate.UnitTesting.ViewModels
{
    [TestClass]
    public class NewsReaderViewModelTests : BaseViewModelTest
    {
        private NewsReaderViewModel viewModel;

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
            viewModel = new NewsReaderViewModel(DataManager);

            //act

            //assert
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void ShouldInitMethodLoadNewsData()
        {
            //arrange
            viewModel = new NewsReaderViewModel(DataManager);
            NewsViewModel newsViewModel = new NewsViewModel()
            {
                ItemTitle = "Sample Title",
                Text = "This is a sample text to showcase features of news reader.",
                Image = "sampleOne",
                Date = DateTime.Now
            };

            //act
            viewModel.Init(newsViewModel);

            //assert
            viewModel.NewsViewModel.ItemTitle.ShouldBe(newsViewModel.ItemTitle);
            viewModel.NewsViewModel.Text.ShouldBe(newsViewModel.Text);
            viewModel.NewsViewModel.Image.ShouldBe(newsViewModel.Image);
            Assert.AreEqual(viewModel.NewsViewModel.Date, newsViewModel.Date);
        }

        [TestMethod]
        public void ShouldExecuteTextSizeIncreaseTextSize()
        {
            //arrange
            viewModel = new NewsReaderViewModel(DataManager);
            double titleTextSize = viewModel.TitleTextSize;
            double textSize = viewModel.TextSize;

            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteIncreaseTextSizeCommandAsync();
            }).GetAwaiter().GetResult();

            //assert
            viewModel.TitleTextSize.ShouldBeGreaterThan(titleTextSize);
            viewModel.TextSize.ShouldBeGreaterThan(textSize);
        }

        [TestMethod]
        public void ShouldExecuteTextSizeDecreaseTextSize()
        {
            //arrange
            viewModel = new NewsReaderViewModel(DataManager);
            double titleTextSize = viewModel.TitleTextSize;
            double textSize = viewModel.TextSize;

            //act
            Task.Run(async () =>
            {
                await viewModel.ExecuteDecreaseTextSizeCommandAsync();
            }).GetAwaiter().GetResult();

            //assert
            viewModel.TitleTextSize.ShouldBeLessThan(titleTextSize);
            viewModel.TextSize.ShouldBeLessThan(textSize);
        }
    }
}
