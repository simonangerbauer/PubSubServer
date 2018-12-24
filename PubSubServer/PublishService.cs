using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PubSubServer
{
    public class PublishService
    {
        public void Start()
        {
            Thread thread = new Thread(new ThreadStart(HostPublisherService)) { IsBackground = false };
            thread.Start();
        }

        private void HostPublisherService()
        {
            IPAddress ipV4 = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEP = new IPEndPoint(ipV4, 10002);
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
                try
                {
                    recv = 0;
                    data = new byte[1024];
                    recv = server.ReceiveFrom(data, ref remoteEP);
                    string messageSendFromClient = Encoding.ASCII.GetString(data, 0, recv);
                    string[] messageParts = messageSendFromClient.Split(",".ToCharArray());
                    String command = messageParts[0];
                    String topicName = messageParts[1];
                    if (!string.IsNullOrEmpty(command))
                    {
                        if (messageParts[0] == "Publish")
                        {
                            if (!string.IsNullOrEmpty(topicName))
                            {
                                List<string> eventParts =
                                        new List<string>(messageParts);
                                eventParts.RemoveRange(0, 1);
                                string message = MakeCommaSeparatedString(eventParts);
                                IEnumerable<EndPoint> subscribers = Filtering.Filter.GetSubscribers(topicName);
                                WorkerThreadParameters workerThreadParameters = new WorkerThreadParameters
                                {
                                    Server = server,
                                    Message = message,
                                    Subscribers = subscribers
                                };

                                ThreadPool.QueueUserWorkItem(new WaitCallback(Publish),
                                                 workerThreadParameters);
                            }
                        }
                    }
                }
                catch
                { }
            }
        }

        public static void Publish(object stateInfo)
        {
            WorkerThreadParameters workerThreadParameters = (WorkerThreadParameters)stateInfo;
            Socket server = workerThreadParameters.Server;
            string message = workerThreadParameters.Message;
            IEnumerable<EndPoint> subsribers = workerThreadParameters.Subscribers;
            int messagelength = message.Length;

            if (subsribers != null)
            {
                foreach (EndPoint endPoint in subsribers)
                {
                    server.SendTo(Encoding.ASCII.GetBytes(message), messagelength, SocketFlags.None, endPoint);

                }
            }
        }

        private string MakeCommaSeparatedString(List<string> eventParts)
        {
            string message = string.Empty;
            foreach (string item in eventParts)
            {
                message = message + item + ",";

            }
            if (message.Length != 0)
            {
                message = message.Remove(message.Length - 1, 1);
            }
            return message;
        }
    }

    class WorkerThreadParameters
    {
        public Socket Server { get; set; }

        public string Message { get; set; }

        public IEnumerable<EndPoint> Subscribers { get; set; }
    }
}
