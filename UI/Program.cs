using System;
using CEIS400_Final_Team5.Data;
using CEIS400_Final_Team5.Logic;

// This will "run" the program. 

namespace CEIS400_Final_Team5.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var data = new DataManager();
            data.Initialize();

            var inv = new InventoryManager(data);
            var userMgr = new UserManager(data);

            Console.WriteLine("Tool Rental System skeleton running.");
            Console.WriteLine($"Seed equipment count: {data.Equipment.Count}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
