using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingLotConsole
{
    class ParkingLot
    {
        Dictionary<string, int> slots = new Dictionary<string, int>();
        List<ParkingTicket> tickets = new List<ParkingTicket>();

        List<Vehicle> parkedVehicles = new List<Vehicle>();
        public ParkingLot(Dictionary<string,int> newSlots)
        {
            slots = newSlots;     
        }

        public void CurrentOccupancy()
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
                    if (parkedVehicles.Exists(currvehicle => currvehicle.VehicleNumber.Equals(vehicle.VehicleNumber)))
                    {
                        Console.WriteLine("Vehicle is already parked.");
                    }
                    else
                    {
                        parkedVehicles.Add(vehicle);
                        //Issue Ticket
                        Console.WriteLine("Generating your Ticket");
                        Console.WriteLine();

                        ParkingTicket ticket = new ParkingTicket(vehicle.VehicleNumber);
                        tickets.Add(ticket);
                        ticket.DisplayTicket();
                        slots[vehicle.VehicleType] -= 1;
                        Console.WriteLine("Vehicle Parked");
                    }
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
                            Console.WriteLine($"Vehicle {ticket.vehicleNumber} unparked at {ticket.outTime}");
                        }

                    }
                    Vehicle vehicle = parkedVehicles.Find(v => v.VehicleNumber.Equals(vehicleNumber));
                    parkedVehicles.Remove(vehicle);
                    slots[vehicle.VehicleType] += 1;
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
    }
}
