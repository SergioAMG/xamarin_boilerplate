using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinBoilerplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : BaseContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = (Xamarin.Forms.ListView)sender;
            listView.SelectedItem = null;
        }
    }
}