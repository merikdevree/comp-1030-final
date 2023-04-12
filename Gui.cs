using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final
{
    public class Gui
    {
        public void Intro()
        {
            Console.WriteLine("Main power online. Boot sequence ready.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            string[] lines = {"Power: ONLINE\n", "Construction Interface: ONLINE\n", "AI Core Systems: NOMINAL\n", "Communications Array: OFFLINE\n"};
            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    Console.Write(c);
                    System.Threading.Thread.Sleep(50);
                    
                }
            }
            Console.WriteLine("");
            lines = new string[] {"System Check Complete.\n", "Displaying Mission Objective...\n"};
            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    Console.Write(c);
                    System.Threading.Thread.Sleep(50);
                    
                }
            }
            Thread.Sleep(500);
            Console.WriteLine("");
            Console.WriteLine("C:/UN/KS-2/MISSION_OBJ.txt");
            lines = new string[] {"Launch Date: 04/08/2152\n", "Location: Earth-Moon L1\n", "The UN KS-2 Satelite is an advanced AI tasked with building a Lunar outpost.\n",
                                "With a network of dones and it's AI core, the system will build and expand itself to build a foothold for humanity on the moon.\n",
                                "In recent years, Kessler Syndrome has become a major concern for the UN. The KS-2 is humanity's last hope.\n"};
            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    Console.Write(c);
                    System.Threading.Thread.Sleep(50);
                    
                }
            }
            Thread.Sleep(4000);
            Console.Clear();

        }
    }
}