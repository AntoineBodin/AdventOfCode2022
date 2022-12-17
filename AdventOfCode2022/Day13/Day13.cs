using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day13
    {
        public static int FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day13\\input.txt";
            string test = @"[1,1,3,1,1]
[1,1,5,1,1]

[[1],[2,3,4]]
[[1],4]

[9]
[[8,7,6]]

[[4,4],4,4]
[[4,4],4,4,4]

[7,7,7,7]
[7,7,7]

[]
[3]

[[[]]]
[[]]

[1,[2,[3,[4,[5,6,7]]]],8,9]
[1,[2,[3,[4,[5,6,0]]]],8,9]";

            byte[] byteArray = Encoding.ASCII.GetBytes(test);
            MemoryStream stream = new MemoryStream(byteArray);

            using StreamReader reader = new StreamReader(inputFilePath);
            string line;
            int total = 0;
            int counter = 1;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    line = reader.ReadLine();
                }
                string firstLine = line;
                string secondLine = reader.ReadLine();

                Day13ListOfElements<Day13Element> firstElement = CreateElement(firstLine.Substring(1, firstLine.Length - 2));
                Day13ListOfElements<Day13Element> secondElement = CreateElement(secondLine.Substring(1, secondLine.Length - 2));

                if (CompareElements(firstElement, secondElement) == 1)
                {
                    total += counter;
                }
                counter++;
            }
            return total;
        }

        public static int SecondStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day13\\input.txt";
            string test = @"[1,1,3,1,1]
[1,1,5,1,1]

[[1],[2,3,4]]
[[1],4]

[9]
[[8,7,6]]

[[4,4],4,4]
[[4,4],4,4,4]

[7,7,7,7]
[7,7,7]

[]
[3]

[[[]]]
[[]]

[1,[2,[3,[4,[5,6,7]]]],8,9]
[1,[2,[3,[4,[5,6,0]]]],8,9]";

            byte[] byteArray = Encoding.ASCII.GetBytes(test);
            MemoryStream stream = new MemoryStream(byteArray);

            using StreamReader reader = new StreamReader(inputFilePath);
            string line;
            int total = 0;
            List<Day13ListOfElements<Day13Element>> inputList = new List<Day13ListOfElements<Day13Element>>();
            while ((line = reader.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    Day13ListOfElements<Day13Element> element = CreateElement(line.Substring(1, line.Length - 2));
                    inputList.Add(element);
                }
            }
            Day13ListOfElements<Day13Element> firstDivider = CreateElement("[2]");
            inputList.Add(firstDivider);
            Day13ListOfElements<Day13Element> secondDivider = CreateElement("[6]");
            inputList.Add(secondDivider);
            inputList.Sort(CompareElements);
            inputList.Reverse();

            int indexFirstDivider = inputList.IndexOf(firstDivider);
            int indexSecondDivider = inputList.IndexOf(secondDivider);

            return (indexFirstDivider + 1) * (indexSecondDivider + 1);
        }

        private static int CompareElements(Day13Element firstElement, Day13Element secondElement)
        {
            if (firstElement is Day13Int firstElementInt && secondElement is Day13Int secondElementInt)
            {
                return CompareElements(firstElementInt, secondElementInt);
            }
            else if (firstElement is Day13ListOfElements<Day13Int> firstList && secondElement is Day13ListOfElements<Day13Int> secondList)
            {
                return CompareElements(firstList, secondList);
            }
            else if (firstElement is Day13ListOfElements<Day13Element> firstList2 && secondElement is Day13Int secondElementInt2)
            {
                return CompareElements(firstList2, new Day13ListOfElements<Day13Int> { Value = new List<Day13Int> { secondElementInt2 } });
            }
            else if (firstElement is Day13Int firstElementInt2 && secondElement is Day13ListOfElements<Day13Element> secondList2)
            {
                return CompareElements(new Day13ListOfElements<Day13Int> { Value = new List<Day13Int> { firstElementInt2 } }, secondList2);
            }
            else if (firstElement is Day13ListOfElements<Day13Element> firstElementList && secondElement is Day13ListOfElements<Day13Element> secondElementList)
            {
                int firstListLen = firstElementList.Value.Count;
                int secondListLen = secondElementList.Value.Count;
                int counter = 0;
                while (counter < firstListLen && counter < secondListLen)
                {
                    int localResult = CompareElements(firstElementList.Value[counter], secondElementList.Value[counter]);
                    if (localResult != 0)
                    {
                        return localResult;
                    }
                    counter++;
                }
                if (secondListLen < firstListLen)
                {
                    return -1;
                }
                else if (secondListLen == firstListLen)
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
                return CompareElements(firstElement, secondElement);
            }
        }
        private static int CompareElements(Day13Int firstElement, Day13Int secondElement)
        {
            if (firstElement.Value < secondElement.Value)
            {
                return 1;
            }
            else if (firstElement.Value == secondElement.Value)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        private static int CompareElements(Day13ListOfElements<Day13Int> firstElement, Day13ListOfElements<Day13Int> secondElement)
        {
            int firstListLen = firstElement.Value.Count;
            int secondListLen = secondElement.Value.Count;
            int counter = 0;
            while (counter < firstListLen && counter < secondListLen)
            {
                if (secondElement.Value[counter].Value < firstElement.Value[counter].Value)
                {
                    return -1;
                }
                else if (secondElement.Value[counter].Value > firstElement.Value[counter].Value)
                {
                    return 1;
                }
            }
            if (secondListLen < firstListLen)
            {
                return -1;
            }
            else if (secondListLen == firstListLen)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        private static int CompareElements(Day13ListOfElements<Day13Element> firstElement, Day13ListOfElements<Day13Int> secondElement)
        {
            int firstListLen = firstElement.Value.Count;
            int secondListLen = secondElement.Value.Count;
            int counter = 0;
            while (counter < firstListLen && counter < secondListLen)
            {
                int localResult = CompareElements(firstElement.Value[counter], secondElement.Value[counter]);
                if (localResult != 0)
                {
                    return localResult;
                }
                counter++;
            }
            if (secondListLen < firstListLen)
            {
                return -1;
            }
            else if (secondListLen == firstListLen)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        private static int CompareElements(Day13ListOfElements<Day13Int> firstElement, Day13ListOfElements<Day13Element> secondElement)
        {
            int firstListLen = firstElement.Value.Count;
            int secondListLen = secondElement.Value.Count;
            int counter = 0;
            while (counter < firstListLen && counter < secondListLen)
            {
                int localResult = CompareElements(firstElement.Value[counter], secondElement.Value[counter]);
                if (localResult != 0)
                {
                    return localResult;
                }
                counter++;
            }
            if (secondListLen < firstListLen)
            {
                return -1;
            }
            else if (secondListLen == firstListLen)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private static Day13ListOfElements<Day13Element> CreateElement(string line)
        {
            Day13ListOfElements<Day13Element> result = new Day13ListOfElements<Day13Element>();
            result.Value = new List<Day13Element>();
            if (string.IsNullOrEmpty(line))
            {
                return result;
            }
            if (!line.Contains('['))
            {
                string[] intTokens = line.Split(",");
                if (intTokens.Length == 0)
                {
                }
                else if (intTokens.Length == 1)
                {
                    result.Value.Add(new Day13Int { Value = int.Parse(intTokens[0]) });
                }
                else
                {
                    foreach (string intToken in intTokens)
                    {
                        result.Value.Add(new Day13Int { Value = int.Parse(intToken) });
                    }
                }
                return result;
            }
            string currentIntToParse = "";
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '[')
                {
                    int squareBracketsCounter = 1;
                    int startBracketIndex = i;
                    int substringLength = 0;
                    while (squareBracketsCounter > 0)
                    {
                        i++;
                        c = line[i];
                        substringLength++;
                        if (c == '[')
                        {
                            squareBracketsCounter++;
                        }
                        else if (c == ']')
                        {
                            squareBracketsCounter--;
                        }
                    }
                    result.Value.Add(CreateElement(line.Substring(startBracketIndex + 1, substringLength - 1)));
                    i++;
                }
                else if (c == ',')
                {
                    if (!string.IsNullOrEmpty(currentIntToParse))
                    {
                        result.Value.Add(new Day13Int { Value = int.Parse(currentIntToParse) });
                        currentIntToParse = "";
                    }
                }
                else
                {
                    currentIntToParse += c;
                }
            }
            if (!string.IsNullOrEmpty(currentIntToParse))
            {
                result.Value.Add(new Day13Int { Value = int.Parse(currentIntToParse) });
            }
            return result;
        }
    }
}
