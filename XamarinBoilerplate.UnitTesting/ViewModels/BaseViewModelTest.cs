using DataManagers.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Xamarin.Forms;
using XamarinBoilerplate.UnitTesting.Services;
using XamarinBoilerplate.ViewModels;

namespace XamarinBoilerplate.UnitTesting.ViewModels
{
    [TestFixture]
    public class BaseViewModelTest
    {
        private IDataService _dataManager;
        private static Application MobileApp;

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

        public IDataService DataManager
        {
            get { return _dataManager ?? (_dataManager = new MockDataWrapperService()); }
            set { _dataManager = value; }
        }
    }
}
