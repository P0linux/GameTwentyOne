using System;

namespace Server
{
    class Program
    {
        public const int localPort = 56501;
        public const int remotePort = 56502;

        static void Main(string[] args)
        {
            var game = new Game();

            Server.OnGameStart += game.InitGame;
            Server.OnMessageReceived += game.ProcessMessage;

            Server.Start(localPort, remotePort);
        }
    }
}
