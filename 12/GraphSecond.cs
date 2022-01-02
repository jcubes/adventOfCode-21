using System;
using System.Collections.Generic;
using System.Linq;

namespace _12
{
    public class GraphSecond
    {
        private readonly Dictionary<string, List<string>> edges;
        private List<string> paths;

        public GraphSecond(Dictionary<string, List<string>> edges)
        {
            this.edges = edges;
        }

        internal List<string> Traverse(string s, string e)
        {
            paths = new List<string>();

            int p = 0;
            Search(s, e, p, s);
            return paths;
        }

        private int Search(string v, string e, int p, string path)
        {
            if (v == e)
                p++;
            else
            {
                foreach (var aa in edges[v])
                {
                    var x = $"{path}-{aa}";
                    var parts = x.Split("-").ToList();
                    var g = parts.GroupBy(s => s);

                    var b = g.Count(g => g.Count() > 1 && g.Key.IsLower());

                    if (!aa.Equals("start") && g.Count(g => g.Count() > 1 && g.Key.IsLower()) < 2 && !g.Any(g => g.Count() > 2 && g.Key.IsLower()))
                    {
                        paths.Add(x);
                    }

                    if (!aa.Equals("start") && g.Count(g => g.Count() > 1 && g.Key.IsLower()) < 2 && !g.Any(g => g.Count() > 2 && g.Key.IsLower()))
                    {
                        p = Search(aa, e, p, x);
                    }
                }

            }
            return p;
        }
    }
}
