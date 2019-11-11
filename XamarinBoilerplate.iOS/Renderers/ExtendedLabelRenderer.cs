using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinBoilerplate.Controls;
using XamarinBoilerplate.iOS.Renderers;

[assembly: ExportRenderer(typeof(ExtendedLabel), typeof(ExtendedLabelRenderer))]
namespace XamarinBoilerplate.iOS.Renderers
{
    public class ExtendedLabelRenderer : LabelRenderer
    {
        protected ExtendedLabel _extendedLabel { get; private set; }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            var _extendedLabel = (Element as ExtendedLabel);

            if (_extendedLabel != null && _extendedLabel.JustifyText)
            {
                Control.TextAlignment = UITextAlignment.Justified;
            }

        }
    }

}