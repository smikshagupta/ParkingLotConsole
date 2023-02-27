using System;
using System.Collections.Generic;
using ParkingLotConsole.enums;
using ParkingLotConsole.Exceptions;

namespace ParkingLotConsole
{
    class ParkingLot
    {
        Dictionary<string, int> slots = new Dictionary<string, int>();
        List<ParkingTicket> tickets = new List<ParkingTicket>();

        List<Vehicle> parkedVehicles = new List<Vehicle>();
        private int totalSlots = 0;
        int start,end = 0;
        int[] slotsArray;
        public ParkingLot(Dictionary<string,int> newSlots)
        {
            slots = newSlots;
            totalSlots= newSlots.Count;
            slotsArray = new int[totalSlots];
        }
        
        public Dictionary<string,int> CurrentOccupancy()
        {
            Dictionary<string, int> currentSlots = new Dictionary<string, int>();
            int start=0;
            foreach(string key in slots.Keys)
            {
                int count = 0;
                int end = start + slots[key];
                for (int i=start;i<end; i++)
                {
                    if (slotsArray[i] == 0)
                    {
                        count += 1;
                    }
                }
                currentSlots[key] = count;
                start = end;
            }
            return currentSlots;
        }

        public string ParkVehicle(Vehicle vehicle)
        {
            //Check if vehicle is already parked.
            string result;
            if (parkedVehicles.Exists(currvehicle => currvehicle.VehicleNumber.Equals(vehicle.VehicleNumber)))
            {
                result="Vehicle is already parked.";
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
                    result="Vehicle Parked";
                }
                else
                {
                    result="No parking slot available.";
                }

            }
            return result;
        }


        public bool UnparkVehicle(string vehicleNumber,out string vNumber,out DateTime? outTime )
        {
            bool isVehicleParked = false;
            vNumber = null;
            outTime = null;
            if (parkedVehicles.Count > 0)
            {

                if (parkedVehicles.Exists(vehicle => vehicle.VehicleNumber.Equals(vehicleNumber)))
                {
                    foreach (ParkingTicket ticket in tickets)
                    {
                        if (ticket.vehicleNumber.Equals(vehicleNumber))
                        {
                            ticket.outTime = DateTime.Now;
                            slotsArray[ticket.slotNumber - 1] = 0;
                            isVehicleParked = true;
                            vNumber = vehicleNumber;
                            outTime = ticket.outTime;
                            break;
                        }
                    }
                    Vehicle vehicle = parkedVehicles.Find(v => v.VehicleNumber.Equals(vehicleNumber));
                    parkedVehicles.Remove(vehicle);
                }
                else
                {
                    throw new VehicleNotFoundException();
                }

            }
            else
            {
                throw new EmptyParkingLotException();
            }
            return isVehicleParked;
        }
    
        public int AssignSlot(VehicleType type)
        {
            switch (type)
            {
                case VehicleType.TwoWheeler:
                    start = 0;
                    break;

                case VehicleType.FourWheeler:
                    start = slots["TwoWheeler"];
                    break;
                case VehicleType.HeavyVehicle:
                    start = totalSlots - slots["HeavyVehicle"];
                    break;
            }
            end = start + slots[type.ToString()];
            for (int i = start; i < end; i++)
            {
                if (slotsArray[i] == 0)
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
