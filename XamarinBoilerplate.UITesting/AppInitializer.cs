using Xamarin.UITest;
using Xamarin.UITest.Configuration;

namespace XamarinBoilerplate.UITesting
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android
                .InstalledApp("com.companyname.xamarinboilerplate")
                .StartApp(AppDataMode.Clear);
            }

            // iOS UI Test are not supported on Windows.
            return ConfigureApp.iOS
                .InstalledApp("com.companyname.xamarinboilerplate")
                .StartApp(AppDataMode.Clear);
        }
    }
}