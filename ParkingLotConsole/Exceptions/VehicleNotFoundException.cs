using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotConsole.Exceptions
{
    class VehicleNotFoundException:ApplicationException
    {
        public override string Message
        {
            get
            {
                return "Vehicle is not parked in the Parking Lot.";
            }
        }
    }
}
