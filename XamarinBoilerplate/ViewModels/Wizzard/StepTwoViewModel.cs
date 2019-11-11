using System.Threading.Tasks;
using System.Windows.Input;
using XamarinBoilerplate.Enums;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.Views.Wizzard;

namespace XamarinBoilerplate.ViewModels.Wizzard
{
    public class StepTwoViewModel : BaseViewModel
    {
        public ICommand _backTutorialCommand;
        public ICommand _nextTutorialCommand;

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

        public ICommand NextTutorialCommand
        {
            get
            {
                return _nextTutorialCommand ?? (_nextTutorialCommand = new CommandExtended(ExecuteNextTutorialCommandAsync));
            }
        }

        private async Task ExecuteBackTutorialCommandAsync()
        {
            await NavigationService.GoBackAsync();
        }

        private async Task ExecuteNextTutorialCommandAsync()
        {
            await NavigationService.NavigateAsync(nameof(StepThreePage), null, true);
        }

        public void RefreshOrientation()
        {
            OnPropertyChanged(nameof(IsScrollViewEnabled));
        }
    }
}
