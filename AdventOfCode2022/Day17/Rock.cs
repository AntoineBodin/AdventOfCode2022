using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Rock
    {
        public List<KeyValuePair<long, int>> Elements { get; set; }

        public Rock(List<KeyValuePair<long, int>> elements)
        {
            Elements = elements;
        }

        public static Rock MinusShape(long startPointHeight)
        {
            List<KeyValuePair<long, int>> elements = new List<KeyValuePair<long, int>>
            {
                new KeyValuePair<long, int>(startPointHeight, 2),
                new KeyValuePair<long, int>(startPointHeight, 3),
                new KeyValuePair<long, int>(startPointHeight, 4),
                new KeyValuePair<long, int>(startPointHeight, 5),
            };

            return new Rock(elements);
        }

        public static Rock PlusShape(long startPointHeight)
        {
            List<KeyValuePair<long, int>> elements = new List<KeyValuePair<long, int>>
            {
                new KeyValuePair<long, int>(startPointHeight + 2, 3),
                new KeyValuePair<long, int>(startPointHeight + 1, 2),
                new KeyValuePair<long, int>(startPointHeight + 1, 3),
                new KeyValuePair<long, int>(startPointHeight + 1, 4),
                new KeyValuePair<long, int>(startPointHeight, 3),
            };

            return new Rock(elements);
        }

        public static Rock LShape(long startPointHeight)
        {
            List<KeyValuePair<long, int>> elements = new List<KeyValuePair<long, int>>
            {
                new KeyValuePair<long, int>(startPointHeight + 2, 4),
                new KeyValuePair<long, int>(startPointHeight + 1, 4),
                new KeyValuePair<long, int>(startPointHeight, 4),
                new KeyValuePair<long, int>(startPointHeight, 3),
                new KeyValuePair<long, int>(startPointHeight, 2),
            };

            return new Rock(elements);
        }

        public static Rock IShape(long startPointHeight)
        {
            List<KeyValuePair<long, int>> elements = new List<KeyValuePair<long, int>>
            {
                new KeyValuePair<long, int>(startPointHeight + 3, 2),
                new KeyValuePair<long, int>(startPointHeight + 2, 2),
                new KeyValuePair<long, int>(startPointHeight + 1, 2),
                new KeyValuePair<long, int>(startPointHeight, 2),
            };

            return new Rock(elements);
        }

        public static Rock SquareShape(long startPointHeight)
        {
            List<KeyValuePair<long, int>> elements = new List<KeyValuePair<long, int>>
            {
                new KeyValuePair<long, int>(startPointHeight, 2),
                new KeyValuePair<long, int>(startPointHeight, 3),
                new KeyValuePair<long, int>(startPointHeight + 1, 2),
                new KeyValuePair<long, int>(startPointHeight + 1, 3),
            };

            return new Rock(elements);
        }

        internal void MoveRight(List<long> map)
        {
            foreach (var element in Elements)
            {
                KeyValuePair<long, int> newPosition = new KeyValuePair<long, int>(element.Key, element.Value + 1);
                if (newPosition.Value > 6 || map[newPosition.Value] >= newPosition.Key)
                {
                    return;
                }
            }
            for (int i = 0; i < Elements.Count; i++)
            {
                KeyValuePair<long, int> newPosition = new KeyValuePair<long, int>(Elements[i].Key, Elements[i].Value + 1);
                Elements[i] = newPosition;
            }
        }

        internal void MoveLeft(List<long> map)
        {
            foreach (var element in Elements)
            {
                KeyValuePair<long, int> newPosition = new KeyValuePair<long, int>(element.Key, element.Value - 1);
                if (newPosition.Value < 0 || map[newPosition.Value] >= newPosition.Key)
                {
                    return;
                }
            }
            for (int i = 0; i < Elements.Count; i++)
            {
                KeyValuePair<long, int> newPosition = new KeyValuePair<long, int>(Elements[i].Key, Elements[i].Value - 1);
                Elements[i] = newPosition;
            }
        }

        internal bool MoveDown(List<long> map)
        {
            foreach (var element in Elements)
            {
                KeyValuePair<long, int> newPosition = new KeyValuePair<long, int>(element.Key - 1, element.Value);
                if (map[newPosition.Value] >= newPosition.Key || newPosition.Key < 1)
                {
                    return false;
                }
            }
            for (int i = 0; i < Elements.Count; i++)
            {
                KeyValuePair<long, int> newPosition = new KeyValuePair<long, int>(Elements[i].Key - 1, Elements[i].Value);
                Elements[i] = newPosition;
            }
            return true;
        }
    }
}
