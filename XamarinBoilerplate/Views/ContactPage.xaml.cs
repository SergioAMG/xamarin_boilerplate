﻿using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels;

namespace XamarinBoilerplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPage : BaseContentPage
    {
        public ContactPage()
        {
            InitializeComponent();
        }

        public ContactPage(int selectedTabIndex)
        {
            InitializeComponent();
            (BindingContext as ContactViewModel).Init(selectedTabIndex);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            DeviceManager.Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
            (BindingContext as ContactViewModel).SetOrientationValues();
            (BindingContext as ContactViewModel).RefreshMainContainerMargins();
        }

        public void EditorFocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            ContactViewModel viewModel = (BindingContext as ContactViewModel);
            if (DeviceManager.IsLandscape && viewModel.IsIOS)
            {
                bool isNavBarVisible = false;
                viewModel.SetNavBarVisibility(isNavBarVisible);
            }
        }

        public void EditorUnfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            ContactViewModel viewModel = (BindingContext as ContactViewModel);
            if (DeviceManager.IsLandscape && viewModel.IsIOS)
            {
                bool isNavBarVisible = true;
                viewModel.SetNavBarVisibility(isNavBarVisible);
            }
        }
    }
}