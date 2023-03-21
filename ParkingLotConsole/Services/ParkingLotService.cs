using System;
using System.Collections.Generic;
using ParkingLotConsole.Exceptions;
using ParkingLotConsole.Models;

namespace ParkingLotConsole
{
    class ParkingLotService
    {
        List<ParkingTicket> tickets = new List<ParkingTicket>();
        ParkingLot parkingLot;
        public ParkingLotService(List<Slot> newSlots)
        {
            parkingLot = new ParkingLot(newSlots);
        }
        
        public List<Slot> CurrentOccupancy()
        {
            return parkingLot.slots.FindAll(slot=>slot.isAvailable);
        }

        public ParkingTicket ParkVehicle(Vehicle vehicle)
        {
            //Check if vehicle is already parked.
            //if (parkedVehicles.Exists(currvehicle => currvehicle.VehicleNumber.Equals(vehicle.VehicleNumber)))
            if(parkingLot.slots.Find(slot=>slot.VehicleNumber==vehicle.VehicleNumber)!=null)
            {
                throw new VehicleAlreadyParkedException();
            }
            else
            {
                foreach (Slot slot in parkingLot.slots)
                    {
                        if (slot.VehicleType==vehicle.VehicleType && slot.isAvailable == true)
                        {
                            ParkingTicket ticket = new ParkingTicket(vehicle.VehicleNumber, slot.SlotNumber);
                            tickets.Add(ticket);
                            slot.isAvailable = false;
                            slot.VehicleNumber = vehicle.VehicleNumber;
                            return ticket;
                        }
                    }
                
            }
            return null;
        }


        public bool UnparkVehicle(string vehicleNumber,out DateTime? outTime )
        {
            outTime = null;
            if (parkingLot.slots.Find(slot=>slot.isAvailable==false)!=null)
            {

                if (parkingLot.slots.Find(slot => slot.VehicleNumber == vehicleNumber) != null)
                {
                    foreach (ParkingTicket ticket in tickets)
                    {
                        if (ticket.vehicleNumber.Equals(vehicleNumber))
                        {
                            ticket.outTime = DateTime.Now;
                            outTime = ticket.outTime;
                            break;
                        }
                    }
                    Slot findSlot = parkingLot.slots.Find(slot => slot.VehicleNumber == vehicleNumber);
                    findSlot.isAvailable = true; //Free the slot while unparking
                    findSlot.VehicleNumber = null;
                    return true;
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
        }
     
    }
}
