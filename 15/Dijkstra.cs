using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15
{
    class Dijkstra
    {
        private readonly int[] matrix;
        private readonly int x;
        private readonly int y;

        public Dijkstra(int[] matrix, int x, int y)
        {
            this.matrix = matrix;
            this.x = x;
            this.y = y;
        }

        public (int[] distance, int[] prev) FindRoute(int source, int target)
        {
            var set = new List<int>();

            var distance = new int[x * y];
            var prev = new int[x * y];

            for (int i = 0; i < matrix.Length; i++)
            {
                distance[i] = int.MaxValue;
                prev[i] = -1;
                set.Add(i);
            }

            distance[source] = 0;

            int j = 0;

            while (set.Count > 0)
            {
                var current = set.OrderBy(s => distance[s]).First();
                Console.WriteLine($"Checking #{j++}/{x * y}, {current}");

                set.Remove(current);

                if (current == target)
                    break;

                var neighbors = GetNeighbors(current);
                foreach (var n in neighbors)
                {
                    var newDist = distance[current] + matrix[n];
                    if (newDist < distance[n])
                    {
                        distance[n] = newDist;
                        prev[n] = current;
                    }

                }
            }

            return (distance, prev);
        }

        public int[] GetNeighbors(int current)
        {
            var n = new List<int>();

            if (current > x - 1) n.Add(current - x);
            if (current < x * y - x) n.Add(current + x);
            if (current % x > 0) n.Add(current - 1);
            if (current % x < x - 1) n.Add(current + 1);

            return n.ToArray();
        }
    }
}
