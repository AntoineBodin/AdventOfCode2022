using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day4
    {
        public static int FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day4\\input.txt";
            using StreamReader reader = new StreamReader(inputFilePath);
            string line;

            int total = 0;
            while ((line = reader.ReadLine()) != null)
            {
                string[] pair = line.Split(',');
                string firstElfRange = pair[0];
                string[] firstRange = firstElfRange.Split('-');
                int startFirstRange = int.Parse(firstRange[0]);
                int endFirstRange = int.Parse(firstRange[1]);

                string secondElfRange = pair[1];
                string[] secondRange = secondElfRange.Split('-');
                int startSecondRange = int.Parse(secondRange[0]);
                int endSecondRange = int.Parse(secondRange[1]);

                if (startFirstRange >= startSecondRange && endFirstRange <= endSecondRange ||
                    startFirstRange <= startSecondRange && endFirstRange >= endSecondRange)
                {
                    total++;
                }
            }
            return total;
        }

        public static int SecondStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day4\\input.txt";
            using StreamReader reader = new StreamReader(inputFilePath);
            string line;

            int total = 0;
            while ((line = reader.ReadLine()) != null)
            {
                string[] pair = line.Split(',');
                string firstElfRange = pair[0];
                string[] firstRange = firstElfRange.Split('-');
                int startFirstRange = int.Parse(firstRange[0]);
                int endFirstRange = int.Parse(firstRange[1]);

                string secondElfRange = pair[1];
                string[] secondRange = secondElfRange.Split('-');
                int startSecondRange = int.Parse(secondRange[0]);
                int endSecondRange = int.Parse(secondRange[1]);
                
                if (startFirstRange >= startSecondRange && startFirstRange <= endSecondRange ||
                    endFirstRange >= startSecondRange && endFirstRange <= endSecondRange ||
                    startSecondRange >= startFirstRange && startSecondRange <= endFirstRange ||
                    endSecondRange >= startFirstRange && endSecondRange <= endFirstRange)
                {
                    total++;
                }
            }
            return total;
        }
    }
}
