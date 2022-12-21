using AdventOfCode2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day18
    {
        public static int FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day18\\input.txt";
            string test = @"2,2,2
1,2,2
3,2,2
2,1,2
2,3,2
2,2,1
2,2,3
2,2,4
2,2,6
1,2,5
3,2,5
2,1,5
2,3,5";

            byte[] byteArray = Encoding.ASCII.GetBytes(test);
            MemoryStream stream = new MemoryStream(byteArray);

            using StreamReader reader = new StreamReader(inputFilePath);

            List<Cube> cubes = new List<Cube>();

            int total = 0;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                int[] tokens = line.Split(',').Select(s => int.Parse(s)).ToArray();
                Cube newCube = new Cube { X = tokens[0], Y = tokens[1], Z = tokens[2] };
                cubes.Add(newCube);
            }
            foreach (Cube cube in cubes)
            {
                Cube topCube = new Cube { X = cube.X, Y = cube.Y, Z = cube.Z + 1 };
                Cube bottomCube = new Cube { X = cube.X, Y = cube.Y, Z = cube.Z - 1 };

                Cube leftCube = new Cube { X = cube.X + 1, Y = cube.Y, Z = cube.Z };
                Cube rightCube = new Cube { X = cube.X - 1, Y = cube.Y, Z = cube.Z };

                Cube frontCube = new Cube { X = cube.X, Y = cube.Y + 1, Z = cube.Z };
                Cube behindCube = new Cube { X = cube.X, Y = cube.Y - 1, Z = cube.Z };

                List<Cube> adjacentCubes = new List<Cube>
                {
                    topCube,
                    bottomCube,
                    leftCube,
                    rightCube,
                    frontCube,
                    behindCube
                };

                total += adjacentCubes.Select(c => cubes.Any(ic => c.X == ic.X && c.Y == ic.Y && c.Z == ic.Z) ? 0 : 1).Sum();
            }
            return total;
        }


        public static int SecondStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day18\\input.txt";
            string test = @"2,2,2
1,2,2
3,2,2
2,1,2
2,3,2
2,2,1
2,2,3
2,2,4
2,2,6
1,2,5
3,2,5
2,1,5
2,3,5";

            byte[] byteArray = Encoding.ASCII.GetBytes(test);
            MemoryStream stream = new MemoryStream(byteArray);

            using StreamReader reader = new StreamReader(inputFilePath);

            List<Cube> cubes = new List<Cube>();

            int total = 0;
            string line;
            List<Cube> set = new List<Cube>();
            while ((line = reader.ReadLine()) != null)
            {
                int[] tokens = line.Split(',').Select(s => int.Parse(s)).ToArray();
                Cube newCube = new Cube { X = tokens[0], Y = tokens[1], Z = tokens[2] };
                cubes.Add(newCube);
                set.Add(newCube);
            }
            int min = int.MaxValue;
            int max = int.MinValue;

            foreach (Cube c in cubes)
            {
                c.FindNeighbors(set);
                min = Math.Min(min, c.X);
                min = Math.Min(min, c.Y);
                min = Math.Min(min, c.Z);
                max = Math.Max(max, c.X);
                max = Math.Max(max, c.Y);
                max = Math.Max(max, c.Z);
            }
            min--;
            max++;

            // Part 1: count unconnected
            int pt1 = 0;
            foreach (Cube c in cubes)
            {
                pt1 += c.Unconnected();
            }

            // Part 2: dfs from min, min, min
            Cube start = new Cube();
            start.X = min;
            start.Y = min;
            start.Z = min;
            List<Cube> visited = new List<Cube>();
            LinkedList<Cube> queue = new LinkedList<Cube>();
            queue.AddFirst(start);
            int pt2 = 0;

            Offset[] offsets = new Offset[]
            {
                new Offset(0, -1, 0, 0),
                new Offset(1, 1, 0, 0),
                new Offset(2, 0, -1, 0),
                new Offset(3, 0, 1, 0),
                new Offset(4, 0, 0, -1),
                new Offset(5, 0, 0, 1),
            };

            while (queue.Count > 0)
            {
                Cube cur = queue.Last();
                queue.RemoveLast();
                if (visited.Any(c => c.X == cur.X && c.Y == cur.Y && c.Z == cur.Z))
                {
                    continue;
                }
                visited.Add(cur);
                foreach (Offset o in offsets)
                {
                    Cube tst = new Cube();
                    tst.X = cur.X + o.X;
                    tst.Y = cur.Y + o.Y;
                    tst.Z = cur.Z + o.Z;
                    if (!tst.InBounds(min, max))
                    {
                        continue;
                    }
                    if (set.Any(c => c.X == tst.X && c.Y == tst.Y && c.Z == tst.Z))
                    {
                        pt2++;
                    }
                    else if (!visited.Any(c => c.X == tst.X && c.Y == tst.Y && c.Z == tst.Z))
                    {
                        queue.AddFirst(tst);
                    }
                }
            }
            return pt2;
        }



        private static int SortingCubesMethod(Cube a, Cube b)
        {
            if (a.Z < b.Z)
            {
                return -1;
            }
            else if (a.Z == b.Z)
            {
                if (a.X < b.X)
                {
                    return -1;
                }
                else if (a.X == b.X)
                {
                    if (a.Y < b.Y)
                    {
                        return 1;
                    }
                    else if (a.Y == b.Y)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return 1;
            }
        }

        private static bool CubesAreEquals(Cube a, Cube b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }
    }
}
