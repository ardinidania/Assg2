using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number : S10270562D
// Student Name : Ardini Dania
// Partner Number: S10266783H
// Partner Name : Gerick Yi
//==========================================================

namespace Assg2
{
    public class Flight
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }
        public string SpecialRequestCode { get; set; }  
        public string BoardingGate { get; set; }        

        // Constructor
        public Flight(string fn, string ori, string dest, DateTime et, string status = "On Time")
        {
            FlightNumber = fn;
            Origin = ori;
            Destination = dest;
            ExpectedTime = et;
            Status = status;
        }

        // Virtual method for calculating fees, allowing subclasses to override
        public virtual double CalculateFees()
        {
        
            return 100.0; // Example fee
        }

        // ToString method for displaying flight information
        public override string ToString()
        {
            return $"Flight: {FlightNumber}\tOrigin: {Origin}\tDestination: {Destination}\tExpectedTime: {ExpectedTime}\tStatus: {Status}";
        }
    }
}
