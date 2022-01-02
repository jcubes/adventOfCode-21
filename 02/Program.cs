using System;
using System.IO;

namespace _02
{
    class Program
    {
        static void Main(string[] args)
        {
            Second();
        }

        private static void Second()
        {
            int[] position = new int[2];

            int aim = 0;

            foreach (string line in File.ReadLines(@"input.txt"))
            {
                var command = line.Split(" ")[0];
                var move = int.Parse(line.Split(" ")[1]);

                if (command.Equals("forward"))
                {
                    position[0] += move;
                    position[1] += move * aim;
                }

                if (command.Equals("down"))
                    aim += move;

                if (command.Equals("up"))
                    aim -= move;
            }

            Console.WriteLine($"Final position is {position[0]} with depth {position[1]}. Multiplied: {position[0] * position[1]}");
        }

        private static void First()
        {
            int[] position = new int[2];

            foreach (string line in File.ReadLines(@"input.txt"))
            {
                var command = line.Split(" ")[0];
                var move = int.Parse(line.Split(" ")[1]);

                if (command.Equals("forward"))
                    position[0] += move;
                if (command.Equals("down"))
                    position[1] += move;
                if (command.Equals("up"))
                    position[1] -= move;
            }

            Console.WriteLine($"Final position is {position[0]} with depth {position[1]}. Multiplied: {position[0] * position[1]}");
        }
    }
}
