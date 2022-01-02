using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _10
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
            var fileLines = File.ReadAllLines(FILE).ToList();

            var opening = new List<char>() { '(', '[', '<', '{' };
            var closing = new List<char>() { ')', ']', '>', '}' };

            var scoring = new Dictionary<char, int> { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 } };
            var score = new List<long>();

            foreach (var line in fileLines)
            {
                var stack = new Stack<char>();

                foreach (var bracket in line)
                {
                    if (opening.Contains(bracket))
                        stack.Push(bracket);

                    if (closing.Contains(bracket))
                    {
                        var a = stack.Pop();
                        if (closing[opening.IndexOf(a)] != bracket)
                        {
                            stack.Clear();
                            break;
                        }
                    }
                }

                if (stack.Count == 0) continue;

                long sum = 0;

                while (stack.Count > 0)
                {
                    sum = sum * 5 + scoring[closing[opening.IndexOf(stack.Pop())]];
                }

                score.Add(sum);
            }

            var medianScore = score.OrderBy(s => s).Skip(score.Count / 2).First();

            Console.WriteLine($"Middle score is: {medianScore}");

        }

        private static void First()
        {
            var fileLines = File.ReadAllLines(FILE).ToList();

            var opening = new List<char>() { '(', '[', '<', '{' };
            var closing = new List<char>() { ')', ']', '>', '}' };
            var scoring = new Dictionary<char, int> { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
            var score = 0;

            foreach (var line in fileLines)
            {
                var stack = new Stack<char>();

                foreach (var bracket in line)
                {
                    if (opening.Contains(bracket))
                        stack.Push(bracket);

                    if (closing.Contains(bracket))
                    {
                        var a = stack.Pop();
                        if (closing[opening.IndexOf(a)] != bracket)
                        {
                            Console.WriteLine($"Expected {closing[opening.IndexOf(a)]}, but found {bracket} instead. score: {scoring[bracket]}");
                            score += scoring[bracket];
                            break;
                        }
                    }
                }
            }

            Console.WriteLine($"syntax error score is: {score}");
        }
    }
}
