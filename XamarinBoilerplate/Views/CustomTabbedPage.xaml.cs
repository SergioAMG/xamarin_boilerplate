using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace XamarinBoilerplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomTabbedPage : Xamarin.Forms.TabbedPage
    {
        public CustomTabbedPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}