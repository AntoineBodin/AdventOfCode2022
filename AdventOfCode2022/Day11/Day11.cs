using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day11
    {
        public static int FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day11\\input.txt";
            string test = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";
            byte[] byteArray = Encoding.ASCII.GetBytes(test);
            MemoryStream stream = new MemoryStream(byteArray);

            using StreamReader reader = new StreamReader(inputFilePath);
            string line;
            List<Monkey> monkeys = new List<Monkey>();
            while (!reader.EndOfStream)
            {
                monkeys.Add(new Monkey(reader));
            }
            for (int i = 0; i < 20; i++)
            {
                foreach (Monkey monkey in monkeys)
                {
                    monkey.PlayTurnFirstStep(monkeys);
                }
            }
            List<Monkey> topMonkeys = monkeys
                .OrderBy(m => m.InspectionsCount)
                .TakeLast(2)
                .ToList();
            return topMonkeys[0].InspectionsCount * topMonkeys[1].InspectionsCount;
        }

        public static int SecondStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day11\\input.txt";
            string test = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";
            byte[] byteArray = Encoding.ASCII.GetBytes(test);
            MemoryStream stream = new MemoryStream(byteArray);

            using StreamReader reader = new StreamReader(inputFilePath);
            string line;
            List<Monkey> monkeys = new List<Monkey>();
            while (!reader.EndOfStream)
            {
                monkeys.Add(new Monkey(reader));
            }
            int modAggregation = 1;
            foreach (Monkey monkey in monkeys)
            {
                modAggregation *= monkey.TestModuloValue;
            }
            for (int i = 0; i < 10000; i++)
            {
                foreach (Monkey monkey in monkeys)
                {
                    monkey.PlayTurnSecondStep(monkeys, modAggregation);
                }
            }
            List<Monkey> topMonkeys = monkeys
                .OrderBy(m => m.InspectionsCount)
                .TakeLast(2)
                .ToList();
            return topMonkeys[0].InspectionsCount * topMonkeys[1].InspectionsCount;
        }
    }
}
