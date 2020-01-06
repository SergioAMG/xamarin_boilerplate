using Android.Widget;
using Xamarin.Forms;
using XamarinBoilerplate.Droid.DependencyServices;
using XamarinBoilerplate.Interfaces;

[assembly: Dependency(typeof(ToastDependencyService))]
namespace XamarinBoilerplate.Droid.DependencyServices
{
    public class ToastDependencyService : IToast
    {
        public void ShowToastMessage(string message, bool shortDuration)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, (shortDuration) ? ToastLength.Short : ToastLength.Long ).Show();
        }
    }
}