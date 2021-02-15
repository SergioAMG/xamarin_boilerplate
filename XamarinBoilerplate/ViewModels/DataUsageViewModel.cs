using DataManagers.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBoilerplate.Interfaces;
using XamarinBoilerplate.Utils;

namespace XamarinBoilerplate.ViewModels
{
    public class DataUsageViewModel : BaseViewModel
    {
        private ICommand _openDrawerCommand;
        private ICommand _commonToolbarItemTapCommand;
        private ICommand _viewMoreOptionsCommand;

        public DataUsageViewModel(IDataService dataManager = null) : base(dataManager)
        {
            if (dataManager != null)
            {
                DataManager = dataManager;
            }
        }

        public bool IsAndroidMenuVisible
        {
            get
            {
                return (IsAndroid);
            }
        }

        public bool IsIOSMenuVisible
        {
            get
            {
                return (IsIOS);
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

        public async Task ExecuteOpenDrawerCommandAsync()
        {
            await NavigationService.OpenDrawer();
        }

        public async Task ExecuteViewMoreOptionsCommandAsync()
        {
            ObservableCollection<string> options = new ObservableCollection<string>()
            {
                Localization.AppResources.ToolbarItemViewByDay,
                Localization.AppResources.ToolbarItemViewByWeek,
                Localization.AppResources.ToolbarItemViewByMonth,
                Localization.AppResources.ToolbarItemViewByYear
            };
            await NavigationService.OpenPopUp(new Views.Popups.UIAlertControllerPopup(options, HandleUserSelection));
        }

        private async Task ExecuteCommonToolbarItemTapCommandAsync(object sender)
        {
            string itemTapped = (string)sender;
            DependencyService.Get<IToast>().ShowToastMessage(Localization.AppResources.CommonToolbarItemTapped + " " + itemTapped, false);
        }

        private async void HandleUserSelection(string userSelection)
        {
            await NavigationService.ClosePopUp();
            DependencyService.Get<IToast>().ShowToastMessage(Localization.AppResources.CommonToolbarItemTapped + " " + userSelection, false);
        }
    }
}
