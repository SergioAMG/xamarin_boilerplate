using DataManagers.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBoilerplate.Controls;
using XamarinBoilerplate.Interfaces;
using XamarinBoilerplate.Utils;

namespace XamarinBoilerplate.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        private ICommand _openDrawerCommand;
        private ICommand _commonToolbarItemTapCommand;
        private ICommand _viewMoreOptionsCommand;

        public MapViewModel(IDataService dataManager = null) : base(dataManager)
        {
            if (dataManager != null)
            {
                DataManager = dataManager;
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

        public ICommand ViewMoreOptionsCommand
        {
            get
            {
                return _viewMoreOptionsCommand ?? (_viewMoreOptionsCommand = new CommandExtended(ExecuteViewMoreOptionsCommandAsync));

            }
        }

        public async Task ExecuteOpenDrawerCommandAsync()
        {
            await NavigationService.OpenDrawer();
        }

        private async Task ExecuteCommonToolbarItemTapCommandAsync(object sender)
        {
            string itemTapped = (string)sender;
            DependencyService.Get<IToast>().ShowToastMessage(Localization.AppResources.CommonToolbarItemTapped + " " + itemTapped, false);
        }

        public async Task ExecuteViewMoreOptionsCommandAsync()
        {
            ObservableCollection<string> options = new ObservableCollection<string>()
            {
                Localization.AppResources.ToolbarItemShowMyLocation,
                Localization.AppResources.ToolbarItemSearchLocation,
                Localization.AppResources.ToolbarItemResetMap,
                Localization.AppResources.ToolbarItemClearPins
            };
            await NavigationService.OpenPopUp(new Views.Popups.UIAlertControllerPopup(options, HandleUserSelection));
        }

        private async void HandleUserSelection(string userSelection)
        {
            await NavigationService.ClosePopUp();
            DependencyService.Get<IToast>().ShowToastMessage(Localization.AppResources.CommonToolbarItemTapped + " " + userSelection, false);
        }
    }
}
