using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBoilerplate.Enums;
using XamarinBoilerplate.Utils;

namespace XamarinBoilerplate.ViewModels.Popups
{
    public class UIAlertControllerPopupViewModel : BaseViewModel
    {
        private ObservableCollection<string> _options;
        private ICommand _closePopupCommand;
        private string _selectedOption;
        public Action<string> ItemSelected { get; set; }

        public UIAlertControllerPopupViewModel(ObservableCollection<string> options, Action<string> itemSelected)
        {
            Options = new ObservableCollection<string>();
            foreach (var option in options)
            {
                Options.Add(option);
            }
            ItemSelected = itemSelected;
        }

        public int ListViewHeight
        {
            get
            {
                return Options.Count * Constants.UIAlertControllerPopupOptionRowHeight;
            }
        }

        public int ListViewWidth
        {
            get
            {
                var longestItem = Options.OrderByDescending(x => x.Length).FirstOrDefault();
                var lenghtOfLongestItem = longestItem.Length;
                return lenghtOfLongestItem * Constants.UIAlertControllerPopupOptionListItemViewWidth;
            }
        }

        public ObservableCollection<string> Options
        {
            get
            {
                return _options;
            }
            set
            {
                if (_options != value)
                {
                    _options = value;
                    OnPropertyChanged(nameof(Options));
                }
            }
        }

        public string SelectedOption
        {
            get
            {
                return _selectedOption;
            }
            set
            {
                if (_selectedOption != value && value != null)
                {
                    _selectedOption = value;
                    OnPropertyChanged(nameof(SelectedOption));
                    ItemSelected.Invoke(SelectedOption);
                    NavigationService.ClosePopUp();
                }
            }
        }

        public bool IsTablet
        {
            get
            {
                return DeviceInfo.Idiom.ToString() == Devices.Tablet.ToString();
            }
        }

        public bool IsCancelVisible
        {
            get
            {
                return (IsTablet) ? false : true;
            }
        }

        public LayoutOptions VerticalAlignmentOfAlert
        {
            get
            {
                return (IsTablet) ? LayoutOptions.CenterAndExpand : LayoutOptions.EndAndExpand;
            }
        }

        public LayoutOptions HorizontalAlignmentOfAlert
        {
            get
            {
                return (IsTablet) ? LayoutOptions.Center : LayoutOptions.FillAndExpand;
            }
        }

        public ScaleAnimation ScaleAnimation
        {
            get
            {
                return (IsTablet) ? new ScaleAnimation(MoveAnimationOptions.Center, MoveAnimationOptions.Center) : new ScaleAnimation(MoveAnimationOptions.Bottom, MoveAnimationOptions.Bottom);
            }
        }

        public ICommand ClosePopupCommand
        {
            get
            {
                return _closePopupCommand ?? (_closePopupCommand = new CommandExtended(ExecuteClosePopupCommandAsync));
            }
        }
        private async Task ExecuteClosePopupCommandAsync()
        {
            await NavigationService.ClosePopUp();
        }
    }
}
