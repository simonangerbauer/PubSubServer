using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Subscriber
{
    class Subscriber
    {
        Socket _client;
        EndPoint _remoteEndPoint;
        byte[] _data;
        int _recv;
        Boolean _isReceivingStarted = false;

        static void Main(string[] args)
        {
            Subscriber sub = new Subscriber();
        }

        public Subscriber()
        {
            string serverIP = "127.0.0.1";
            IPAddress serverIPAddress = IPAddress.Parse(serverIP);
            int serverPort = 10001;

            _client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _remoteEndPoint = new IPEndPoint(serverIPAddress, serverPort);

            string topicName = "topic";

            string Command = "Subscribe";

            string message = Command + "," + topicName;
            _client.SendTo(Encoding.ASCII.GetBytes(message), _remoteEndPoint);

            if (_isReceivingStarted == false)
            {
                _isReceivingStarted = true;
                _data = new byte[1024];
                Thread thread1 = new Thread(new ThreadStart(ReceiveDataFromServer))
                {
                    IsBackground = false
                };
                thread1.Start();
            }
        }


        void ReceiveDataFromServer()
        {
            EndPoint publisherEndPoint = _client.LocalEndPoint;
            while (true)
            {
                _recv = _client.ReceiveFrom(_data, ref publisherEndPoint);
                string msg = Encoding.ASCII.GetString(_data, 0, _recv) + "," + publisherEndPoint.ToString();
                Debug.WriteLine(msg);
            }
        }
    }
}
