using System;
using Newtonsoft.Json;

namespace UDPTester
{
    public class DataPacket
    {
        public ulong PacketId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public TimeSpan IntendedPacketOffset { get; set; }
        public DateTime ReceiveDateTime { get; set; }


        public DataPacket()
        {
            ReceiveDateTime = DateTime.Now;
        }

        public DataPacket(ulong packetId, TimeSpan intendedPacketOffset)
        {
            PacketId = packetId;
            CreatedDateTime = DateTime.Now;
            IntendedPacketOffset = intendedPacketOffset;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}