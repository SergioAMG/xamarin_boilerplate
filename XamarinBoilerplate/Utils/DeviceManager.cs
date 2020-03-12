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

        public static AppleDeviceType GetAppleDeviceType()
        {
            AppleDeviceType deviceType;
            string resolution = App.ScreenHeight.ToString() + "x" + App.ScreenWidth.ToString();

            switch (resolution)
            {
                // Source of information: https://ios-resolution.com/
                // Also https://iosref.com/ios/
                case "960x640"  : 
                case "640x960"  : deviceType = AppleDeviceType.iPhone4; break;
                case "1136x640" : 
                case "640x1136" : deviceType = AppleDeviceType.iPhoneSE_5; break;
                case "1334x750" : 
                case "750x1134" : deviceType = AppleDeviceType.iPhone8_7_6; break;
                case "2208x1242": 
                case "1242x2208": deviceType = AppleDeviceType.iPhone8Plus_7Plus_6SPlus_6Plus; break;
                case "2436x1125": 
                case "1125x2436": deviceType = AppleDeviceType.iPhoneX_XS_11Pro; break;
                case "1792x828" : 
                case "828x1792" : deviceType = AppleDeviceType.iPhone11_XR; break;
                case "2688x1242": 
                case "1242x2688": deviceType = AppleDeviceType.iPhone11ProMax_XSMax; break;
                case "1024x768" : 
                case "768x1024" : deviceType = AppleDeviceType.iPad_2_Mini; break;
                case "2048x1536": 
                case "1536x2048": deviceType = AppleDeviceType.iPad3_4_Air_Mini2_Mini3_Air2_Mini4_Pro_2017_2018; break;
                case "2224x1668": 
                case "1668x2224": deviceType = AppleDeviceType.iPadProSec10; break;
                case "2732x2048": 
                case "2048x2732": deviceType = AppleDeviceType.iPadProSec12; break;
                default         : deviceType = AppleDeviceType.iPhone11_XR; break;
            };

            return deviceType;
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
