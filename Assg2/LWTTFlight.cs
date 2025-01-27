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
 
    class LWTTFlight : Flight
    {
        public LWTTFlight(string fn, string ori, string dest, DateTime et)
            : base(fn, ori, dest, et)
        {
        }

        public double CalculateLWTTFees()
        {
            double fee = 0;
            if (Origin == "Singapore (SIN)")
                fee += 800; 
            if (Destination == "Singapore (SIN)")
                fee += 500; 
            fee += 300; 
            fee += 500;
            return fee;
        }
    }
}
