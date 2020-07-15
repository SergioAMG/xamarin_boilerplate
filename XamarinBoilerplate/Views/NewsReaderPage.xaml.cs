using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels;
using XamarinBoilerplate.ViewModels.DataObjects;

namespace XamarinBoilerplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsReaderPage : BaseContentPage
    {
        public NewsReaderPage(NewsViewModel newsViewModel)
        {
            InitializeComponent();
            (BindingContext as NewsReaderViewModel).Init(newsViewModel);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            DeviceManager.Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
            (BindingContext as NewsReaderViewModel).RefreshMainContainerMargins();
        }
    }
}