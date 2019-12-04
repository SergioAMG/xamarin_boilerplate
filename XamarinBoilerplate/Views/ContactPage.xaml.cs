using Xamarin.Forms.Xaml;
using XamarinBoilerplate.ViewModels;

namespace XamarinBoilerplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPage : BaseContentPage
    {
        public ContactPage()
        {
            InitializeComponent();
        }

        public ContactPage(int selectedTabIndex)
        {
            InitializeComponent();
            (BindingContext as ContactViewModel).Init(selectedTabIndex);
        }
    }
}