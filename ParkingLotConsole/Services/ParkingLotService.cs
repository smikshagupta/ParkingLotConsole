using System;
using System.Collections.Generic;
using ParkingLotConsole.enums;
using ParkingLotConsole.Exceptions;
using ParkingLotConsole.Models;

namespace ParkingLotConsole
{
    class ParkingLotService
    {
        //Dictionary<string, int> slots;
        List<Slot> slots = new List<Slot>();
        List<ParkingTicket> tickets = new List<ParkingTicket>();
        List<Vehicle> parkedVehicles = new List<Vehicle>();
        private int totalSlots = 0;
        int start,end = 0;
        int[] slotsArray;
        public ParkingLotService(List<Slot> newSlots)
        {
            slots = newSlots;
            //foreach(string key in newSlots.Keys)
            //{
            //    totalSlots += newSlots[key];
            //}

            foreach(Slot slot in slots)
            {
                totalSlots += slot.slots;
            }
           slotsArray = new int[totalSlots];
        }
        
        public List<Slot> CurrentOccupancy()
        {
            List<Slot> currentSlots = new List<Slot>();
            int start=0;
            foreach(Slot slot in slots)
            {
                int count = 0;
                int end = start + slot.slots;
                for (int i=start;i<end; i++)
                {
                    if (slotsArray[i] == 0)
                    {
                        count += 1;
                    }
                }
                currentSlots.Add(new Slot(slot.vehicleType, count));
                start = end;
            }
            return currentSlots;
        }

        public ParkingTicket ParkVehicle(Vehicle vehicle)
        {
            //Check if vehicle is already parked.
            if (parkedVehicles.Exists(currvehicle => currvehicle.VehicleNumber.Equals(vehicle.VehicleNumber)))
            {
                throw new VehicleAlreadyParkedException();
            }
            else
            {
                int slotNumber = AssignSlot(vehicle.VehicleType);
                if (slotNumber != 0)
                {
                    parkedVehicles.Add(vehicle);
                    //Console.WriteLine("Generating your Ticket\n");
                    ParkingTicket ticket = new ParkingTicket(vehicle.VehicleNumber, slotNumber);
                    tickets.Add(ticket);
                    return ticket;
                }
            }
            return null;
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
                    start = slots.Find(slot => slot.vehicleType == VehicleType.TwoWheeler).slots;
                    break;
                case VehicleType.HeavyVehicle:
                    start = totalSlots - slots.Find(slot => slot.vehicleType == type).slots;
                    break;
            }
            end = start + slots.Find(slot => slot.vehicleType == type).slots;
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

    }
}
