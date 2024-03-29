﻿using DataManagers.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBoilerplate.Interfaces;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels.DataObjects;
using XamarinBoilerplate.Views;

namespace XamarinBoilerplate.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private bool _isLoading;
        private ICommand _openDrawerCommand;
        private ICommand _viewMoreOptionsCommand;
        private ICommand _commonToolbarItemTapCommand;
        private ICommand _floatingButtonCommand;
        private NewsViewModel _itemSelected;
        private ObservableCollection<NewsViewModel> _newsItems;

        public HomeViewModel(IDataService dataManager = null) : base(dataManager)
        {
            if (dataManager != null)
            {
                DataManager = dataManager;
            };
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadData();
        }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged(nameof(IsLoading));
                }
            }
        }

        public double LoadingIndicatorScale
        {
            get
            {
                return (IsAndroid) ? 1.5 : 2.0;
            }
        }

        public NewsViewModel ItemSelected
        {
            get
            {
                return _itemSelected;
            }
            set
            {
                if (value != null)
                {
                    _itemSelected = value;
                    OnPropertyChanged(nameof(ItemSelected));
                    NavigationService.NavigateAsync(nameof(NewsReaderPage), ItemSelected, true);
                }
            }
        }

        public ObservableCollection<NewsViewModel> NewsItems
        {
            get
            {
                return _newsItems;
            }
            set
            {
                if (_newsItems != value)
                {
                    _newsItems = value;
                    OnPropertyChanged(nameof(NewsItems));
                }
            }
        }

        public ICommand ViewMoreOptionsCommand
        {
            get
            {
                return _viewMoreOptionsCommand ?? (_viewMoreOptionsCommand = new CommandExtended(ExecuteViewMoreOptionsCommandAsync));

            }
        }

        public ICommand OpenDrawerCommand
        {
            get
            {
                return _openDrawerCommand ?? (_openDrawerCommand = new CommandExtended(ExecuteOpenDrawerCommandAsync));
            }
        }

        public ICommand CommonToolbarItemTapCommand
        {
            get
            {
                return _commonToolbarItemTapCommand ?? (_commonToolbarItemTapCommand = new CommandExtended(ExecuteCommonToolbarItemTapCommandAsync));
            }
        }

        public ICommand FloatingButtonCommand
        {
            get
            {
                return _floatingButtonCommand ?? (_floatingButtonCommand = new CommandExtended(ExecuteFloatingButtonCommanddAsync));
            }
        }

        public async Task LoadData()
        {
            if (NewsItems == null)
            {
                NewsItems = new ObservableCollection<NewsViewModel>();
                var news = await DataManager.News.GetNews();

                // Mappings
                foreach (var item in news)
                {
                    NewsViewModel newsViewModel = new NewsViewModel()
                    {
                        ItemTitle = item.ItemTitle,
                        Image = item.Image,
                        Text = item.Text,
                        Date = item.Date
                    };
                    NewsItems.Add(newsViewModel);
                }
                if (!UnitTestingManager.IsRunningFromNUnit)
                {
                    await NavigationService.HideLoadingIndicator();
                }
            }
        }

        public async Task ExecuteOpenDrawerCommandAsync()
        {
            await NavigationService.OpenDrawer();
        }

        public async Task ExecuteViewMoreOptionsCommandAsync()
        {
            ObservableCollection<string> options = new ObservableCollection<string>()
            {
                Localization.AppResources.ToolbarItemOrderByDate,
                Localization.AppResources.ToolbarItemOrderByPopularity,
                Localization.AppResources.ToolbarItemIncreaseTextSize,
                Localization.AppResources.ToolbarItemDecreaseTextSize
            };
            await NavigationService.OpenPopUp(new Views.Popups.UIAlertControllerPopup(options, HandleUserSelection));
        }

        private async Task ExecuteCommonToolbarItemTapCommandAsync(object sender)
        {
            string itemTapped = (string)sender;
            DependencyService.Get<IToast>().ShowToastMessage(Localization.AppResources.CommonToolbarItemTapped + " " + itemTapped, false);
        }

        private async Task ExecuteFloatingButtonCommanddAsync()
        {
            await NavigationService.CurrentMasterDetailPage.DisplayAlert(Localization.AppResources.SearchNewsText, 
                Localization.AppResources.SearchButtonTapped, Localization.AppResources.Okay);
        }

        private async void HandleUserSelection(string userSelection)
        {
            await NavigationService.ClosePopUp();
            DependencyService.Get<IToast>().ShowToastMessage(Localization.AppResources.CommonToolbarItemTapped + " " + userSelection, false);
        }
    }
}
