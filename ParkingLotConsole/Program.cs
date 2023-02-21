using System;
using System.Collections;
using System.Collections.Generic;

namespace ParkingLotConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Parking Lot Console App");
            Console.WriteLine();

            Console.WriteLine("Please Enter Parking slots");
            Console.WriteLine();

            Dictionary<string, int> slots = new Dictionary<string, int>(); 
            Console.WriteLine("Slots for Two Wheeler");
            slots["Two Wheeler"] = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Slots for Four Wheeler");
            slots["Four Wheeler"] = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Slots for Heavy Vehicle");
            slots["Heavy Vehicle"] = Convert.ToInt32(Console.ReadLine());

            ParkingLot parkingLot = new ParkingLot(slots);

            parkingLot.currentOccupancy();

            Console.WriteLine("How can we help you?");
            Console.WriteLine("1 Park Vehicle");
            Console.WriteLine("2 UnPark Vehicle");
            Console.WriteLine("Enter 0 to exit");

            while (true)
            {
                var option = Convert.ToInt32(Console.ReadLine());
                if (option == 1)
                {
                    Console.WriteLine("Enter Vehicle Details");
                    Console.WriteLine("Enter Vehicle Number");
                    var vehicleNumber = Console.ReadLine();

                    Console.WriteLine("Enter Vehicle Type");
                    var type = Console.ReadLine();

                    Vehicle vehicle = new Vehicle(vehicleNumber, type);
                    parkingLot.ParkVehicle(vehicle);
                }

                else if (option == 2)
                {

                }
                else if (option == 0)
                    break;
                else
                {
                    Console.WriteLine("Invalid option");
                }
                Console.WriteLine("Choose an option");
            }
            

        }
    }
}
