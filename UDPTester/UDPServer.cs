using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

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

        public void Receive(string outputFile = "",  bool outputStatistics = false)
        {
            if (outputFile == null) throw new ArgumentNullException(nameof(outputFile));
            if (outputFile == string.Empty)
                return;

            var writer = new StreamWriter(outputFile) {AutoFlush = true};

            var lastPacket = new DataPacket(0, new TimeSpan());

            do
            {
                var receivedData = _server.Receive(ref _listenEndPoint);

                try
                {
                    var packet = JsonConvert.DeserializeObject<DataPacket>(Encoding.ASCII.GetString(receivedData));
                    packet.ReceiveDateTime = DateTime.Now;
                    var packetStats = new PacketStatistics(packet, lastPacket);

                    writer.WriteLine(outputStatistics ? packetStats.ToString() : packet.ToString());

                    lastPacket = packet;
                }
                catch (Exception ex)
                {
                    // ignored
                }
            } while (true);
        }

        public void Receive(bool outputStatistics = false)
        {
            var lastPacket = new DataPacket(0, new TimeSpan());
            do
            {
                var receivedData = _server.Receive(ref _listenEndPoint);

                try
                {
                    var packet = JsonConvert.DeserializeObject<DataPacket>(Encoding.ASCII.GetString(receivedData));
                    packet.ReceiveDateTime = DateTime.Now;
                    var packetStats = new PacketStatistics(packet, lastPacket);

                    Console.WriteLine(outputStatistics ? packetStats.ToString() : packet.ToString());

                    lastPacket = packet;
                }
                catch (Exception ex)
                {
                    // ignored
                }
            } while (true);
        }
    }
}