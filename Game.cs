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
        public BuildingsList buildings = new BuildingsList();
        private bool gameEnd;
        private int power;
        private int maxPower;
        private int cycles;
        private Objective objective;
        private int objectiveProgress;
        public Game()
        {
            inventory.AddItem(new Item("aluminum", 2));
            inventory.AddItem(new Item("iron", 2));
            inventory.AddItem(new Item("silicon", 2));
            inventory.AddItem(new Item("electronics", 1, new Item[] { new Item("aluminum", 1), new Item("silicon", 2) }));
            inventory.AddItem(new Item("glass", 0, new Item[] { new Item("silicon", 2) }));
            inventory.AddItem(new Item("lightweight alloy", 0, new Item[] { new Item("aluminum", 2), new Item("titanium", 1) }));
            inventory.AddItem(new Item("titanium", 0));
            buildings.AddBuilding(Building.ironMine);
            buildings.AddBuilding(Building.aluminumMine);
            buildings.AddBuilding(Building.solarPanel);
            buildings.AddBuilding(Building.solarPanel);

            objective = new Objective("Repair comms array", "Repair the comms array to begin recieving orders from Earth", new Inventory(), new Inventory(), -1);
            objective.requiredItems.AddItem(new Item("electronics", 2));
            objective.requiredItems.AddItem(new Item("aluminum", 3));
            maxPower = 2;
            power = maxPower;
            gameEnd = false;
            cycles = 0;
            objectiveProgress = 0;

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
                string[] inputArray = new string[3];
                inputArray = input.Split(' ');
                //refactor into a switch
                switch (inputArray[0])
                {
                    case "help":
                        if (inputArray.Length == 1)
                        {
                            Help();
                            break;
                        }
                        Help(inputArray[1]);
                        break;
                    case "exit":
                        gameEnd = true;
                        break;
                    case "buildings":
                        ShowBuildings();
                        break;
                    case "inventory":
                        ShowInventory();
                        break;
                    case "objective":
                        ShowObjective();
                        break;
                    case "craft":
                        if (inputArray.Length == 1)
                        {
                            input = GetInput("Enter an item to craft:");
                            Craft(input);
                            break;
                        }
                        Craft(inputArray[1]);
                        break;
                    case "mine":
                        if (inputArray.Length == 1)
                        {
                            input = GetInput("Enter an item to mine:");
                            Mine(input);
                            break;
                        }
                        Mine(inputArray[1]);
                        break;
                    case "build":
                        if (inputArray.Length == 1)
                        {
                            input = GetInput("Enter a building to build:");
                            Build(input);
                            break;
                        }
                        string building = inputArray[1] + " " + inputArray[2];
                        Build(building);
                        break;
                    case "ship":
                        if (inputArray.Length == 1)
                        {
                            string item = GetInput("Enter an item to ship:");
                            int amount = int.Parse(GetInput("Enter an amount to ship:"));
                            Ship(item, amount);
                            break;
                        }
                        //Ship(inputArray[1], int.Parse(inputArray[2]));
                        break;
                    case "end":
                        Update();
                        break;
                    default:
                        Console.WriteLine("Invalid command. Type 'help' for a list of commands.");
                        break;
                }
                if (power == 0) Update();
            }
        }

        void Update()
        {

            cycles++;
            foreach (Item item in objective.requiredItems.InventoryItems)
            {
                if (item.Amount == 0)
                {
                    objective.completed = true;
                }
                else
                {
                    objective.completed = false;
                }
            }
            if (objective.deadline != -1)
            {
                objective.deadline--;
                if (objective.deadline == 0)
                {
                    Console.WriteLine("Mission Failed");
                    gameEnd = true;
                }
            }
            if (objective.completed == true && objective.name == "Repair comms array")
            {
                objective = new Objective("Start shuttle construction", "Send 5 Lightweight Alloys to Earth", new Inventory(), new Inventory(), 10);
                //StoryAct1();
                objective.requiredItems.AddItem(new Item("lightweight alloy", 5));
            }
            else if (objective.completed == true)
            {
                Console.WriteLine("Mission Complete");
                gameEnd = true;
            }
            // count number of solar panels in buildings
            maxPower = 0;
            foreach (Building building in buildings.Buildings)
            {
                if (building.Name == "solar panel")
                {
                    maxPower += building.PowerGenerated;
                }
            }
            power = maxPower;
        }


        void Mine(string input)
        {
            input = input + " mine";
            if (buildings.HasBuilding(input) && buildings.Search(input).PowerRequired <= power)
            {
                power -= buildings.Search(input).PowerRequired;
                inventory.AddItem(buildings.Search(input).ItemProduced);
            }
            else
            {
                Console.WriteLine("Not enough power");
            }
        }

        void Ship(string item, int amount)
        {
            if (power < 1)
            {
                Console.WriteLine("Not enough power");
                return;
            }

            if (objective.requiredItems.Search(item).Type == "null")
            {
                Console.WriteLine("Invalid item");
                return;
            }
            if (inventory.HasItem(item, amount))
            {
                Item add = inventory.Search(item);
                add.Amount = amount;
                objective.inventory.AddItem(add);
                inventory.RemoveItem(item, amount);
                objective.requiredItems.RemoveItem(item, amount);
            }
            else
            {
                Console.WriteLine("Not enough items");
            }
            power--;
        }

        void Craft(string input)
        {
            input = input + " factory";
            if (buildings.HasBuilding(input) && buildings.Search(input).PowerRequired <= power)
            {
                power -= buildings.Search(input).PowerRequired;
                inventory.AddItem(buildings.Search(input).ItemProduced);
            }
            else
            {
                Console.WriteLine("Not enough power");
            }
        }

        void Build(string input)
        {
            input = input.ToLower();
            Item[] cost = new Item[1];
            switch (input)
            {
                case "solar panel":
                    cost = Building.solarPanel.ItemsRequired;
                    break;
                case "iron mine":
                    cost = Building.ironMine.ItemsRequired;
                    break;
                case "aluminum mine":
                    cost = Building.aluminumMine.ItemsRequired;
                    break;
                case "silicon mine":
                    cost = Building.siliconMine.ItemsRequired;
                    break;
                case "titanium mine":
                    cost = Building.titaniumMine.ItemsRequired;
                    break;
                case "electronics factory":
                    cost = Building.electronicsFactory.ItemsRequired;
                    break;
                case "glass factory":
                    cost = Building.glassFactory.ItemsRequired;
                    break;
                case "lightweight alloy factory":
                    cost = Building.lightweightAlloyFactory.ItemsRequired;
                    break;
                default:
                    Console.WriteLine("Invalid building");
                    return;
            }
            foreach (Item item in cost)
            {
                if (inventory.HasItem(item.Type) && inventory.Search(item.Type).Amount >= item.Amount)
                {
                    inventory.RemoveItem(item.Type, item.Amount);
                }
                else
                {
                    Console.WriteLine("Not enough " + item.Type);
                    return;
                }
            }
            switch (input)
            {
                case "solar panel":
                    buildings.AddBuilding(Building.solarPanel);
                    break;
                case "iron mine":
                    buildings.AddBuilding(Building.ironMine);
                    break;
                case "aluminum mine":
                    buildings.AddBuilding(Building.aluminumMine);
                    break;
                case "silicon mine":
                    buildings.AddBuilding(Building.siliconMine);
                    break;
                case "titanium mine":
                    buildings.AddBuilding(Building.titaniumMine);
                    break;
                case "electronics factory":
                    buildings.AddBuilding(Building.electronicsFactory);
                    break;
                case "glass factory":
                    buildings.AddBuilding(Building.glassFactory);
                    break;
                case "lightweight alloy factory":
                    buildings.AddBuilding(Building.lightweightAlloyFactory);
                    break;
            }
            power--;
        }
        void DisplayStats()
        {
            //Console.Clear();
            Console.Write("Power: " + power + "/" + maxPower);
            Console.Write("\t\t\tCycle: " + cycles);
            Console.WriteLine("\t\t\tCurrent Objective:" + objective.name);

        }
        private void Help(string command = "")
        {
            if (command == "build")
            {
                Console.WriteLine("build <building> - build a building");
                Console.WriteLine("Buildings:");
                Console.WriteLine("Solar Panel - Generates 1 power per cycle, costs 1 electronics and 2 glass");
                Console.WriteLine("Iron Mine - Produces iron, costs 1 electronics and 2 aluminum");
                Console.WriteLine("Aluminum Mine - Produces aluminum, costs 1 electronics and 2 aluminum");
                Console.WriteLine("Silicon Mine - Produces silicon, costs 2 iron and 2 aluminum");
                Console.WriteLine("Titanium Mine - Produces titanium, costs 2 aluminum and 1 electronics, power required: 2");
                Console.WriteLine("Electronics Factory - Produces electronics, costs 2 silicon and 2 aluminum, power required: 2");
                Console.WriteLine("Glass Factory - Produces glass, costs 2 silicon and 2 iron");
                Console.WriteLine("Lightweight Alloy Factory - Produces lightweight alloy, costs 2 titanium and 2 electronics, power required: 3");

            }
            else if (command == "ship")
            {
                Console.WriteLine("ship <item> <amount> - send item towards objective");
            }
            else if (command == "craft")
            {
                Console.WriteLine("craft <item> - craft an item, requires building that crafts it");
                Console.WriteLine("Items:");
                Console.WriteLine("Electronics - requires 2 silicon and 1 aluminum");
                Console.WriteLine("Glass - requires 2 silicon");
                Console.WriteLine("Lightweight Alloy - requires 2 aluminum and 1 titanium");
            }
            else if (command == "mine")
            {
                Console.WriteLine("mine <item> - mine an item");
            }
            else if (command == "help")
            {
                Console.WriteLine("help - show this message");
                Console.WriteLine("help <command> - show help for a command");
            }
            else if (command == "exit")
            {
                Console.WriteLine("exit - exit the game");
            }
            else if (command == "inventory")
            {
                Console.WriteLine("inventory - show inventory");
            }
            else if (command == "objective")
            {
                Console.WriteLine("objective - show objective");
            }
            else if (command == "buildings")
            {
                Console.WriteLine("buildings - show buildings");
            }
            else if (command == "end")
            {
                Console.WriteLine("end - end the current cycle");
            }
            else
            {
                Console.WriteLine("Commands:");
                Console.WriteLine("help - show this message");
                //Console.WriteLine("help <command> - show help for a command");
                Console.WriteLine("exit - exit the game");
                Console.WriteLine("inventory - show inventory");
                Console.WriteLine("objective - show objective");
                Console.WriteLine("craft <item> - craft an item");
                Console.WriteLine("mine <item> - mine an item");
                Console.WriteLine("build <building> - build a building");
                Console.WriteLine("ship - send item towards objective");
                Console.WriteLine("buildings - show buildings");
                Console.WriteLine("end - end the current cycle");
            }

        }

        public void ShowBuildings()
        {
            Console.WriteLine("Buildings:");
            foreach (Building building in buildings.Buildings)
            {
                Console.WriteLine(building.Name);
            }
        }
        public void ShowObjective()
        {
            Console.WriteLine("Objective: " + objective.name);
            if (objective.deadline != -1)
            {
                Console.WriteLine("Timeline: " + objective.deadline);
            }
            else
            {
                Console.WriteLine("No timer");
            }
            Console.WriteLine("Required Items:");
            foreach (Item item in objective.requiredItems.InventoryItems)
            {
                Console.WriteLine(item.Type + ": " + item.Amount);
            }
            Console.WriteLine("Shipped Items:");
            foreach (Item item in objective.inventory.InventoryItems)
            {
                Console.WriteLine(item.Type + ": " + item.Amount);
            }
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