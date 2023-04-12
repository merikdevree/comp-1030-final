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
        public TechTree techTree = new TechTree();
        private bool gameEnd;
        
        public Game()
        {
            Gui gui = new Gui();
            gui.Intro();
        }
        public void Run()
        {
            while (gameEnd == false)
            {
                
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