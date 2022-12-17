using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day3
    {
        public static int FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day3\\input.txt";
            using StreamReader reader = new StreamReader(inputFilePath);
            string line;

            int total = 0;
            while ((line = reader.ReadLine()) != null)
            {
                int length = line.Length;
                IEnumerable<char> firstCompartiment = line.Take(length / 2);
                IEnumerable<char> secondCompartiment = line.TakeLast(length / 2);

                char commonItem = firstCompartiment.First(f => secondCompartiment.Contains(f));

                if (commonItem >= 'a' && commonItem <= 'z')
                {
                    total += commonItem - 96;
                }
                else
                {
                    total += commonItem - 38; 
                }
            }
            return total;
        }
        public static int SecondStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day3\\input.txt";
            using StreamReader reader = new StreamReader(inputFilePath);
            string line;

            int total = 0;
            while ((line = reader.ReadLine()) != null)
            {
                string firstElfRugsack = line;
                string secondElfRugsack = reader.ReadLine();
                string thirdElfRugsack = reader.ReadLine();
                char badge = firstElfRugsack.First(f => secondElfRugsack.Contains(f) && thirdElfRugsack.Contains(f));

                if (badge >= 'a' && badge <= 'z')
                {
                    total += badge - 96;
                }
                else
                {
                    total += badge - 38;
                }
            }
            return total;
        }
    }
}
