using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels;

namespace XamarinBoilerplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : BaseContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            DeviceManager.Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
            (BindingContext as MenuViewModel).RefreshMainMargins();
        }
    }
}