using System.Text.RegularExpressions;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final
{

    public class Game
    {

        public Inventory inventory = new Inventory();
        private bool gameEnd;
        private int power;
        private int maxPower;
        private int cycles;
        private string currentObjective;
        public Game()
        {
            maxPower = 2;
            power = maxPower;
            gameEnd = false;
            cycles = 0;
            currentObjective = "None";
        }

        public string GetInput(string prompt)
        {
            Console.WriteLine(prompt);
            try
            {
                string input = Console.ReadLine();
                return input;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "error";
            }
            
        }

        public void Run(bool skipIntro = false)
        {
            if (skipIntro == false)
            {
                //Game Intro
                Console.WriteLine("Main power online. Boot sequence ready.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

                string[] lines = { "\nPower: ONLINE\n", "Construction Interface: ONLINE\n", "AI Core Systems: NOMINAL\n", "Communications Array: OFFLINE\n" };
                foreach (string line in lines)
                {
                    foreach (char c in line)
                    {
                        Console.Write(c);
                        Thread.Sleep(50);
                    }
                }
                Console.WriteLine("");
                lines = new string[] { "System Check Complete.\n", "Displaying Mission Objective...\n" };
                foreach (string line in lines)
                {
                    foreach (char c in line)
                    {
                        Console.Write(c);
                        Thread.Sleep(50);
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
                        Thread.Sleep(50);
                    }
                }
                Thread.Sleep(4000);
                Console.Clear();
            }
           
            while (gameEnd == false)
            {
                DisplayStats();
                string input = GetInput("Enter a command:");
                string[] regex = { @"^help", @"^exit", @"^inventory", @"^craft", @"^research" }; // regex for commands

                if (Regex.IsMatch(regex[0], input))
                {
                    Help();
                }
                else if (Regex.IsMatch(regex[1], input))
                {
                    gameEnd = true;
                }
                else if (Regex.IsMatch(regex[2], input))
                {
                    ShowInventory();
                }
                else if (Regex.IsMatch(regex[3], input))
                {
                    //Craft(input);
                }
                else if (Regex.IsMatch(regex[4], input))
                {
                    //Research(input);
                }
                else
                {
                    Console.WriteLine("Invalid command");
                }
            }
        }

        //TODO: finish commands, add research, add crafting, add file reading
        // devote power to things. building more buildings ++ production
        void CraftItem()
        {
            if (power > 0)
            {
                string itemName = GetInput("What would you like to craft?");
                Item itemToCraft = inventory.Search(itemName);
                if (itemToCraft.Type == "null")
                {
                    Console.WriteLine("Item not found");
                }
                else
                {
                     Item[] required = itemToCraft.requiredItems;
                    foreach (Item item in required)
                    {
                        // search inventory for item and check if amount is greater than or equal to required amount
                        
                    }
                }
            }
        }

        void DisplayStats()
        {
            Console.Clear();
            Console.Write("Power: " + power + "/" + maxPower);
            Console.Write("\t\t\tCycle: " + cycles);
            Console.WriteLine("\t\t\tCurrent Objective:" + currentObjective);
        }
        private void Help()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("help - show this message");
            Console.WriteLine("help <command> - show help for a command");
            Console.WriteLine("exit - exit the game");
            Console.WriteLine("inventory - show inventory");
            Console.WriteLine("craft <item> - craft an item");
            Console.WriteLine("research - show research tree");
            Console.WriteLine("research <tech> - research a tech");
        }

        public void ShowInventory()
        {
            Console.WriteLine("Inventory:");
            foreach (Item item in inventory.InventoryItems)
            {
                Console.WriteLine(item.Type + ": " + item.Amount);
            }
        }
    }
}