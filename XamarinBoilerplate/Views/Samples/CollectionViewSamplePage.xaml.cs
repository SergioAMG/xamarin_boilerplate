using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels.Samples;

namespace XamarinBoilerplate.Views.Samples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollectionViewSamplePage : BaseContentPage
    {
        public CollectionViewSamplePage()
        {
            InitializeComponent();
        }

        private async void searchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            CollectionViewSampleViewModel viewModel = (BindingContext as CollectionViewSampleViewModel);

            // If iOS Clear
            if (searchBar.SearchCommandParameter != null)
            {
                // If Android Clear
                if (!string.IsNullOrEmpty(searchBar.SearchCommandParameter.ToString()))
                {
                    await viewModel.ExecuteOnPerformSearchCommandAsync(searchBar);
                }
                else
                {
                    if (viewModel.IsAndroid)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            searchBar.Unfocus();
                        });
                    }
                    await viewModel.ExecuteOnPerformSearchCommandAsync(searchBar);
                }
            }
            else
            {
                searchBar.SearchCommandParameter = "";
                searchBar.Text = "";
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            DeviceManager.Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
            (BindingContext as CollectionViewSampleViewModel).SetOrientationValues();
            (BindingContext as CollectionViewSampleViewModel).RefreshMainContainerMargins();
        }
    }
}