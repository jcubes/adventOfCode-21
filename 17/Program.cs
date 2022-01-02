using System;

namespace _17
{
    class Program
    {
        const int TARGET_Y_UP = -67;
        const int TARGET_Y_DOWN = -93;

        const int TARGET_X_FAR = 238;
        const int TARGET_X_CLOSE = 195;

        static void Main(string[] args)
        {
            //target area: x=195..238, y=-93..-67

            const int startY = 0;
            const int startX = 0;

            int[] startVelocity = new int[] { 1, TARGET_Y_DOWN };

            int targetCount = 0;

            while (startVelocity[0] <= TARGET_X_FAR)
            {
                startVelocity[1] = TARGET_Y_DOWN;

                while (startVelocity[1] < 100)
                {
                    //Console.WriteLine($"Start: [{startVelocity[0]},{startVelocity[1]}]");

                    int y = startY;
                    int x = startX;

                    int[] velocity = new int[] { startVelocity[0], startVelocity[1] };

                    int steps = 0;

                    while (y > TARGET_Y_DOWN)
                    {
                        x += velocity[0];
                        y += velocity[1];

                        velocity[0] += velocity[0] > 0 ? -1 : velocity[0] < 0 ? 1 : 0;
                        velocity[1]--;
                        
                        steps++;

                        if (IsInTarget(x, y))
                        {
                            targetCount++;
                            Console.WriteLine($"Target reached after {steps} steps. Start velocity={startVelocity[0]},{startVelocity[1]};");
                            break;
                        }

                    }

                    startVelocity[1]++;
                }

                startVelocity[0]++;
            }

            Console.WriteLine($"Target reached with {targetCount} starting velocities");

            //const int startX = 0;
            //const int startY = 0;

            //int[] velocity = new int[] { new Random().Next(0, 100), new Random().Next(0, 10) };

            //while (true)
            //{
            //    var x = startX;
            //    var y = startY;

            //    Console.WriteLine($"Starting velocity: X: {velocity[0]}, Y: {velocity[1]}");

            //    int stepCount = 0;

            //    int[] currentVelocity = new int[] { velocity[0], velocity[1] };

            //    bool inTarget = false;
            //    while (y > TARGET_Y_DOWN)
            //    {
            //        x += currentVelocity[0];
            //        y += currentVelocity[1];
            //        currentVelocity[0] += currentVelocity[0] > 0 ? -1 : currentVelocity[0] < 0 ? 1 : 0;
            //        currentVelocity[1]--;
            //        Console.WriteLine($"Position after step #{stepCount++}, X: {x}, Y: {y}");
            //        inTarget = inTarget | IsInTarget(x, y);
            //        if (IsInTarget(x, y))
            //            Console.WriteLine("IN TARGET!");
            //    }
            //    if (!inTarget)
            //    {
            //        if (x < TARGET_X_CLOSE) velocity[0]++;
            //        if (x > TARGET_X_FAR) velocity[0]--;
            //    }
            //}
        }

        public static bool IsInTarget(int x, int y)
        {
            return x <= TARGET_X_FAR && x >= TARGET_X_CLOSE && y >= TARGET_Y_DOWN && y <= TARGET_Y_UP;
        }

        public static bool IsInTarget(int y)
        {
            return y >= TARGET_Y_DOWN && y <= TARGET_Y_UP;
        }
    }
}
