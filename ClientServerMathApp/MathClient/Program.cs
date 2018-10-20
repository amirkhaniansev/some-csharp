using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MathClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var serverIpEndPoint = new IPEndPoint(IPAddress.Loopback, 3000);
            var mathClient = new MathClient(serverIpEndPoint);
            mathClient.Run();
        }
    }
}
