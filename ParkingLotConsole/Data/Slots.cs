using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotConsole.Data
{
    class Slots
    {
        public static Dictionary<string, int> slots = new Dictionary<string, int>();

        public Dictionary<string, int> GetSlots()
        {
            return slots;
            //retrun database
        }

    }
}
