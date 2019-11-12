using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinBoilerplate.Controls
{
    public class ExtendedLabel : Label
    {
        public static readonly BindableProperty JustifyTextProperty = BindableProperty.Create(nameof(JustifyText), typeof(Boolean), typeof(ExtendedLabel), false, BindingMode.OneWay);
        public static readonly BindableProperty LongPressCommandProperty = BindableProperty.Create(nameof(LongPressCommand), typeof(ICommand), typeof(ExtendedLabel));
        public static readonly BindableProperty TapPressCommandProperty = BindableProperty.Create(nameof(TapPressCommand), typeof(ICommand), typeof(ExtendedLabel));
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ExtendedLabel), null);

        public bool JustifyText
        {
            get
            {
                return (Boolean)GetValue(JustifyTextProperty);
            }
            set
            {
                SetValue(JustifyTextProperty, value);
            }
        }

        public bool IsLongPressEnable
        {
            get
            {
                return LongPressCommand != null;
            }
        }

        public ICommand LongPressCommand
        {
            get
            {
                return (ICommand)GetValue(LongPressCommandProperty);
            }
            set
            {
                SetValue(LongPressCommandProperty, value);
            }
        }

        public bool IsTapPressEnable
        {
            get
            {
                return TapPressCommand != null;
            }
        }

        public ICommand TapPressCommand
        {
            get
            {
                return (ICommand)GetValue(TapPressCommandProperty);
            }
            set
            {
                SetValue(TapPressCommandProperty, value);
            }
        }

        public object CommandParameter
        {
            get
            {
                return (object)GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }
    }

}
