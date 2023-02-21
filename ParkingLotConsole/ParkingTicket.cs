using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotConsole
{
    class ParkingTicket
    {
        public string vehicleNumber;

        public static int slotNumber=0;

        public DateTime inTime;

        public DateTime outTime;

        public ParkingTicket(string vehicleNumber)
        {
            this.vehicleNumber = vehicleNumber;
            slotNumber += 1;
            inTime = DateTime.Now;
            outTime = DateTime.Now;
        }

        public void DisplayTicket()
        {
            Console.WriteLine("---Parking Ticket---");

            Console.WriteLine($"Vehicle Number: {vehicleNumber}");
            Console.WriteLine($"Slot Number: {slotNumber}");
            Console.WriteLine($"InTime :{inTime} ");
            Console.WriteLine();
        }

    }
}
