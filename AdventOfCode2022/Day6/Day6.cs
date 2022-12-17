using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2022
{
    public class Day6
    {
        public static int FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day6\\input.txt";

            using StreamReader reader = new StreamReader(inputFilePath);

            int result = 0;
            int c;
            List<char> chars = new List<char>();
            while ((c = reader.Read()) != -1)
            {
                result++;
                char letter = (char)c;
                if (chars.Contains(letter))
                {
                    int indexOfSameLetter = 0;
                    foreach (char ch in chars)
                    {
                        if (ch == letter)
                        {
                            break;
                        }
                        indexOfSameLetter++;
                    }
                    chars.RemoveRange(0, indexOfSameLetter + 1);
                }
                if (chars.Count == 3 && !chars.Contains(letter))
                {
                    return result;
                }
                chars.Add(letter);
            }
            return 0;
        }

        public static int SecondStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day6\\input.txt";

            using StreamReader reader = new StreamReader(inputFilePath);

            int result = 0;
            int c;
            List<char> chars = new List<char>();
            while ((c = reader.Read()) != -1)
            {
                result++;
                char letter = (char)c;
                if (chars.Contains(letter))
                {
                    int indexOfSameLetter = 0;
                    foreach (char ch in chars)
                    {
                        if (ch == letter)
                        {
                            break;
                        }
                        indexOfSameLetter++;
                    }
                    chars.RemoveRange(0, indexOfSameLetter + 1);
                }
                if (chars.Count == 3 && !chars.Contains(letter))
                {
                    return result;
                }
                chars.Add(letter);
            }
            return 0;
        }
    }
}
