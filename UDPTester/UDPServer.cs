using System;
using System.Net;
using System.Text;

namespace UDPTester
{
    public class UdpServer
    {
        private System.Net.Sockets.UdpClient _server;
        private IPEndPoint _listenEndPoint;

        public void StartServer(int listenPort = 5000)
        {
            _server = new System.Net.Sockets.UdpClient(listenPort);
            _listenEndPoint = new IPEndPoint(IPAddress.Any, listenPort);
        }

        public void Receive()
        {

            ConsoleKeyInfo keyEventValue; //todo: fix the escape key with a thread.. 
            do
            {
                var receivedData = _server.Receive(ref _listenEndPoint);
                Console.WriteLine(Encoding.ASCII.GetString(receivedData));

               
         
            } while (true);
        }
    }
}