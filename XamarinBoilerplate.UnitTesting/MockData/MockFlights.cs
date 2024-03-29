﻿using DataManagers.Entities;
using DataManagers.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinBoilerplate.UnitTesting.MockData
{
    public class MockFlights : IFlights
    {
        public async Task<List<Flights>> GetFlights()
        {
            List<Flights> ListItems = new List<Flights>
            {
                new Flights() { Airline = "Lufthansa", FlightNumber = "2711", GateNumber = "E33", DepartureAirportFullName = "SOFIA", DepartureAirport = "SOF", ArrivalAirportFullName = "MUNICH", ArrivalAirport = "MUC", Date = "10FEB2020", BoardingClass = "Economy", ExtraData = "API", PassengerName = "Ortinau, Davidmark", Status = "M/M" },
                new Flights() { Airline = "Lufthansa", FlightNumber = "2856", GateNumber = "F30", DepartureAirportFullName = "ANGELES", DepartureAirport = "LAX", ArrivalAirportFullName = "CHICAGO", ArrivalAirport = "CHI", Date = "15FEB2020", BoardingClass = "Premier", ExtraData = "API", PassengerName = "John, Doe", Status = "M/M" },
                new Flights() { Airline = "Lufthansa", FlightNumber = "2023", GateNumber = "D22", DepartureAirportFullName = "DALLAS", DepartureAirport = "DLX", ArrivalAirportFullName = "TEXAS", ArrivalAirport = "TXS", Date = "20FEB2020", BoardingClass = "Economy", ExtraData = "API", PassengerName = "Michael, Douglas", Status = "M/M" }
            };
            return await Task.FromResult<List<Flights>>(ListItems);
        }
    }
}
