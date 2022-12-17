using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day9
    {
        public static int FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day9\\input.txt";

            using StreamReader reader = new StreamReader(inputFilePath);
            string line;

            List<KeyValuePair<int, int>> matrix = new List<KeyValuePair<int, int>>()
            {
                new KeyValuePair<int, int>(0, 0)
            };
            KeyValuePair<int, int> head = new KeyValuePair<int, int>(0, 0);
            KeyValuePair<int, int> tail = new KeyValuePair<int, int>(0, 0);
            while ((line = reader.ReadLine()) != null)
            {
                string[] tokens = line.Split(" ");
                string direction = tokens[0];
                int count = int.Parse(tokens[1]);
                for (int i = 0; i < count; i++)
                {
                    switch (direction)
                    {
                        case "U":
                            {
                                head = MoveUp(head);
                                break;
                            }
                        case "D":
                            {
                                head = MoveDown(head);
                                break;
                            }
                        case "L":
                            {
                                head = MoveLeft(head);
                                break;
                            }
                        case "R":
                            {
                                head = MoveRight(head);
                                break;
                            }
                    }
                    if (IsTooFar(head, tail))
                    {
                        tail = MoveTail(head, tail);
                        matrix.Add(tail);
                    }
                }
            }
            return matrix.Distinct().Count();
        }

        public static int SecondStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day9\\input.txt";

            using StreamReader reader = new StreamReader(inputFilePath);
            string line;

            List<KeyValuePair<int, int>> tailPositions = new List<KeyValuePair<int, int>>()
            {
                new KeyValuePair<int, int>(0, 0)
            };

            KeyValuePair<int, int> head = new KeyValuePair<int, int>(0, 0);
            KeyValuePair<int, int> tail = new KeyValuePair<int, int>(0, 0);

            List<KeyValuePair<int, int>> knots = new List<KeyValuePair<int, int>>()
            {
                head,
                new KeyValuePair<int, int>(0, 0),
                new KeyValuePair<int, int>(0, 0),
                new KeyValuePair<int, int>(0, 0),
                new KeyValuePair<int, int>(0, 0),
                new KeyValuePair<int, int>(0, 0),
                new KeyValuePair<int, int>(0, 0),
                new KeyValuePair<int, int>(0, 0),
                new KeyValuePair<int, int>(0, 0),
                tail
            };

            while ((line = reader.ReadLine()) != null)
            {
                string[] tokens = line.Split(" ");
                string direction = tokens[0];
                int count = int.Parse(tokens[1]);
                for (int i = 0; i < count; i++)
                {
                    switch (direction)
                    {
                        case "U":
                            {
                                knots[0] = MoveUp(knots[0]);
                                break;
                            }
                        case "D":
                            {
                                knots[0] = MoveDown(knots[0]);
                                break;
                            }
                        case "L":
                            {
                                knots[0] = MoveLeft(knots[0]);
                                break;
                            }
                        case "R":
                            {
                                knots[0] = MoveRight(knots[0]);
                                break;
                            }
                    }
                    if (IsTooFar(knots[0], knots[1]))
                    {
                        MoveKnots(knots, tailPositions, 0);
                    }
                }
            }
            return tailPositions.Distinct().Count();
        }

        private static void MoveKnots(List<KeyValuePair<int, int>> knots, List<KeyValuePair<int, int>> tailPositions, int headIndex)
        {
            if (IsTooFar(knots[headIndex], knots[headIndex + 1]))
            {
                knots[headIndex + 1] = MoveTail(knots[headIndex], knots[headIndex + 1]);
                if (headIndex == 8)
                {
                    tailPositions.Add(knots[headIndex + 1]);
                }
                else
                {
                    MoveKnots(knots, tailPositions, headIndex + 1);
                }
            }
        }

        private static KeyValuePair<int, int> MoveTail(KeyValuePair<int, int> head, KeyValuePair<int, int> tail)
        {
            int verticalDistance = head.Key - tail.Key;
            int horizontalDistance = head.Value - tail.Value;
            //Right
            if (verticalDistance == 0 && horizontalDistance == 2)
            {
                tail = MoveRight(tail);
            }
            //Left
            else if (verticalDistance == 0 && horizontalDistance == -2)
            {
                tail = MoveLeft(tail);
            }
            //Up
            else if (verticalDistance == 2 && horizontalDistance == 0)
            {
                tail = MoveUp(tail);
            }
            //Down
            else if (verticalDistance == -2 && horizontalDistance == 0)
            {
                tail = MoveDown(tail);
            }
            //UpRight
            else if (verticalDistance > 0 && horizontalDistance > 0)
            {
                tail = MoveUpRight(tail);
            }
            //UpLeft
            else if (verticalDistance > 0 && horizontalDistance < 0)
            {
                tail = MoveUpLeft(tail);
            }
            //DownRight
            else if (verticalDistance < 0 && horizontalDistance > 0)
            {
                tail = MoveDownRight(tail);
            }
            //DownLeft
            else if (verticalDistance < 0 && horizontalDistance < 0)
            {
                tail = MoveDownLeft(tail);
            }
            else
            {
                throw new Exception("hum hum");
            }
            return tail;
        }

        private static bool IsTooFar(KeyValuePair<int, int> head, KeyValuePair<int, int> tail)
        {
            int verticalDistance = head.Key - tail.Key;
            int horizontalDistance = head.Value - tail.Value;
            return verticalDistance > 1 || verticalDistance < -1 || horizontalDistance > 1 || horizontalDistance < -1;
        }

        private static KeyValuePair<int, int> MoveUp(KeyValuePair<int, int> head)
        {
            return new KeyValuePair<int, int>(head.Key + 1, head.Value);
        }

        private static KeyValuePair<int, int> MoveDown(KeyValuePair<int, int> head)
        {
            return new KeyValuePair<int, int>(head.Key - 1, head.Value);
        }

        private static KeyValuePair<int, int> MoveRight(KeyValuePair<int, int> head)
        {
            return new KeyValuePair<int, int>(head.Key, head.Value + 1);
        }

        private static KeyValuePair<int, int> MoveLeft(KeyValuePair<int, int> head)
        {
            return new KeyValuePair<int, int>(head.Key, head.Value - 1);
        }

        private static KeyValuePair<int, int> MoveUpLeft(KeyValuePair<int, int> head)
        {
            return new KeyValuePair<int, int>(head.Key + 1, head.Value - 1);
        }

        private static KeyValuePair<int, int> MoveUpRight(KeyValuePair<int, int> head)
        {
            return new KeyValuePair<int, int>(head.Key + 1, head.Value + 1);
        }

        private static KeyValuePair<int, int> MoveDownLeft(KeyValuePair<int, int> head)
        {
            return new KeyValuePair<int, int>(head.Key - 1, head.Value - 1);
        }

        private static KeyValuePair<int, int> MoveDownRight(KeyValuePair<int, int> head)
        {
            return new KeyValuePair<int, int>(head.Key - 1, head.Value + 1);
        }
    }
}
