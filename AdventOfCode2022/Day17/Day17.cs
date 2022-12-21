using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCode2022
{
    public class Day17
    {
        public static long FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day17\\input.txt";
            string test = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";

            byte[] byteArray = Encoding.ASCII.GetBytes(test);
            MemoryStream stream = new MemoryStream(byteArray);

            using StreamReader reader = new StreamReader(stream);

            int total = 0;
            List<long> map = new List<long>
            {
                0, 0, 0, 0, 0, 0, 0
            };
            string line = reader.ReadLine();
            int commandIndex = 0;
            int commandsCount = line.Length;
            long highestRock = 0;
            Func<long, Rock>[] rocks =
            {
                Rock.MinusShape,
                Rock.PlusShape,
                Rock.LShape,
                Rock.IShape,
                Rock.SquareShape
            };
            int rockNumber = rocks.Length;
            int loopLength = rockNumber * line.Length;
            long loopIterations = 1000000000000 / loopLength;
            long indexAfterLoopIterations = loopLength * loopIterations;
            for (long i = 0; i < loopLength; i++)
            {
                Rock rock = rocks[i % rockNumber].Invoke(highestRock + 4);
                bool isRested = false;
                while (!isRested)
                {
                    char command = line[commandIndex % commandsCount];
                    commandIndex++;
                    if (command == '<')
                    {
                        rock.MoveLeft(map);
                    }
                    if (command == '>')
                    {
                        rock.MoveRight(map);
                    }
                    isRested = !rock.MoveDown(map);
                }
                foreach(var element in rock.Elements)
                {
                    if (map[element.Value] < element.Key)
                    {
                        map[element.Value] = element.Key;
                        if (element.Key > highestRock)
                        {
                            highestRock = element.Key;
                        }
                    }
                }
            }
            highestRock *= loopIterations;
            for (long i = 0; i < 22; i++)
            {
                Rock rock = rocks[i % rockNumber].Invoke(highestRock + 4);
                bool isRested = false;
                while (!isRested)
                {
                    char command = line[commandIndex % commandsCount];
                    commandIndex++;
                    if (command == '<')
                    {
                        rock.MoveLeft(map);
                    }
                    if (command == '>')
                    {
                        rock.MoveRight(map);
                    }
                    isRested = !rock.MoveDown(map);
                }
                foreach (var element in rock.Elements)
                {
                    if (map[element.Value] < element.Key)
                    {
                        map[element.Value] = element.Key;
                        if (element.Key > highestRock)
                        {
                            highestRock = element.Key;
                        }
                    }
                }
            }
            return highestRock;
        }
    }
}
