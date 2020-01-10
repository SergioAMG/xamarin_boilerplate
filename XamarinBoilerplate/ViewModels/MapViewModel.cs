using DataManagers.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBoilerplate.Controls;
using XamarinBoilerplate.Effects;
using XamarinBoilerplate.Enums;
using XamarinBoilerplate.Interfaces;
using XamarinBoilerplate.Utils;

namespace XamarinBoilerplate.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        private bool _hasSubMenu;
        private ICommand _viewMoreOptionsCommand;
        private ICommand _openDrawerCommand;
        private ICommand _commonToolbarItemTapCommand;
        private ICommand _favoritesCommand;
        private ObservableCollection<ImageButton> _buttons;
        private ObservableCollection<ExtendedLabel> _subMenu;

        public MapViewModel(IDataService dataManager = null) : base(dataManager)
        {
            if (dataManager != null)
            {
                DataManager = dataManager;
            }
            CreateOptionsMenu();
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

        public ICommand OpenDrawerCommand
        {
            get
            {
                return _openDrawerCommand ?? (_openDrawerCommand = new CommandExtended(ExecuteOpenDrawerCommandAsync));
            }
        }

        public ICommand ViewMoreOptionsCommand
        {
            get
            {
                return _viewMoreOptionsCommand ?? (_viewMoreOptionsCommand = new CommandExtended(ExecuteViewMoreOptionsCommandAsync));

            }
        }

        public ICommand CommonToolbarItemTapCommand
        {
            get
            {
                return _commonToolbarItemTapCommand ?? (_commonToolbarItemTapCommand = new CommandExtended(ExecuteCommonToolbarItemTapCommandAsync));
            }
        }

        public ICommand FavoritesCommand
        {
            get
            {
                return _favoritesCommand ?? (_favoritesCommand = new CommandExtended(ExecuteFavoritesCommandAsync));
            }
        }

        public void CreateOptionsMenu()
        {
            Buttons = new ObservableCollection<ImageButton>();

            ImageButton favButton = new ImageButton()
            {
                Source = "baseline_star_border_black_24",
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(0, 0, 18, 0),
                Command = FavoritesCommand
            };
            TintEffect.SetTintColor(favButton, (Color)Application.Current.Resources["ActionBarIconsColor"]);

            Buttons.Add(favButton);

            ExtendedPopupMenuButton optionsMenu = new ExtendedPopupMenuButton()
            {
                Source = "baseline_more_vert_black_24",
                IsVisible = true,
                BackgroundColor = Color.Transparent
            };
            TintEffect.SetTintColor(optionsMenu, (Color)Application.Current.Resources["ActionBarIconsColor"]);

            ExtendedLabel optionA = new ExtendedLabel() { Text = Localization.AppResources.ToolbarItemShowMyLocation, TapPressCommand = CommonToolbarItemTapCommand, CommandParameter = Localization.AppResources.ToolbarItemShowMyLocation };
            ExtendedLabel optionB = new ExtendedLabel() { Text = Localization.AppResources.ToolbarItemSearchLocation, TapPressCommand = CommonToolbarItemTapCommand, CommandParameter = Localization.AppResources.ToolbarItemSearchLocation };
            ExtendedLabel optionC = new ExtendedLabel() { Text = Localization.AppResources.ToolbarItemResetMap, TapPressCommand = CommonToolbarItemTapCommand, CommandParameter = Localization.AppResources.ToolbarItemResetMap };
            ExtendedLabel optionD = new ExtendedLabel() { Text = Localization.AppResources.ToolbarItemClearPins, TapPressCommand = CommonToolbarItemTapCommand, CommandParameter = Localization.AppResources.ToolbarItemClearPins };

            optionsMenu.Options.Clear();
            optionsMenu.Options.Add(optionA);
            optionsMenu.Options.Add(optionB);
            optionsMenu.Options.Add(optionC);
            optionsMenu.Options.Add(optionD);
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

        public async void HandleUserSelection(string userSelection)
        {
            await NavigationService.ClosePopUp();
            DependencyService.Get<IToast>().ShowToastMessage(Localization.AppResources.CommonToolbarItemTapped + " " + userSelection, false);
        }

        public async Task ExecuteOpenDrawerCommandAsync()
        {
            await NavigationService.OpenDrawer();
        }

        public async Task ExecuteViewMoreOptionsCommandAsync()
        {
            ObservableCollection<string> options = new ObservableCollection<string>();
            foreach (ExtendedLabel label in SubMenu)
            {
                options.Add(label.Text);
            }
            await NavigationService.OpenPopUp(new Views.Popups.UIAlertControllerPopup(options, HandleUserSelection));
        }

        private async Task ExecuteCommonToolbarItemTapCommandAsync(object sender)
        {
            string itemTapped = (string)sender;
            DependencyService.Get<IToast>().ShowToastMessage(Localization.AppResources.CommonToolbarItemTapped + " " + itemTapped, false);
        }

        private async Task ExecuteFavoritesCommandAsync()
        {
            DependencyService.Get<IToast>().ShowToastMessage(Localization.AppResources.FavoritesButtonPressed, true);
        }
    }
}
