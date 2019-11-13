using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Xamarin.Forms;

namespace XamarinBoilerplate.UnitTesting.ViewModels
{
    [TestFixture]
    public class BaseViewModelTest
    {
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
    }
}
