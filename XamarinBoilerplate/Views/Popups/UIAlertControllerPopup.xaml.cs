using Rg.Plugins.Popup.Enums;
using System;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBoilerplate.Enums;
using XamarinBoilerplate.ViewModels.Popups;

namespace XamarinBoilerplate.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UIAlertControllerPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public UIAlertControllerPopup(ObservableCollection<string> options, Action<string> itemSelected)
        {
            InitializeComponent();
            BindingContext = new UIAlertControllerPopupViewModel(options, itemSelected);
            CreateAnimation();
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = (ListView)sender;
            listView.SelectedItem = null;
        }

        public void CreateAnimation()
        {
            var isIpad = DeviceInfo.Idiom.ToString() == Devices.Tablet.ToString();

            AnimationEffect.PositionIn = (isIpad) ? MoveAnimationOptions.Center : MoveAnimationOptions.Bottom;
            AnimationEffect.PositionOut = (isIpad) ? MoveAnimationOptions.Center : MoveAnimationOptions.Bottom;
            AnimationEffect.ScaleIn = 1.2;
            AnimationEffect.ScaleOut = 0.8;
            AnimationEffect.DurationIn = 300;
            AnimationEffect.DurationOut = 300;
            AnimationEffect.HasBackgroundAnimation = true;
            AnimationEffect.EasingIn = Easing.SinOut;
            AnimationEffect.EasingOut = Easing.SinIn;
        }
    }
}