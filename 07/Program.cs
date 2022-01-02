using System;
using System.IO;
using System.Linq;

namespace _07
{
    class Program
    {
        const string FILE = "input.txt";

        static void Main(string[] args)
        {
            var line = File.ReadAllText(FILE).Trim();

            var numbers = line.Split(",").Select(s => int.Parse(s)).ToList();

            //var h = Enumerable.Range(0, numbers.Max()).Select(i => numbers.Select(a => Math.Abs(a - i)).Sum()).ToList();
            var h = Enumerable.Range(0, numbers.Max()).Select(i => numbers.Select(a => Math.Abs(a - i) * (1 + Math.Abs(a - i)) / 2).Sum()).ToList();
            Console.WriteLine($"{h.IndexOf(h.Min())}: {h.Min()}");
        }
    }
}
