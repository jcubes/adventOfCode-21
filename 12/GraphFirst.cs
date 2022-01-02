using System.Collections.Generic;

namespace _12
{
    public class GraphFirst
    {
        private readonly Dictionary<string, List<string>> edges;
        private List<string> paths;

        public GraphFirst(Dictionary<string, List<string>> edges)
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
                    paths.Add(x);
                    if (!path.Contains(aa) || aa.IsUpper())
                        p = Search(aa, e, p, x);
                }

            }
            return p;
        }
    }
}
