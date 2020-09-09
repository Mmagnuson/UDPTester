using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPTester
{
    public class UdpServer
    {
        public void Listen(int listenPort = 5000)
        {
            var done = false;

            using var listener = new UdpClient(listenPort);
            var listenEndPoint = new IPEndPoint(IPAddress.Any, listenPort);


            while (!done)
            {
                var receivedData = listener.Receive(ref listenEndPoint);
                Console.WriteLine(Encoding.ASCII.GetString(receivedData));
            }
        }
    }
}