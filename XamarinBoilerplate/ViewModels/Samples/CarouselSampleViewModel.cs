using DataManagers.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels.DataObjects;

namespace XamarinBoilerplate.ViewModels.Samples
{
    public class CarouselSampleViewModel : BaseViewModel
    {
        private ObservableCollection<FlightsViewModel> _flights;

        public ObservableCollection<FlightsViewModel> Flights
        {
            get
            {
                return _flights;
            }
            set
            {
                if (_flights != value)
                {
                    _flights = value;
                    OnPropertyChanged(nameof(Flights));
                }
            }
        }

        public bool ExtraInfoVisible
        {
            get
            {
                return !DeviceManager.IsLandscape;
            }
        }

        public CarouselSampleViewModel(IDataService dataManager = null) : base(dataManager)
        {
            if (dataManager != null)
            {
                DataManager = dataManager;
            }
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadData();
        }

        public async Task LoadData()
        {
            if (Flights == null)
            {
                Flights = new ObservableCollection<FlightsViewModel>();
                var flights = await DataManager.Flights.GetFlights();

                // Mappings
                foreach (var item in flights)
                {
                    FlightsViewModel flightsViewModel = new FlightsViewModel()
                    {
                        Airline = item.Airline,
                        FlightNumber = item.FlightNumber,
                        GateNumber = item.GateNumber,
                        DepartureAirport = item.DepartureAirport,
                        DepartureAirportFullName = item.DepartureAirportFullName,
                        ArrivalAirport = item.ArrivalAirport,
                        ArrivalAirportFullName = item.ArrivalAirportFullName,
                        Date = item.Date,
                        BoardingClass = item.BoardingClass,
                        ExtraData = item.ExtraData,
                        PassengerName = item.PassengerName,
                        Status = item.Status
                    };
                    Flights.Add(flightsViewModel);
                }
                if (!UnitTestingManager.IsRunningFromNUnit)
                {
                    await NavigationService.HideLoadingIndicator();
                }
            }
        }

        public void SetOrientationValues()
        {
            OnPropertyChanged(nameof(ExtraInfoVisible));
        }
    }
}
