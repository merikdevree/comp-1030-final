using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final
{
    public class Building
    {
        static public Building ironMine = new Building("iron mine", new Item[] { new Item("aluminum", 2), new Item("electronics", 1) }, 1, 0, new Item("iron", 1));
        static public Building aluminumMine = new Building("aluminum mine", new Item[] { new Item("aluminum", 2), new Item("electronics", 1) }, 1,0, new Item("aluminum", 1));
        static public Building titaniumMine = new Building("titanium mine", new Item[] { new Item("aluminum", 2), new Item("electronics", 1) }, 2, 0, new Item("titanium", 1));
        static public Building siliconMine = new Building("silicon mine", new Item[] { new Item("aluminum", 2), new Item("iron", 2) }, 1, 0, new Item("silicon", 1));
        static public Building solarPanel = new Building("solar panel", new Item[] { new Item("electronics", 1), new Item("glass", 2) }, 0, 1);
        static public Building electronicsFactory = new Building("electronics factory", new Item[] { new Item("aluminum", 2), new Item("silicon", 2) }, 2, 0, new Item("electronics", 1));
        static public Building glassFactory = new Building("glass factory", new Item[] { new Item("silicon", 2), new Item("iron", 2)}, 1, 0, new Item("glass", 1));
        static public Building lightweightAlloyFactory = new Building("lightweight alloy factory", new Item[] { new Item("titanium", 2), new Item("electronics", 2)}, 3, 0, new Item("lightweight alloy", 1));
        public string Name;
        public int PowerRequired;
        public int PowerGenerated;
        public Item ItemProduced;
        public Item[] ItemsRequired;
        public Building(string name, Item[] itemsRequired, int powerRequired = 0, int powerGenerated = 0, Item itemProduced = null)
        {
            Name = name;
            PowerRequired = powerRequired;
            PowerGenerated = powerGenerated;
            ItemProduced = itemProduced;
            ItemsRequired = itemsRequired;
        }
    }
}