using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AdventOfCode2022
{
    public class Day1
    {
        public static int FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day1\\input.txt";

            int currentMax = 0;
            int currentCount = 0;

            using StreamReader reader = new StreamReader(inputFilePath);
            string calories;
            while ((calories = reader.ReadLine()) != null)
            {
                if (calories == "")
                {
                    if (currentCount > currentMax)
                    {
                        currentMax = currentCount;
                    }
                    currentCount = 0;
                    continue;
                }
                currentCount += int.Parse(calories);
            }

            return currentMax;
        }

        public static int SecondStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day1\\input.txt";

            int currentCount = 0;
            List<int> maxs = new List<int> { 0, 0, 0 };
            using StreamReader reader = new StreamReader(inputFilePath);
            string calories;
            while ((calories = reader.ReadLine()) != null)
            {
                if (calories == "")
                {
                    maxs.Sort();
                    for (int i = 0; i < 3; i++)
                    {
                        if (currentCount > maxs[i])
                        {
                            maxs[i] = currentCount;
                            break;
                        }
                    }
                    currentCount = 0;
                    continue;
                }
                currentCount += int.Parse(calories);
            }

            return maxs[0] + maxs[1] + maxs[2];
        }
    }
}
