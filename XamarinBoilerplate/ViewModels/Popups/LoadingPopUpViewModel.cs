using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBoilerplate.Enums;

namespace XamarinBoilerplate.ViewModels.Popups
{
    public class LoadingPopUpViewModel : BaseViewModel
    {
        public Thickness PopupTopPadding
        {
            get
            {
                return (DeviceInfo.Platform.ToString() == Devices.iOS.ToString()) ?
                    new Thickness(0, 20, 0, 0) : new Thickness(0, 0, 0, 0);
            }
        }
    }
}
