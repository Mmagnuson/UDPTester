using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UDPTester
{
    public class UDPClient
    {
        private UdpClient client;
        private IPEndPoint server;

        public void Connect(string ipAddress, int sendPort)
        {
            try
            {
                client = new UdpClient();

                server = new IPEndPoint(IPAddress.Parse(ipAddress), sendPort);

                client.Connect(server);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Send(TimeSpan testLength, TimeSpan packetOffset)
        {
            ulong counter = 0;
            var endTime = DateTime.Now.Add(testLength);
            while (endTime > DateTime.Now)
            {
                counter++;

                var data = Encoding.ASCII.GetBytes("Packet: ," + Convert.ToString(counter) + ", Time: ," +
                                                   DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt") +
                                                   ", Seconds: ," + DateTime.Now.ToString("ss.fff"));

                client.Send(data, data.Length);
                Thread.Sleep(packetOffset);
            }
        }

        private void Receive()
        {
            bool done = false;

            while (!done)
            {
                var receivedData = client.Receive(ref server);
                Console.WriteLine(Encoding.ASCII.GetString(receivedData));
            }
        }
    }
}