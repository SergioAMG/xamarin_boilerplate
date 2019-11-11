using System.Threading.Tasks;
using System.Windows.Input;
using XamarinBoilerplate.Enums;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.Views.Wizzard;

namespace XamarinBoilerplate.ViewModels.Wizzard
{
    public class StepOneViewModel : BaseViewModel
    {
        public ICommand _skipTutorialCommand;
        public ICommand _nextTutorialCommand;
        public ICommand _lastTutorialCommand;

        public bool IsScrollViewEnabled
        {
            get
            {
                return DeviceManager.Orientation == Devices.Landscape.ToString();
            }
        }

        public ICommand SkipTutorialCommand
        {
            get
            {
                return _skipTutorialCommand ?? (_skipTutorialCommand = new CommandExtended(ExecuteSkipTutorialCommandAsync));
            }
        }

        public ICommand NextTutorialCommand
        {
            get
            {
                return _nextTutorialCommand ?? (_nextTutorialCommand = new CommandExtended(ExecuteNextTutorialCommandAsync));
            }
        }

        public ICommand LastTutorialCommand
        {
            get
            {
                return _lastTutorialCommand ?? (_lastTutorialCommand = new CommandExtended(ExecuteLastTutorialCommandAsync));
            }
        }

        private async Task ExecuteSkipTutorialCommandAsync()
        {
            // TODO: Implement Go to Dashboard by setting RootPage
        }

        private async Task ExecuteNextTutorialCommandAsync()
        {
            await NavigationService.NavigateAsync(nameof(StepTwoPage), null, true);
        }

        private async Task ExecuteLastTutorialCommandAsync()
        {
            await NavigationService.NavigateAsync(nameof(StepThreePage), null, true);
        }

        public void RefreshOrientation()
        {
            OnPropertyChanged(nameof(IsScrollViewEnabled));
        }

    }
}
