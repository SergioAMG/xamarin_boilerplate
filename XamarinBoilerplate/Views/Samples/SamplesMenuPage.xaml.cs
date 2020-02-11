using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBoilerplate.ViewModels.Samples;

namespace XamarinBoilerplate.Views.Samples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SamplesMenuPage : BaseContentPage
    {
        public SamplesMenuPage(int selectedTabIndex)
        {
            InitializeComponent();
            (BindingContext as SamplesMenuViewModel).Init(selectedTabIndex);
        }

        private void ListViewItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;
            listView.SelectedItem = null;
        }
    }
}