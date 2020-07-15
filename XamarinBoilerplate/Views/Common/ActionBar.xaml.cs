using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBoilerplate.Effects;
using XamarinBoilerplate.Utils;

namespace XamarinBoilerplate.Views.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActionBar : ContentView
    {
        public static readonly BindableProperty BackButtonCommandProperty = BindableProperty.Create(nameof(BackButtonCommand), typeof(ICommand), typeof(BaseContentPage), propertyChanged: CommandBackHandler);
        public static readonly BindableProperty TopTitleProperty = BindableProperty.Create(nameof(TopTitle), typeof(string), typeof(ActionBar), string.Empty, propertyChanged: HandleBindingPropertyChangedDelegate);
        public static readonly BindableProperty TitleLabelStyleProperty = BindableProperty.Create(propertyName: nameof(TitleLabelStyle), returnType: typeof(Style), declaringType: typeof(ActionBar), defaultValue: null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: TitleLabelStylePropertyChanged);
        public static readonly BindableProperty IsMasterProperty = BindableProperty.Create(propertyName: nameof(IsMaster), returnType: typeof(bool), declaringType: typeof(ActionBar), defaultValue: false, defaultBindingMode: BindingMode.TwoWay, propertyChanged: IsMasterPropertyChanged);
        public static readonly BindableProperty ActionBarMenuProperty = BindableProperty.Create(nameof(Menu), typeof(ObservableCollection<ImageButton>), typeof(ActionBar), defaultValue: new ObservableCollection<ImageButton>(), defaultBindingMode: BindingMode.TwoWay, propertyChanged: ButtonsChanged);
        public static readonly BindableProperty CenterBartitleProperty = BindableProperty.Create(nameof(CenterBartitle), typeof(bool), typeof(ActionBar), defaultBindingMode: BindingMode.TwoWay, propertyChanged: TextAlignmentChanged);
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(ActionBar), string.Empty, propertyChanged: HandleBindingTitlePropertyChangedDelegate);
        public static readonly BindableProperty TopTitleLabelStyleProperty = BindableProperty.Create(propertyName: nameof(TopTitleLabelStyle), returnType: typeof(Style), declaringType: typeof(ActionBar), defaultValue: null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: TopTitleLabelStylePropertyChanged);
        public static readonly BindableProperty IsModalProperty = BindableProperty.Create(propertyName: nameof(IsModal), returnType: typeof(bool), declaringType: typeof(ActionBar), defaultValue: false, defaultBindingMode: BindingMode.TwoWay, propertyChanged: IsModalPropertyChanged);
        public static readonly BindableProperty HideBackButtonProperty = BindableProperty.Create(nameof(HideBackButton), typeof(bool), typeof(ActionBar), defaultBindingMode: BindingMode.TwoWay, propertyChanged: HideBackButtonChanged);
        public static readonly BindableProperty ShowIconProperty = BindableProperty.Create(nameof(ShowIcon), typeof(bool), typeof(ActionBar), defaultBindingMode: BindingMode.TwoWay, defaultValue: true, propertyChanged: IsShowIconPropertyChanged);

        public ActionBar()
        {
            InitializeComponent();
            baseContentArea.Padding = ActionBarPadding();
        }

        public bool CenterBartitle
        {
            get
            {
                return (bool)GetValue(CenterBartitleProperty);
            }
            set
            {
                SetValue(CenterBartitleProperty, value);
            }
        }

        public ObservableCollection<ImageButton> Menu
        {
            get
            {
                return (ObservableCollection<ImageButton>)GetValue(ActionBarMenuProperty);
            }
            set
            {
                SetValue(ActionBarMenuProperty, value);
            }
        }

        public Style TitleLabelStyle
        {
            get
            {
                return (Style)GetValue(TitleLabelStyleProperty);
            }
            set
            {
                SetValue(TitleLabelStyleProperty, value);
            }
        }

        public Style TopTitleLabelStyle
        {
            get
            {
                return (Style)GetValue(TopTitleLabelStyleProperty);
            }
            set
            {
                SetValue(TopTitleLabelStyleProperty, value);
            }
        }

        public string TopTitle
        {
            get
            {
                return (string)GetValue(TopTitleProperty);
            }
            set
            {
                SetValue(TopTitleProperty, value);
            }
        }

        private static void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
            ActionBar targetView;

            targetView = (ActionBar)bindable;

            targetView.TopTitleLabel.IsVisible = true;
            targetView.TopTitleLabel.Text = newValue.ToString();

            if (!targetView.CenterBartitle)
            {
                targetView.TitleLabel.Margin = new Thickness(72, 26, 0, 0);
                targetView.TitleLabel.VerticalOptions = LayoutOptions.Start;
                targetView.TitleLabel.VerticalTextAlignment = TextAlignment.Start;
                targetView.TitleLabel.HorizontalOptions = LayoutOptions.Start;
                targetView.TitleLabel.HorizontalTextAlignment = TextAlignment.Start;


                targetView.TopTitleLabel.Margin = new Thickness(72, 7, 0, 0);
                targetView.TopTitleLabel.HorizontalOptions = LayoutOptions.Start;
                targetView.TopTitleLabel.HorizontalTextAlignment = TextAlignment.Start;
            }
            else
            {
                targetView.TitleLabel.Margin = new Thickness(0, 26, 0, 0);

                targetView.TitleLabel.HorizontalOptions = LayoutOptions.Center;
                targetView.TitleLabel.HorizontalTextAlignment = TextAlignment.Center;


                targetView.TopTitleLabel.Margin = new Thickness(0, 7, 0, 0);
                targetView.TopTitleLabel.HorizontalOptions = LayoutOptions.Center;
                targetView.TopTitleLabel.HorizontalTextAlignment = TextAlignment.Center;
            }
        }

        private static void IsMasterPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ActionBar targetView;

            targetView = (ActionBar)bindable;

            var isMaster = (bool)newValue;

            targetView.IsMaster = isMaster;

            if (isMaster)
            {
                targetView.BackButton.Source = "baseline_menu_black_24";
                targetView.TitleLabel.HorizontalOptions = LayoutOptions.Start;
                targetView.TitleLabel.HorizontalTextAlignment = TextAlignment.Start;
            }
        }

        private static void TextAlignmentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ActionBar = (ActionBar)bindable;

            var margin = new Thickness(0, 0, 0, 0);
            var marginsubTitle = new Thickness(0, 0, 0, 0);

            if (!ActionBar.CenterBartitle)
            {
                margin = new Thickness(72, 0, 0, 0);

                if (!string.IsNullOrEmpty(ActionBar.TopTitleLabel.Text))
                {
                    marginsubTitle = new Thickness(72, 7, 0, 0);

                    margin = new Thickness(72, 26, 0, 0);

                    ActionBar.TitleLabel.VerticalOptions = LayoutOptions.Start;
                    ActionBar.TitleLabel.VerticalTextAlignment = TextAlignment.Start;
                }

                ActionBar.TitleLabel.HorizontalOptions = LayoutOptions.Start;
                ActionBar.TitleLabel.HorizontalTextAlignment = TextAlignment.Start;

                ActionBar.TopTitleLabel.HorizontalOptions = LayoutOptions.Start;
                ActionBar.TopTitleLabel.HorizontalTextAlignment = TextAlignment.Start;
            }
            else
            {
                if (!string.IsNullOrEmpty(ActionBar.TopTitleLabel.Text))
                {
                    marginsubTitle = new Thickness(0, 7, 0, 0);

                    margin = new Thickness(0, 26, 0, 0);

                    ActionBar.TitleLabel.VerticalOptions = LayoutOptions.Start;
                    ActionBar.TitleLabel.VerticalTextAlignment = TextAlignment.Start;
                }

                ActionBar.TopTitleLabel.HorizontalOptions = LayoutOptions.Center;
                ActionBar.TopTitleLabel.HorizontalTextAlignment = TextAlignment.Center;

                Device.BeginInvokeOnMainThread(() =>
                {
                    ActionBar.TitleLabel.HorizontalOptions = LayoutOptions.Center;
                    ActionBar.TitleLabel.HorizontalTextAlignment = TextAlignment.Center;
                });

            }

            ActionBar.TopTitleLabel.Margin = marginsubTitle;
            ActionBar.TitleLabel.Margin = margin;
        }

        private static void IsModalPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var form = (ActionBar)bindable;

            var isModal = (bool)newValue;

            form.IsModal = isModal;

            if (form.IsModal)
            {
                form.BackButton.Source = "baseline_close_black_24";
                if (form.CenterBartitle)
                {
                    var margin = new Thickness(0, 0, 0, 0);

                    form.TitleLabel.Margin = margin;
                    form.TitleLabel.HorizontalOptions = LayoutOptions.Center;
                    form.TitleLabel.HorizontalTextAlignment = TextAlignment.Center;

                    if (!string.IsNullOrEmpty(form.TopTitleLabel.Text))
                    {
                        var marginsubTitle = new Thickness(0, 7, 0, 0);

                        margin = new Thickness(0, 26, 0, 0);

                        form.TitleLabel.VerticalOptions = LayoutOptions.Start;
                        form.TitleLabel.VerticalTextAlignment = TextAlignment.Start;
                        form.TopTitleLabel.Margin = marginsubTitle;
                    }
                }
            }
            else
            {
                form.BackButton.Source = "baseline_arrow_back_black_24";
            }
        }

        private static void HideBackButtonChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var form = (ActionBar)bindable;

            var hideBackButton = (bool)newValue;

            form.HideBackButton = hideBackButton;

            form.BackButton.IsVisible = !hideBackButton;
        }

        public bool HasButtons
        {
            get
            {
                return Menu?.Count > 0 ? true : false;
            }
        }

        private static void ButtonsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newButtons = newValue as ObservableCollection<ImageButton>;
            if (newButtons == null) return;

            var form = (ActionBar)bindable;
            form.options.Children.Clear();
            form.options.IsVisible = true;
            form.TitleLabel.HorizontalOptions = LayoutOptions.Center;
            form.TitleLabel.HorizontalTextAlignment = TextAlignment.Center;

            foreach (var button in newButtons)
            {
                button.WidthRequest = 24;
                button.HeightRequest = 24;
                form.options.Children.Add(button);
            }

            if (form.IsModal)
            {
                if (!form.CenterBartitle)
                {
                    var margin = new Thickness(30, 0, 0, 0);

                    form.TitleLabel.HorizontalOptions = LayoutOptions.Start;
                    form.TitleLabel.HorizontalTextAlignment = TextAlignment.Start;
                }
                else
                {
                    var margin = new Thickness(10, 0, 0, 0);

                    form.TitleLabel.HorizontalOptions = LayoutOptions.Center;
                    form.TitleLabel.HorizontalTextAlignment = TextAlignment.Center;
                }
            }
            else
            {
                if (form.IsMaster)
                {
                    var margin = new Thickness(30, 0, 0, 0);

                    form.TitleLabel.HorizontalOptions = LayoutOptions.Start;
                    form.TitleLabel.HorizontalTextAlignment = TextAlignment.Start;
                }
                else if (!form.CenterBartitle)
                {
                    form.TitleLabel.HorizontalOptions = LayoutOptions.Start;
                    form.TitleLabel.HorizontalTextAlignment = TextAlignment.Start;
                }
            }
        }

        private static void TitleLabelStylePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ActionBar)bindable;
            control.TitleLabel.Style = (Style)newValue;
        }

        private static void TopTitleLabelStylePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ActionBar)bindable;
            control.TopTitleLabel.Style = (Style)newValue;
        }

        private static void HandleBindingTitlePropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ActionBar)bindable;

            control.TitleLabel.HorizontalOptions = LayoutOptions.Start;
            control.TitleLabel.HorizontalTextAlignment = TextAlignment.Start;
            control.TitleLabel.Text = (string)newValue;
        }

        private static void IsShowIconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ActionBar)bindable;
            control.Icon.IsVisible = (bool)newValue;
        }


        public ICommand BackButtonCommand
        {
            get
            {
                return (ICommand)GetValue(BackButtonCommandProperty);
            }
            set
            {
                SetValue(BackButtonCommandProperty, value);
            }
        }

        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public Object BackButtonTintColor
        {
            get
            {
                return null;
            }
            set
            {
                var dinamicResourceKey = (value as Xamarin.Forms.Internals.DynamicResource).Key;
                TintEffect.SetTintColor(BackButton, (Color)App.Current.Resources[dinamicResourceKey]);
            }
        }

        public Style TitleStyle
        {
            get
            {
                return null;
            }
            set
            {
                TitleLabel.Style = value;
            }
        }

        public bool IsModal
        {
            get
            {
                return (bool)GetValue(IsModalProperty);
            }
            set
            {
                SetValue(IsModalProperty, value);
            }
        }

        public bool IsMaster
        {
            get
            {
                return (bool)GetValue(IsMasterProperty);
            }
            set
            {
                SetValue(IsMasterProperty, value);
            }
        }

        public bool HideBackButton
        {
            get
            {
                return (bool)GetValue(HideBackButtonProperty);
            }
            set
            {
                SetValue(HideBackButtonProperty, value);
            }
        }

        public bool ShowIcon
        {
            get
            {
                return (bool)GetValue(ShowIconProperty);
            }
            set
            {
                SetValue(ShowIconProperty, value);
            }
        }

        private static void CommandBackHandler(BindableObject bindable, object oldValue, object newValue)
        {
            ActionBar targetView;

            targetView = (ActionBar)bindable;

            targetView.BackButtonCommand = (ICommand)newValue;

            targetView.BackButton.Command = targetView.BackButtonCommand;
        }

        private async void HandleClicked(object sender, System.EventArgs e)
        {
            if (BackButtonCommand != null)
            {
                return;
            }

            await App.NavigationService.GoBackAsync();
        }

        public Thickness ActionBarPadding()
        {
            if (DeviceManager.IsIOS)
            {
                double supportedIOSVersion = Constants.SupportedIOSVersion;
                string[] rawVersionArray = DeviceManager.Version.ToString().Split('.');
                double currentIOSVersion = double.Parse(rawVersionArray[0] + '.' + rawVersionArray[1]);

                if (currentIOSVersion < supportedIOSVersion)
                {
                    return new Thickness(0, 20, 0, 0);
                }
                else
                {
                    return new Thickness(0, 0, 0, 0);
                }
            }
            else
            {
                return new Thickness(0, 0, 0, 0);
            }
        }
    }
}