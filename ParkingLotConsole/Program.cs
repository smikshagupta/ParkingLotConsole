using ParkingLotConsole.enums;
using System;
using System.Collections.Generic;
using ParkingLotConsole.Exceptions;
using ParkingLotConsole.Models;

namespace ParkingLotConsole
{
    class Program
    {
        
        private static void MainMenu()
        {
            Console.WriteLine("How can we help you?");
            Console.WriteLine("1 Park Vehicle");
            Console.WriteLine("2 UnPark Vehicle");
            Console.WriteLine("3 Check Available slots");
            Console.WriteLine("4 Exit\n");
        }

        private static void DisplayTicket(ParkingTicket ticket)
        {
            Console.WriteLine("---Parking Ticket---");
            Console.WriteLine($"Vehicle Number: {ticket.vehicleNumber}");
            Console.WriteLine($"Slot Number: {ticket.slotNumber}");
            Console.WriteLine($"InTime :{ticket.inTime} \n");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Parking Lot Console App\n");
            Console.WriteLine("Please Enter Parking slots \n");
            List<Slot> availableSlots = new List<Slot>();
            foreach(string vehicleType in Enum.GetNames(typeof(VehicleType)))
            {
                Console.WriteLine($"Slots for {vehicleType}");
                availableSlots.Add(new Slot((VehicleType)Enum.Parse(typeof(VehicleType),vehicleType), int.Parse(Console.ReadLine())));
            }
            ParkingLotService parkingLot = new ParkingLotService(availableSlots);
            MainMenu();
            while (true)
            {
                UserActions option = (UserActions) int.Parse(Console.ReadLine());
                switch (option)
                {
                    case UserActions.Exit:
                        return;
                        
                    case UserActions.ParkVehicle:
                        Console.WriteLine("Enter Vehicle Details");
                        Console.WriteLine("Enter Vehicle Number");
                        var vehicleNumber = Console.ReadLine();
                        Console.WriteLine("Choose Vehicle Type:");
                        Console.WriteLine("1 Two Wheeler");
                        Console.WriteLine("2 Four Wheeler");
                        Console.WriteLine("3 Heavy Vehicle");
                        // Enum.Parse(typeof(VehicleType), Console.ReadLine());
                        try
                        {
                            VehicleType type = (VehicleType)int.Parse(Console.ReadLine());
                            Vehicle vehicle = new Vehicle(vehicleNumber, type);
                            ParkingTicket ticket= parkingLot.ParkVehicle(vehicle);
                            if(ticket is null)
                            {
                                Console.WriteLine("No parking slot available.");
                            }
                            else
                            {
                                DisplayTicket(ticket);
                            }
                        }
                        catch(Exception e)
                        {
                            if (e is VehicleAlreadyParkedException)
                            {
                                Console.WriteLine(e);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Vehicle type");
                            }
                        }
                        break;
                    
                    case UserActions.UnParkVehicle:
                        Console.WriteLine("Enter vehicle Number");
                        try
                        {
                            string vNumber;
                            DateTime? outTime;
                            bool isUnparked=parkingLot.UnparkVehicle(Console.ReadLine(),out vNumber,out outTime);
                            if (isUnparked)
                                Console.WriteLine($"Vehicle {vNumber} unparked at {outTime}");
                        }
                        catch (Exception e)
                        { 
                            if(e is VehicleNotFoundException || e is EmptyParkingLotException)
                                Console.WriteLine(e);
                        }
                        break;
                    
                    case UserActions.CurrentOccupancy:
                        List<Slot> currentSlots=parkingLot.CurrentOccupancy();
                        foreach(Slot slot in currentSlots)
                        {
                            Console.WriteLine($"No. of {slot.vehicleType} slots: {slot.slots}");
                        }
                        break;
                    
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

                MainMenu();
            }   

        }
    }
}
