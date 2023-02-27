using ParkingLotConsole.enums;
using System;
using System.Collections.Generic;
using ParkingLotConsole.Exceptions;
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
            Console.WriteLine("Enter 0 to exit\n");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Parking Lot Console App\n");

            Console.WriteLine("Please Enter Parking slots \n");

            Dictionary<string, int> slots = new Dictionary<string, int>(); 
            Console.WriteLine("Slots for Two Wheeler");
            slots["TwoWheeler"] = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Slots for Four Wheeler");
            slots["FourWheeler"] = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Slots for Heavy Vehicle");
            slots["HeavyVehicle"] = Convert.ToInt32(Console.ReadLine());

            ParkingLot parkingLot = new ParkingLot(slots);

            MainMenu();
            while (true)
            {
                var option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 0:
                        return;
                        
                    case 1:
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
                            string parkingMessage = parkingLot.ParkVehicle(vehicle);
                            Console.WriteLine(parkingMessage);
                        }
                        catch(Exception)
                        {
                            Console.WriteLine("Invalid Vehicle type");
                        }
                        break;
                    case 2:
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
                    case 3:
                        Dictionary<string,int> availableSlots=parkingLot.CurrentOccupancy();
                        foreach(string key in availableSlots.Keys)
                        {
                            Console.WriteLine($"No. of {key} slots: {availableSlots[key]}");
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
