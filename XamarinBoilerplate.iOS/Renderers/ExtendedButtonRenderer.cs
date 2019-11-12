using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinBoilerplate.Controls;
using XamarinBoilerplate.iOS.Renderers;

[assembly: ExportRenderer(typeof(ExtendedButton), typeof(ExtendedButtonRenderer))]
namespace XamarinBoilerplate.iOS.Renderers
{
    public class ExtendedButtonRenderer : ButtonRenderer
    {
    }
}