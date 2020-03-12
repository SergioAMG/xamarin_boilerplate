using DataManagers.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
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
        private string _yourName;
        private string _yourPhone;
        private string _yourEmail;
        private string _yourMessage;
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

        public string YourName
        {
            get
            {
                return _yourName;
            }
            set
            {
                if (_yourName != value)
                {
                    _yourName = value;
                    OnPropertyChanged(nameof(YourName));
                }
            }
        }

        public string YourPhone
        {
            get
            {
                return _yourPhone;
            }
            set
            {
                if (_yourPhone != value)
                {
                    _yourPhone = value;
                    OnPropertyChanged(nameof(YourPhone));
                }
            }
        }

        public string YourEmail
        {
            get
            {
                return _yourEmail;
            }
            set
            {
                if (_yourEmail != value)
                {
                    _yourEmail = value;
                    OnPropertyChanged(nameof(YourEmail));
                }
            }
        }

        public string YourMessage
        {
            get
            {
                return _yourMessage;
            }
            set
            {
                if (_yourMessage != value)
                {
                    _yourMessage = value;
                    OnPropertyChanged(nameof(YourMessage));
                }
            }
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

        public Thickness BottomMarginForSubmitButton
        {
            get
            {
                if (DeviceManager.IsIOSVersionGreaterOrEqualToSupportedIOSVersion())
                {
                    Thickness customMargin;
                    switch (DeviceManager.GetAppleDeviceType())
                    {
                        case AppleDeviceType.iPhone4                       : 
                        case AppleDeviceType.iPhoneSE_5                    : 
                        case AppleDeviceType.iPhone8_7_6                   : 
                        case AppleDeviceType.iPhone8Plus_7Plus_6SPlus_6Plus: customMargin = new Thickness(0, 0, 0, 40); break;
                        case AppleDeviceType.iPhoneX_XS_11Pro              : 
                        case AppleDeviceType.iPhone11_XR                   : 
                        case AppleDeviceType.iPhone11ProMax_XSMax          : customMargin = new Thickness(0, 0, 0, 20); break;
                        default                                            : customMargin = new Thickness(0, 0, 0, 20); break;
                    }
                    return customMargin;
                }
                else
                {
                    return new Thickness(0, 0, 0, 20);
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
            Page currentDetailsPage = NavigationService.GetCurrentDetailsPage();

            if (string.IsNullOrWhiteSpace(YourName) ||
                string.IsNullOrWhiteSpace(YourPhone) ||
                string.IsNullOrWhiteSpace(YourEmail) ||
                string.IsNullOrWhiteSpace(YourMessage))
            {
                await currentDetailsPage.DisplayAlert(
                        Localization.AppResources.Error,
                        Localization.AppResources.AllFieldsRequired,
                        Localization.AppResources.Okay);
            }
            else
            {
                await currentDetailsPage.DisplayAlert(
                        Localization.AppResources.Success,
                        Localization.AppResources.MessageSent,
                        Localization.AppResources.Okay);

                YourName = string.Empty;
                YourPhone = string.Empty;
                YourEmail = string.Empty;
                YourMessage = string.Empty;
            }
        }
    }
}
