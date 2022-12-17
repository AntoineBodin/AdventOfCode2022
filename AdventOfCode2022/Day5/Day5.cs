using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day5
    {
        public static string FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day5\\input.txt";
            using StreamReader reader = new StreamReader(inputFilePath);
            string line;

            string result = "";

            // read stacks
            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
                Console.WriteLine(line);
            }
            List<List<char>> stacks = new List<List<char>>() {
                new List<char>
                {
                    '0'
                },
                new List<char>
                {
                    'W', 'B', 'D', 'N', 'C', 'F', 'J'
                },
                new List<char>
                {
                    'P', 'Z', 'V', 'Q', 'L', 'S', 'T'
                },
                new List<char>
                {
                    'P', 'Z', 'B', 'G', 'J', 'T'
                },
                new List<char>
                {
                    'D', 'T', 'L', 'J', 'Z', 'B', 'H', 'C'
                },
                new List<char>
                {
                    'G', 'V', 'B', 'J', 'S'
                },
                new List<char>
                {
                    'P', 'S', 'Q'
                },
                new List<char>
                {
                    'B', 'V', 'D', 'F', 'L', 'M', 'P', 'N'
                },
                new List<char>
                {
                    'P', 'S', 'M', 'F', 'B', 'D', 'L', 'R'
                },
                new List<char>
                {
                    'V', 'D', 'T', 'R'
                }
            };
            //read commands
            while ((line = reader.ReadLine()) != null)
            {
                string[] commands = line.Split(' ');
                int nbOfMoves = int.Parse(commands[1]);
                int source = int.Parse(commands[3]);
                int destination = int.Parse(commands[5]);

                for (int i = 0; i < nbOfMoves; i++)
                {
                    List<char> sourceStack = stacks[source];
                    List<char> destinationStack = stacks[destination];
                    char popped = sourceStack.Last();
                    sourceStack.RemoveAt(sourceStack.Count - 1);
                    destinationStack.Add(popped);
                }
            }
            foreach (List<char> stack in stacks)
            {
                result += stack.Last();
            }
            return result;
        }

        public static string SecondStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day5\\input.txt";
            using StreamReader reader = new StreamReader(inputFilePath);
            string line;

            string result = "";

            // read stacks
            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
                Console.WriteLine(line);
            }
            List<List<char>> stacks = new List<List<char>>() {
                new List<char>
                {
                    '0'
                },
                new List<char>
                {
                    'W', 'B', 'D', 'N', 'C', 'F', 'J'
                },
                new List<char>
                {
                    'P', 'Z', 'V', 'Q', 'L', 'S', 'T'
                },
                new List<char>
                {
                    'P', 'Z', 'B', 'G', 'J', 'T'
                },
                new List<char>
                {
                    'D', 'T', 'L', 'J', 'Z', 'B', 'H', 'C'
                },
                new List<char>
                {
                    'G', 'V', 'B', 'J', 'S'
                },
                new List<char>
                {
                    'P', 'S', 'Q'
                },
                new List<char>
                {
                    'B', 'V', 'D', 'F', 'L', 'M', 'P', 'N'
                },
                new List<char>
                {
                    'P', 'S', 'M', 'F', 'B', 'D', 'L', 'R'
                },
                new List<char>
                {
                    'V', 'D', 'T', 'R'
                }
            };
            //read commands
            while ((line = reader.ReadLine()) != null)
            {
                string[] commands = line.Split(' ');
                int nbOfMoves = int.Parse(commands[1]);
                int source = int.Parse(commands[3]);
                int destination = int.Parse(commands[5]);

                List<char> sourceStack = stacks[source];
                List<char> destinationStack = stacks[destination];
                var popped = sourceStack.TakeLast(nbOfMoves).ToList();
                sourceStack.RemoveRange(sourceStack.Count - nbOfMoves, nbOfMoves);
                destinationStack.AddRange(popped);

            }
            foreach (List<char> stack in stacks)
            {
                result += stack.Last();
            }
            return result;
        }
    }
}
