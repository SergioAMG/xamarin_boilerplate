using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace XamarinBoilerplate.iOS.Effects
{
    public class NoScrollListViewEffect : PlatformEffect
    {
        private UITableView _nativeList => Control as UITableView;

        protected override void OnAttached()
        {
            if (_nativeList != null)
            {
                _nativeList.ScrollEnabled = false;
            }
        }

        protected override void OnDetached()
        {
        }
    }
}