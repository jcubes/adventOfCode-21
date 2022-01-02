using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _04
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
            IEnumerable<string> lines = File.ReadAllLines(FILE).ToList();
            var numbers = lines.First().Split(",").Select(s => int.Parse(s)).ToList();

            List<int[]> matrices = new();


            for (int i = 0; i < lines.Count() - 2; i += 6)
            {
                var s = lines.Skip(2 + i).Take(5);
                var s2 = s.SelectMany(r => r.Trim().Replace("  ", " ").Split(" "));
                matrices.Add(s.SelectMany(r => r.Trim().Replace("  ", " ").Split(" ").Select(s => int.Parse(s))).ToArray());
            }

            List<int[]> counters = new List<int[]>();

            for (int i = 0; i < matrices.Count; i++)
            {
                counters.Add(new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1 });
            }

            for (int i = 0; i < numbers.Count(); i++)
            {
                var n = numbers[i];

                for (int j = 0; j < matrices.Count; j++)
                {
                    var m = matrices[j];
                    for (int k = 0; k < m.Length; k++)
                    {
                        if (m[k] == n)
                        {
                            counters[j][k / 5]++;
                            counters[j][k % 5 + 5]++;

                            if (counters[j].Take(10).Any(x => x == 5) && counters[j][10] == -1)
                                counters[j][10] = i;
                        }
                    }
                }
            }

            var winningMatrixIndex = 0;
            for (int i = 1; i < counters.Count; i++)
            {
                if (counters[i][10] > counters[winningMatrixIndex][10]) winningMatrixIndex = i;
            }

            var numberToWinIndex = counters[winningMatrixIndex][10];

            var matrix = matrices[winningMatrixIndex].ToList();
            for (int i = 0; i <= numberToWinIndex; i++)
            {
                var aa = matrix.IndexOf(numbers[i]);
                if (aa >= 0) matrix[aa] = 0;
            }
            var result = matrix.Sum() * numbers[numberToWinIndex];
            Console.WriteLine($"Result: {matrix.Sum()} * {numbers[numberToWinIndex]} =  {result}");
        }

        private static void First()
        {
            IEnumerable<string> lines = File.ReadAllLines(FILE).ToList();
            var numbers = lines.First().Split(",").Select(s => int.Parse(s)).ToList();

            List<int[]> matrices = new();


            for (int i = 0; i < lines.Count() - 2; i += 6)
            {
                var s = lines.Skip(2 + i).Take(5);
                var s2 = s.SelectMany(r => r.Trim().Replace("  ", " ").Split(" "));
                matrices.Add(s.SelectMany(r => r.Trim().Replace("  ", " ").Split(" ").Select(s => int.Parse(s))).ToArray());
            }

            List<int[]> counters = new List<int[]>();

            for (int i = 0; i < matrices.Count; i++)
            {
                counters.Add(new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1 });
            }

            for (int i = 0; i < numbers.Count(); i++)
            {
                var n = numbers[i];

                for (int j = 0; j < matrices.Count; j++)
                {
                    var m = matrices[j];
                    for (int k = 0; k < m.Length; k++)
                    {
                        if (m[k] == n)
                        {
                            counters[j][k / 5]++;
                            counters[j][k % 5 + 5]++;

                            if (counters[j].Take(10).Any(x => x == 5) && counters[j][10] == -1)
                                counters[j][10] = i;
                        }
                    }
                }
            }

            var winningMatrixIndex = 0;
            for (int i = 1; i < counters.Count; i++)
            {
                if (counters[i][10] < counters[winningMatrixIndex][10]) winningMatrixIndex = i;
            }

            var numberToWinIndex = counters[winningMatrixIndex][10];

            var matrix = matrices[winningMatrixIndex].ToList();
            for (int i = 0; i <= numberToWinIndex; i++)
            {
                var aa = matrix.IndexOf(numbers[i]);
                if (aa >= 0) matrix[aa] = 0;
            }
            var result = matrix.Sum() * numbers[numberToWinIndex];
            Console.WriteLine($"Result: {matrix.Sum()} * {numbers[numberToWinIndex]} =  {result}");
        }
    }
}
