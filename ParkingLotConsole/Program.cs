using System;
using System.Collections;
using System.Collections.Generic;

namespace ParkingLotConsole
{
    class Program
    {
        public static void MainMenu()
        {
            Console.WriteLine("How can we help you?");
            Console.WriteLine("1 Park Vehicle");
            Console.WriteLine("2 UnPark Vehicle");
            Console.WriteLine("3 Check Available slots");
            Console.WriteLine("Enter 0 to exit");
            Console.WriteLine();
        }
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

            MainMenu();
            while (true)
            {
                var option = Convert.ToInt32(Console.ReadLine());
                if (option == 1)
                {
                    Console.WriteLine("Enter Vehicle Details");
                    Console.WriteLine("Enter Vehicle Number");
                    var vehicleNumber = Console.ReadLine();

                    Console.WriteLine("Choose Vehicle Type:");
                    Console.WriteLine("1 Two Wheeler");
                    Console.WriteLine("2 Four Wheeler");
                    Console.WriteLine("3 Heavy Vehicle");

                    var type = Console.ReadLine();
                    switch ( type)
                    {
                        case "1":
                            type = "Two Wheeler";
                            break;
                        case "2":
                            type = "Four Wheeler";
                            break;
                        case "3":
                            type = "Heavy Vehicle";
                            break;
                        default:
                            Console.WriteLine("Invalid vehicle type");
                            break;
                    }

                    Vehicle vehicle = new Vehicle(vehicleNumber, type);
                    parkingLot.ParkVehicle(vehicle);
                }

                else if (option == 2)
                {
                    Console.WriteLine("Enter vehicle Number");
                    parkingLot.UnparkVehicle(Console.ReadLine());
                }
                else if (option == 3)
                {
                    parkingLot.currentOccupancy();
                }
                else if (option == 0)
                    break;
                else
                {
                    Console.WriteLine("Invalid option");
                }
                MainMenu();
            }
            

        }
    }
}
