using System;
using System.ComponentModel;
using System.Linq;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinBoilerplate.Effects;

[assembly: ResolutionGroupName("XamarinBoilerplate")]
[assembly: ExportEffect(typeof(XamarinBoilerplate.Droid.Effects.ImageTintEffect), "ImageTintEffect")]
namespace XamarinBoilerplate.Droid.Effects
{
    public class ImageTintEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var effect = (XamarinBoilerplate.Effects.ImageTintEffect)Element.Effects.FirstOrDefault(e => e is XamarinBoilerplate.Effects.ImageTintEffect);
                if (effect != null)
                {
                    ApplyTintColor();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("CANNOT SET PROPERTY ON ATTACHED CONTROL. ERROR: " + ex.Message);
            }
        }

        protected override void OnDetached()
        {

        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            try
            {
                if (args.PropertyName.Equals("Source"))
                {
                    var effect = (XamarinBoilerplate.Effects.ImageTintEffect)Element.Effects.FirstOrDefault(e => e is XamarinBoilerplate.Effects.ImageTintEffect);
                    if (effect != null)
                    {
                        ApplyTintColor();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("CANNOT SET PROPERTY ON ATTACHED CONTROL. ERROR: ", ex.Message);
            }
        }

        private void ApplyTintColor()
        {
            var tintColor = GetTintColor();
            var colorFilter = new PorterDuffColorFilter(tintColor.ToAndroid(), PorterDuff.Mode.SrcIn);

            if (Control is global::Android.Widget.ImageView)
            {
                var control = Control as global::Android.Widget.ImageView;

                control?.SetColorFilter(colorFilter);
            }
            else if (Control is global::Android.Widget.ImageButton)
            {
                var control = Control as global::Android.Widget.ImageButton;
                control?.SetColorFilter(colorFilter);
            }
            else if (Control is global::Android.Widget.Button)
            {
                var control = Control as global::Android.Widget.Button;
                control.CompoundDrawableTintMode = PorterDuff.Mode.SrcIn;

                int[][] states = new int[][]
                  {
                    new int[] { Android.Resource.Attribute.StateEnabled},
                    new int[] {-Android.Resource.Attribute.StateEnabled},
                    new int[] {-Android.Resource.Attribute.StateChecked},
                    new int[] { Android.Resource.Attribute.StateChecked } 
                  };

                int[] colors = new int[]
                {
                tintColor.ToAndroid(),
                tintColor.ToAndroid(),
                tintColor.ToAndroid(),
                tintColor.ToAndroid(),
                };

                control.CompoundDrawableTintList = new Android.Content.Res.ColorStateList(states, colors);
            }
        }

        private Xamarin.Forms.Color GetTintColor()
        {
            var tintColor = TintEffect.GetTintColor(Element);

            if (tintColor == Xamarin.Forms.Color.Default)
            {
                return (Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["TintColor"];
            }
            else
            {
                return tintColor;
            }

        }
    }

}