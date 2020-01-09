using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace XamarinBoilerplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : BaseContentPage
    {
        private bool initialLoadNeeded = true;

        public MapPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            CustomMap.MyLocationEnabled = false;
            CustomMap.UiSettings.ZoomControlsEnabled = true;
            CustomMap.UiSettings.ZoomGesturesEnabled = true;
            CustomMap.UiSettings.MyLocationButtonEnabled = false;
            CustomMap.UiSettings.CompassEnabled = true;

            // TODO: Remove this line and activate GPS permissions 
            //var location = await Xamarin.Essentials.Geolocation.GetLocationAsync();
            var location = new Xamarin.Essentials.Location(20.697657, -103.372877);

            Pin currentLocationPin = new Pin()
            {
                Icon = BitmapDescriptorFactory.DefaultMarker(Color.Gray),
                Type = PinType.Place,
                Label = "Epam Systems Mexico",
                Position = new Position(location.Latitude, location.Longitude),
                ZIndex = 5
            };

            CustomMap.Pins.Add(currentLocationPin);
            CustomMap.SelectedPin = currentLocationPin;

            if (initialLoadNeeded)
            {
                CustomMap.MoveToRegion(
                  MapSpan.FromCenterAndRadius(
                  new Position(location.Latitude, location.Longitude), Distance.FromMiles(3)));
                initialLoadNeeded = false;
            }
        }
    }
}