using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using XamarinBoilerplate.Utils;

namespace XamarinBoilerplate.Views.Wizzard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StepThreePage : BaseContentPage
    {
        public StepThreePage()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            DeviceManager.Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
            (BindingContext as ViewModels.Wizzard.StepThreeViewModel).RefreshOrientation();
        }
    }
}