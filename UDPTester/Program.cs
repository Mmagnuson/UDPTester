using CommandLine;
using System;
using System.Net;

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
                        if (!IPAddress.TryParse(o.Client, out _))
                        {
                            Console.WriteLine("Not a valid server ip.");
                            return;
                        }

                        Console.WriteLine("Client Connecting To: " + o.Client);
                        Console.WriteLine("Port: " + o.Port);
                        Console.WriteLine("Test Period: " + o.Time + " Seconds");
                        Console.WriteLine("Packet Offset Period: " + o.PacketOffset.ToString() + " Milliseconds");

                        var client = new UdpClient();
                        client.Connect(o.Client, o.Port);

                        Console.WriteLine("Data Direction: Client > Server");
                        client.Send(TimeSpan.FromSeconds(Convert.ToDouble(o.Time)),
                            TimeSpan.FromMilliseconds(Convert.ToDouble(o.PacketOffset)), o.PacketSize);
                    }

                    if (o.Server && o.Client == string.Empty)
                    {
                        Console.WriteLine("Server Listening: ");
                        Console.WriteLine("Port: " + o.Port);
                        Console.WriteLine("Statistics:" + o.Statistics.ToString());

                        var server = new UdpServer();
                        server.StartServer(o.Port);

                        Console.WriteLine("Data Direction: Client > Server");
                        if (o.File != string.Empty) Console.WriteLine("Output File: " + o.File);

                        if (o.File != string.Empty)
                            server.Receive(o.File, o.Statistics);
                        else
                            server.Receive(o.Statistics);
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
            public double PacketOffset { get; set; }

            [Option('f', "file", Default = "", Required = false,
                HelpText = "Save output to file -f filename.log")]
            public string File { get; set; }

            [Option("Statistics", Default = false, Required = false,
                HelpText = "Display or save packet statistics.")]
            public bool Statistics { get; set; }

            [Option("PacketSize", Default = (int) -1, Required = false,
                HelpText = "Override the packet size and set number of bytes per packet. <= 0 is ignored.")]
            public int PacketSize { get; set; }
        }
    }
}