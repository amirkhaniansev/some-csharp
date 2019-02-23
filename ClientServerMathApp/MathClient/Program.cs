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
            var serverIpEndPoint = new IPEndPoint(IPAddress.Parse("185.114.38.138"), 3000);
            var mathClient = new MathClient(serverIpEndPoint);
            mathClient.Run();
        }
    }
}
