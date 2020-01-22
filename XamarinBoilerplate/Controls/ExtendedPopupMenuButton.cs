using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace XamarinBoilerplate.Controls
{
    public class ExtendedPopupMenuButton : ImageButton
    {
        public static readonly BindableProperty OptionsProperty = BindableProperty.Create(
            nameof(Options), typeof(ObservableCollection<ExtendedLabel>), typeof(ExtendedPopupMenuButton), defaultValue: new ObservableCollection<ExtendedLabel>(), defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty XamlPopupProperty = BindableProperty.Create(nameof(IsXamlPopup), typeof(bool), typeof(bool), false, BindingMode.TwoWay);
        public Action CallBackOpenMenu { get; set; }

        public ObservableCollection<ExtendedLabel> Options
        {
            get
            {
                return (ObservableCollection<ExtendedLabel>)GetValue(OptionsProperty);
            }
            set
            {
                SetValue(OptionsProperty, value);
            }
        }

        public bool IsXamlPopup
        {
            get
            {
                return (Boolean)GetValue(XamlPopupProperty);
            }
            set
            {
                SetValue(XamlPopupProperty, value);
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            foreach (var item in Options)
            {
                item.BindingContext = this.BindingContext;
            }
        }

        public void OpenMenu()
        {
            CallBackOpenMenu?.Invoke();
        }
    }
}
