﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotConsole.Exceptions
{
    class EmptyParkingLotException:Exception
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
