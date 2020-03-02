using DataManagers.Interfaces;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBoilerplate.Enums;
using XamarinBoilerplate.Interfaces;
using XamarinBoilerplate.Services;
using XamarinBoilerplate.Utils;

namespace XamarinBoilerplate.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private string _title;
        private string _subTitle;
        private INavigationService _navigationService;
        private ICommand _goBackCommand;
        private IDataService _dataManager;

        protected BaseViewModel()
        {
            _navigationService = App.NavigationService;
        }

        protected BaseViewModel(IDataService dataManager)
        {
            _dataManager = dataManager;
            _navigationService = App.NavigationService;
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Subtitle
        {
            get
            {
                return _subTitle;
            }
            set
            {
                _subTitle = value;
                OnPropertyChanged(Subtitle);
            }
        }

        public bool IsTitleCentered
        {
            get
            {
                if (DeviceInfo.Platform.ToString() == Devices.Android.ToString())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public bool IsAndroid
        {
            get
            {
                return DeviceManager.Platform == Devices.Android.ToString();
            }
        }

        public bool IsIOS
        {
            get
            {
                return DeviceManager.Platform == Devices.iOS.ToString();
            }
        }

        public Thickness MarginForLeftIconOfActionBar
        {
            get
            {
                return (IsAndroid) ? Constants.MarginForLeftIconOfActionBarAndroid : Constants.MarginForLeftIconOfActionBarIOS;
            }
        }

        public Thickness MarginForRightIconOfActionBar
        {
            get
            {
                return (IsAndroid) ? Constants.MarginForRightIconOfActionBarAndroid : Constants.MarginForRightIconOfActionBarIOS;
            }
        }

        public Thickness MarginForOptionsIconOfActionBar
        {
            get
            {
                return (IsAndroid) ? Constants.MarginForOptionsIconOfActionBarAndroid : Constants.MarginForOptionsIconOfActionBarIOS;
            }
        }

        public Thickness MarginForSingleIconOfActionBar
        {
            get
            {
                return (IsAndroid) ? Constants.MarginForSingleIconOfActionBarAndroid : Constants.MarginForSingleIconOfActionBarIOS;
            }
        }

        public Thickness GetMainContainerMargin
        {
            get
            {
                if (DeviceManager.IsIOSVersionGreaterOrEqualToSupportedIOSVersion())
                {
                    if (DeviceManager.IsLandscape)
                    {
                        Thickness customMargin;
                        switch (DeviceManager.GetIPhoneType())
                        {
                            case IPhoneType.iPhone4                       : customMargin = new Thickness(0); break;
                            case IPhoneType.iPhoneSE_5                    : customMargin = new Thickness(0); break; 
                            case IPhoneType.iPhone8_7_6                   : customMargin = new Thickness(0); break;
                            case IPhoneType.iPhone8Plus_7Plus_6SPlus_6Plus: customMargin = new Thickness(0, 0, 0, -35); break;
                            case IPhoneType.iPhoneX_XS_11Pro              : customMargin = new Thickness(-45, 0, -45, -35); break;
                            case IPhoneType.iPhone11_XR                   : customMargin = new Thickness(-45, 0, -45, -35); break;
                            case IPhoneType.iPhone11ProMax_XSMax          : customMargin = new Thickness(-45, 0, -45, -35); break;
                            default                                       : customMargin = new Thickness(-45, 0, -45, -35); break;
                        }
                        return customMargin;
                    }
                    else
                    {
                        return new Thickness(0, 0, 0, -35);
                    }
                }
                else
                {
                    if (DeviceManager.IsLandscape)
                    {
                        if (DeviceManager.IsIOS)
                        {
                            Thickness customMargin;
                            switch (DeviceManager.GetIPhoneType())
                            {
                                case IPhoneType.iPhone4: customMargin = new Thickness(0, -20, 0, 0); break;
                                case IPhoneType.iPhoneSE_5: customMargin = new Thickness(0, -20, 0, 0); break;
                                case IPhoneType.iPhone8_7_6: customMargin = new Thickness(0, -20, 0, 0); break;
                                default: customMargin = new Thickness(0, -20, 0, 0); break;
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
        }

        public bool IsLeftIconVisible
        {
            get
            {
                if (DeviceManager.GetIPhoneType() == IPhoneType.iPhone4 || DeviceManager.GetIPhoneType() == IPhoneType.iPhoneSE_5)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public INavigationService NavigationService
        {
            get
            {
                return _navigationService;
            }
        }

        public IDataService DataManager
        {
            get
            {
                return _dataManager ?? (_dataManager = new DataWrapperService());
            }
            set
            {
                _dataManager = value;
            }
        }

        public virtual ICommand GoBackCommand
        {
            get
            {
                return _goBackCommand ?? (_goBackCommand = new CommandExtended(async () => await ExecuteGoBackCommandAsync()));
            }
        }

        private async Task ExecuteGoBackCommandAsync()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await NavigationService.GoBackAsync();
            });
        }

        public Action<object> CallbackAction { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }

        public void RefreshMainContainerMargins()
        {
            OnPropertyChanged(nameof(GetMainContainerMargin));
        }
    }

}
