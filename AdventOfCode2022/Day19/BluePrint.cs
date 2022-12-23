using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class BluePrint
    {
        public int Id { get; set; }
        public List<Robot> RobotCosts { get; set; }
        public int GeodeMaxCount(int minute, Inventory inventory)
        {
            if (minute == 24)
            {
                Console.WriteLine("FINITO");
                return inventory.Resources[Resource.GEODE];
            }
            else
            {
                Console.WriteLine(minute);
                int maxGeodes = 0;
                foreach (Robot robotCost in RobotCosts)
                {
                    if (inventory.CanBuildRobot(robotCost))
                    {
                        Inventory copyInventory = new Inventory(inventory);
                        copyInventory.GenerateResources();
                        copyInventory.BuildRobot(robotCost);
                        int maxGeodeBranch = GeodeMaxCount(minute + 1, copyInventory);
                        if (maxGeodeBranch > maxGeodes)
                        {
                            maxGeodes = maxGeodeBranch;
                        }
                    }
                }

                inventory.GenerateResources();
                int maxGeodesDoingNothing = GeodeMaxCount(minute + 1, inventory);

                if (maxGeodesDoingNothing > maxGeodes)
                {
                    maxGeodes = maxGeodesDoingNothing;
                }

                return maxGeodes;
            }
        }
    }
}
