using XamarinBoilerplate.Interfaces;
using XamarinBoilerplate.Utils;
using Xamarin.Forms;
using XamarinBoilerplate.iOS.DependencyServices;
using Foundation;
using UIKit;

[assembly: Dependency(typeof(ToastDependencyService))]
namespace XamarinBoilerplate.iOS.DependencyServices
{
    public class ToastDependencyService : IToast
    {
        NSTimer alertDelay;
        UIAlertController alert;

        public void ShowToastMessage(string message, bool shortDuration)
        {
            double seconds = (shortDuration) ? Constants.ShortToastDuration : Constants.LongToastDuration;
            ShowAlert(message, seconds);
        }

        public void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                DismissAlert();
            });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        public void DismissAlert()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}