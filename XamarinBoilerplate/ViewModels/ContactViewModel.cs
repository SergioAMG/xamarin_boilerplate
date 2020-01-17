using DataManagers.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBoilerplate.Controls;
using XamarinBoilerplate.Effects;
using XamarinBoilerplate.Enums;
using XamarinBoilerplate.Interfaces;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.Views;

namespace XamarinBoilerplate.ViewModels
{
    public class ContactViewModel : BaseViewModel
    {
        private int _selectedTabIndex;
        private double _detailsViewWidth;
        private bool _isNavBarVisible;
        private ICommand _viewMoreOptionsCommand;
        private ICommand _backFromDetailsCommand;
        private ICommand _commonToolbarItemTapCommand;
        private ICommand _sendMessageCommand;

        public ContactViewModel(IDataService dataManager = null) : base(dataManager)
        {
            if (dataManager != null)
            {
                DataManager = dataManager;
            }
            SetOrientationValues();
            bool isNavBarVisible = true;
            SetNavBarVisibility(isNavBarVisible);
        }

        public int SelectedTabIndex
        {
            get
            {
                return _selectedTabIndex;
            }
            set
            {
                if (_selectedTabIndex != value)
                {
                    _selectedTabIndex = value;
                    OnPropertyChanged((nameof(SelectedTabIndex)));
                }
            }
        }

        public bool IsBottomButtonVisible
        {
            get
            {
                return !DeviceManager.IsLandscape;
            }
        }

        public bool IsLandscapeButtonVisible
        {
            get
            {
                return DeviceManager.IsLandscape;
            }
        }

        public double DetailsViewWidth
        {
            get
            {
                return _detailsViewWidth;
            }
            set
            {
                if (_detailsViewWidth != value)
                {
                    _detailsViewWidth = value;
                    OnPropertyChanged(nameof(DetailsViewWidth));
                }
            }
        }

        public StackOrientation MainContainerOrientation
        {
            get
            {
                return DeviceManager.Orientation == Devices.Landscape.ToString() ? StackOrientation.Horizontal : StackOrientation.Vertical;
            }
        }

        public bool IsNavBarVisible
        {
            get
            {
                return _isNavBarVisible;
            }
            set
            {
                if (_isNavBarVisible != value)
                {
                    _isNavBarVisible = value;
                    OnPropertyChanged(nameof(IsNavBarVisible));
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

        public ICommand BackFromDetailsCommand
        {
            get
            {
                return _backFromDetailsCommand ?? (_backFromDetailsCommand = new CommandExtended(ExecuteBackFromDetailsCommandAsync));

            }
        }

        public ICommand CommonToolbarItemTapCommand
        {
            get
            {
                return _commonToolbarItemTapCommand ?? (_commonToolbarItemTapCommand = new CommandExtended(ExecuteCommonToolbarItemTapCommandAsync));
            }
        }

        public ICommand SendMessageCommand
        {
            get
            {
                return _sendMessageCommand ?? (_sendMessageCommand = new CommandExtended(ExecuteSendMessageCommandAsync));
            }
        }

        public void Init(int selectedTabIndex)
        {
            SelectedTabIndex = selectedTabIndex;
        }

        public void SetNavBarVisibility(bool visibility)
        {
            IsNavBarVisible = visibility;
        }

        public void SetOrientationValues()
        {
            DetailsViewWidth = (DeviceManager.Orientation == DisplayOrientation.Landscape.ToString()) ? (App.ScreenWidth * Constants.ContactPageDetailsViewWidthFactor) : App.ScreenWidth;
            OnPropertyChanged(nameof(MainContainerOrientation));
            OnPropertyChanged(nameof(IsBottomButtonVisible));
            OnPropertyChanged(nameof(IsLandscapeButtonVisible));
        }

        private async Task ExecuteViewMoreOptionsCommandAsync()
        {
            ObservableCollection<string> options = new ObservableCollection<string>()
            {
                Localization.AppResources.ToolbarItemPhoneContact,
                Localization.AppResources.ToolbarItemEmailContact,
                Localization.AppResources.ToolbarItemWhatsAppContact
            };
            await NavigationService.OpenPopUp(new Views.Popups.UIAlertControllerPopup(options, HandleUserSelection));
        }

        private async void HandleUserSelection(string userSelection)
        {
            await NavigationService.ClosePopUp();
            DependencyService.Get<IToast>().ShowToastMessage(Localization.AppResources.CommonToolbarItemTapped + " " + userSelection, false);
        }

        public async Task ExecuteBackFromDetailsCommandAsync()
        {
            NavigationService.NavigateDetails(nameof(CustomTabbedPage), SelectedTabIndex);
        }

        private async Task ExecuteCommonToolbarItemTapCommandAsync(object sender)
        {
            string itemTapped = (string)sender;
            DependencyService.Get<IToast>().ShowToastMessage(Localization.AppResources.CommonToolbarItemTapped + " " + itemTapped, false);
        }

        private async Task ExecuteSendMessageCommandAsync()
        {
            DependencyService.Get<IToast>().ShowToastMessage(Localization.AppResources.Button + " " + Localization.AppResources.Submit + " " + Localization.AppResources.Tapped, false);
        }
    }
}
