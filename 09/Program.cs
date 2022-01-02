using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _09
{
    class Program
    {
        const string FILE = "input.txt";

        static void Main(string[] args)
        {
            //First();

            Second();
        }

        private static void Second()
        {
            var fileLines = File.ReadAllLines(FILE);

            var ySize = fileLines.Length;

            var m = fileLines.SelectMany(l => l.Select(c => int.Parse(c.ToString())).Append(9).Prepend(9).ToArray());

            var xSize = m.Count() / ySize;

            for (int i = 0; i < xSize; i++)
            {
                m = m.Prepend(9);
                m = m.Append(9);
            }

            var n = m.ToArray();

            ySize += 2;

            var lowest = new List<int>();

            for (int i = 1; i < ySize - 1; i++)
            {
                for (int j = 1; j < xSize - 1; j++)
                {
                    var c = n[i * xSize + j];
                    if (
                        c < n[(i - 1) * xSize + j] &&
                        c < n[(i + 1) * xSize + j] &&
                        c < n[i * xSize + (j - 1)] &&
                        c < n[i * xSize + (j + 1)]
                        )
                    {
                        lowest.Add(i * xSize + j);
                    }

                }
            }

            var basins = new Dictionary<int, List<int>>();

            foreach (var l in lowest)
            {
                var q = new List<int>();
                q.Add(l);

                var i = 0;

                while (i < q.Count)
                {
                    var cIndex = q.ElementAt(i);
                    var c = n[cIndex];

                    if (n[cIndex - 1] > c && n[cIndex - 1] != 9 && !q.Contains(cIndex - 1))
                        q.Add(cIndex - 1);

                    if (n[cIndex + 1] > c && n[cIndex + 1] != 9 && !q.Contains(cIndex + 1))
                        q.Add(cIndex + 1);

                    if (n[cIndex - xSize] > c && n[cIndex - xSize] != 9 && !q.Contains(cIndex - xSize))
                        q.Add(cIndex - xSize);

                    if (n[cIndex + xSize] > c && n[cIndex + xSize] != 9 && !q.Contains(cIndex + xSize))
                        q.Add(cIndex + xSize);

                    i++;
                }

                basins.Add(l, q);
            }

            for (int i = 1; i < ySize - 1; i++)
            {
                for (int j = 1; j < xSize - 1; j++)
                {
                    var c = n[i * xSize + j];

                    if (basins.Any(v => v.Value.Contains(i * xSize + j)))
                        Console.ForegroundColor = ConsoleColor.DarkYellow;

                    if (lowest.Contains(i * xSize + j))
                        Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write(c);

                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }

            var mul = basins.Values.Select(b => b.Count).OrderBy(b => b).TakeLast(3).Aggregate((sum, next) => sum * next);

            foreach(var s in basins.Values.Select(b => b.Count).OrderBy(b => b).TakeLast(3))
            {
                Console.WriteLine($"Basin size: {s}");
            }

            Console.WriteLine($"Multiplied size of largest basins: {mul}");
        }

        private static void First()
        {
            var fileLines = File.ReadAllLines(FILE);

            var ySize = fileLines.Length;

            var m = fileLines.SelectMany(l => l.Select(c => int.Parse(c.ToString())).Append(9).Prepend(9).ToArray());

            var xSize = m.Count() / ySize;

            for (int i = 0; i < xSize; i++)
            {
                m = m.Prepend(9);
                m = m.Append(9);
            }

            var n = m.ToArray();

            ySize += 2;

            var lowest = new List<int>();

            for (int i = 1; i < ySize - 1; i++)
            {
                for (int j = 1; j < xSize - 1; j++)
                {
                    var c = n[i * xSize + j];
                    if (
                        c < n[(i - 1) * xSize + j] &&
                        c < n[(i + 1) * xSize + j] &&
                        c < n[i * xSize + (j - 1)] &&
                        c < n[i * xSize + (j + 1)]
                        )
                    {
                        lowest.Add(i * xSize + j);
                    }

                }
            }

            for (int i = 1; i < ySize - 1; i++)
            {
                for (int j = 1; j < xSize - 1; j++)
                {
                    Console.ForegroundColor = ConsoleColor.White;

                    var c = n[i * xSize + j];
                    if (lowest.Contains(i * xSize + j))
                        Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(c);
                }
                Console.WriteLine();
            }

            var sum = 0;
            foreach (var l in lowest)
                sum += n[l] + 1;

            Console.WriteLine($"Sum of risks: {sum}");

        }
    }
}
