using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinBoilerplate.Droid.Effects;

[assembly: ExportEffect(typeof(NoScrollListViewEffect), "NoScrollListViewEffect")]
namespace XamarinBoilerplate.Droid.Effects
{
    public class NoScrollListViewEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            // Effect not available on Android.
        }

        protected override void OnDetached()
        {
            // Effect not available on Android.
        }
    }
}