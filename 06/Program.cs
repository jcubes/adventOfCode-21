using System;
using System.IO;
using System.Linq;

namespace _06
{
    class Program
    {
        const string FILE = "input.txt";

        static void Main(string[] args)
        {
            var line = File.ReadAllText(FILE).Trim();

            var numbers = line.Split(",").Select(s => int.Parse(s));

            long[] count = new long[9];

            var h = numbers.GroupBy(x => x).Select(g => new { g.Key, Count = g.Count() });
            foreach (var a in h)
            {
                count[a.Key] = a.Count;
            }

            Console.WriteLine($"Day 0: sum: {count.Sum()}");

            for (int i = 0; i < 256; i++)
            {
                long newFishes = count[0];
                for (int j = 0; j < count.Length-1; j++)
                {
                    count[j] = count[j + 1];
                }
                count[8] = newFishes;
                count[6] += newFishes;
                Console.WriteLine($"Day {i + 1}: sum: {count.Sum()}");
                //Console.WriteLine($"Day {i + 1}: {string.Join(",", count.SelectMany((a, i) => Enumerable.Repeat(i.ToString(), a)))} - sum: {count.Sum()}");
            }
        }
    }
}
