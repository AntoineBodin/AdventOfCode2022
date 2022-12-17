using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2022
{
    public class Day8
    {
        public static int FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day8\\input.txt";

            using StreamReader reader = new StreamReader(inputFilePath);
            string line;

            int total = 0;

            List<List<int>> matrix = new List<List<int>>();
            while ((line = reader.ReadLine()) != null)
            {
                List<int> currentList = new List<int>();
                foreach (char c in line)
                {
                    currentList.Add(int.Parse(c.ToString()));
                }
                matrix.Add(currentList);
            }

            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0 ; j < matrix[0].Count; j++)
                {
                    if (IsVisible(matrix, i, j))
                    {
                        total++;
                    }
                }
            }

            return total;
        }

        public static int SecondStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day8\\input.txt";

            using StreamReader reader = new StreamReader(inputFilePath);
            string line;

            int total = 0;

            List<List<int>> matrix = new List<List<int>>();
            while ((line = reader.ReadLine()) != null)
            {
                List<int> currentList = new List<int>();
                foreach (char c in line)
                {
                    currentList.Add(int.Parse(c.ToString()));
                }
                matrix.Add(currentList);
            }

            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[0].Count; j++)
                {
                    int score = ScenicScore(matrix, i, j);
                    if (score > total)
                    {
                        total = score;
                    }
                }
            }

            return total;
        }

        private static int ScenicScore(List<List<int>> matrix, int i, int j)
        {
            int distanceTop = DistanceTop(matrix, i, j);
            int distanceBottom = DistanceBottom(matrix, i, j);
            int distanceRight = DistanceRight(matrix[i], j);
            int distanceLeft = DistanceLeft(matrix[i], j);
            return  distanceTop * distanceBottom * distanceRight * distanceLeft;
        }

        private static int DistanceTop(List<List<int>> matrix, int i, int j)
        {
            int value = matrix[i][j];
            int distance = 0;
            for (int k = i - 1; k >= 0; k--)
            {
                distance++;
                if (matrix[k][j] >= value)
                {
                    return distance;
                }
            }
            return distance;
        }

        private static int DistanceBottom(List<List<int>> matrix, int i, int j)
        {
            int value = matrix[i][j];
            int distance = 0;
            for (int k = i + 1; k < matrix.Count; k++)
            {
                distance++;
                if (matrix[k][j] >= value)
                {
                    return distance;
                }
            }
            return distance;
        }

        private static int DistanceRight(List<int> line, int j)
        {
            int value = line[j];
            int distance = 0;
            for (int k = j + 1; k < line.Count; k++)
            {
                distance++;
                if (line[k] >= value)
                {
                    return distance;
                }
            }
            return distance;
        }

        private static int DistanceLeft(List<int> line, int j)
        {
            int value = line[j];
            int distance = 0;
            for (int k = j - 1; k >= 0; k--)
            {
                distance++;
                if (line[k] >= value)
                {
                    return distance;
                }
            }
            return distance;
        }

        private static bool IsVisible(List<List<int>> matrix, int i, int j)
        {
            if (i == 0 || j == 0 || i == matrix.Count - 1 || j == matrix[0].Count)
            {
                return true;
            }
            return IsVisibleTop(matrix, i, j) || IsVisibleBottom(matrix, i, j) || IsVisibleRight(matrix[i], j) || IsVisibleLeft(matrix[i], j); 
        }

        private static bool IsVisibleLeft(List<int> line, int j)
        {
            int value = line[j];
            for (int k = 0; k < j; k++)
            {
                if (line[k] >= value)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsVisibleRight(List<int> line, int j)
        {
            int value = line[j];
            for (int k = j + 1; k < line.Count; k++)
            {
                if (line[k] >= value)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsVisibleBottom(List<List<int>> matrix, int i, int j)
        {
            int value = matrix[i][j];
            for (int k = i + 1; k < matrix.Count; k++)
            {
                if (matrix[k][j] >= value)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsVisibleTop(List<List<int>> matrix, int i, int j)
        {
            int value = matrix[i][j];
            for (int k = 0; k < i; k++)
            {
                if (matrix[k][j] >= value)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
