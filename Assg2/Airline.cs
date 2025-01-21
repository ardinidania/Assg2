using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assg2
{
    class Airline
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Dictionary<string, Flight> Flights { get; private set; } = new Dictionary<string, Flight>();


        public Airline(string c, string n)
        {
            Code = c;
            Name = n;
        }

        public bool AddFlight(Flight flight)
        {
            if (!Flights.ContainsKey(flight.FlightNumber))
            {
                Flights[flight.FlightNumber] = flight;
                return true;
            }
            return false;
        }

        public double CalculateFees()
        {
            double total = 0;
            foreach (var flight in Flights.Values)
            {
                total += flight.CalculateFees();
            }
            return total;
        }

        public bool RemoveFlight(Flight flight)
        {
            return Flights.Remove(flight.FlightNumber);
        }

        public override string ToString()
        {
            return "Code: " + Code +
                "\tName: " + Name;
        }
    }
}
