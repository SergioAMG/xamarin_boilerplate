using System.ComponentModel;
using Android.Content;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinBoilerplate.Controls;
using XamarinBoilerplate.Droid.Renderers;

[assembly: ExportRenderer(typeof(ExtendedButton), typeof(ExtendedButtonRenderer))]
namespace XamarinBoilerplate.Droid.Renderers
{
    public class ExtendedButtonRenderer : ButtonRenderer
    {
        public new ExtendedButton Element
        {
            get
            {
                return (ExtendedButton)base.Element;
            }
        }

        public ExtendedButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                return;
            }

            SetTextAlignment();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ExtendedButton.HorizontalTextAlignmentProperty.PropertyName)
            {
                SetTextAlignment();
            }
        }

        public void SetTextAlignment()
        {
            Control.Gravity = GravityFlags.CenterVertical | Element.HorizontalTextAlignment.ToHorizontalGravityFlags();
        }
    }

    public static class AlignmentHelper
    {
        public static GravityFlags ToHorizontalGravityFlags(this Xamarin.Forms.TextAlignment alignment)
        {
            if (alignment == Xamarin.Forms.TextAlignment.Center)
            {
                return GravityFlags.AxisSpecified;
            }

            return alignment == Xamarin.Forms.TextAlignment.End ? GravityFlags.Right : GravityFlags.Left;
        }
    }
}