using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Threading;

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

        public void Send(TimeSpan testLength, TimeSpan packetOffset)
        {
            ulong packetCounter = 0;
            var endTime = DateTime.Now.Add(testLength);
            while (endTime > DateTime.Now)
            {
                packetCounter++;


                // Todo: turn the packet into a json object with data that can be parsed easily

                var packet = new DataPacket(packetCounter, packetOffset);
                var encodedPacket = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(packet));


                _client.Send(encodedPacket, encodedPacket.Length);
                Thread.Sleep(packetOffset);
            }
        }

   
    }
}