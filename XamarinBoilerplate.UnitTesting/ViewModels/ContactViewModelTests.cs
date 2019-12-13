﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using XamarinBoilerplate.ViewModels;
using XamarinBoilerplate.Views;

namespace XamarinBoilerplate.UnitTesting.ViewModels
{
    [TestClass]
    public class ContactViewModelTests : BaseViewModelTest
    {
        private ContactViewModel viewModel;

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
            viewModel = new ContactViewModel();

            //act

            //assert
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void ShouldCreateOptionsMenuCreateToolbarItems()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            viewModel.CreateOptionsMenu();

            //assert
            Assert.IsNotNull(viewModel.Buttons);
        }

        [TestMethod]
        public void ShouldCreateOptionsMenuCreateAlsoSubMenu()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            viewModel.CreateOptionsMenu();

            //assert
            Assert.IsTrue(viewModel.HasSubMenu);
            Assert.IsNotNull(viewModel.SubMenu);
        }

        [TestMethod]
        public void ShouldInitMethodUpdateSelectedTabIndex()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            int selectedIndex = 1;
            viewModel.Init(selectedIndex);

            //asert
            Assert.AreEqual(viewModel.SelectedTabIndex, selectedIndex);
        }

        [TestMethod]
        public void ShouldBackFromDetailsCommandTakeYouBackToSelectedTabActive()
        {
            //arrange
            viewModel = new ContactViewModel();

            //act
            viewModel.NavigationService.SetRootPage(nameof(DashboardPage), new DashboardViewModel());
            viewModel.NavigationService.NavigateAsync(nameof(CustomTabbedPage), null, false);
            int selectedTab = 1;
            viewModel.Init(selectedTab);
            Task.Run(async () =>
            {
                await viewModel.ExecuteBackFromDetailsCommandAsync();
            }).GetAwaiter().GetResult();

            //assert
            var currentPage = viewModel.NavigationService.CurrentPage;
        }
    }
}
