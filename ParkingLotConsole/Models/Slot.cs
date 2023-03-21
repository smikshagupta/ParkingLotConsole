using ParkingLotConsole.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotConsole.Models
{
    class Slot
    {
        public Guid ID { get; set; }
        public int SlotNumber { get; set; }

        public VehicleType VehicleType { get; set; }
        public bool isAvailable;

        public string VehicleNumber { get; set; }

        public Slot(int slotNumber, VehicleType vehicleType, bool isAvailable)
        {
            SlotNumber = slotNumber;
            VehicleType = vehicleType;
            this.isAvailable = isAvailable;
        }
    }
}
