using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace _16
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

            var code = fileLines.First();

            Console.WriteLine(code);

            var binaryPacket = ConvertHexStringToByteArray(code);
            Console.WriteLine(string.Join("-", binaryPacket.Select(x => Convert.ToString(x, 2).PadLeft(8, '0'))));

            var stringPacket = string.Join("", binaryPacket.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));

            var a = Parser.Parse(stringPacket);

            Console.WriteLine($"Version sum: {a.Item1.GetVersionSum()}");
         
            Console.WriteLine($"Execute: {a.Item1.Execute()}");
        }

        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }

            byte[] data = new byte[hexString.Length / 2];
            for (int index = 0; index < data.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                data[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return data;
        }
    }
}
