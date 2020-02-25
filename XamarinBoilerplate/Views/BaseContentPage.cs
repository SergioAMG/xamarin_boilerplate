using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using XamarinBoilerplate.Services;
using XamarinBoilerplate.ViewModels;

namespace XamarinBoilerplate.Views
{
    public class BaseContentPage : ContentPage
    {
        public static readonly BindableProperty EnableHardwareBackButtonOverrideProperty = BindableProperty.Create(nameof(EnableHardwareBackButtonOverride), typeof(bool), typeof(BaseContentPage), false);
        public static readonly BindableProperty EnableLogicalBackButtonOverrideProperty = BindableProperty.Create(nameof(EnableLogicalBackButtonOverride), typeof(bool), typeof(BaseContentPage), false);
        public static readonly BindableProperty HardwareBackButtonCommandProperty = BindableProperty.Create(nameof(HardwareBackButtonCommand), typeof(ICommand), typeof(BaseContentPage));
        public static readonly BindableProperty LogicalBackButtonCommandProperty = BindableProperty.Create(nameof(LogicalBackButtonCommand), typeof(ICommand), typeof(BaseContentPage));

        public BaseContentPage()
        {
            var pageType = App.NavigationService.ViewModelByKey(GetType().Name);

            if (pageType != null)
            {
                var viewModel = ViewModelContainer.Current.Resolve(pageType);

                if (viewModel != null)
                {
                    BindingContext = viewModel;
                }
            }

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        public bool EnableHardwareBackButtonOverride
        {
            get
            {
                return (bool)GetValue(EnableHardwareBackButtonOverrideProperty);
            }
            set
            {
                SetValue(EnableHardwareBackButtonOverrideProperty, value);
            }
        }

        public bool EnableLogicalBackButtonOverride
        {
            get
            {
                return (bool)GetValue(EnableLogicalBackButtonOverrideProperty);
            }
            set
            {
                SetValue(EnableLogicalBackButtonOverrideProperty, value);
            }
        }

        public ICommand HardwareBackButtonCommand
        {
            get
            {
                return (ICommand)GetValue(HardwareBackButtonCommandProperty);
            }
            set
            {
                SetValue(HardwareBackButtonCommandProperty, value);
            }
        }

        public ICommand LogicalBackButtonCommand
        {
            get
            {
                return (ICommand)GetValue(LogicalBackButtonCommandProperty);
            }
            set
            {
                SetValue(LogicalBackButtonCommandProperty, value);
            }
        }

        public Action<object> CallbackAction
        {
            set
            {
                (this.BindingContext as BaseViewModel).CallbackAction = value;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (EnableHardwareBackButtonOverride)
            {
                HardwareBackButtonCommand.Execute(null);
                return true;
            }

            var viewModel = BindingContext as BaseViewModel;

            viewModel.NavigationService.GoBackAsync();

            return true;
        }

        public bool HasNavigationBar
        {
            set
            {
                Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, value);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var pageType = App.NavigationService.ViewModelByKey(GetType().Name);

            if (pageType != null)
            {
                var viewModel = BindingContext as BaseViewModel;

                viewModel.OnAppearing();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var pageType = App.NavigationService.ViewModelByKey(GetType().Name);

            if (pageType != null)
            {
                var viewModel = BindingContext as BaseViewModel;

                viewModel.OnDisappearing();
            }
        }
    }
}
