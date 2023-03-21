using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotConsole.Models
{
    class ParkingLot
    {
        public List<Slot> slots { get; set; }

        public ParkingLot(List<Slot> slots)
        {
            this.slots = slots;   
        }
    }
}
