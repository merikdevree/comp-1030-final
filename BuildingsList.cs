using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final
{
    public class BuildingsList
    {
        public List<Building> Buildings { get; set; }

        public BuildingsList()
        {
            Buildings = new List<Building>();
        }

        public void AddBuilding(Building building)
        {
            Buildings.Add(building);
        }

        public Building Search(string name)
        {
            foreach (Building building in Buildings)
            {
                if (building.Name == name)
                {
                    return building;
                }
            }
            return new Building("null", new Item[] { new Item("null", 0) });
        }
        public bool HasBuilding(string name)
        {
            foreach (Building b in Buildings)
            {
                if (b.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}