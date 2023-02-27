using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotConsole.Exceptions
{
    class EmptyParkingLotException:ApplicationException
    {
        public override string Message
        {
            get
            {
                return "Parking Lot is empty.";
            }
        }
    }
}
