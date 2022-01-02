using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _11
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

            var m = fileLines.SelectMany(l => l.Select(c => int.Parse(c.ToString())).ToArray());

            var xSize = m.Count() / ySize;

            var n = m.ToArray();

            Print(xSize, n);

            int stepsCount = 0;

            var stepFlashCount = 0;

            while (stepFlashCount < n.Length)
            {
                stepFlashCount = 0;
                stepsCount++;

                var q = new Queue<int>();
                for (int j = 0; j < n.Length; j++)
                {
                    n[j]++;
                    if (n[j] > 9) q.Enqueue(j);
                }

                while (q.Count > 0)
                {
                    var j = q.Dequeue();
                    if (n[j] == 0) continue;

                    n[j] = 0;
                    stepFlashCount++;

                    if (j % xSize > 0 && n[j - 1] != 0 && n[j - 1]++ >= 9) q.Enqueue(j - 1);
                    if (j % xSize < xSize - 1 && n[j + 1] != 0 && n[j + 1]++ >= 9) q.Enqueue(j + 1);
                    if (j / ySize > 0 && n[j - xSize] != 0 && n[j - xSize]++ >= 9) q.Enqueue(j - xSize);
                    if (j / ySize < ySize - 1 && n[j + xSize] != 0 && n[j + xSize]++ >= 9) q.Enqueue(j + xSize);

                    if ((j % xSize > 0 && j / ySize > 0) && n[j - xSize - 1] != 0 && n[j - xSize - 1]++ >= 9) q.Enqueue(j - xSize - 1);
                    if ((j % xSize < xSize - 1 && j / ySize > 0) && n[j - xSize + 1] != 0 && n[j - xSize + 1]++ >= 9) q.Enqueue(j - xSize + 1);
                    if ((j % xSize > 0 && j / ySize < ySize - 1) && n[j + xSize - 1] != 0 && n[j + xSize - 1]++ >= 9) q.Enqueue(j + xSize - 1);
                    if ((j % xSize < xSize - 1 && j / ySize < ySize - 1) && n[j + xSize + 1] != 0 && n[j + xSize + 1]++ >= 9) q.Enqueue(j + xSize + 1);
                }
            }

            Print(xSize, n);

            Console.WriteLine($"At step {stepsCount}, octopuses will flash simultaneously.");
        }

        private static void First()
        {
            var fileLines = File.ReadAllLines(FILE);

            var ySize = fileLines.Length;

            var m = fileLines.SelectMany(l => l.Select(c => int.Parse(c.ToString())).ToArray());

            var xSize = m.Count() / ySize;

            var n = m.ToArray();

            Print(xSize, n);

            var flashCount = 0;

            const int steps = 100;

            for (int i = 0; i < steps; i++)
            {
                var q = new Queue<int>();
                for (int j = 0; j < n.Length; j++)
                {
                    n[j]++;
                    if (n[j] > 9) q.Enqueue(j);
                }

                while (q.Count > 0)
                {
                    var j = q.Dequeue();
                    if (n[j] == 0) continue;

                    n[j] = 0;
                    flashCount++;

                    if (j % xSize > 0 && n[j - 1] != 0 && n[j - 1]++ >= 9) q.Enqueue(j - 1);
                    if (j % xSize < xSize - 1 && n[j + 1] != 0 && n[j + 1]++ >= 9) q.Enqueue(j + 1);
                    if (j / ySize > 0 && n[j - xSize] != 0 && n[j - xSize]++ >= 9) q.Enqueue(j - xSize);
                    if (j / ySize < ySize - 1 && n[j + xSize] != 0 && n[j + xSize]++ >= 9) q.Enqueue(j + xSize);

                    if ((j % xSize > 0 && j / ySize > 0) && n[j - xSize - 1] != 0 && n[j - xSize - 1]++ >= 9) q.Enqueue(j - xSize - 1);
                    if ((j % xSize < xSize - 1 && j / ySize > 0) && n[j - xSize + 1] != 0 && n[j - xSize + 1]++ >= 9) q.Enqueue(j - xSize + 1);
                    if ((j % xSize > 0 && j / ySize < ySize - 1) && n[j + xSize - 1] != 0 && n[j + xSize - 1]++ >= 9) q.Enqueue(j + xSize - 1);
                    if ((j % xSize < xSize - 1 && j / ySize < ySize - 1) && n[j + xSize + 1] != 0 && n[j + xSize + 1]++ >= 9) q.Enqueue(j + xSize + 1);
                }

            }

            Print(xSize, n);

            Console.WriteLine($"After {steps} steps, there have been a total of {flashCount} flashes.");
        }

        private static void Print(int xSize, int[] n)
        {
            for (int j = 0; j < n.Length; j++)
            {
                if (n[j] == 0)
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.Write(n[j]);

                Console.ForegroundColor = ConsoleColor.White;

                if ((j + 1) % xSize == 0)
                {
                    Console.WriteLine();
                }
            }

            Console.WriteLine();
        }
    }
}
