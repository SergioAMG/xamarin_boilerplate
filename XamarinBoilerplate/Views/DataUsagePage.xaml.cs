using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
    }
}