using System;
using System.Collections.Generic;
using System.Text;
using ParkingLotConsole.enums;
namespace ParkingLotConsole
{
    class Vehicle
    {
        public string VehicleNumber { get; set; }

        public VehicleType VehicleType { get; set; }

        public Vehicle(string number,VehicleType type)
        {
            VehicleNumber = number;
            VehicleType = type;
        }
    }
}
