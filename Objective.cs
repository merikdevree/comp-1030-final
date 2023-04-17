using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final
{
    public class Objective
    {
        public string name;
        public string description;
        public int deadline;
        public Inventory inventory;
        public Inventory requiredItems;
        public bool completed;
        public Objective(string name, string description, Inventory inventory, Inventory required, int deadline)
        {
            this.name = name;
            this.description = description;
            this.deadline = deadline;
            this.inventory = inventory;
            this.requiredItems = required;
            this.completed = false;

        }
    }
}