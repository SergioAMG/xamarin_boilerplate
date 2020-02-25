using Xamarin.Forms.Xaml;
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
    }
}