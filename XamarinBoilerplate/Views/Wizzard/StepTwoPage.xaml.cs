using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using XamarinBoilerplate.Utils;

namespace XamarinBoilerplate.Views.Wizzard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StepTwoPage : BaseContentPage
    {
        public StepTwoPage()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            DeviceManager.Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
            (BindingContext as ViewModels.Wizzard.StepTwoViewModel).RefreshOrientation();
        }
    }
}