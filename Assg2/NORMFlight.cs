using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // tostring
    }
}
