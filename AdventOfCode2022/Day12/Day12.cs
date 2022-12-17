using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day12
    {
        public static int FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day12\\input.txt";
            string test = @"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi";
            byte[] byteArray = Encoding.ASCII.GetBytes(test);
            MemoryStream stream = new MemoryStream(byteArray);

            using StreamReader reader = new StreamReader(inputFilePath);
            List<string> map = new List<string>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                map.Add(line);
            }


            Tile start = new Tile();
            Tile finish = new Tile();

            start.X = 20;
            start.Y = 0;
            start.Value = 'a';

            finish.X = map.FindIndex(x => x.Contains('E'));
            finish.Y = map[finish.X].IndexOf('E');
            finish.Value = 'z';

            start.SetDistance(finish.X, finish.Y);

            return GetSmallerPathLength(map, start, finish);
        }

        public static int SecondStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day12\\input.txt";
            string test = @"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi";
            byte[] byteArray = Encoding.ASCII.GetBytes(test);
            MemoryStream stream = new MemoryStream(byteArray);

            using StreamReader reader = new StreamReader(inputFilePath);
            List<string> map = new List<string>();
            string line;
            int counter = 0;
            List<KeyValuePair<int, int>> starts = new List<KeyValuePair<int, int>>();
            while ((line = reader.ReadLine()) != null)
            {
                map.Add(line);
                if (line.Contains('a') || line.Contains('S'))
                {
                    int yCounter = 0;
                    foreach (char c in line)
                    {
                        if (c == 'a' || c == 'S')
                        {
                            starts.Add(new KeyValuePair<int, int>(counter, yCounter));
                        }
                        yCounter++;
                    }
                }
                counter++;
            }
            Tile finish = new Tile();
            finish.X = map.FindIndex(x => x.Contains('E'));
            finish.Y = map[finish.X].IndexOf('E');
            finish.Value = 'z';

            int minDistance = int.MaxValue;

            foreach (KeyValuePair<int, int> startPoint in starts)
            {
                Tile start = new Tile();

                start.X = startPoint.Key;
                start.Y = startPoint.Value;
                start.Value = 'a';

                start.SetDistance(finish.X, finish.Y);

                int distance = GetSmallerPathLength(map, start, finish);
                if (distance < minDistance && distance != 0)
                {
                    Console.WriteLine("Smaller!");
                    minDistance = distance;
                }
            }
            return minDistance;
        }

        private static int GetSmallerPathLength(List<string> map, Tile start, Tile finish)
        {
            List<Tile> activeTiles = new List<Tile>();
            activeTiles.Add(start);
            List<Tile> visitedTiles = new List<Tile>();

            while (activeTiles.Any())
            {
                Tile checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == finish.X && checkTile.Y == finish.Y)
                {
                    Tile currentTile = checkTile;
                    int count = 0;
                    while (currentTile.Parent != null)
                    {
                        count++;
                        currentTile = currentTile.Parent;
                    }
                    Console.WriteLine(count);
                    //We can actually loop through the parents of each tile to find our exact path which we will show shortly. 
                    return count;
                }

                visitedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);

                List<Tile> walkableTiles = GetWalkableTiles(map, checkTile, finish);

                foreach (Tile walkableTile in walkableTiles)
                {
                    //We have already visited this tile so we don't need to do so again!
                    if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                    {
                        continue;
                    }
                    //It's already in the active list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 
                    if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                    {
                        Tile existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                        if (existingTile.CostDistance > checkTile.CostDistance)
                        {
                            activeTiles.Remove(existingTile);
                            activeTiles.Add(walkableTile);
                        }
                    }
                    else
                    {
                        //We've never seen this tile before so add it to the list. 
                        activeTiles.Add(walkableTile);
                    }
                }
            }

            Console.WriteLine("No Path Found!");

            return 0;
        }

        private static List<Tile> GetWalkableTiles(List<string> map, Tile currentTile, Tile targetTile)
        {
            var neighbourTiles = new List<Tile>()
    {
        new Tile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
        new Tile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
        new Tile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
        new Tile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
    };

            neighbourTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

            var maxY = map.First().Length - 1;
            var maxX = map.Count - 1;

            List<Tile> possibleTiles = neighbourTiles
                    .Where(tile => tile.X >= 0 && tile.X <= maxX)
                    .Where(tile => tile.Y >= 0 && tile.Y <= maxY)
                    .ToList();

            possibleTiles.ForEach(t => t.Value = map[t.X][t.Y]);

            return possibleTiles
                    .Where(tile => tile.Value == currentTile.Value + 1 || tile.Value <= currentTile.Value && tile.Value != 'E' || (currentTile.Value == 'z' || currentTile.Value == 'y') && tile.Value == 'E')
                    .ToList();

        }
    }
}
