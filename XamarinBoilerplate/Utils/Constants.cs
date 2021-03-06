﻿using Xamarin.Forms;
using XamarinBoilerplate.Views.Samples;

namespace XamarinBoilerplate.Utils
{
    public static class Constants
    {
        public static readonly string WizzardComplete = "WizzardComplete";
        public static readonly string LoggedIn = "LoggedIn";
        public static readonly int UIAlertControllerPopupOptionRowHeight = 53;
        public static readonly int UIAlertControllerPopupOptionListItemViewWidth = 15;
        public static readonly double ShortToastDuration = 2.0;
        public static readonly double LongToastDuration = 4.0;
        public static readonly double ContactPageDetailsViewWidthFactor = 0.17;
        public static readonly int MaxCharForListItemsDisplay = 80;
        public static readonly int MaxCharForListItemsTitleDisplay = 45;
        public static readonly Thickness MarginForLeftIconOfActionBarAndroid = new Thickness(0, 0, 22, 0);
        public static readonly Thickness MarginForLeftIconOfActionBarIOS = new Thickness(0, 0, 27, 0);
        public static readonly Thickness MarginForRightIconOfActionBarAndroid = new Thickness(0, 0, 25, 0);
        public static readonly Thickness MarginForRightIconOfActionBarIOS = new Thickness(0, 0, 32, 0);
        public static readonly Thickness MarginForOptionsIconOfActionBarAndroid = new Thickness(0, 0, 3, 0);
        public static readonly Thickness MarginForOptionsIconOfActionBarIOS = new Thickness(0, 0, 5, 0);
        public static readonly Thickness MarginForSingleIconOfActionBarAndroid = new Thickness(0, 0, 8, 0);
        public static readonly Thickness MarginForSingleIconOfActionBarIOS = new Thickness(0, 0, 10, 0);
        public static readonly string CarouselViewMenu = nameof(CarouselSamplePage);
        public static readonly string CollectionViewMenu = nameof(CollectionViewSamplePage);
        public static readonly string PortraitMode = "Portrait";
        public static readonly string LandscapeMode = "Landscape";
        public static readonly double InitialTextSizeForNewsItem = 14;
        public static readonly double InitialTitleSizeForNewsItem = 16;
        public static readonly double TextIncreaseFactor = 1.10;
        public static readonly double TextDecreaseFactor = 0.90;
        public static readonly double SupportedIOSVersion = 11.0;
    }
}
