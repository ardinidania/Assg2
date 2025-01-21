using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assg2
{
    abstract class Flight
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }

        protected Flight(string fn, string ori, string dest, DateTime et, string status = "On Time")
        {
            FlightNumber = fn;
            Origin = ori;
            Destination = dest;
            ExpectedTime = et;
            Status = status;
        }

        public abstract double CalculateFees();

        public override string ToString()
        {
            return "Flight: " + FlightNumber +
                "\tOrigin: " + Origin +
                "\tDestination: " + Destination +
                "\tExpectedTime: " + ExpectedTime +
                "\tStatus: " + Status;
                // $"Flight: {FlightNumber} | {Origin} -> {Destination} | {ExpectedTime:HH:mm} | {Status}";
        }
    }
}
