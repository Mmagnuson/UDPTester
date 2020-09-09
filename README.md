# UDPTester

UDP Tester (need a better name) is a simple application that allows for UDP packet reordering quantification between two computers. It's written in .NET Core allowing it to run on linux and windows machines. 






#Flags

UDPTester 1.0.1
Copyright (C) 2020 Matthew Magnuson

  -c, --client          (Default: ) Launch the tester in client mode specifying the remote IP. Ex: -c 192.168.0.10
  -s, --server          Launch the tester in server mode.
  -p                    (Default: 5000) Specify the port number.
  -t, --time            (Default: 60) Specify test period in seconds.
  -o, --PacketOffset    (Default: 100) Specify time between packets in milliseconds.
  -f, --file            (Default: ) Save output to file -f filename.log
  --help                Display this help screen.
  --version             Display version information.



#Examples
Example Usage 1:
In this example, to send packets from computer 1 to computer 2.  The test will run for 60 seconds (1 minute) and delay/space each packet by at least 5 milliseconds.

[Computer 1] 
UDPTester -c 10.20.2.249 -t 60 -o 5


[Computer 2]
UDPTester -s