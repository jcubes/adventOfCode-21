using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _12
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

            var edges = new Dictionary<string, List<string>>();

            foreach (var s in fileLines.Select(s => s.Split("-")))
            {
                if (edges.TryGetValue(s[0], out List<string> verticeEdges))
                {
                    if (!verticeEdges.Contains(s[1])) verticeEdges.Add(s[1]);
                }
                else
                    edges.Add(s[0], new List<string>() { s[1] });

                if (edges.TryGetValue(s[1], out List<string> verticeEdgesS))
                {
                    if (!verticeEdgesS.Contains(s[0])) verticeEdgesS.Add(s[0]);
                }
                else
                    edges.Add(s[1], new List<string>() { s[0] });
            }

            var g = new GraphSecond(edges);

            var paths = g.Traverse("start", "end");


            Console.WriteLine($"Paths to end: {paths.Count(p => p.EndsWith("end"))}");
        }

        private static void First()
        {
            var fileLines = File.ReadAllLines(FILE).ToList();

            var edges = new Dictionary<string, List<string>>();

            foreach (var s in fileLines.Select(s => s.Split("-")))
            {
                if (edges.TryGetValue(s[0], out List<string> verticeEdges))
                {
                    if (!verticeEdges.Contains(s[1])) verticeEdges.Add(s[1]);
                }
                else
                    edges.Add(s[0], new List<string>() { s[1] });

                if (edges.TryGetValue(s[1], out List<string> verticeEdgesS))
                {
                    if (!verticeEdgesS.Contains(s[0])) verticeEdgesS.Add(s[0]);
                }
                else
                    edges.Add(s[1], new List<string>() { s[0] });
            }

            var g = new GraphFirst(edges);

            var paths = g.Traverse("start", "end");

            Console.WriteLine($"Paths to end: {paths.Count(p => p.EndsWith("end"))}");
        }
    }

    static class Extensions
    {
        public static bool IsUpper(this string value)
        {
            // Consider string to be uppercase if it has no lowercase letters.
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsLower(value[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsLower(this string value)
        {
            // Consider string to be lowercase if it has no uppercase letters.
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsUpper(value[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
