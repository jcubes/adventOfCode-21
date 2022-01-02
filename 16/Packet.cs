using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16
{
    abstract class Packet
    {
        public Packet(int version, int type)
        {
            Version = version;
            Type = type;
        }

        public int Version { get; set; }
        public int Type { get; set; }

        public abstract int GetVersionSum();

        public abstract long Execute();
    }

    class LiteralPacket : Packet
    {
        public LiteralPacket(int version, int type) : base(version, type)
        {
        }

        public long Literal { get; set; }

        public override long Execute()
        {
            return Literal;
        }

        public override int GetVersionSum()
        {
            return Version;
        }

        public override string ToString()
        {
            return $"Version: {Version} | Type: {Type} | Literal: {Literal}";
        }
    }

    class OperatorPacket : Packet
    {
        public OperatorPacket(int version, int type) : base(version, type)
        {
            SubPackets = new List<Packet>();
        }

        public List<Packet> SubPackets { get; set; }

        public override long Execute()
        {
            switch (Type)
            {
                case 0: return SubPackets.Sum(s => s.Execute());
                case 1: return SubPackets.Aggregate(1L, (sum, next) => sum * next.Execute());
                case 2: return SubPackets.Min(s => s.Execute());
                case 3: return SubPackets.Max(s => s.Execute());

                case 5: return SubPackets[0].Execute() > SubPackets[1].Execute() ? 1 : 0;
                case 6: return SubPackets[0].Execute() < SubPackets[1].Execute() ? 1 : 0;
                case 7: return SubPackets[0].Execute() == SubPackets[1].Execute() ? 1 : 0;
                default: return 0;
            }
        }

        public override int GetVersionSum()
        {
            return SubPackets.Sum(s => s.GetVersionSum()) + Version;
        }

        public override string ToString()
        {
            return $"Version: {Version} | Type: {Type} | # of subs: {SubPackets.Count}";
        }
    }

    class Parser
    {
        public static (Packet, int) Parse(string data)
        {
            var i = 0;
            Packet p = null;

            while (i < data.Length)
            {
                var packetVer = Convert.ToInt16(data.Substring(i, 3), 2);
                var typeId = Convert.ToInt16(data.Substring(i + 3, 3), 2);
                i += 6;

                if (typeId == 4)
                {
                    LiteralPacket newPacket = new LiteralPacket(packetVer, typeId);
                    var literal = "";
                    while (true)
                    {
                        var chunk = data.Substring(i, 5);
                        i += 5;
                        literal += chunk.Substring(1);
                        if (chunk.StartsWith("0"))
                            break;
                    }
                    //i += 8 - (i - (i / 8 * 8));
                    newPacket.Literal = Convert.ToInt64(literal, 2);
                    p = newPacket;
                    break;
                }
                else
                {
                    OperatorPacket newPacket = new OperatorPacket(packetVer, typeId);

                    var lengthType = Convert.ToInt16(data.Substring(i, 1), 2);
                    i++;
                    if (lengthType == 1)
                    {
                        var subPacketCount = Convert.ToInt64(data.Substring(i, 11), 2);
                        i += 11;
                        for (int j = 0; j < subPacketCount; j++)
                        {
                            var subPacketString = data.Substring(i);
                            var subP = Parse(subPacketString);
                            i = i + subP.Item2;
                            newPacket.SubPackets.Add(subP.Item1);
                        }
                    }
                    else
                    {
                        var subPacketLength = Convert.ToInt32(data.Substring(i, 15), 2);
                        i += 15;
                        while (i < i + subPacketLength)
                        {
                            var subPacketString = data.Substring(i, subPacketLength);
                            var subP = Parse(subPacketString);
                            i = i + subP.Item2;
                            subPacketLength -= subP.Item2;
                            newPacket.SubPackets.Add(subP.Item1);
                        }
                    }

                    p = newPacket;
                    break;
                }
            }

            return (p, i);
        }
    }
}
