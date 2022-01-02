using System;
using System.IO;

namespace _01
{
    class Program
    {
        static void Main(string[] args)
        {
            Second();
        }

        public static void First()
        {

        }

        public static void Second()
        {
            int counter = 0;


            int[] a = new int[4];
            int i = 4;

            foreach (string line in File.ReadLines(@"input.txt"))
            {
                int measurement = int.Parse(line);

                a[i % 4] = measurement;

                var x = i;

                int sum1 = 0;
                int sum2 = 0;

                int x1 = i % 4;
                int x2 = (i + 1) % 4;

                if (i >= 7)
                {
                    sum1 = a[0] + a[1] + a[2] + a[3] - a[x1];
                    sum2 = a[0] + a[1] + a[2] + a[3] - a[x2];
                    if (sum1 < sum2)
                        counter++;
                }

                Console.WriteLine($"{i}[{i % 4}]: {string.Join(" | ", a)} / [{x1}]{sum1} - [{x2}]{sum2}");
                i++;
            }

            Console.WriteLine($"Measurement increased: {counter} times");
        }
    }
}
