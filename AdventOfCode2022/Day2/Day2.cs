using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day2
    {
        public static int FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day2\\input.txt";
            using StreamReader reader = new StreamReader(inputFilePath);

            int totalPoints = 0;

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                char ennemyPlay = line[0];
                char myPlay = line[2];

                totalPoints += CalculatePoints(ennemyPlay, myPlay);
            }
            return totalPoints;
        }

        public static int SecondStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day2\\input.txt";
            using StreamReader reader = new StreamReader(inputFilePath);

            int totalPoints = 0;

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                char ennemyPlay = line[0];
                char result = line[2];

                totalPoints += CalculatePointsStep2(ennemyPlay, result);
            }
            return totalPoints;
        }

        private static int CalculatePointsStep2(char ennemyPlay, char result)
        {
            int myPlay = 0;
            if (result == 'Y')
            {
                myPlay = ennemyPlay + 23;
            }
            if (result == 'Z')
            {
                myPlay = ennemyPlay switch
                {
                    'A' => 'Y',
                    'B' => 'Z',
                    'C' => 'X',
                };
            }
            if (result == 'X')
            {
                myPlay = ennemyPlay switch
                {
                    'A' => 'Z',
                    'B' => 'X',
                    'C' => 'Y',
                };
            }
            return CalculatePoints(ennemyPlay, (char)myPlay);

        }

        private static int CalculatePoints(char ennemyPlay, char myPlay)
        {
            int myPlayPoints = myPlay - 87;
            if (myPlay - 23 == ennemyPlay)
            {
                return 3 + myPlayPoints;
            }
            else if (myPlay - 23 - ennemyPlay == 1 || myPlay - 23 - ennemyPlay == -2)
            {
                return myPlayPoints + 6;
            }
            
            return myPlayPoints;
        }
    }
}
