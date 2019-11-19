using Microsoft.VisualStudio.TestTools.UnitTesting;
using XamarinBoilerplate.ViewModels;

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
        public void ShouldViewModelbeInitializedAndAssociated()
        {
            //arrange
            viewModel = new HomeViewModel();

            //act

            //assert
            Assert.IsNotNull(viewModel);
        }
    }
}
