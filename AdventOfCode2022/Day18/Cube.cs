using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Cube
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Cube[] Neighbors { get; set; } = new Cube[6];

        Offset[] offsets = new Offset[]
        {
            new Offset(0, -1, 0, 0),
            new Offset(1, 1, 0, 0),
            new Offset(2, 0, -1, 0),
            new Offset(3, 0, 1, 0),
            new Offset(4, 0, 0, -1),
            new Offset(5, 0, 0, 1),
        };

        public bool InBounds(int min, int max)
        {
            return X >= min && X <= max && Y >= min && Y <= max && Z >= min && Z <= max;
        }

        public int Unconnected()
        {
            int sum = 0;
            foreach (Cube c in Neighbors)
            {
                if (c == null)
                {
                    sum++;
                }
            }
            return sum;
        }

        public void FindNeighbors(List<Cube> set)
        {
            foreach (Offset o in offsets)
            {
                int newX = X + o.X;
                int newY = Y + o.Y;
                int newZ = Z + o.Z;
                
                Cube? c = set.FirstOrDefault(c => c.X == newX && c.Y == newY && c.Z == newZ);

                if (c != null)
                {
                    Neighbors[o.Idx] = c;
                }
            }
        }
    }
}
