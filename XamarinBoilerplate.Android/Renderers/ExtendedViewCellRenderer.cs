using Android.Content;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinBoilerplate.Controls;
using XamarinBoilerplate.Droid.Renderers;

[assembly: ExportRenderer(typeof(ExtendedViewCell), typeof(ExtendedViewCellRenderer))]
namespace XamarinBoilerplate.Droid.Renderers
{
    public class ExtendedViewCellRenderer : ViewCellRenderer
    {
        private Android.Views.View cellCore;
        private bool isSelected;
        protected override Android.Views.View GetCellCore(Cell item,
                                                      Android.Views.View convertView,
                                                      ViewGroup parent,
                                                      Context context)
        {
            cellCore = base.GetCellCore(item, convertView, parent, context);
            isSelected = false;
            return cellCore;
        }

        protected override void OnCellPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
        {
            base.OnCellPropertyChanged(sender, args);
            if (args.PropertyName == "IsSelected")
            {
                isSelected = !isSelected;
                var extendedViewCell = sender as ViewCell;
                if (isSelected)
                    cellCore.SetBackgroundColor(Android.Graphics.Color.Transparent);
                else
                    cellCore.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}