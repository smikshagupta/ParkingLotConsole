using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotConsole
{
    class Vehicle
    {
        public string VehicleNumber { get; set; }

        public string VehicleType { get; set; }

        public Vehicle(string number,string type)
        {
            VehicleNumber = number;
            VehicleType = type;
        }
    }
}
