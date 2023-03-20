using ParkingLotConsole.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotConsole.Models
{
    class Slot
    {
        public VehicleType vehicleType { get; set; }
        public int slots { get; set; }

        public Slot(VehicleType type,int slots)
        {
            vehicleType = type;
            this.slots = slots;
        }
    }
}
