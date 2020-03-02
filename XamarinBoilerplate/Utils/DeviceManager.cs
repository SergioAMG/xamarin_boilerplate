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

        public static IPhoneType GetIPhoneType()
        {
            IPhoneType phoneType = new IPhoneType();
            string resolution = App.ScreenHeight.ToString() + "x" + App.ScreenWidth.ToString();

            switch (resolution)
            {
                // Source of information: https://ios-resolution.com/
                // Also https://iosref.com/ios/
                case "960x640"  : phoneType = IPhoneType.iPhone4; break;
                case "1136x640" : phoneType = IPhoneType.iPhoneSE_5; break;
                case "1334x750" : phoneType = IPhoneType.iPhone8_7_6; break;
                case "2208x1242": phoneType = IPhoneType.iPhone8Plus_7Plus_6SPlus_6Plus; break;
                case "2436x1125": phoneType = IPhoneType.iPhoneX_XS_11Pro; break;
                case "1792x828" : phoneType = IPhoneType.iPhone11_XR; break;
                case "2688x1242": phoneType = IPhoneType.iPhone11ProMax_XSMax; break;
                default         : phoneType = IPhoneType.iPhone11_XR; break;
            };

            return phoneType;
        }

        public static bool IsIOSVersionGreaterOrEqualToSupportedIOSVersion()
        {
            if (IsIOS)
            {
                double currentIOSVersion;
                string[] rawVersionArray = Version.ToString().Split('.');

                if (rawVersionArray.Length > 1)
                {
                    currentIOSVersion = double.Parse(rawVersionArray[0] + '.' + rawVersionArray[1]);
                }
                else
                {
                    currentIOSVersion = double.Parse(rawVersionArray[0]);
                }

                return currentIOSVersion >= Constants.SupportedIOSVersion;
            }
            else
            {
                return false;
            }
        }
    }
}
