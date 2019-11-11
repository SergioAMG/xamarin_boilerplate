using System;
using System.ComponentModel;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinBoilerplate.Effects;

[assembly: ResolutionGroupName("XamarinBoilerplate")]
[assembly: ExportEffect(typeof(XamarinBoilerplate.iOS.Effects.ImageTintEffect), "ImageTintEffect")]
namespace XamarinBoilerplate.iOS.Effects
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

        private void ApplyTintColor()
        {
            var tintColor = GetTintColor();

            if (Control is UIImageView)
            {
                var control = Control as UIImageView;
                control.Image = control.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                control.TintColor = tintColor.ToUIColor();
            }
            else if (Control is UIButton)
            {
                var control = Control as UIButton;
                var image = control.ImageView.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                control.SetImage(image, UIControlState.Normal);
                control.TintColor = tintColor.ToUIColor();
            }
        }
    }

}