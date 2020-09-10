using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace UDPTester
{
    public class UdpServer
    {
        private IPEndPoint _listenEndPoint;
        private System.Net.Sockets.UdpClient _server;

        public void StartServer(int listenPort = 5000)
        {
            _server = new System.Net.Sockets.UdpClient(listenPort);
            _listenEndPoint = new IPEndPoint(IPAddress.Any, listenPort);
        }

        public void Receive(string outputFile = "")
        {
            if (outputFile == string.Empty)
                return;

            var writer = new StreamWriter(outputFile) { AutoFlush = true };

            var lastPacket = new DataPacket(0, new TimeSpan());
            do
            {
                var receivedData = _server.Receive(ref _listenEndPoint);

                try
                {
                    var packet = JsonConvert.DeserializeObject<DataPacket>(Encoding.ASCII.GetString(receivedData));
                    packet.RecieveDateTime = DateTime.Now;

                    var packetIdDeviation = packet.PacketId - lastPacket.PacketId;
                    var packetTimeOffsetRaw = packet.RecieveDateTime - lastPacket.RecieveDateTime;
                    var packetTimeOffsetAdjusted = packetTimeOffsetRaw.Subtract(packet.IntendedPacketOffset);

                    Console.WriteLine(packet.PacketId.ToString() + "," + packetIdDeviation.ToString() + "," + packetTimeOffsetRaw.TotalMilliseconds + "," + packetTimeOffsetAdjusted.TotalMilliseconds);

                    Console.WriteLine(packet.ToString());
                    //writer.WriteLine(Encoding.ASCII.GetString(receivedData));

                    lastPacket = packet;
                }
                catch (Exception ex)
                {
                }
            } while (true);
        }

        public void Receive()
        {
            var lastPacket = new DataPacket(0, new TimeSpan());
            do
            {
                var receivedData = _server.Receive(ref _listenEndPoint);

                try
                {
                    var packet = JsonConvert.DeserializeObject<DataPacket>(Encoding.ASCII.GetString(receivedData));
                    packet.RecieveDateTime = DateTime.Now;

                    var packetIdDeviation = packet.PacketId - lastPacket.PacketId;
                    var packetTimeOffsetRaw = packet.RecieveDateTime - lastPacket.RecieveDateTime;
                    var packetTimeOffsetAdjusted = packetTimeOffsetRaw.Subtract(packet.IntendedPacketOffset);

                    Console.WriteLine(packet.PacketId.ToString() + "," + packetIdDeviation.ToString() + "," + packetTimeOffsetRaw.TotalMilliseconds + "," + packetTimeOffsetAdjusted.TotalMilliseconds);
                    //writer.WriteLine(Encoding.ASCII.GetString(receivedData));

                    lastPacket = packet;
                }
                catch (Exception ex)
                {
                }
            } while (true);
        }
    }
}