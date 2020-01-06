using Xamarin.Forms;

namespace XamarinBoilerplate.Controls
{
    public class ExtendedButton : Button
    {
        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(ExtendedButton), TextAlignment.Center);

        public TextAlignment HorizontalTextAlignment
        {
            get
            {
                return (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            }
            set
            {
                SetValue(HorizontalTextAlignmentProperty, value);
            }
        }
    }
}
