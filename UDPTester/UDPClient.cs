using System;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace UDPTester
{
    public class UdpClient
    {
        private System.Net.Sockets.UdpClient _client;
        private IPEndPoint _server;

        public void Connect(string ipAddress, int sendPort)
        {
            try
            {
                _client = new System.Net.Sockets.UdpClient();
                _server = new IPEndPoint(IPAddress.Parse(ipAddress), sendPort);
                _client.Connect(_server);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Send(TimeSpan testLength, TimeSpan packetOffset, int packetBytes = -1)
        {
            ulong packetCounter = 0;
            var endTime = DateTime.Now.Add(testLength);
            bool displaySpeed = true;
            while (endTime > DateTime.Now)
            {
                packetCounter++;

                // Todo: turn the packet into a json object with data that can be parsed easily

                var packet = new DataPacket(packetCounter, packetOffset);
                var encodedPacket = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(packet));

                if (packetBytes > 0)
                {
                    if (encodedPacket.Length > packetBytes)
                    {
                        Console.WriteLine("Error: Packet getting concatenated!!    Data Size: " + encodedPacket.Length +
                                          " bytes    Packet Size:" + packetBytes + " bytes");
                        Console.WriteLine("     Data Size: " + encodedPacket.Length + " bytes");
                        Console.WriteLine("     Packet Size:" + packetBytes + " bytes");
                    }

                    Array.Resize(ref encodedPacket, packetBytes);
                }

                if (displaySpeed)
                {
                    displaySpeed = false;
                    Console.WriteLine("Data mbps: " +
                                      NetworkSpeedCalculator.mbps(encodedPacket.Length * 8, packetOffset));
                }

                _client.Send(encodedPacket, encodedPacket.Length);
                Thread.Sleep(packetOffset);
            }
        }
    }
}