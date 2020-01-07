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
    }

}
