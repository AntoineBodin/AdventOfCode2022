using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Offset
    {
        public Offset(int idx, int x, int y, int z)
        {
            Idx = idx;
            X = x;
            Y = y;
            Z = z;
        }
        public int Idx { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}
