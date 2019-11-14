using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBoilerplate.Enums;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.Views.Wizzard;

namespace XamarinBoilerplate.ViewModels.Wizzard
{
    public class StepThreeViewModel : BaseViewModel
    {
        public ICommand _backTutorialCommand;
        public ICommand _doneTutorialCommand;
        public ICommand _startTutorialCommand;

        public StackOrientation MainContainerOrientation
        {
            get
            {
                return DeviceManager.Orientation == Devices.Landscape.ToString() ? StackOrientation.Horizontal : StackOrientation.Vertical;
            }
        }

        public ICommand BackTutorialCommand
        {
            get
            {
                return _backTutorialCommand ?? (_backTutorialCommand = new CommandExtended(ExecuteBackTutorialCommandAsync));
            }
        }

        public ICommand DoneTutorialCommand
        {
            get
            {
                return _doneTutorialCommand ?? (_doneTutorialCommand = new CommandExtended(ExecuteDoneTutorialCommandAsync));
            }
        }

        public ICommand StartTutorialCommand
        {
            get
            {
                return _startTutorialCommand ?? (_startTutorialCommand = new CommandExtended(ExecuteStartTutorialCommandAsync));
            }
        }

        public async Task ExecuteBackTutorialCommandAsync()
        {
            await NavigationService.GoBackAsync();
        }

        public async Task ExecuteDoneTutorialCommandAsync()
        {
            // TODO: Implement Go to Dashboard by setting RootPage
            // await NavigationService.SetRootPage(Pagename, ViewModel);
        }

        public async Task ExecuteStartTutorialCommandAsync()
        {
            await NavigationService.NavigateAsync(nameof(StepOnePage), null, true);
        }

        public void RefreshOrientation()
        {
            OnPropertyChanged(nameof(MainContainerOrientation));
        }
    }
}
