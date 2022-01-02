using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _15
{
    class Program
    {
        const string FILE = "input.txt";

        static void Main(string[] args)
        {
            //First();

            Second();
        }

        private static int Inc(int n, int i)
        {
            var x = (n + i) % 10;
            if ((n+i) / 10 > 0) x = x + 1;
            return x;
        }

        private static void Second()
        {
            var fileLines = File.ReadAllLines(FILE).ToList();

            var withRows = fileLines.SelectMany(line => Enumerable.Range(0, 5).SelectMany(i => line.Select(c => int.Parse(c.ToString())).Select(n => Inc(n, i))).ToArray());

            var matrix = Enumerable.Range(0, 5).Select(i => withRows.Select(n => Inc(n, i)).ToArray()).SelectMany(r => r).ToArray();

            var xSize = fileLines.First().Length * 5;
            var ySize = fileLines.Count * 5;

            //for (int i = 0; i < ySize; i++)
            //{
            //    for (int j = 0; j < xSize; j++)
            //    {
            //        Console.Write(matrix[j + i * xSize]);
            //    }
            //    Console.WriteLine();
            //}

            //Console.WriteLine();

            var d = new Dijkstra(matrix, xSize, ySize);

            var res = d.FindRoute(0, xSize * ySize);

            Console.WriteLine($"Risk to get to the target is: {res.distance[xSize * ySize - 1]}");
        }

        private static void First()
        {
            var fileLines = File.ReadAllLines(FILE).ToList();

            var matrix = fileLines.SelectMany(l => l.Select(c => int.Parse(c.ToString()))).ToArray();

            var xSize = fileLines.First().Length;
            var ySize = fileLines.Count;

            var d = new Dijkstra(matrix, xSize, ySize);

            var res = d.FindRoute(0, xSize * ySize);

            Console.WriteLine($"Risk to get to the target is: {res.distance[xSize * ySize - 1]}");
        }
    }
}
