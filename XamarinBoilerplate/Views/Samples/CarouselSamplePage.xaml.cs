using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels.Samples;

namespace XamarinBoilerplate.Views.Samples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarouselSamplePage : BaseContentPage
    {
        public CarouselSamplePage()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            DeviceManager.Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
            (BindingContext as CarouselSampleViewModel).SetOrientationValues();
            (BindingContext as CarouselSampleViewModel).RefreshMainContainerMargins();
        }
    }
}