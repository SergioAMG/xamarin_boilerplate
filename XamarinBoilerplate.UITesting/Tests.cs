using NUnit.Framework;
using Xamarin.UITest;

namespace XamarinBoilerplate.UITesting
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void SampleTest()
        {
            // TODO: UI Test must be properly implemented on the Solution.
            Assert.IsTrue(true);
        }
    }
}
