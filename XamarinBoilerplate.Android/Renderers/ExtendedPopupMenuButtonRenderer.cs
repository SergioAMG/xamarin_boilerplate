using System;
using System.Linq;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinBoilerplate.Controls;
using XamarinBoilerplate.Droid.Renderers;

[assembly: ExportRenderer(typeof(ExtendedPopupMenuButton), typeof(ExtendedPopupMenuButtonRenderer))]
namespace XamarinBoilerplate.Droid.Renderers
{
    public class ExtendedPopupMenuButtonRenderer : ImageButtonRenderer
    {
        private Context _context;
        private ExtendedPopupMenuButton _extendedPopupMenuButton;

        public ExtendedPopupMenuButtonRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ImageButton> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                _extendedPopupMenuButton = (ExtendedPopupMenuButton)e.NewElement;
                _extendedPopupMenuButton.Clicked -= ExtendedButtonClicked;
                _extendedPopupMenuButton.Clicked += ExtendedButtonClicked;
                _extendedPopupMenuButton.CallBackOpenMenu -= OpenMenu;
                _extendedPopupMenuButton.CallBackOpenMenu += OpenMenu;
            }
        }

        private void OpenMenu()
        {
            ShowPopupMenu(this);
        }

        private void ExtendedButtonClicked(object sender, EventArgs e)
        {
            ShowPopupMenu(this);
        }

        private void ShowPopupMenu(Android.Views.View view)
        {
            Android.Support.V7.Widget.PopupMenu popupMenu;
            if (_extendedPopupMenuButton.IsXamlPopup)
            {
                popupMenu  = new Android.Support.V7.Widget.PopupMenu(_context, view, 1, 0, Resource.Style.CustomPopupMenuStyle);
            }
            else
            {
                popupMenu = new Android.Support.V7.Widget.PopupMenu(_context, view, 1, 0, Resource.Style.PopupMenuStyle);
            }

            foreach (var item in _extendedPopupMenuButton.Options)
            {
                if (item.IsVisible)
                {
                    Java.Lang.ICharSequence option = new Java.Lang.String(item.Text);
                    popupMenu.Menu.Add(option);
                }
            }

            if (popupMenu.Menu.Size() > 0)
            {
                popupMenu.MenuItemClick -= PopupMenuItemClick;
                popupMenu.MenuItemClick += PopupMenuItemClick;
                popupMenu.Show();
            }
        }

        private void PopupMenuItemClick(object sender, Android.Support.V7.Widget.PopupMenu.MenuItemClickEventArgs e)
        {
            var optionSelected = _extendedPopupMenuButton.Options.FirstOrDefault(x => x.Text.Equals(e.Item.TitleFormatted.ToString()));

            if (optionSelected.TapPressCommand != null && optionSelected.TapPressCommand.CanExecute(null))
            {
                optionSelected.TapPressCommand.Execute(optionSelected.CommandParameter);
            }
        }
    }
}