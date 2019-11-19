using Microsoft.VisualStudio.TestTools.UnitTesting;
using XamarinBoilerplate.ViewModels;

namespace XamarinBoilerplate.UnitTesting.ViewModels
{
    [TestClass]
    public class CustomTabbedViewModelTests : BaseViewModelTest
    {
        private CustomTabbedViewModel viewModel;

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
            viewModel = new CustomTabbedViewModel();

            //act
            // Tabs navigations is handled by Xamarin

            //assert
            Assert.IsNotNull(viewModel);
        }
    }
}
