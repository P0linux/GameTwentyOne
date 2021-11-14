using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class Server
    {
        private static int _localPort;
        private static int _remotePort;

        private static Socket _socket;

        public static Action<String> OnMessageReceived;
        public static Action OnGameStart;

        private static bool IsServerRunning;
        

        public static void Start(int localPort, int remotePort)
        {
            try
            {
                Init(localPort, remotePort);

                Task listeningTask = new Task(Listen);
                listeningTask.Start();

                OnGameStart.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
            }
            
        }

        private static void Init(int localPort, int remotePort)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IsServerRunning = true;

            _localPort = localPort;
            _remotePort = remotePort;
        }

        public static void Send(string message)
        {
            var data = Encoding.Unicode.GetBytes(message);
            EndPoint remotePoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), _remotePort);
            _socket.SendTo(data, remotePoint);
        }

        public static void Listen()
        {
            try
            {
                // Port to listen on
                IPEndPoint localIp = new IPEndPoint(IPAddress.Parse("127.0.0.1"), _localPort);
                _socket.Bind(localIp);

                while (IsServerRunning)
                {
                    StringBuilder builder = new StringBuilder();

                    int bytesCount = 0; // amount of received bytes
                    byte[] data = new byte[256]; //buffer for received data

                    //Address to receive from
                    EndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0);

                    do
                    {
                        bytesCount = _socket.ReceiveFrom(data, ref remoteIp);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytesCount));
                    }
                    while (_socket.Available > 0);

                    OnMessageReceived.Invoke(builder.ToString());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Close()
        {
            if (_socket != null)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
                _socket = null;
            }
        }

        public static void Stop()
        {
            Close();
            IsServerRunning = false;
        }
    }
}
