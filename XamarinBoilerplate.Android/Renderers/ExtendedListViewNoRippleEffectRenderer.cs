using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinBoilerplate.Controls;
using XamarinBoilerplate.Droid.Renderers;

[assembly: ExportRenderer(typeof(ExtendedListViewNoRippleEffect), typeof(ExtendedListViewNoRippleEffectRenderer))]
namespace XamarinBoilerplate.Droid.Renderers
{
    public class ExtendedListViewNoRippleEffectRenderer : ListViewRenderer
    {
        public ExtendedListViewNoRippleEffectRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
            Control.SetSelector(Resource.Layout.no_selector);
        }
    }
}