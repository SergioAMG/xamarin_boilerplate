using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Plugin.Fingerprint.Dialog;

namespace XamarinBoilerplate.Droid.Fragments
{
    public class CustomFingerprintDialogFragment : FingerprintDialogFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            var image = view.FindViewById<ImageView>(Resource.Id.fingerprint_imgFingerprint);
            image.SetImageResource(Resource.Drawable.baseline_fingerprint_black_48);
            image.SetColorFilter(Color.ParseColor("#7D7D7D"));
            return view;
        }
    }
}