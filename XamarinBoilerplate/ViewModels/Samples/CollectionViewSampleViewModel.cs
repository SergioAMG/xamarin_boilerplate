using DataManagers.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBoilerplate.Interfaces;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels.DataObjects;

namespace XamarinBoilerplate.ViewModels.Samples
{
    public class CollectionViewSampleViewModel : BaseViewModel
    {
        private bool _isRefreshing;
        private string _searchText;
        private ICommand _onRefreshCommand;
        private ICommand _onDeleteCommand;
        private ICommand _onFavoriteCommand;
        private ICommand _onPerformSearchCommand;
        private PopularBrandsViewModel _selectedBrand;
        private ObservableCollection<PopularBrandsViewModel> _popularBrands;
        private ObservableCollection<PopularBrandsViewModel> _popularBrandsFromServer;

        public Thickness SearchMargin
        {
            get
            {
                if (IsAndroid)
                {
                    return new Thickness(0, 0, 13, 5);
                }
                else
                {
                    return new Thickness(0, 0, 0, 5);
                }
            }
        }

        public GridLength FirstColumnWidth
        {
            get
            {
                return (DeviceManager.IsLandscape) ? new GridLength(2.5, GridUnitType.Star) : new GridLength(3, GridUnitType.Star);
            }
        }

        public GridLength SecondColumnWidth
        {
            get
            {
                return (DeviceManager.IsLandscape) ? new GridLength(6, GridUnitType.Star) : new GridLength(5.5, GridUnitType.Star);
            }
        }

        public GridLength ThirdColumnWidth
        {
            get
            {
                return new GridLength(1.5, GridUnitType.Star);
            }
        }

        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    OnPropertyChanged(nameof(IsRefreshing));
                }
            }
        }

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                }
            }
        }

        public PopularBrandsViewModel SelectedBrand
        {
            get
            {
                return _selectedBrand;
            }
            set
            {
                if (_selectedBrand != value)
                {
                    _selectedBrand = value;
                    OnPropertyChanged(nameof(SelectedBrand));
                    if (!UnitTestingManager.IsRunningFromNUnit)
                    {
                        DependencyService.Get<IToast>().ShowToastMessage(Localization.AppResources.ItemSelected + ": " + SelectedBrand.ItemTitle, false);
                    }
                }
            }
        }

        public ObservableCollection<PopularBrandsViewModel> PopularBrands
        {
            get
            {
                return _popularBrands;
            }
            set
            {
                if (_popularBrands != value)
                {
                    _popularBrands = value;
                    OnPropertyChanged(nameof(PopularBrands));
                }
            }
        }

        public ObservableCollection<PopularBrandsViewModel> PopularBrandsFromServer
        {
            get
            {
                return _popularBrandsFromServer;
            }
            set
            {
                if (_popularBrandsFromServer != value)
                {
                    _popularBrandsFromServer = value;
                    OnPropertyChanged(nameof(PopularBrandsFromServer));
                }
            }
        }

        public ICommand OnRefreshCommand
        {
            get
            {
                return _onRefreshCommand ?? (_onRefreshCommand = new CommandExtended(ExecuteOnRefreshCommandAsync));
            }
        }

        public ICommand OnDeleteCommand
        {
            get
            {
                return _onDeleteCommand ?? (_onDeleteCommand = new CommandExtended(ExecuteOnDeleteCommandAsync));
            }
        }

        public ICommand OnFavoriteCommand
        {
            get
            {
                return _onFavoriteCommand ?? (_onFavoriteCommand = new CommandExtended(ExecuteOnFavoriteCommandAsync));
            }
        }

        public ICommand OnPerformSearchCommand
        {
            get
            {
                return _onPerformSearchCommand ?? (_onPerformSearchCommand = new CommandExtended(ExecuteOnPerformSearchCommandAsync));
            }
        }

        public CollectionViewSampleViewModel(IDataService dataManager = null) : base(dataManager)
        {
            if (dataManager != null)
            {
                DataManager = dataManager;
            }
        }

        public async Task ExecuteOnRefreshCommandAsync()
        {
            SearchText = null;
            bool forceReload = true;
            await LoadData(forceReload);
        }

        public async Task ExecuteOnDeleteCommandAsync(object sender)
        {
            PopularBrandsViewModel popularBrandsViewModel = (PopularBrandsViewModel)sender;
            PopularBrands.Remove(popularBrandsViewModel);
            PopularBrandsFromServer.Remove(popularBrandsViewModel);
        }

        public async Task ExecuteOnFavoriteCommandAsync(object sender)
        {
            PopularBrandsViewModel popularBrandsViewModel = (PopularBrandsViewModel)sender;
            popularBrandsViewModel.IsFavorite = !popularBrandsViewModel.IsFavorite;
            PopularBrandsFromServer[PopularBrandsFromServer.IndexOf(popularBrandsViewModel)].IsFavorite = popularBrandsViewModel.IsFavorite;
        }

        public async Task ExecuteOnPerformSearchCommandAsync(object sender)
        {
            string textToSearch = string.Empty;
            try
            {
                textToSearch = (string)sender;
            }
            catch (Exception ex)
            {
                SearchBar searchBar = (SearchBar)sender;
                textToSearch = (searchBar.Text == null) ? "" : searchBar.Text;
            }

            PopularBrands = new ObservableCollection<PopularBrandsViewModel>(
                PopularBrandsFromServer.Where(x => x.ItemTitle.ToUpper().Contains(textToSearch.ToUpper()) ||
                                    x.Text.ToUpper().Contains(textToSearch.ToUpper())));
        }

        public void SetOrientationValues()
        {
            OnPropertyChanged(nameof(FirstColumnWidth));
            OnPropertyChanged(nameof(SecondColumnWidth));
            OnPropertyChanged(nameof(ThirdColumnWidth));
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadData();
        }

        public async Task LoadData(bool forceReload = false)
        {
            if (PopularBrands == null || forceReload)
            {
                PopularBrands = new ObservableCollection<PopularBrandsViewModel>();
                PopularBrandsFromServer = new ObservableCollection<PopularBrandsViewModel>();

                var brands = await DataManager.Brands.GetBrands();
                foreach (var item in brands)
                {
                    PopularBrandsViewModel popularBrandsViewModel = new PopularBrandsViewModel()
                    {
                        ItemTitle = item.ItemTitle,
                        Text = item.Text,
                        Image = item.Image,
                        IsFavorite = item.IsFavorite
                    };
                    PopularBrands.Add(popularBrandsViewModel);
                    PopularBrandsFromServer.Add(popularBrandsViewModel);
                }
            }

            if(forceReload)
                IsRefreshing = false;
        }
    }
}
