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
            var writer = new StreamWriter(outputFile) {AutoFlush = true};

            ConsoleKeyInfo keyEventValue; //todo: fix the escape key with a thread..
            do
            {
                var receivedData = _server.Receive(ref _listenEndPoint);

                if (outputFile != string.Empty)
                {
                    writer.WriteLine(Encoding.ASCII.GetString(receivedData));
                }

                Console.WriteLine(Encoding.ASCII.GetString(receivedData));

               
         
            } while (true);
        }
    }
}