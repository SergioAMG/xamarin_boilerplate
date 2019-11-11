using System.Threading.Tasks;
using System.Windows.Input;
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

        public bool IsScrollViewEnabled
        {
            get
            {
                return DeviceManager.Orientation == Devices.Landscape.ToString();
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

        private async Task ExecuteBackTutorialCommandAsync()
        {
            await NavigationService.GoBackAsync();
        }

        private async Task ExecuteDoneTutorialCommandAsync()
        {
            // TODO: Implement Go to Dashboard by setting RootPage
            // await NavigationService.SetRootPage(Pagename, ViewModel);
        }

        private async Task ExecuteStartTutorialCommandAsync()
        {
            await NavigationService.NavigateAsync(nameof(StepOnePage), null, true);
        }

        public void RefreshOrientation()
        {
            OnPropertyChanged(nameof(IsScrollViewEnabled));
        }
    }
}
