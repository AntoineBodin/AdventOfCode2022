using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day15
    {
        public static int FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day15\\input.txt";
            string test = @"Sensor at x=2, y=18: closest beacon is at x=-2, y=15
Sensor at x=9, y=16: closest beacon is at x=10, y=16
Sensor at x=13, y=2: closest beacon is at x=15, y=3
Sensor at x=12, y=14: closest beacon is at x=10, y=16
Sensor at x=10, y=20: closest beacon is at x=10, y=16
Sensor at x=14, y=17: closest beacon is at x=10, y=16
Sensor at x=8, y=7: closest beacon is at x=2, y=10
Sensor at x=2, y=0: closest beacon is at x=2, y=10
Sensor at x=0, y=11: closest beacon is at x=2, y=10
Sensor at x=20, y=14: closest beacon is at x=25, y=17
Sensor at x=17, y=20: closest beacon is at x=21, y=22
Sensor at x=16, y=7: closest beacon is at x=15, y=3
Sensor at x=14, y=3: closest beacon is at x=15, y=3
Sensor at x=20, y=1: closest beacon is at x=15, y=3";

            byte[] byteArray = Encoding.ASCII.GetBytes(test);
            MemoryStream stream = new MemoryStream(byteArray);

            using StreamReader reader = new StreamReader(inputFilePath);
            string line;
            int total = 0;
            List<KeyValuePair<int, int>> map = new List<KeyValuePair<int, int>>();
            List<KeyValuePair<int, int>> beaconsOnLine = new List<KeyValuePair<int, int>>();
            int lineNumber = 10;
            while ((line = reader.ReadLine()) != null)
            {
                string[] tokens = line.Split(":");
                string sensorPosition = tokens[0][10..];
                string beaconPosition = tokens[1][22..];
                string[] sensorCoords = sensorPosition.Split(", ");
                string[] beaconCoords = beaconPosition.Split(", ");
                KeyValuePair<int, int> sensor = new KeyValuePair<int, int>(int.Parse(sensorCoords[0].Substring(2)), int.Parse(sensorCoords[1].Substring(2)));
                KeyValuePair<int, int> beacon = new KeyValuePair<int, int>(int.Parse(beaconCoords[0].Substring(2)), int.Parse(beaconCoords[1].Substring(2)));

                AddIfOnLine(beaconsOnLine, beacon, lineNumber);

                int manhattanDistanceBeacon = ManhattanDistance(sensor, beacon);
                int manhattanDistanceLine = sensor.Value - lineNumber;
                if (manhattanDistanceLine < 0)
                    manhattanDistanceLine *= -1;

                int value = manhattanDistanceBeacon - manhattanDistanceLine;

                for (int x = sensor.Key - value; x <= sensor.Key + value; x++)
                {
                    map.Add(new KeyValuePair<int, int>(x, lineNumber));
                }

            }
            return map.Distinct().Count() - beaconsOnLine.Distinct().Count();
        }
        public static int SecondStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day15\\input.txt";
            string test = @"Sensor at x=2, y=18: closest beacon is at x=-2, y=15
Sensor at x=9, y=16: closest beacon is at x=10, y=16
Sensor at x=13, y=2: closest beacon is at x=15, y=3
Sensor at x=12, y=14: closest beacon is at x=10, y=16
Sensor at x=10, y=20: closest beacon is at x=10, y=16
Sensor at x=14, y=17: closest beacon is at x=10, y=16
Sensor at x=8, y=7: closest beacon is at x=2, y=10
Sensor at x=2, y=0: closest beacon is at x=2, y=10
Sensor at x=0, y=11: closest beacon is at x=2, y=10
Sensor at x=20, y=14: closest beacon is at x=25, y=17
Sensor at x=17, y=20: closest beacon is at x=21, y=22
Sensor at x=16, y=7: closest beacon is at x=15, y=3
Sensor at x=14, y=3: closest beacon is at x=15, y=3
Sensor at x=20, y=1: closest beacon is at x=15, y=3";

            byte[] byteArray = Encoding.ASCII.GetBytes(test);
            MemoryStream stream = new MemoryStream(byteArray);

            using StreamReader reader = new StreamReader(inputFilePath);
            string line;
            int total = 0;
            List<KeyValuePair<int, int>> beacons = new List<KeyValuePair<int, int>>();
            List<KeyValuePair<int, int>> sensors = new List<KeyValuePair<int, int>>();
            while ((line = reader.ReadLine()) != null)
            {
                string[] tokens = line.Split(":");
                string sensorPosition = tokens[0][10..];
                string beaconPosition = tokens[1][22..];
                string[] sensorCoords = sensorPosition.Split(", ");
                string[] beaconCoords = beaconPosition.Split(", ");
                KeyValuePair<int, int> sensor = new KeyValuePair<int, int>(int.Parse(sensorCoords[0].Substring(2)), int.Parse(sensorCoords[1].Substring(2)));
                KeyValuePair<int, int> beacon = new KeyValuePair<int, int>(int.Parse(beaconCoords[0].Substring(2)), int.Parse(beaconCoords[1].Substring(2)));

                beacons.Add(beacon);
                sensors.Add(sensor);
            }
            Dictionary<int, List<KeyValuePair<int, int>>> rangesPerLine = new Dictionary<int, List<KeyValuePair<int, int>>>();

            List<int> map = new List<int>();
            for (int i = 0; i < beacons.Count; i++)
            {
                KeyValuePair<int, int> beacon = beacons[i];
                KeyValuePair<int, int> sensor = sensors[i];
                int manhattanDistanceBeacon = ManhattanDistance(sensor, beacon);
                int lineStart = sensor.Value - manhattanDistanceBeacon;
                if (lineStart < 0)
                {
                    lineStart = 0;
                }
                int lineEnd = sensor.Value + manhattanDistanceBeacon;
                if (lineEnd > 4000000)
                {
                    lineEnd = 4000000;
                };
                for (int lineNumber = lineStart; lineNumber < lineEnd; lineNumber++)
                {
                    int manhattanDistanceLine = sensor.Value - lineNumber;
                    if (manhattanDistanceLine < 0)
                        manhattanDistanceLine *= -1;

                    int value = manhattanDistanceBeacon - manhattanDistanceLine;
                    int startX = sensor.Key - value;
                    if (startX < 0)
                    {
                        startX = 0;
                    }
                    int endX = sensor.Key + value;
                    if (endX > 4000000)
                    {
                        endX = 4000000;
                    }
                    AddOrUpdate(rangesPerLine, lineNumber, startX, endX);
                }
            }
            foreach (var rangesLine in rangesPerLine)
            {
                var ranges = rangesLine.Value;
                ranges.Sort(Compare);
                Merge(ranges);
                if (ranges.Count > 1)
                {
                    int value = 4000000 * (ranges[0].Value + 1);
                    return rangesLine.Key + value;
                }
            }

            if (map.Distinct().Count() != 4000000)
            {
                return 0;
            }


            return 1;
        }

        private static void Merge(List<KeyValuePair<int, int>> ranges)
        {
            int i = 0;
            int count = ranges.Count;
            while (i < count - 1)
            {
                KeyValuePair<int, int> range = ranges[i];
                KeyValuePair<int, int> nextRange = ranges[i + 1];
                if (nextRange.Key > range.Value)
                {
                    i++;
                }
                else if (range.Key <= nextRange.Key && range.Value >= nextRange.Value)
                {
                    ranges.RemoveAt(i + 1);
                }
                else if (nextRange.Key <= range.Value && nextRange.Value > range.Value)
                {
                    ranges[i] = new KeyValuePair<int, int>(range.Key, nextRange.Value);
                    ranges.RemoveAt(i + 1);
                }
                count = ranges.Count;
            }
        }

        private static int Compare(KeyValuePair<int, int> element1, KeyValuePair<int, int> element2)
        {
            if (element1.Key < element2.Key)
            {
                return -1;
            }
            else if (element1.Key > element2.Key)
            {
                return 1;
            }
            else
            {
                if (element1.Value < element2.Value)
                {
                    return -1;
                }
                else if (element1.Value > element2.Value)
                {
                    return 1;
                }
                return 0;
            }
        }

        private static void AddOrUpdate(Dictionary<int, List<KeyValuePair<int, int>>> dic, int lineNumber, int startX, int endX)
        {
            if (!dic.ContainsKey(lineNumber))
            {
                dic.Add(lineNumber, new List<KeyValuePair<int, int>> {
                    new KeyValuePair<int, int>(startX, endX)
                    });
            }
            else
            {
                dic[lineNumber].Add(new KeyValuePair<int, int>(startX, endX));
                /*List<KeyValuePair<int, int>> ranges = dic[lineNumber];
                for (int i = 0; i < ranges.Count - 1; i++)
                {
                    KeyValuePair<int, int> range = ranges[i];
                    KeyValuePair<int, int> nextRange = ranges[i + 1];
                    if (startX >= range.Key && endX <= range.Value)
                    {
                        return;
                    }
                    if (startX <= range.Value && endX >= nextRange.Key)
                    {
                        ranges[i] = new KeyValuePair<int, int>(range.Key, nextRange.Value);
                        ranges.RemoveAt(i+1);
                        return;
                    }
                }*/

            }
        }

        private static void AddIfOnLine(List<KeyValuePair<int, int>> map, KeyValuePair<int, int> element, int lineNumber)
        {
            if (element.Value == lineNumber)
            {
                map.Add(element);
            }
        }

        private static int ManhattanDistance(KeyValuePair<int, int> firstElement, KeyValuePair<int, int> secondElement)
        {
            int xDistance = firstElement.Key - secondElement.Key;
            int yDistance = firstElement.Value - secondElement.Value;

            if (xDistance < 0)
            {
                xDistance *= -1;
            }
            if (yDistance < 0)
            {
                yDistance *= -1;
            }

            return xDistance + yDistance;
        }
    }
}
