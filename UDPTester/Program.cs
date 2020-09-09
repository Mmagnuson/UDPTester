using System;
using System.Net;
using CommandLine;

namespace UDPTester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    if (o.Client != string.Empty && !o.Server)
                    {
                        if (!IPAddress.TryParse(o.Client, out var address))
                        {
                            Console.WriteLine("Not a valid server ip.");
                            return;
                        }

                        Console.WriteLine("Client Connecting To: " + o.Client);
                        Console.WriteLine("Port: " + o.Port);
                        Console.WriteLine("Test Period: " + o.Time + " Seconds");
                        Console.WriteLine("Packet Offset Period: " + o.PacketOffset + " Milliseconds");
                        Console.WriteLine("Data Direction: Client > Server");

                        var client = new UDPClient();
                        client.Connect(o.Client, o.Port);
                        client.Send(TimeSpan.FromSeconds(Convert.ToDouble(o.Time)),  TimeSpan.FromMilliseconds(Convert.ToDouble(o.PacketOffset)));
                    }

                    if (o.Server && o.Client == string.Empty)
                    {
                        Console.WriteLine("Server Listening: ");
                        Console.WriteLine("Port: " + o.Port);

                        var server = new UdpServer();
                        server.Listen(o.Port);
                    }
                });
        }

        public class Options
        {
            [Option('c', "client", Default = "", Required = false,
                HelpText = "Launch the tester in client mode specifying the remote IP. Ex: -c 192.168.0.10")]
            public string Client { get; set; }

            [Option('s', "server", Required = false, HelpText = "Launch the tester in server mode.")]
            public bool Server { get; set; }

            [Option('p', Default = 5000, Required = false, HelpText = "Specify the port number.")]
            public int Port { get; set; }

            [Option('t', "time", Default = 60, Required = false, HelpText = "Specify test period in seconds.")]
            public int Time { get; set; }

            [Option('o', "PacketOffset", Default = 100, Required = false,
                HelpText = "Specify time between packets in milliseconds.")]
            public int PacketOffset { get; set; }
        }
    }
}