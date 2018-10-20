using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MathClient
{
    /// <summary>
    /// Class for client
    /// </summary>
    public class MathClient
    {
        /// <summary>
        /// Tcp client
        /// </summary>
        private TcpClient tcpClient;

        /// <summary>
        /// Udp client
        /// </summary>
        private UdpClient udpClient;

        /// <summary>
        /// Server IP endpoint
        /// </summary>
        private IPEndPoint serverIpEndPoint;

        /// <summary>
        /// Constructs new instance of Client class.
        /// </summary>
        /// <param name="serverIpEndPoint"> Server IP endpoint</param>
        public MathClient(IPEndPoint serverIpEndPoint)
        {
            this.serverIpEndPoint = serverIpEndPoint;
            this.tcpClient = new TcpClient();
            this.udpClient = new UdpClient();
        }

        /// <summary>
        /// Runs client.
        /// </summary>
        public void Run()
        {
            //Asking the user to choose service type: UDP or TCP
            while (true)
            {
                Console.WriteLine("Please,select protocol typing UDP or TCP...");
                Console.Write("ProtocolType: ");

                var protocolTypeInput = Console.ReadLine().ToLower();

                //if user entered "tcp" then start interactions with server using tcp
                if (protocolTypeInput == "tcp")
                {
                    this.RunTcp();break;
                }

                //otherwise if user entered "udp" then start interactions with server using udp
                else if (protocolTypeInput == "udp")
                {
                    this.RunUdp();break;
                }

                //if user didn't enter any of available service then ask him/her to repeat input process
                else
                {
                    Console.WriteLine("Invalid Input.");
                }
            }

        }

        /// <summary>
        /// Starts interactions with server using TCP.
        /// </summary>
        private void RunTcp()
        {
            //Connection to server with IP endpoiny
            this.tcpClient.Connect(this.serverIpEndPoint);

            //objects for processing and storing data
            var protocolText = "";
            var resultText = "";

            //stream helpers and providers
            var stream = this.tcpClient.GetStream();
            var writer = new BinaryWriter(stream);
            var reader = new BinaryReader(stream);

            try
            {
                while (this.tcpClient.Connected)
                {
                    //asking user to input operation in  defined protocol format
                    Console.WriteLine("Please write operation in the following format" + Environment.NewLine +
                        "operator: first_value:second_value");
                    protocolText = Console.ReadLine();


                    //if user entered "exit" then break the loop
                    if (protocolText.ToLower() == "exit")
                        break;

                    //sending protocol text to server
                    writer.Write(protocolText);

                    //receiving operation result from server
                    resultText = reader.ReadString();

                    //Printing result of operation in the console.
                    Console.WriteLine("Result of operation: " + resultText);
                }
            }
            catch(Exception)
            {
                Console.WriteLine("An error occured.");
            }
            finally
            {
                //closing connections
                stream.Close();
                writer.Close();
                reader.Close();
                this.tcpClient.Close();
            }
        }

        /// <summary>
        /// Starts client interactions with server using UDP
        /// </summary>
        private void RunUdp()
        {
            try
            {
                while (true)
                {
                    //asking user to input operation in  defined protocol format
                    Console.WriteLine("Please write operation in the following format" + Environment.NewLine +
                        "operator: first_value:second_value");

                    //Getting the input from console.
                    var protocolText = Console.ReadLine();

                    //if user entered "exit" then break the loop
                    if (protocolText.ToLower() == "exit")
                        break;

                    //Converting input to array of bytes
                    var buffer = Encoding.UTF8.GetBytes(protocolText);

                    //Sending protocol text to server
                    this.udpClient.Send(buffer, buffer.Length, this.serverIpEndPoint);

                    //Receiving result of operation from server
                    buffer = this.udpClient.Receive(ref this.serverIpEndPoint);

                    //Printing the result in the console.
                    Console.WriteLine("Result of operation: " + Encoding.UTF8.GetString(buffer));
                }
            }
            catch(Exception)
            {
                this.udpClient.Close();
            }
        }

        
    }
}