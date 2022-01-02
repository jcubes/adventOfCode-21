using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _03
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
            IEnumerable<string> numbers = File.ReadLines(FILE).ToList();

            var i = 0;

            while (numbers.Count() > 1)
            {
                var ones = numbers.Count(n => n.Substring(i).StartsWith('1'));
                if (ones >= numbers.Count() - ones)
                    numbers = numbers.Where(n => n.Substring(i).StartsWith('1')).ToList();
                else
                    numbers = numbers.Where(n => n.Substring(i).StartsWith('0')).ToList();
                i++;
            }


            var oxygenStr = numbers.First();
            Console.WriteLine($"oxygen: {oxygenStr}");

            numbers = File.ReadLines(FILE).ToList();

            i = 0;

            while (numbers.Count() > 1)
            {
                var ones = numbers.Count(n => n.Substring(i).StartsWith('1'));
                if (ones >= numbers.Count() -ones)
                    numbers = numbers.Where(n => n.Substring(i).StartsWith('0')).ToList();
                else
                    numbers = numbers.Where(n => n.Substring(i).StartsWith('1')).ToList();
                i++;
            }

            var co2Str = numbers.First();
            Console.WriteLine($"CO2: {co2Str}");

            int oxygen = Convert.ToInt16(oxygenStr, 2);
            int co2 = Convert.ToInt16(co2Str, 2);

            Console.WriteLine($"Result: {oxygen} * {co2} = {oxygen * co2}");
        }

        private static void First()
        {
            int[] oneRate = new int[12];

            int lineCount = 0;


            foreach (string line in File.ReadLines(FILE))
            {
                var l = line.ToCharArray();
                for (int i = 0; i < line.Length; i++)
                {
                    oneRate[i] += int.Parse(l[i].ToString());
                }
                lineCount++;
            }

            double gammaRate = 0;
            double epsilonRate = 0;

            Console.WriteLine($"Number of numbers: {lineCount}");

            for (int i = 0; i < oneRate.Length; i++)
            {
                Console.Write($"{oneRate[i]}({lineCount / 2}), ");

                if (oneRate[i] > (lineCount / 2))
                    gammaRate += Math.Pow(2, 11 - i);
                else
                    epsilonRate += Math.Pow(2, 11 - i);
            }

            Console.WriteLine();

            Console.WriteLine($"Gamma rate: {gammaRate}, epsilon rate: {epsilonRate}, Multiplied: {gammaRate * epsilonRate}");
        }
    }
}
