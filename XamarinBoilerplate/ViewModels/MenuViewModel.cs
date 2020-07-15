using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBoilerplate.Enums;
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
                return Localization.AppResources.AppVersion + " " + 
                       ((!UnitTestingManager.IsRunningFromNUnit) ? VersionTracking.CurrentVersion : Localization.AppResources.NotAvailable);
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
                    if (DeviceManager.IsIOSVersionGreaterOrEqualToSupportedIOSVersion())
                    {
                        return new Thickness(0, 1, 15, 0);
                    }
                    else
                    {
                        return (DeviceManager.IsLandscape) ? new Thickness(0, 0, 15, 0) : new Thickness(0, 20, 15, 0);
                    }
                }
            }
        }

        public bool IsCloseButtonVisible
        {
            get
            {
                return (DeviceManager.IsTablet && DeviceManager.IsLandscape) ? false : true;
            }
        }

        public Thickness FooterMargin
        {
            get
            {
                if (DeviceManager.IsIOSVersionGreaterOrEqualToSupportedIOSVersion())
                {
                    Thickness customMargin;
                    switch (DeviceManager.GetAppleDeviceType())
                    {
                        case AppleDeviceType.iPhone4                       : 
                        case AppleDeviceType.iPhoneSE_5                    : 
                        case AppleDeviceType.iPhone8_7_6                   : 
                        case AppleDeviceType.iPhone8Plus_7Plus_6SPlus_6Plus: customMargin = new Thickness(0); break;
                        case AppleDeviceType.iPhoneX_XS_11Pro              : 
                        case AppleDeviceType.iPhone11_XR                   : 
                        case AppleDeviceType.iPhone11ProMax_XSMax          : customMargin = new Thickness(0, 0, 0, -35); break;
                        default                                            : customMargin = new Thickness(0, 0, 0, -35); break;
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
                        switch (DeviceManager.GetAppleDeviceType())
                        {
                            case AppleDeviceType.iPhone4                                         : 
                            case AppleDeviceType.iPhoneSE_5                                      : 
                            case AppleDeviceType.iPhone8_7_6                                     : 
                            case AppleDeviceType.iPhone8Plus_7Plus_6SPlus_6Plus                  : customMargin = new Thickness(0); break;
                            case AppleDeviceType.iPhoneX_XS_11Pro                                : 
                            case AppleDeviceType.iPhone11_XR                                     : 
                            case AppleDeviceType.iPhone11ProMax_XSMax                            : customMargin = new Thickness(-45, 0, 0, 0); break;
                            case AppleDeviceType.iPad_2_Mini                                     :
                            case AppleDeviceType.iPad3_4_Air_Mini2_Mini3_Air2_Mini4_Pro_2017_2018:
                            case AppleDeviceType.iPadProSec10                                    :
                            case AppleDeviceType.iPadProSec12                                    : customMargin = new Thickness(0); break;
                            default                                                              : customMargin = new Thickness(-45, 0, 0, 0); break;
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

        public int BottomPanelHeight
        {
            get
            {
                if (DeviceManager.IsAndroid)
                {
                    if (DeviceManager.IsLandscape)
                    {
                        return (DeviceManager.IsTablet) ? 56 : 60;
                    }
                    else
                    {
                        return 57;
                    }
                }
                else
                {
                    if (DeviceManager.IsIOSVersionGreaterOrEqualToSupportedIOSVersion())
                    {
                        int customHeight;
                        switch (DeviceManager.GetAppleDeviceType())
                        {
                            case AppleDeviceType.iPhone4                                         : customHeight = (DeviceManager.IsLandscape) ? 33 : 51; break;
                            case AppleDeviceType.iPhoneSE_5                                      : customHeight = (DeviceManager.IsLandscape) ? 33 : 51; break;
                            case AppleDeviceType.iPhone8_7_6                                     : customHeight = (DeviceManager.IsLandscape) ? 33 : 50; break;
                            case AppleDeviceType.iPhone8Plus_7Plus_6SPlus_6Plus                  : customHeight = 50; break;
                            case AppleDeviceType.iPhoneX_XS_11Pro                                : customHeight = (DeviceManager.IsLandscape) ? 75 : 85; break;
                            case AppleDeviceType.iPhone11_XR                                     : customHeight = (DeviceManager.IsLandscape) ? 91 : 85; break;
                            case AppleDeviceType.iPhone11ProMax_XSMax                            : customHeight = (DeviceManager.IsLandscape) ? 91 : 85; break;
                            case AppleDeviceType.iPad_2_Mini                                     :
                            case AppleDeviceType.iPad3_4_Air_Mini2_Mini3_Air2_Mini4_Pro_2017_2018:
                            case AppleDeviceType.iPadProSec10                                    :
                            case AppleDeviceType.iPadProSec12                                    : customHeight = (DeviceManager.IsLandscape) ? 86 : 84; break;
                            default                                                              : customHeight = (DeviceManager.IsLandscape) ? 91 : 85; break;
                        }
                        return customHeight;
                    }
                    else
                    {
                        int customHeight;
                        switch (DeviceManager.GetAppleDeviceType())
                        {
                            case AppleDeviceType.iPhone4                                         : customHeight = (DeviceManager.IsLandscape) ? 75 : 51; break;
                            case AppleDeviceType.iPhoneSE_5                                      : customHeight = (DeviceManager.IsLandscape) ? 75 : 51; break;
                            case AppleDeviceType.iPhone8_7_6                                     : customHeight = (DeviceManager.IsLandscape) ? 53 : 50; break;
                            case AppleDeviceType.iPhone8Plus_7Plus_6SPlus_6Plus                  : customHeight = (DeviceManager.IsLandscape) ? 51 : 50; break;
                            case AppleDeviceType.iPad_2_Mini                                     : 
                            case AppleDeviceType.iPad3_4_Air_Mini2_Mini3_Air2_Mini4_Pro_2017_2018: customHeight = (DeviceManager.IsLandscape) ? 50 : 49; break;
                            case AppleDeviceType.iPadProSec10                                    : 
                            case AppleDeviceType.iPadProSec12                                    : customHeight = (DeviceManager.IsLandscape) ? 51 : 49; break;
                            default                                                              : customHeight = (DeviceManager.IsLandscape) ? 51 : 50; break;
                        }
                        return customHeight;
                    }
                }
            }
        }

        public int BottomTextSpacing
        {
            get
            {
                if (DeviceManager.IsAndroid)
                {
                    return 0;
                }
                else
                {
                    if (DeviceManager.IsIOSVersionGreaterOrEqualToSupportedIOSVersion())
                    {
                        int customSpacing;
                        switch (DeviceManager.GetAppleDeviceType())
                        {
                            case AppleDeviceType.iPhone4                                         : 
                            case AppleDeviceType.iPhoneSE_5                                      : 
                            case AppleDeviceType.iPhone8_7_6                                     : customSpacing = (DeviceManager.IsLandscape) ? -5 : 0; break;
                            default                                                              : customSpacing = 0; break;
                        }
                        return customSpacing;
                    }
                    else
                    {
                        return (DeviceManager.IsLandscape) ? -5 : 0;
                    }
                }
            }
        }
        
        public Thickness BottomPadding
        {
            get
            {
                if (DeviceManager.IsIOSVersionGreaterOrEqualToSupportedIOSVersion())
                {
                    Thickness customMargin;
                    switch (DeviceManager.GetAppleDeviceType())
                    {
                        case AppleDeviceType.iPad_2_Mini                                     :
                        case AppleDeviceType.iPad3_4_Air_Mini2_Mini3_Air2_Mini4_Pro_2017_2018: 
                        case AppleDeviceType.iPadProSec10                                    : 
                        case AppleDeviceType.iPadProSec12                                    : customMargin = new Thickness(20, -25, 0, 0); break;
                        default                                                              : customMargin = new Thickness(20, 0, 0, 0); break;
                    }
                    return customMargin;
                }
                else
                {
                    return new Thickness(20, 0, 0, 0);
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
            OnPropertyChanged(nameof(BottomPanelHeight));
            OnPropertyChanged(nameof(CloseButtonMargin));
            OnPropertyChanged(nameof(BottomTextSpacing));
            OnPropertyChanged(nameof(BottomPadding));
            OnPropertyChanged(nameof(IsCloseButtonVisible));
        }
    }
}
