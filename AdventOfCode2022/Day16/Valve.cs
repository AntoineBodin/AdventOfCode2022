using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Valve
    {
        public Valve(string name, int pressure, string[] connectedTunnels)
        {
            Name = name;
            Pressure = pressure;
            ConnectedTunnels = connectedTunnels;
        }
        public string Name { get; set; }
        public int Pressure { get; set; }
        public string[] ConnectedTunnels { get; set; }

        public Dictionary<string, int> shortestPath = new Dictionary<string, int>();
    }
}
