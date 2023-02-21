using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingLotConsole
{
    class ParkingLot
    {
        Dictionary<string, int> slots = new Dictionary<string, int>();

        List<Vehicle> parkedVehicles = new List<Vehicle>();
        public ParkingLot(Dictionary<string,int> newSlots)
        {
            slots = newSlots;     
        }

        public void currentOccupancy()
        {
            foreach(string key in slots.Keys)
            {
                Console.WriteLine($" No. of {key} slots are {slots[key]}");
            }
        }

        public void ParkVehicle(Vehicle vehicle)
        {
            if (slots.Keys.Contains(vehicle.VehicleType))
            {
                
                if (slots[vehicle.VehicleType] > 0) 
                {
                    parkedVehicles.Add(vehicle);
                    //Issue Ticket
                    Console.WriteLine("Generating your Ticket");
                    slots[vehicle.VehicleType] -= 1;
                    Console.WriteLine("Vehicle Parked");
                }
                else
                {
                    Console.WriteLine("No parking slot available");
                }
            }
            else
            {
                Console.WriteLine("Invalid Vehicle Type.");
            }
        }
    }
}
