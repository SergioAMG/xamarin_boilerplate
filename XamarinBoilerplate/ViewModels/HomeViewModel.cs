using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBoilerplate.Controls;
using XamarinBoilerplate.Effects;
using XamarinBoilerplate.Enums;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.Views;

namespace XamarinBoilerplate.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private ICommand _openDrawerCommand;
        private ObservableCollection<ImageButton> _buttons;
        private ObservableCollection<ExtendedLabel> _subMenu;
        private ICommand _viewMoreOptionsCommand;
        private ICommand _backFromDetailsCommand;

        public HomeViewModel()
        {
            CreateOptionsMenu();
        }

        public bool IsAndroid
        {
            get
            {
                return DeviceInfo.Platform.ToString() == Devices.Android.ToString();
            }
        }

        public bool IsIOS
        {
            get
            {
                return DeviceInfo.Platform.ToString() == Devices.iOS.ToString();
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

        public ICommand OpenDrawerCommand
        {
            get
            {
                return _openDrawerCommand ?? (_openDrawerCommand = new CommandExtended(ExecuteOpenDrawerCommandAsync));
            }
        }

        private async Task ExecuteOpenDrawerCommandAsync()
        {
            await NavigationService.OpenDrawer();
        }

        public void CreateOptionsMenu()
        {
            Buttons = new ObservableCollection<ImageButton>();

            ImageButton closeButton = new ImageButton()
            {
                Source = "baseline_sync_black_24",
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(0, 0, 23, 0),
                Command = BackFromDetailsCommand
            };
            TintEffect.SetTintColor(closeButton, (Color)Application.Current.Resources["ActionBarIconsColor"]);

            Buttons.Add(closeButton);

            ImageButton closeButtonTwo = new ImageButton()
            {
                Source = "baseline_star_border_black_24",
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(0, 0, 18, 0),
                Command = BackFromDetailsCommand
            };
            TintEffect.SetTintColor(closeButtonTwo, (Color)Application.Current.Resources["ActionBarIconsColor"]);

            Buttons.Add(closeButtonTwo);

            ExtendedPopupMenuButton optionsMenu = new ExtendedPopupMenuButton()
            {
                Source = "baseline_more_vert_black_24",
                IsVisible = true,
                BackgroundColor = Color.Transparent
            };
            TintEffect.SetTintColor(optionsMenu, (Color)Application.Current.Resources["ActionBarIconsColor"]);

            ExtendedLabel optionA = new ExtendedLabel() { Text = Localization.AppResources.ToolbarItemOrderByDate, TapPressCommand = BackFromDetailsCommand };
            ExtendedLabel optionB = new ExtendedLabel() { Text = Localization.AppResources.ToolbarItemOrderByPopularity, TapPressCommand = BackFromDetailsCommand };
            ExtendedLabel optionC = new ExtendedLabel() { Text = Localization.AppResources.ToolbarItemIncreaseTextSize, TapPressCommand = BackFromDetailsCommand };
            ExtendedLabel optionD = new ExtendedLabel() { Text = Localization.AppResources.ToolbarItemDecreaseTextSize, TapPressCommand = BackFromDetailsCommand };

            optionsMenu.Options.Clear();
            optionsMenu.Options.Add(optionA);
            optionsMenu.Options.Add(optionB);
            optionsMenu.Options.Add(optionC);
            optionsMenu.Options.Add(optionD);
            SubMenu = optionsMenu.Options;

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

        private async Task ExecuteViewMoreOptionsCommandAsync()
        {
            ObservableCollection<string> options = new ObservableCollection<string>();
            foreach (ExtendedLabel label in SubMenu)
            {
                options.Add(label.Text);
            }
            await NavigationService.OpenPopUp(new Views.Popups.UIAlertControllerPopup(options, HandleUserSelection));
        }

        private void HandleUserSelection(string userSelection)
        {

        }

        private async Task ExecuteBackFromDetailsCommandAsync()
        {
            
        }
    }
}
