using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPEchoClientSendingMultipleRequest
{
    class Program
    {
        private static TcpClient _clientSocket = null;
        private static Stream _nstream = null;
        private static StreamWriter _sWriter = null;
        private static StreamReader _sReader = null;
        static void Main(string[] args)
        {
            try
            {
                // Create 3-way Handshake 

                // Step no : 1
                //TCP establish connection via its socket through request to server

                // Step no: 3 
                // when TCP connection is established then client send (bytes of data) to server
                // 1) we can use "localhost" , it mean our own local machine IP
                // 2) "127.0.0.1" loopback IP ( when Client and Server both used in a same machine)
                // 3) Hostname ( server machine IP name --> 192.168.1.33)- Client and server are on the different machine 
                // In client socket you should refer the server hostname in terms of IP address for this particular machine
                using (_clientSocket = new TcpClient("127.0.0.1", 6789))
                {
                    using (_nstream = _clientSocket.GetStream())
                    {
                        // Data will be flushed from the buffer to the stream after each write operation
                        _sWriter = new StreamWriter(_nstream) { AutoFlush = true };
                        Console.WriteLine("Client ready to send bytes of data to server...");
                        Console.WriteLine(" Your message send to server 100 times");
                        // Clients wants to send 5 times data to Server , it is need for loop
                        for (int i = 0; i < 100; i++)
                        {
                            Thread.Sleep(1000);
                            string clientMsg = "Zuhair";
                            Console.WriteLine("Current Date:" + DateTime.Now);
                            Console.WriteLine("Counting no:" + i);
                            // client ready to send (bytes of data) which has collected through user input
                            _sWriter.WriteLine(clientMsg);
                            // Step no: 6 ........................................
                            // Client recieved the (modified server Message) sent back by server to client 
                            // perform write operation 
                            _sReader = new StreamReader(_nstream);
                            string rdMsgFromServer = _sReader.ReadLine();
                            if (rdMsgFromServer != null)
                            {
                                Console.WriteLine(".....................................................");
                                Console.WriteLine("Client recieved Message from Server:" + rdMsgFromServer);
                                Console.WriteLine(".....................................................");

                            }
                            else
                            {
                                Console.WriteLine("Client recieved null message from Server ");
                            }
                        }
                        
                    }
                }
                Console.WriteLine("Press enter to stop the client!");
                Console.ReadKey();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
        }

    }
}
