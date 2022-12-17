using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day13ListOfElements<T> : Day13Element where T : Day13Element
    {
        public List<T> Value { get; set; }
    }
}
