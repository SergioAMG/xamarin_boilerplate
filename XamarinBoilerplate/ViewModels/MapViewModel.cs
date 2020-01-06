﻿using System.Threading.Tasks;
using System.Windows.Input;
using XamarinBoilerplate.Utils;

namespace XamarinBoilerplate.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        private ICommand _openDrawerCommand;

        public ICommand OpenDrawerCommand
        {
            get
            {
                return _openDrawerCommand ?? (_openDrawerCommand = new CommandExtended(ExecuteOpenDrawerCommandAsync));
            }
        }

        private async Task ExecuteOpenDrawerCommandAsync()
        {
            await NavigationService.OpenDrawer();
        }
    }
}
