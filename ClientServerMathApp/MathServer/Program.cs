using System.Net;

namespace MathServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Any, 3000);
            var mathServer = new MathServer(ipEndPoint);
            mathServer.RunServer();
        }
    }
}
