using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _13
{
    class Program
    {
        const string FILE = "input.txt";

        static void Main(string[] args)
        {
            First();

        }

        private static void First()
        {
            var fileLines = File.ReadAllLines(FILE).ToList();

            List<string> dotRows = new List<string>();

            int ii = 0;
            while (!string.IsNullOrEmpty(fileLines[ii].Trim()))
            {
                dotRows.Add(fileLines[ii]);
                ii++;
            }

            var dots = dotRows.Select(l => l.Split(",").Select(s => int.Parse(s)).ToArray()).ToList();

            var x = dots.Select(d => d[0]).Max() + 1;
            var y = dots.Select(d => d[1]).Max() + 1;

            Console.WriteLine($"X: {x}, Y: {y}");

            var cmds = fileLines.Where(l => l.StartsWith("fold"));
            foreach (var c in cmds)
                Console.WriteLine(c);

            var arr = new int[x * y];
            foreach (var d in dots)
            {
                var dotIndex = d[1] * x + d[0];
                arr[dotIndex] = 1;
            }

            //Print(arr, x, y);

            int[] newArr = arr;
            int newX = x;
            int newY = y;

            foreach (var cmd in cmds)
            {
                var p = cmd.Split(" ").Last().Split("=");
                var direction = p[0].First();
                var foldIndex = int.Parse(p[1]);

                if (direction == 'y')
                {
                    var newSize = ((newY - 1) / 2) * newX;

                    var tempArr = new int[newSize];

                    for (int i = 0; i < newSize; i++)
                    {
                        var a = newArr[i];
                        var bIndex = (newX * newY) + (i % newX) - (((i / newX) + 1) * newX);
                        var b = newArr[bIndex];

                        tempArr[i] = a | b;
                    }

                    newX = newX;
                    newY = newY / 2;
                    newArr = tempArr;
                }

                if (direction == 'x')
                {
                    var newSize = ((newX - 1) / 2) * newY;

                    var tempArr = new int[newSize];

                    for (int i = 0; i < newSize; i++)
                    {
                        var aIndex = (i % (newX / 2)) + ((i / (newX / 2)) * (newX));
                        var a = newArr[aIndex];
                        var bIndex = ((i / (newX / 2)) * (newX)) + (newX - (aIndex % newX) - 1);
                        var b = newArr[bIndex];

                        tempArr[i] = a | b;
                    }

                    newX = newX / 2;
                    newY = newY;
                    newArr = tempArr;
                }

            }

            Print(newArr, newX, newY);

        }

        public static void Print(int[] arr, int x, int y)
        {
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    var c = arr[i * x + j];
                    if (c == 1)
                        Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(c);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }

            Console.WriteLine($"Number of dots: {arr.Count(d => d == 1)}");
            Console.WriteLine();
        }
    }
}
