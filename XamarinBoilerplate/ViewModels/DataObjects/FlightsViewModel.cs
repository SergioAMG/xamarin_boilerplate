using XamarinBoilerplate.Utils;

namespace XamarinBoilerplate.ViewModels.DataObjects
{
    public class FlightsViewModel : BaseViewModel
    {
        private string _airline;
        private string _flightNumber;
        private string _gateNumber;
        private string _departureAirport;
        private string _departureAirportFullName;
        private string _arrivalAirport;
        private string _arrivalAirportFullName;
        private string _date;
        private string _boardingClass;
        private string _extraData;
        private string _passengerName;
        private string _status;

        public string Airline
        {
            get
            {
                return _airline;
            }
            set
            {
                if (_airline != value)
                {
                    _airline = value;
                    OnPropertyChanged(nameof(Airline));
                }
            }
        }

        public string FlightNumber
        {
            get
            {
                return _flightNumber;
            }
            set
            {
                if (_flightNumber != value)
                {
                    _flightNumber = value;
                    OnPropertyChanged(nameof(FlightNumber));
                }
            }
        }

        public string GateNumber
        {
            get
            {
                return _gateNumber;
            }
            set
            {
                if (_gateNumber != value)
                {
                    _gateNumber = value;
                    OnPropertyChanged(nameof(GateNumber));
                }
            }
        }

        public string DepartureAirport
        {
            get
            {
                return _departureAirport;
            }
            set
            {
                if (_departureAirport != value)
                {
                    _departureAirport = value;
                    OnPropertyChanged(nameof(DepartureAirport));
                }
            }
        }

        public string DepartureAirportFullName
        {
            get
            {
                return _departureAirportFullName;
            }
            set
            {
                if (_departureAirportFullName != value)
                {
                    _departureAirportFullName = value;
                    OnPropertyChanged(nameof(DepartureAirportFullName));
                }
            }
        }

        public string ArrivalAirport
        {
            get
            {
                return _arrivalAirport;
            }
            set
            {
                if (_arrivalAirport != value)
                {
                    _arrivalAirport = value;
                    OnPropertyChanged(nameof(ArrivalAirport));
                }
            }
        }

        public string ArrivalAirportFullName
        {
            get
            {
                return _arrivalAirportFullName;
            }
            set
            {
                if (_arrivalAirportFullName != value)
                {
                    _arrivalAirportFullName = value;
                    OnPropertyChanged(nameof(ArrivalAirportFullName));
                }
            }
        }

        public string Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        public string BoardingClass
        {
            get
            {
                return _boardingClass;
            }
            set
            {
                if (_boardingClass != value)
                {
                    _boardingClass = value;
                    OnPropertyChanged(nameof(BoardingClass));
                }
            }
        }

        public string ExtraData
        {
            get
            {
                return _extraData;
            }
            set
            {
                if (_extraData != value)
                {
                    _extraData = value;
                    OnPropertyChanged(nameof(ExtraData));
                }
            }
        }

        public string PassengerName
        {
            get
            {
                return _passengerName;
            }
            set
            {
                if (_passengerName != value)
                {
                    _passengerName = value;
                    OnPropertyChanged(nameof(PassengerName));
                }
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }
    }
}
