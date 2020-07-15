﻿using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels.Samples;

namespace XamarinBoilerplate.Views.Samples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SamplesMenuPage : BaseContentPage
    {
        public SamplesMenuPage(int selectedTabIndex)
        {
            InitializeComponent();
            (BindingContext as SamplesMenuViewModel).Init(selectedTabIndex);
        }

        private void ListViewItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;
            listView.SelectedItem = null;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            DeviceManager.Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
            (BindingContext as SamplesMenuViewModel).SetOrientationValues();
            (BindingContext as SamplesMenuViewModel).RefreshMainContainerMargins();
        }
    }
}