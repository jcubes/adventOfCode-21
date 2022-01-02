using System;
using System.IO;
using System.Linq;

namespace _08
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

            long c = 0;

            foreach (var line in fileLines)
            {
                string[] digits = new string[10];

                var notes = line.Split(" | ").First().Split(" ").Select(s => new string(s.OrderBy(c => c).ToArray())).ToList();

                digits[1] = notes.Single(n => n.Length == 2);
                digits[4] = notes.Single(n => n.Length == 4);
                digits[7] = notes.Single(n => n.Length == 3);
                digits[8] = notes.Single(n => n.Length == 7);

                var eg = new string(digits[8].Except(digits[7]).Except(digits[4]).ToArray());

                digits[9] = notes.Where(n => n.Length == 6).Single(n => n.Except(digits[4]).Except(digits[7]).Count() == 1);
                digits[0] = notes.Where(n => n.Length == 6).Single(n => n.Except(digits[7]).Except(eg).Count() == 1);
                digits[6] = notes.Where(n => n.Length == 6).Single(n => n.Except(digits[0]).Count() != 0 && n.Except(digits[9]).Count() != 0);

                digits[3] = notes.Where(n => n.Length == 5).Single(n => n.Except(digits[1]).Count() == 3);
                digits[5] = notes.Where(n => n.Length == 5).Single(n => n.Except(digits[4]).Count() == 2 && !n.Equals(digits[3]));
                digits[2] = notes.Where(n => n.Length == 5).Single(n => n.Except(digits[3]).Count() != 0 && n.Except(digits[5]).Count() != 0);


                //Console.WriteLine(line.Split(" | ").First());
                //for (int i = 0; i < 10; i++)
                //{
                //    Console.Write($"{i} = {digits[i]}, ");
                //}
                //Console.WriteLine();

                var encodedNumbers = line.Split(" | ").Last().Split(" ").Select(s => new string(s.OrderBy(c => c).ToArray())).ToList();

                var decodedNumbers = encodedNumbers.Select(s => digits.ToList().IndexOf(s));

                //Console.WriteLine($"{string.Join(", ", encodedNumbers)}");
                //Console.WriteLine($"{int.Parse(string.Join("", decodedNumbers))}");
                c += int.Parse(string.Join("", decodedNumbers));
            }

            Console.WriteLine($"Final sum: {c}");
        }

        private static void First()
        {
            var fileLines = File.ReadAllLines(FILE).ToList();

            int[] u = new int[] { 2, 3, 4, 7 };

            foreach (var l in fileLines)
                Console.WriteLine($"{l.Split(" | ").Last().Split(" ").Where(s => u.Contains(s.Length)).Count()}: {l.Split(" | ").Last()}");

            Console.WriteLine(fileLines.Select(l => l.Split(" | ").Last().Split(" ").Where(s => u.Contains(s.Length)).Count()).Sum());
        }
    }
}
