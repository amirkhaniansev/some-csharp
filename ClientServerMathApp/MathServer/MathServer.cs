using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MathServer
{
    /// <summary>
    /// Math Server class for doing mathematical operations and interacting with clients.
    /// </summary>
    public class MathServer
    {
        /// <summary>
        /// TCP listener
        /// </summary>
        private TcpListener tcpServer;

        /// <summary>
        /// UDP server
        /// </summary>
        private UdpClient udpServer;

        /// <summary>
        /// Server IP Endpoint
        /// </summary>
        private IPEndPoint ipEndPoint;

        /// <summary>
        /// Math Service for doing mathematical operations.
        /// </summary>
        private MathService mathService;

        /// <summary>
        /// Constructs new instance of MathServer class.
        /// </summary>
        /// <param name="ipEndPoint"> Server IP endpoint.</param>
        public MathServer(IPEndPoint ipEndPoint)
        {
            this.ipEndPoint = ipEndPoint;           
            this.tcpServer = new TcpListener(this.ipEndPoint);
            this.udpServer = new UdpClient(this.ipEndPoint);
            this.mathService = new MathService();
        }

        /// <summary>
        /// Runs server
        /// </summary>
        public void RunServer()
        {
            Console.WriteLine("Server started running on ip: {0} at {1}", this.ipEndPoint.Address,
                DateTime.Now.ToString());

            //Running interaction working with TCP and UDP in separate tasks
            var tcpTask = Task.Run(() => this.RunTcpServer());
            var udpTask = Task.Run(() => this.RunUdpServer());
            Task.WaitAll(tcpTask, udpTask);
        }

        /// <summary>
        /// Runs tcp server.
        /// </summary>
        private void RunTcpServer()
        {
            //starting tcp listener
            this.tcpServer.Start();

            while (true)
            {
                //Accepting tcp clients
                var tcpClient = this.tcpServer.AcceptTcpClient();
                Console.WriteLine("Client with tcp has connected to server.IP: {0} at {1} ",
                    ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address, DateTime.Now);

                //Working with every tcp client in a separate task
                Task.Run(() =>
                {
                    //strings for proccesing and storing data
                    var protocolText = "";
                    var resultText = "";

                    //Stream helpers and providers
                    var stream = tcpClient.GetStream();
                    var writer = new BinaryWriter(stream);
                    var reader = new BinaryReader(stream);

                    while (tcpClient.Connected)
                    {
                        //Receiving protocol text
                        protocolText = reader.ReadString();

                        //Calculating operation result.
                        resultText = this.GetOperationResult(protocolText);

                        //Sending operation result to tcp client
                        writer.Write(resultText);
                    }

                    //closing connections
                    stream.Close();
                    writer.Close();
                    reader.Close();
                    tcpClient.Close();
                });
            }
        }

        /// <summary>
        /// Runs udp server
        /// </summary>
        private void RunUdpServer()
        {
            while (true)
            {
                //Making current thread to sleep for preventing CPU core 100% load
                Thread.Sleep(100);


                //Working with every client in a separate task
                Task.Run(() =>
                {
                    while (true)
                    {
                        var clientIp = null as IPEndPoint;

                            //receiving protocol text from client
                            var buffer = this.udpServer.Receive(ref clientIp);

                            //Converting array of bytes to string
                            var protocolText = Encoding.UTF8.GetString(buffer);

                            //calculating operation result
                            var resultText = this.GetOperationResult(protocolText);

                            //sending operation result to client
                            buffer = Encoding.UTF8.GetBytes(resultText);
                        this.udpServer.Send(buffer, buffer.Length, clientIp);
                    }

                });

            }
        }

        /// <summary>
        /// Gets operation result.
        /// </summary>
        /// <param name="protocolText"> Protocol text.</param>
        /// <returns> Returns operation result in a  string format. </returns>
        private string GetOperationResult(string protocolText)
        {
            //this is in try catch because protocolText can be inconvertible to ProtocolInfo
            try
            {
                //Constructing instance of ProtocolInfo from protocol text
                var protocolInfo = ProtocolParser.Parse(protocolText);

                var result = 0.0;

                switch (protocolInfo.Operator)
                {
                    case '+':
                        result = this.mathService.Add(protocolInfo.FirstValue, protocolInfo.SecondValue);
                        break;
                    case '-':
                        result = this.mathService.Sub(protocolInfo.FirstValue, protocolInfo.SecondValue);
                        break;
                    case '/':
                        //this slice of code is in try catch because there is probabality that
                        //user will attempt to divide number by zero
                        //we need to process that case
                        try
                        {
                            result = this.mathService.Div(protocolInfo.FirstValue, protocolInfo.SecondValue);
                        }
                        catch (DivideByZeroException ex)
                        {
                            result = 0.0;
                        }

                        break;
                    case '*':
                        result = this.mathService.Mult(protocolInfo.FirstValue, protocolInfo.SecondValue);
                        break;
                }

                //if user attempted to divide number by 0
                //then inform him/her about the impossibility of operation
                if (result == 0.0)
                {
                    return "Attempt to divide by 0";
                }

                return result.ToString();
            }
            catch (FormatException ex)
            {
                //if protocol info text is not convertible to ProtocolInfo
                //return exception message
                return ex.Message;
            }
        }

    }
}
