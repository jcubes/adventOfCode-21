using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _14
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

            var template = fileLines.First();

            var rules = fileLines.Skip(2);


            var d = new Dictionary<string, string[]>();

            foreach (var rule in rules)
            {
                var splitRule = rule.Split(" -> ");
                d.Add(splitRule[0], new string[] { $"{splitRule[0].First()}{splitRule[1]}", $"{splitRule[1]}{splitRule[0].Last()}" });
            }

            var h = new Dictionary<string, long>();

            for (int i = 0; i < template.Length - 1; i++)
            {
                var p = template.Substring(i, 2);
                if (h.ContainsKey(p))
                    h[p] = h[p] + 1;
                else
                    h.Add(p, 1);
            }

            var iterDict = h;

            for (int i = 0; i < 40; i++)
            {
                Console.WriteLine($"Step {i}: {iterDict.Sum(s => s.Value) + 1}");
                var tempDict = new Dictionary<string, long>();
                foreach (var kvp in iterDict)
                {
                    var newToAdd = d[kvp.Key];
                    foreach (var toAdd in newToAdd)
                    {
                        if (tempDict.ContainsKey(toAdd))
                            tempDict[toAdd] = tempDict[toAdd] + kvp.Value;
                        else
                            tempDict.Add(toAdd, kvp.Value);
                    }
                }

                iterDict = tempDict;
            }

            Console.WriteLine($"End: {iterDict.Sum(s => s.Value) + 1}");
            Console.WriteLine();

            var count = iterDict.SelectMany(s => new[] { new { c = s.Key[0], v = s.Value }, new { c = s.Key[1], v = s.Value } }).GroupBy(s => s.c);

            foreach (var g in count)
            {
                Console.WriteLine($"{g.Key}: {(g.Sum(g => g.v) + 1) / 2}");
            }
            Console.WriteLine();

            var res = count.Max(s => (s.Sum(g => g.v) + 1) / 2) - count.Min(s => (s.Sum(g => g.v) + 1) / 2);

            Console.WriteLine($"Result: {res}");
        }
    }
}
