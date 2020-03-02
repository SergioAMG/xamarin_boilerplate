using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBoilerplate.Enums;
using XamarinBoilerplate.Interfaces;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.Views;
using XamarinBoilerplate.Views.Samples;
using XamarinBoilerplate.Views.Wizzard;

namespace XamarinBoilerplate.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private ICommand _goToContactCommand;
        private ICommand _goToWizzardStep1Command;
        private ICommand _goToSamplesCommand;
        private ICommand _goToLiveHelpCommand;
        private ICommand _closeCommand;

        public string AppVersion
        {
            get
            {
                return Localization.AppResources.AppVersion + " " + ((!UnitTestingManager.IsRunningFromNUnit) ? VersionTracking.CurrentVersion : Localization.AppResources.NotAvailable);
            }
        }

        public Thickness CloseButtonMargin
        {
            get
            {
                if (DeviceManager.IsAndroid)
                {
                    return new Thickness(0, 0, 15, 0);
                }
                else
                {
                    Thickness customMargin;
                    switch (DeviceManager.GetIPhoneType())
                    {
                        case IPhoneType.iPhone4                       : customMargin = new Thickness(0); break;
                        case IPhoneType.iPhoneSE_5                    : customMargin = new Thickness(0); break;
                        case IPhoneType.iPhone8_7_6                   : customMargin = new Thickness(0, 1, 15, 0); break;
                        case IPhoneType.iPhone8Plus_7Plus_6SPlus_6Plus: customMargin = new Thickness(0, 1, 15, 0); break;
                        case IPhoneType.iPhoneX_XS_11Pro              : customMargin = new Thickness(0, 1, 15, 0); break;
                        case IPhoneType.iPhone11_XR                   : customMargin = new Thickness(0, 1, 15, 0); break;
                        case IPhoneType.iPhone11ProMax_XSMax          : customMargin = new Thickness(0, 1, 15, 0); break;
                        default                                       : customMargin = new Thickness(0, 1, 15, 0); break;
                    }
                    return customMargin;
                }
            }
        }

        public Thickness FooterMargin
        {
            get
            {
                if (DeviceManager.IsIOSVersionGreaterOrEqualToSupportedIOSVersion())
                {
                    Thickness customMargin;
                    switch (DeviceManager.GetIPhoneType())
                    {
                        case Enums.IPhoneType.iPhone4                       : customMargin = new Thickness(0); break;
                        case Enums.IPhoneType.iPhoneSE_5                    : customMargin = new Thickness(0); break;
                        case Enums.IPhoneType.iPhone8_7_6                   : customMargin = new Thickness(0); break;
                        case Enums.IPhoneType.iPhone8Plus_7Plus_6SPlus_6Plus: customMargin = new Thickness(0); break;
                        case Enums.IPhoneType.iPhoneX_XS_11Pro              : customMargin = new Thickness(0, 0, 0, -35); break;
                        case Enums.IPhoneType.iPhone11_XR                   : customMargin = new Thickness(0, 0, 0, -35); break;
                        case Enums.IPhoneType.iPhone11ProMax_XSMax          : customMargin = new Thickness(0, 0, 0, -35); break;
                        default                                             : customMargin = new Thickness(0, 0, 0, -35); break;
                    }
                    return customMargin;
                }
                else
                {
                    return new Thickness(0);
                }
            }
        }

        public Thickness LeftMainMargin
        {
            get
            {
                if (DeviceManager.IsLandscape)
                {
                    if (DeviceManager.IsIOSVersionGreaterOrEqualToSupportedIOSVersion())
                    {
                        Thickness customMargin;
                        switch (DeviceManager.GetIPhoneType())
                        {
                            case Enums.IPhoneType.iPhone4                       : customMargin = new Thickness(0); break;
                            case Enums.IPhoneType.iPhoneSE_5                    : customMargin = new Thickness(0); break;
                            case Enums.IPhoneType.iPhone8_7_6                   : customMargin = new Thickness(0); break;
                            case Enums.IPhoneType.iPhone8Plus_7Plus_6SPlus_6Plus: customMargin = new Thickness(0); break;
                            case Enums.IPhoneType.iPhoneX_XS_11Pro              : customMargin = new Thickness(-45, 0, 0, 0); break;
                            case Enums.IPhoneType.iPhone11_XR                   : customMargin = new Thickness(-45, 0, 0, 0); break;
                            case Enums.IPhoneType.iPhone11ProMax_XSMax          : customMargin = new Thickness(-45, 0, 0, 0); break;
                            default                                             : customMargin = new Thickness(-45, 0, 0, 0); break;
                        }
                        return customMargin;
                    }
                    else
                    {
                        return new Thickness(0);
                    }
                }
                else
                {
                    return new Thickness(0);
                }
            }
        }

        public Thickness BottomPadding
        {
            get
            {
                if (DeviceManager.IsTablet)
                {
                    return new Thickness(20, 7, 20, 7);
                }
                else
                {
                    return new Thickness(20);
                }
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

        public ICommand GoToSamplesCommand
        {
            get
            {
                return _goToSamplesCommand ?? (_goToSamplesCommand = new CommandExtended(ExecuteGoToSamplesCommandAsync));
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
            var selectedTabIndex = NavigationService.GetCurrentSelectedTabIndexOverMasterDetailPageWithTabbedPage();
            NavigationService.NavigateDetails(nameof(ContactPage), selectedTabIndex);
            await NavigationService.CloseDrawer();
        }

        public async Task ExecuteGoToWizzardStep1CommandAsync()
        {
            await NavigationService.CloseDrawer();
            await NavigationService.NavigateAsync(nameof(StepOnePage));
        }

        public async Task ExecuteGoToSamplesCommandAsync()
        {
            var selectedTabIndex = NavigationService.GetCurrentSelectedTabIndexOverMasterDetailPageWithTabbedPage();
            NavigationService.NavigateDetails(nameof(SamplesMenuPage), selectedTabIndex);
            await NavigationService.CloseDrawer();
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

        public void RefreshMainMargins()
        {
            OnPropertyChanged(nameof(LeftMainMargin));
            OnPropertyChanged(nameof(FooterMargin));
        }
    }
}
