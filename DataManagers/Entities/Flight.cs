namespace DataManagers.Entities
{
    public class Flight
    {
        public string Airline { get; set; }
        public string FlightNumber { get; set; }
        public string GateNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string DepartureAirportFullName { get; set; }
        public string ArrivalAirport { get; set; }
        public string ArrivalAirportFullName { get; set; }
        public string Date { get; set; }
        public string BoardingClass { get; set; }
        public string ExtraData { get; set; }
        public string PassengerName { get; set; }
        public string Status { get; set; }
    }
}
