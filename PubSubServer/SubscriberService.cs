using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PubSubServer
{
    public class SubscriberService
    {
        public void Start()
        {
            Thread thread = new Thread(new ThreadStart(HostSubscriberService)) { IsBackground = false };
            thread.Start();
        }

        private void HostSubscriberService()
        {
            IPAddress ipV4 = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEP = new IPEndPoint(ipV4, 10001);
            Socket server = new Socket(AddressFamily.InterNetwork,
                                SocketType.Dgram, ProtocolType.Udp);
            server.Bind(localEP);
            StartListening(server);
        }

        private void StartListening(Socket server)
        {
            EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
             int recv = 0;
            byte[] data = new byte[1024];
            while (true)
            {

                recv = 0;
                data = new byte[1024];
                recv = server.ReceiveFrom(data, ref remoteEP);
                string messageSendFromClient = Encoding.ASCII.GetString(data, 0, recv);
                string[] messageParts = messageSendFromClient.Split(",".ToCharArray());

                if (!string.IsNullOrEmpty(messageParts[0]))
                {
                    switch (messageParts[0])
                    {
                        case "Subscribe":
                            if (!string.IsNullOrEmpty(messageParts[1]))
                            {
                                Filtering.Filter.AddSubscriber(messageParts[1], remoteEP);
                            }
                            break; 
                        case "UnSubscribe":

                            if (!string.IsNullOrEmpty(messageParts[1]))
                            {
                                Filtering.Filter.RemoveSubscriber(messageParts[1], remoteEP);
                            }
                            break;
                    }
                }
            }
        }
    }
}
