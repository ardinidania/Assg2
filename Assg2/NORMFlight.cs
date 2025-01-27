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
    class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status = "On Time")
            : base(flightNumber, origin, destination, expectedTime, status) { }

        public override double CalculateFees()
        {
            return 100.0; 
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
