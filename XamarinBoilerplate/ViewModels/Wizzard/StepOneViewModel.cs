using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.Views.Wizzard;
using XamarinBoilerplate.Enums;

namespace XamarinBoilerplate.ViewModels.Wizzard
{
    public class StepOneViewModel : BaseViewModel
    {
        public ICommand _skipTutorialCommand;
        public ICommand _nextTutorialCommand;
        public ICommand _lastTutorialCommand;

        public StackOrientation MainContainerOrientation
        {
            get
            {
                return DeviceManager.Orientation == Devices.Landscape.ToString() ? StackOrientation.Horizontal : StackOrientation.Vertical;
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

        public async Task ExecuteSkipTutorialCommandAsync()
        {
            // TODO: Implement Go to Dashboard by setting RootPage
        }

        public async Task ExecuteNextTutorialCommandAsync()
        {
            await NavigationService.NavigateAsync(nameof(StepTwoPage), null, true);
        }

        public async Task ExecuteLastTutorialCommandAsync()
        {
            await NavigationService.NavigateAsync(nameof(StepThreePage), null, true);
        }

        public void RefreshOrientation()
        {
            OnPropertyChanged(nameof(MainContainerOrientation));
        }
    }
}
