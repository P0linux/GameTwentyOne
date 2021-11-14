using System;

namespace Client
{
    class Program
    {
        public const int localPort = 56502;
        public const int remotePort = 56501;


        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start");

            Client.Start(localPort, remotePort);
        }
    }
}
