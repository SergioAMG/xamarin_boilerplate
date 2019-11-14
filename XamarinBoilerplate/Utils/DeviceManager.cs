using XamarinBoilerplate.Enums;

namespace XamarinBoilerplate.Utils
{
    public static class DeviceManager
    {
        public static string Platform { get; set; }
        public static string Manufacturer { get; set; }
        public static string Version { get; set; }
        public static string Idiom { get; set; }
        public static string Device { get; set; }
        public static string Orientation { get; set; }


        public static bool IsAndroid
        {
            get
            {
                return Platform == Devices.Android.ToString();
            }
        }
        
        public static bool IsIOS
        {
            get
            {
                return Platform == Devices.iOS.ToString();
            }
        }

        public static bool IsTablet
        {
            get
            {
                return Idiom == Devices.Tablet.ToString();
            }
        }

        public static bool IsLandscape
        {
            get
            {
                return Orientation == Devices.Landscape.ToString();
            }
        }
    }
}
