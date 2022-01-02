using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _05
{
    class Program
    {
        const string FILE = "input.txt";

        static void Main(string[] args)
        {
            First();

            Second();
        }

        private static void Second()
        {
            var fileLines = File.ReadAllLines(FILE).ToList();

            var lines = fileLines.Select(l => l.Replace(" -> ", ",").Split(",").Select(s => int.Parse(s)).ToArray()).ToList();

            var xSize = lines.Select(l => l[0]).Union(lines.Select(l => l[2])).Max() + 1;
            var ySize = lines.Select(l => l[1]).Union(lines.Select(l => l[3])).Max() + 1;

            List<int> overlaps = new List<int>();

            for (int i = 0; i < lines.Count(); i++)
            {
                var l = lines[i];

                int x1 = l[0], x2 = l[2], y1 = l[1], y2 = l[3];
                int vert = 0, hor = 0;

                if (x1 > x2) hor = -1;
                else if (x1 < x2) hor = 1;

                if (y1 > y2) vert = -1;
                else if (y1 < y2) vert = 1;

                while (x1 != x2 || y1 != y2)
                {
                    overlaps.Add(x1 + y1 * xSize);
                    x1 += hor;
                    y1 += vert;
                }
                overlaps.Add(x1 + y1 * xSize);
            }


            var h = overlaps.GroupBy(x => x).Select(g => new { g.Key, Count = g.Count() });
            //for (int i = 0; i < xSize * ySize; i++)
            //{
            //    var n = h.SingleOrDefault(x => x.Key == i);
            //    var s = n == null ? "x" : n.Count.ToString();
            //    Console.Write($"{s} ");
            //    if ((i + 1) % xSize == 0)
            //        Console.WriteLine();
            //}
            Console.WriteLine($"Lines overlaps at {h.Count(x => x.Count > 1)}");
        }

        private static void First()
        {
            var fileLines = File.ReadAllLines(FILE).ToList();

            var lines = fileLines.Select(l => l.Replace(" -> ", ",").Split(",").Select(s => int.Parse(s)).ToArray());

            var hvLines = lines.Where(l => l[0] == l[2] || l[1] == l[3]).ToList();

            var xSize = lines.Select(l => l[0]).Union(lines.Select(l => l[2])).Max() + 1;
            var ySize = lines.Select(l => l[1]).Union(lines.Select(l => l[3])).Max() + 1;

            List<int> overlaps = new List<int>();

            for (int i = 0; i < hvLines.Count(); i++)
            {
                var l = hvLines[i];

                int x1 = l[0], x2 = l[2], y1 = l[1], y2 = l[3];
                int vert = 0, hor = 0;

                if (x1 > x2) hor = -1;
                else if (x1 < x2) hor = 1;

                if (y1 > y2) vert = -1;
                else if (y1 < y2) vert = 1;

                while (x1 != x2 || y1 != y2)
                {
                    overlaps.Add(x1 + y1 * xSize);
                    x1 += hor;
                    y1 += vert;
                }
                overlaps.Add(x1 + y1 * xSize);
            }


            var h = overlaps.GroupBy(x => x).Select(g => new { g.Key, Count = g.Count() });
            //for (int i = 0; i < xSize * ySize; i++)
            //{
            //    var n = h.SingleOrDefault(x => x.Key == i);
            //    var s = n == null ? "x" : n.Count.ToString();
            //    Console.Write($"{s} ");
            //    if ((i + 1) % xSize == 0)
            //        Console.WriteLine();
            //}
            Console.WriteLine($"Lines overlaps at {h.Count(x => x.Count > 1)}");
        }


        private static int max(int a, int b)
        {
            return a > b ? a : b;
        }

        private static int min(int a, int b)
        {
            return a < b ? a : b;
        }
    }
}
