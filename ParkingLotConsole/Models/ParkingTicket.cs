using System;


namespace ParkingLotConsole
{
    class ParkingTicket
    {
        internal string vehicleNumber;

        internal int slotNumber=0;

        internal DateTime inTime;

        internal DateTime outTime;

        public ParkingTicket(string vehicleNumber,int slot)
        {
            this.vehicleNumber = vehicleNumber;
            slotNumber =slot;
            inTime = DateTime.Now;
            
        }

    }
}
