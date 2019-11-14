using Xamarin.Forms;

namespace XamarinBoilerplate.Effects
{
    public class TintEffect
    {
        public static readonly BindableProperty TintColorProperty = BindableProperty.CreateAttached("TintColor", typeof(Color), typeof(TintEffect), Xamarin.Forms.Color.Default, propertyChanged: OnHasShadowChanged);

        public static Color GetTintColor(BindableObject view)
        {
            return (Color)view.GetValue(TintColorProperty);
        }

        public static void SetTintColor(BindableObject view, Color value)
        {
            view.SetValue(TintColorProperty, value);
        }

        static void OnHasShadowChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;

            if (view == null)
            {
                return;
            }

            view.Effects.Add(new ImageTintEffect());
        }
    }

}
