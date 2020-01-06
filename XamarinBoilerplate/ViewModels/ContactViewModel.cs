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
        private bool _hasSubMenu;
        private bool _isNavBarVisible;
        private ICommand _helpCommand;
        private ICommand _viewMoreOptionsCommand;
        private ICommand _backFromDetailsCommand;
        private ICommand _commonToolbarItemTapCommand;
        private ObservableCollection<ImageButton> _buttons;
        private ObservableCollection<ExtendedLabel> _subMenu;

        public ContactViewModel()
        {
            CreateOptionsMenu();
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

        public bool IsAndroid
        {
            get
            {
                return DeviceManager.Platform == Devices.Android.ToString();
            }
        }

        public bool IsIOS
        {
            get
            {
                return DeviceManager.Platform == Devices.iOS.ToString();
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

        public bool HasSubMenu
        {
            get
            {
                return _hasSubMenu;
            }
            set
            {
                if (_hasSubMenu != value)
                {
                    _hasSubMenu = value;
                    OnPropertyChanged(nameof(HasSubMenu));
                }
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

        public ObservableCollection<ImageButton> Buttons
        {
            get
            {
                return _buttons;
            }
            set
            {
                if (_buttons != value)
                {
                    _buttons = value;
                    OnPropertyChanged((nameof(_buttons)));
                }
            }
        }

        public ObservableCollection<ExtendedLabel> SubMenu
        {
            get
            {
                return _subMenu;
            }
            set
            {
                if (_subMenu != value)
                {
                    _subMenu = value;
                    OnPropertyChanged((nameof(SubMenu)));
                }
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

        public ICommand HelpCommand
        {
            get
            {
                return _helpCommand ?? (_helpCommand = new CommandExtended(ExecuteHelpCommandAsync));
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

        public void CreateOptionsMenu()
        {
            Buttons = new ObservableCollection<ImageButton>();

            ImageButton helpButton = new ImageButton()
            {
                Source = "baseline_help_outline_black_24",
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(0, 0, 18, 0),
                Command = HelpCommand
            };
            TintEffect.SetTintColor(helpButton, (Color)Application.Current.Resources["ActionBarIconsColor"]);

            Buttons.Add(helpButton);

            ExtendedPopupMenuButton optionsMenu = new ExtendedPopupMenuButton()
            {
                Source = "baseline_more_vert_black_24",
                IsVisible = true,
                BackgroundColor = Color.Transparent
            };
            TintEffect.SetTintColor(optionsMenu, (Color)Application.Current.Resources["ActionBarIconsColor"]);

            ExtendedLabel optionA = new ExtendedLabel() { Text = Localization.AppResources.ToolbarItemPhoneContact, TapPressCommand = CommonToolbarItemTapCommand, CommandParameter = Localization.AppResources.ToolbarItemPhoneContact };
            ExtendedLabel optionB = new ExtendedLabel() { Text = Localization.AppResources.ToolbarItemEmailContact, TapPressCommand = CommonToolbarItemTapCommand, CommandParameter = Localization.AppResources.ToolbarItemEmailContact };
            ExtendedLabel optionC = new ExtendedLabel() { Text = Localization.AppResources.ToolbarItemWhatsAppContact, TapPressCommand = CommonToolbarItemTapCommand, CommandParameter = Localization.AppResources.ToolbarItemWhatsAppContact };

            optionsMenu.Options.Clear();
            optionsMenu.Options.Add(optionA);
            optionsMenu.Options.Add(optionB);
            optionsMenu.Options.Add(optionC);
            SubMenu = optionsMenu.Options;

            HasSubMenu = true;

            Buttons.Add(optionsMenu);
            OnPropertyChanged(nameof(Buttons));

            if (IsIOS)
            {
                Buttons.Remove(optionsMenu);

                ImageButton optionsMenuIOS = new ImageButton()
                {
                    Source = "baseline_more_vert_black_24",
                    Margin = new Thickness(0, 0, 10, 0),
                    BackgroundColor = Color.Transparent,
                    VerticalOptions = LayoutOptions.Center,
                    Command = ViewMoreOptionsCommand
                };

                TintEffect.SetTintColor(optionsMenuIOS, (Color)Application.Current.Resources["ActionBarIconsColor"]);

                Buttons.Add(optionsMenuIOS);
                OnPropertyChanged(nameof(Buttons));
            }
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
            ObservableCollection<string> options = new ObservableCollection<string>();
            foreach (ExtendedLabel label in SubMenu)
            {
                options.Add(label.Text);
            }
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

        private async Task ExecuteHelpCommandAsync()
        {
            DependencyService.Get<IToast>().ShowToastMessage(Localization.AppResources.HelpButtonPressed, true);
        }
    }
}
