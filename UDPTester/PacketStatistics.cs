using Newtonsoft.Json;
using System;

namespace UDPTester
{
    public class PacketStatistics
    {
        [JsonIgnore]
        public DataPacket CurrentPacket;

        [JsonIgnore]
        public DataPacket PreviousPacket;

        public int IdDeviation => Convert.ToInt32(CurrentPacket.PacketId - PreviousPacket.PacketId);
        public TimeSpan TimeOffsetRaw => CurrentPacket.ReceiveDateTime - PreviousPacket.ReceiveDateTime;
        public TimeSpan TimeOffsetAdjusted => TimeOffsetRaw.Subtract(CurrentPacket.IntendedPacketOffset);
        public bool IsOutOfOrder => IdDeviation != 1;

        public DateTime CreatedTime => CurrentPacket.CreatedDateTime;
        public DateTime ReceivedTime => CurrentPacket.ReceiveDateTime;

        public PacketStatistics(DataPacket current, DataPacket previousPacket)
        {
            CurrentPacket = current;
            PreviousPacket = previousPacket;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}