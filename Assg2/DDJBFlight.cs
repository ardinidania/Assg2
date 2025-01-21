using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assg2
{
    class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }

        public DDJBFlight(string flightNumber, string origin, string destination, DateTime expectedTime, double requestFee, string status = "On Time")
            : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }

        public override double CalculateFees()
        {
            return 400.0 + RequestFee;
        }

        public override string ToString()
        {
            return base.ToString() + "Request Fee: " + RequestFee;
        }
    }
}
