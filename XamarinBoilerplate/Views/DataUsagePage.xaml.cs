using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels;

namespace XamarinBoilerplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataUsagePage : BaseContentPage
    {
        public DataUsagePage()
        {
            InitializeComponent();
        }

        private void SliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            var slider = (Slider)sender;

            if (e.NewValue > e.OldValue)
            {
                progressBar.Progress = progressBar.Progress + slider.Value * 0.001;
            }
            else
            {
                progressBar.Progress = progressBar.Progress - slider.Value * 0.001;
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            DeviceManager.Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
            (BindingContext as DataUsageViewModel).RefreshMainContainerMargins();
        }
    }
}