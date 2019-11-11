using System;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinBoilerplate.Controls;
using XamarinBoilerplate.Droid.Renderers;

[assembly: ExportRenderer(typeof(ExtendedLabel), typeof(ExtendedLabelRenderer))]
namespace XamarinBoilerplate.Droid.Renderers
{
    public class ExtendedLabelRenderer : LabelRenderer
    {
        protected ExtendedLabel _extendedLabel { get; private set; }

        public ExtendedLabelRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            _extendedLabel = (Element as ExtendedLabel);

            if (_extendedLabel != null && _extendedLabel.JustifyText)
            {
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
                {
                    Control.JustificationMode = Android.Text.JustificationMode.InterWord;
                }
            }

            if (_extendedLabel != null && _extendedLabel.IsLongPressEnable)
            {
                Control.LongClick += HandleLongClick;
            }

            if (_extendedLabel != null && _extendedLabel.IsTapPressEnable)
            {
                Control.Click += HandleClick;
            }
        }

        private void HandleLongClick(object sender, LongClickEventArgs e)
        {
            _extendedLabel.LongPressCommand?.Execute(_extendedLabel);
        }

        private void HandleClick(object sender, EventArgs e)
        {
            _extendedLabel.TapPressCommand?.Execute(_extendedLabel);
        }
    }

}