using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Inventory
    {
        public Dictionary<Resource, int> Resources { get; set; }
        public List<Robot> Robots { get; set; }

        public Inventory()
        {
            Resources = new Dictionary<Resource, int>
            {
                { Resource.ORE, 0 },
                { Resource.CLAY, 0 },
                { Resource.OBSIDIAN, 0 },
                { Resource.GEODE, 0 }
            };
            Robots = new List<Robot> { new Robot { Income = Resource.ORE } };
        }
        public Inventory(Inventory inventory)
        {
            Resources = new Dictionary<Resource, int>
            {
                { Resource.ORE, inventory.Resources[Resource.ORE] },
                { Resource.CLAY, inventory.Resources[Resource.CLAY] },
                { Resource.OBSIDIAN, inventory.Resources[Resource.OBSIDIAN] },
                { Resource.GEODE, inventory.Resources[Resource.GEODE] }
            };
            Robots = inventory.Robots.Select(r => new Robot { Cost = r.Cost, Income = r.Income }).ToList();
        }

        public void GenerateResources()
        {
            foreach (Robot robot in Robots)
            {
                Resources[robot.Income]++;
            }
        }

        public bool CanBuildRobot(Robot robotCost)
        {
            bool canBuild = true;
            foreach (ResourceStack cost in robotCost.Cost)
            {
                canBuild &= cost.Quantity <= Resources[cost.Resource];
            }

            return canBuild;
        }

        public void BuildRobot(Robot robotCost)
        {
            foreach (ResourceStack cost in robotCost.Cost)
            {
                Resources[cost.Resource] -= cost.Quantity;
            }
            Robot newRobot = new Robot { Income = robotCost.Income };
            Robots.Add(newRobot);
        }
    }
}
