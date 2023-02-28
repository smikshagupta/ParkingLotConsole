using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotConsole.Exceptions
{
    class VehicleAlreadyParkedException:Exception
    {
        public override string Message
        {
            get
            {
                return "Vehicle is already parked.";
            }
        }

    }
}
