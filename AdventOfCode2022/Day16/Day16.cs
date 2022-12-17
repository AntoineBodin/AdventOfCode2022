using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day16
    {
        public static int FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day15\\input.txt";
            string test = @"Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
Valve BB has flow rate=13; tunnels lead to valves CC, AA
Valve CC has flow rate=2; tunnels lead to valves DD, BB
Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE
Valve EE has flow rate=3; tunnels lead to valves FF, DD
Valve FF has flow rate=0; tunnels lead to valves EE, GG
Valve GG has flow rate=0; tunnels lead to valves FF, HH
Valve HH has flow rate=22; tunnel leads to valve GG
Valve II has flow rate=0; tunnels lead to valves AA, JJ
Valve JJ has flow rate=21; tunnel leads to valve II";

            byte[] byteArray = Encoding.ASCII.GetBytes(test);
            MemoryStream stream = new MemoryStream(byteArray);

            using StreamReader reader = new StreamReader(stream);
            string line;
            int total = 0;
            Dictionary<string, Valve> valves = new Dictionary<string, Valve>();
            while ((line = reader.ReadLine()) != null)
            {
                string[] tokens = line.Split(" ");
                string name = tokens[1];
                int pressure = int.Parse(tokens[4][5..^1]);
                string[] tunnels = tokens[9..].Select(tunnel => tunnel[0..2]).ToArray();
                Valve newVale = new Valve(name, pressure, tunnels);
                valves[name] = newVale;
            }
            return total;
        }

        private void CalculateShortestPath(Dictionary<string, Valve> valves)
        {
            foreach (KeyValuePair<string, Valve> valve in valves)
            {
                valve.Value.shortestPath[valve.Key] = 0;
                SpToTarget(valves, valve.Value, valve.Key);
            }
        }

        private void SpToTarget(Dictionary<string, Valve> valves, Valve valve, string name)
        {
            HashSet<string> visited = new HashSet<string>();

            while (valve != null && visited.Count < valves.Count)
            {
                visited.Add(name);
                int distance = valve.shortestPath[name] + 1;
                foreach (string tunnel in valve.ConnectedTunnels)
                {
                    Valve v = valves[tunnel];
                    if (!visited.Contains(tunnel))
                    {
                        if (distance < v.shortestPath[name])
                        {
                            v.shortestPath[name] = distance;
                        }
                    }
                    else
                    {
                        v.shortestPath[name] = distance;
                    }
                }
            }
        }
    }
}
