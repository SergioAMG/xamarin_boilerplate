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
    public class ContactViewModel : BaseViewModel
    {
        private int _selectedTabIndex;
        private ICommand _viewMoreOptionsCommand;
        private ICommand _backFromDetailsCommand;
        private ObservableCollection<ImageButton> _buttons;
        private ObservableCollection<ExtendedLabel> _subMenu;

        public ContactViewModel()
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

        public void Init(int selectedTabIndex)
        {
            SelectedTabIndex = selectedTabIndex;
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
            NavigationService.NavigateDetails(nameof(CustomTabbedPage), SelectedTabIndex);
        }

        public void CreateOptionsMenu()
        {
            Buttons = new ObservableCollection<ImageButton>();

            ImageButton helpButton = new ImageButton()
            {
                Source = "baseline_help_outline_black_24",
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(0, 0, 18, 0),
                Command = BackFromDetailsCommand
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

            ExtendedLabel optionA = new ExtendedLabel() { Text = Localization.AppResources.ToolbarItemPhoneContact, TapPressCommand = BackFromDetailsCommand };
            ExtendedLabel optionB = new ExtendedLabel() { Text = Localization.AppResources.ToolbarItemEmailContact, TapPressCommand = BackFromDetailsCommand };
            ExtendedLabel optionC = new ExtendedLabel() { Text = Localization.AppResources.ToolbarItemWhatsAppContact, TapPressCommand = BackFromDetailsCommand };

            optionsMenu.Options.Clear();
            optionsMenu.Options.Add(optionA);
            optionsMenu.Options.Add(optionB);
            optionsMenu.Options.Add(optionC);
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

        private async Task ExecuteBackFromDetailsCommandAsync()
        {
            NavigationService.NavigateDetails(nameof(CustomTabbedPage), SelectedTabIndex);
        }
    }
}
