using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day14
    {
        public static int FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day14\\input.txt";
            string test = @"498,4 -> 498,6 -> 496,6
503,4 -> 502,4 -> 502,9 -> 494,9";

            byte[] byteArray = Encoding.ASCII.GetBytes(test);
            MemoryStream stream = new MemoryStream(byteArray);

            using StreamReader reader = new StreamReader(inputFilePath);
            string line;
            int total = 0;
            List<KeyValuePair<int, int>> rocks = new List<KeyValuePair<int, int>>();
            while ((line = reader.ReadLine()) != null)
            {
                string[] rockAngles = line.Split(" -> ");
                string[] firstRockCoords = rockAngles[0].Split(",");
                KeyValuePair<int, int> firstRock = new KeyValuePair<int, int>(int.Parse(firstRockCoords[0]), int.Parse(firstRockCoords[1]));
                rocks.Add(firstRock);
                for (int i = 0; i < rockAngles.Length - 1; i++)
                {
                    string[] lastRockCoords = rockAngles[i + 1].Split(",");
                    KeyValuePair<int, int> lastRock = new KeyValuePair<int, int>(int.Parse(lastRockCoords[0]), int.Parse(lastRockCoords[1]));

                    //x axis
                    if (firstRock.Key != lastRock.Key)
                    {
                        if (firstRock.Key < lastRock.Key)
                        {
                            for (int j = firstRock.Key; j <= lastRock.Key; j++)
                            {
                                KeyValuePair<int, int> newRock = new KeyValuePair<int, int>(j, firstRock.Value);
                                rocks.Add(newRock);
                            }
                        }
                        else
                        {
                            for (int j = firstRock.Key; j >= lastRock.Key; j--)
                            {
                                KeyValuePair<int, int> newRock = new KeyValuePair<int, int>(j, firstRock.Value);
                                rocks.Add(newRock);
                            }
                        }
                    }
                    //y axis
                    else
                    {
                        if (firstRock.Value < lastRock.Value)
                        {
                            for (int j = firstRock.Value; j <= lastRock.Value; j++)
                            {
                                KeyValuePair<int, int> newRock = new KeyValuePair<int, int>(firstRock.Key, j);
                                rocks.Add(newRock);
                            }
                        }

                        else
                        {
                            for (int j = firstRock.Value; j >= lastRock.Value; j--)
                            {
                                KeyValuePair<int, int> newRock = new KeyValuePair<int, int>(firstRock.Key, j);
                                rocks.Add(newRock);
                            }
                        }
                    }
                    firstRock = lastRock;
                }
            }
            rocks = rocks.Distinct().ToList();
            int leftestPoint = rocks.MinBy(r => r.Key).Key;
            int rightestPoint = rocks.MaxBy(r => r.Key).Key;
            int deepestPoint = rocks.MaxBy(r => r.Value).Value;
            bool canAddSand = true;
            while (canAddSand)
            {
                canAddSand = AddSand(rocks, leftestPoint, rightestPoint);
                if (total % 150 == 0)
                {
                    Print(rocks, leftestPoint, rightestPoint, deepestPoint);
                }
                total++;
            }
            return total - 1;
        }

        public static int SecondStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day14\\input.txt";
            string test = @"498,4 -> 498,6 -> 496,6
503,4 -> 502,4 -> 502,9 -> 494,9";

            byte[] byteArray = Encoding.ASCII.GetBytes(test);
            MemoryStream stream = new MemoryStream(byteArray);

            using StreamReader reader = new StreamReader(inputFilePath);
            string line;
            int total = 0;
            List<KeyValuePair<int, int>> rocks = new List<KeyValuePair<int, int>>();
            while ((line = reader.ReadLine()) != null)
            {
                string[] rockAngles = line.Split(" -> ");
                string[] firstRockCoords = rockAngles[0].Split(",");
                KeyValuePair<int, int> firstRock = new KeyValuePair<int, int>(int.Parse(firstRockCoords[0]), int.Parse(firstRockCoords[1]));
                rocks.Add(firstRock);
                for (int i = 0; i < rockAngles.Length - 1; i++)
                {
                    string[] lastRockCoords = rockAngles[i + 1].Split(",");
                    KeyValuePair<int, int> lastRock = new KeyValuePair<int, int>(int.Parse(lastRockCoords[0]), int.Parse(lastRockCoords[1]));

                    //x axis
                    if (firstRock.Key != lastRock.Key)
                    {
                        if (firstRock.Key < lastRock.Key)
                        {
                            for (int j = firstRock.Key; j <= lastRock.Key; j++)
                            {
                                KeyValuePair<int, int> newRock = new KeyValuePair<int, int>(j, firstRock.Value);
                                rocks.Add(newRock);
                            }
                        }
                        else
                        {
                            for (int j = firstRock.Key; j >= lastRock.Key; j--)
                            {
                                KeyValuePair<int, int> newRock = new KeyValuePair<int, int>(j, firstRock.Value);
                                rocks.Add(newRock);
                            }
                        }
                    }
                    //y axis
                    else
                    {
                        if (firstRock.Value < lastRock.Value)
                        {
                            for (int j = firstRock.Value; j <= lastRock.Value; j++)
                            {
                                KeyValuePair<int, int> newRock = new KeyValuePair<int, int>(firstRock.Key, j);
                                rocks.Add(newRock);
                            }
                        }

                        else
                        {
                            for (int j = firstRock.Value; j >= lastRock.Value; j--)
                            {
                                KeyValuePair<int, int> newRock = new KeyValuePair<int, int>(firstRock.Key, j);
                                rocks.Add(newRock);
                            }
                        }
                    }
                    firstRock = lastRock;
                }
            }
            rocks = rocks.Distinct().ToList();
            int leftestPoint = rocks.MinBy(r => r.Key).Key;
            int rightestPoint = rocks.MaxBy(r => r.Key).Key;
            int deepestPoint = rocks.MaxBy(r => r.Value).Value;

            for (int i = -2000; i < 2000; i++)
            {
                KeyValuePair<int, int> newRock = new KeyValuePair<int, int>(i, deepestPoint + 2);
                rocks.Add(newRock);
            }
            bool canAddSand = true;
            while (canAddSand)
            {
                canAddSand = AddSand(rocks, leftestPoint, rightestPoint);
                if (total % 150 == 0)
                {
                    Print(rocks, leftestPoint, rightestPoint, deepestPoint + 2);
                }
                total++;
            }
            Print(rocks, leftestPoint, rightestPoint, deepestPoint + 2);
            return total;
        }

        private static bool AddSand(List<KeyValuePair<int, int>> map, int leftestPoint, int rightestPoint)
        {
            bool isRested = false;
            KeyValuePair<int, int> sandPosition = new KeyValuePair<int, int>(500, 0);
            while (!isRested)
            {
                KeyValuePair<int, int> possibleDestinationDown = new KeyValuePair<int, int>(sandPosition.Key, sandPosition.Value + 1);
                KeyValuePair<int, int> possibleDestinationDownLeft = new KeyValuePair<int, int>(sandPosition.Key - 1, sandPosition.Value + 1);
                KeyValuePair<int, int> possibleDestinationDownRight = new KeyValuePair<int, int>(sandPosition.Key + 1, sandPosition.Value + 1);

                if (CanGoThere(possibleDestinationDown, map))
                {
                    sandPosition = possibleDestinationDown;
                }
                else if (CanGoThere(possibleDestinationDownLeft, map))
                {
                    sandPosition = possibleDestinationDownLeft;
                }
                else if (CanGoThere(possibleDestinationDownRight, map))
                {
                    sandPosition = possibleDestinationDownRight;
                }
                else
                {
                    isRested = true;
                }
            }
            map.Add(sandPosition);
            if (sandPosition.Key == 500 && sandPosition.Value == 0)
            {
                return false;
            }
            return true;
        }

        private static void Print(List<KeyValuePair<int, int>> map, int leftest, int rightest, int deepest)
        {
            for (int j = 0; j <= deepest; j++)
            {
                Console.Write("||");
                for (int i = leftest; i <= rightest; i++)
                {
                    if (i == 500 && j == 0)
                    {
                        Console.Write("S");
                    }
                    if (map.Any(m => m.Key == i && m.Value == j))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("||\n");
            }
            Console.WriteLine("=================================================================================================");
        }

        private static bool CanGoThere(KeyValuePair<int, int> dest, List<KeyValuePair<int, int>> map)
        {
            return !map.Contains(dest);
        }
    }
}
