using System;
using System.Collections.Generic;

namespace ParkingLotConsole
{
    class ParkingLot
    {
        Dictionary<string, int> currentSlots = new Dictionary<string, int>();
        List<ParkingTicket> tickets = new List<ParkingTicket>();

        List<Vehicle> parkedVehicles = new List<Vehicle>();
        private int totalSlots = 0;
        int start,end = 0;
        int[] slotsArray;
        public ParkingLot(Dictionary<string,int> newSlots)
        {
            currentSlots = newSlots;
            foreach(int slot in currentSlots.Values)
            {
                totalSlots += slot;
            }
            slotsArray = new int[totalSlots];
            for(int i = 0; i < totalSlots; i++)
            {
                slotsArray[i] = -1;
            }
        }
        
        public void CurrentOccupancy()
        {
            int start=0;
            foreach(string key in currentSlots.Keys)
            {
                int count = 0;
                int end = start + currentSlots[key];
                for (int i=start;i<end; i++)
                {
                    if (slotsArray[i] == -1)
                    {
                        count += 1;
                    }
                }
                Console.WriteLine($" No. of {key} slots are {count}");
                start = end;
            }
        }

        public void ParkVehicle(Vehicle vehicle)
        {
            //Check if vehicle is already parked.
            if (parkedVehicles.Exists(currvehicle => currvehicle.VehicleNumber.Equals(vehicle.VehicleNumber)))
            {
                Console.WriteLine("Vehicle is already parked.");
            }
            else
            {
                int slotNumber = AssignSlot(vehicle.VehicleType);
                if (slotNumber != 0)
                {
                    parkedVehicles.Add(vehicle);
                    //Issue Ticket
                    Console.WriteLine("Generating your Ticket\n");
                    ParkingTicket ticket = new ParkingTicket(vehicle.VehicleNumber, slotNumber);
                    tickets.Add(ticket);
                    DisplayTicket(ticket);
                    Console.WriteLine("Vehicle Parked");
                }
                else
                {
                    Console.WriteLine("No parking slot available.");
                }

            }   
        }


        public void UnparkVehicle(string vehicleNumber)
        {
            if (parkedVehicles.Count > 0)
            {

                if (parkedVehicles.Exists(vehicle => vehicle.VehicleNumber.Equals(vehicleNumber)))
                {
                    foreach (ParkingTicket ticket in tickets)
                    {
                        if (ticket.vehicleNumber.Equals(vehicleNumber))
                        {
                            ticket.outTime = DateTime.Now;
                            slotsArray[ticket.slotNumber - 1] = -1;
                            Console.WriteLine($"Vehicle {ticket.vehicleNumber} unparked at {ticket.outTime}");
                        }

                    }
                    Vehicle vehicle = parkedVehicles.Find(v => v.VehicleNumber.Equals(vehicleNumber));
                    parkedVehicles.Remove(vehicle);
                    //slots[vehicle.VehicleType] += 1;
                }
                else
                {
                    Console.WriteLine($"Vehicle {vehicleNumber} is not parked in the parking lot.");
                }

            }
            else
            {
                Console.WriteLine("No Vehicles parked.");
            }
        }
    
        public int AssignSlot(string type)
        {
            switch (type)
            {
                case "TwoWheeler":
                    start = 0;
                    break;

                case "FourWheeler":
                    start = currentSlots["TwoWheeler"];
                    break;
                case "HeavyVehicle":
                    start = totalSlots - currentSlots["HeavyVehicle"];
                    break;
            }
            end = start + currentSlots[type];
            for (int i = start; i < end; i++)
            {
                if (slotsArray[i] == -1)
                {
                    slotsArray[i] = i + 1;
                    return i+1;
                }
            }
            return 0;
        }

        public void DisplayTicket(ParkingTicket ticket)
        {
            Console.WriteLine("---Parking Ticket---");
            Console.WriteLine($"Vehicle Number: {ticket.vehicleNumber}");
            Console.WriteLine($"Slot Number: {ticket.slotNumber}");
            Console.WriteLine($"InTime :{ticket.inTime} \n");
        }
    }
}
