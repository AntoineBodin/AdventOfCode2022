using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day19
    {
        public static int FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day19\\input.txt";
            string sampleFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day19\\sample.txt";

            using StreamReader reader = new StreamReader(sampleFilePath);
            int total = 0;

            List<BluePrint> bluePrints = CreateBluePrints(reader);

            foreach (BluePrint bluePrint in bluePrints)
            {
                Inventory inventory = new Inventory();
                int maxGeodes = bluePrint.GeodeMaxCount(1, inventory);
                int qualityLevel = maxGeodes * bluePrint.Id;
                total += qualityLevel;
            }

            return total;
        }

        private static List<BluePrint> CreateBluePrints(StreamReader reader)
        {
            List<BluePrint> bluePrints = new List<BluePrint>();
            string line;
            int counter = 1;
            while ((line = reader.ReadLine()) != null)
            {
                BluePrint newBluePrint = new BluePrint()
                {
                    Id = counter++,
                    RobotCosts = new List<Robot>()
                };
                string[] robotsCosts = line[14..^1].ToString().Split(". ");
                foreach (string robotCost in robotsCosts)
                {
                    string[] tokens = robotCost.Split(" ");
                    Resource robotType = FromString(tokens[1]);
                    int resource1Quantity = int.Parse(tokens[4]);
                    Resource resource1 = FromString(tokens[5]);
                    ResourceStack robotCost1 = new ResourceStack
                    {
                        Resource = resource1,
                        Quantity = resource1Quantity
                    };
                    Robot robot = new Robot
                    {
                        Cost = new List<ResourceStack> { robotCost1 },
                        Income = robotType
                    };
                    if (tokens.Length > 6)
                    {
                        int resource2Quantity = int.Parse(tokens[7]);
                        Resource resource2 = FromString(tokens[8]);
                        ResourceStack robotCost2 = new ResourceStack
                        {
                            Resource = resource2,
                            Quantity = resource2Quantity
                        };
                        robot.Cost.Add(robotCost2);
                    }
                    newBluePrint.RobotCosts.Add(robot);
                }
                bluePrints.Add(newBluePrint);
            }
            return bluePrints;
        }

        private static Resource FromString(string resourceName)
        {
            return resourceName switch
            {
                "ore" => Resource.ORE,
                "clay" => Resource.CLAY,
                "obsidian" => Resource.OBSIDIAN,
                _ => Resource.GEODE,
            };
        }
    }

}
