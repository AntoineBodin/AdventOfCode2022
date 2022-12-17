using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Monkey
    {
        public List<long> Items { get; set; }
        public Func<long, long> Operation { get; set; }
        public Func<long, bool> Test { get; set; }
        public int TestModuloValue { get; set; }
        public int MonkeyTrue { get; set; }
        public int MonkeyFalse { get; set; }
        public int InspectionsCount { get; set; }

        public Monkey(StreamReader reader)
        {
            reader.ReadLine();
            string startingItemsLine = reader.ReadLine();
            List<long> items = startingItemsLine
                .Substring(18)
                .Split(", ")
                .Select(t => long.Parse(t))
                .ToList();

            string operationLine = reader.ReadLine();
            string[] operationMembers = operationLine.Substring(23).Split(" ");
            string operationSign = operationMembers[0];
            string operationValueString = operationMembers[1];
            int operationValue;
            bool isUsingOldValue = operationValueString == "old";
            if (isUsingOldValue)
            {
                operationValue = 0;
            }
            else
            {
                operationValue = int.Parse(operationMembers[1]);
            }
            Func<long, long> operation;
            if (operationSign == "*")
            {
                operation = ((t) => t * (isUsingOldValue ? t : operationValue));
            }
            else
            {
                operation = ((t) => t + (isUsingOldValue ? t : operationValue));
            }

            string testLine = reader.ReadLine();
            int divisibleTestValue = int.Parse(testLine.Substring(21));
            Func<long, bool> test = ((t) => t % divisibleTestValue == 0);

            string monkeyTrueLine = reader.ReadLine();
            int monkeyTrue = int.Parse(monkeyTrueLine.Substring(29));

            string monkeyFalseLine = reader.ReadLine();
            int monkeyFalse = int.Parse(monkeyFalseLine.Substring(30));

            reader.ReadLine();
            Items = items;
            Operation = operation;
            Test = test;
            MonkeyTrue = monkeyTrue;
            MonkeyFalse = monkeyFalse;
            InspectionsCount = 0;
            TestModuloValue = divisibleTestValue;
        }

        internal void PlayTurnFirstStep(List<Monkey> monkeys)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                InspectionsCount++;
                Items[i] = Operation(Items[i]);
                Items[i] /= 3;
                int destinationMonkey;
                if (Test(Items[i]))
                {
                    destinationMonkey = MonkeyTrue;
                }
                else
                {
                    destinationMonkey = MonkeyFalse;
                }
                monkeys[destinationMonkey].Items.Add(Items[i]);
            }
            Items = new List<long>();
        }
        internal void PlayTurnSecondStep(List<Monkey> monkeys, int modAggregation)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                InspectionsCount++;
                Items[i] = Operation(Items[i]);
                Items[i] = Items[i] % modAggregation;
                int destinationMonkey;
                if (Test(Items[i]))
                {
                    destinationMonkey = MonkeyTrue;
                }
                else
                {
                    destinationMonkey = MonkeyFalse;
                }
                monkeys[destinationMonkey].Items.Add(Items[i]);
            }
            Items = new List<long>();
        }
    }
}
