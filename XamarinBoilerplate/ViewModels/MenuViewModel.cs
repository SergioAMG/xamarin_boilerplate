﻿using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.Views.Wizzard;

namespace XamarinBoilerplate.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private ICommand _goToContactCommand;
        private ICommand _goToWizzardStep1Command;
        private ICommand _goToSearchCommand;
        private ICommand _goToLiveHelpCommand;
        private ICommand _closeCommand;
        public string _appVersion;

        public string AppVersion
        {
            get
            {
                return Localization.AppResources.AppVersion + " " + ( (!UnitTestingManager.IsRunningFromNUnit) ? VersionTracking.CurrentVersion : Localization.AppResources.NotAvailable);
            }
        }

        public ICommand GoToContactCommand
        {
            get
            {
                return _goToContactCommand ?? (_goToContactCommand = new CommandExtended(ExecuteGoToContactAsync));
            }
        }

        public ICommand GoToWizzardStep1Command
        {
            get
            {
                return _goToWizzardStep1Command ?? (_goToWizzardStep1Command = new CommandExtended(ExecuteGoToWizzardStep1CommandAsync));
            }
        }

        public ICommand GoToSearchCommand
        {
            get
            {
                return _goToSearchCommand ?? (_goToSearchCommand = new CommandExtended(ExecuteGoToSearchCommandAsync));
            }
        }

        public ICommand GoToLiveHelpCommand
        {
            get
            {
                return _goToLiveHelpCommand ?? (_goToLiveHelpCommand = new CommandExtended(ExecuteGoToLiveHelpCommandAsync));
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new CommandExtended(ExecuteCloseCommandAsync));
            }
        }

        public async Task ExecuteGoToContactAsync()
        {
            await NavigationService.CloseDrawer();
            // TODO: Add navigation to target page
            //await NavigationService.NavigateAsync(nameof(ContactPage));
        }

        public async Task ExecuteGoToWizzardStep1CommandAsync()
        {
            await NavigationService.CloseDrawer();
            await NavigationService.NavigateAsync(nameof(StepOnePage));
        }

        public async Task ExecuteGoToSearchCommandAsync()
        {
            await NavigationService.CloseDrawer();
            // TODO: Add navigation to target page
            //await NavigationService.NavigateAsync(nameof(SearchPage));
        }

        public async Task ExecuteGoToLiveHelpCommandAsync()
        {
            await NavigationService.CloseDrawer();
            // TODO: Add navigation to target page
            //await NavigationService.NavigateAsync(nameof(LiveChatPage));
        }

        public async Task ExecuteCloseCommandAsync()
        {
            await NavigationService.CloseDrawer();
        }
    }
}
