using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using State;

namespace PubSubServer
{
    public class SubscriberService
    {
        private static ManualResetEvent _resetEvent = new ManualResetEvent(false);

        public void Start()
        {
            Thread thread = new Thread(new ThreadStart(HostSubscriberService)) { IsBackground = false };
            thread.Start();
        }

        private void HostSubscriberService()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endpoint = new IPEndPoint(ip, 10001);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(endpoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    _resetEvent.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("SubscriberService: Waiting for a connection...");
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    // Wait until a connection is made before continuing.  
                    _resetEvent.WaitOne();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private static void AcceptCallback(IAsyncResult result)
        {
            _resetEvent.Set();

            Socket listener = (Socket)result.AsyncState;
            Socket handler = listener.EndAccept(result);
            SocketState state = new SocketState(handler);
            handler.BeginReceive(state.Buffer, 0, SocketState.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);

        }


        private static void ReceiveCallback(IAsyncResult result)
        {
            String message = String.Empty;
            SocketState state = (SocketState)result.AsyncState;

            int bytesRead = state.Socket.EndReceive(result);
            if (bytesRead > 0)
            {
                state.StringBuilder.Append(Encoding.ASCII.GetString(state.Buffer, 0, bytesRead));
                message = state.StringBuilder.ToString();
                if (message.IndexOf("^@", StringComparison.Ordinal) > -1)
                {
                    string[] messageParts = message.Split(",".ToCharArray());
                    if (!string.IsNullOrEmpty(messageParts[0]))
                    {
                        switch (messageParts[0])
                        {
                            case "Subscribe":
                                if (!string.IsNullOrEmpty(messageParts[1]))
                                {
                                    Filtering.Filter.AddSubscriber(messageParts[1], state);
                                }
                                break;
                            case "UnSubscribe":

                                if (!string.IsNullOrEmpty(messageParts[1]))
                                {
                                    Filtering.Filter.RemoveSubscriber(messageParts[1], state);
                                }
                                break;
                        }
                    }
                }
                else
                {
                    state.Socket.BeginReceive(state.Buffer, 0, SocketState.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
                }
            }
        }


        //private void StartListening(Socket server)
        //{
        //    EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
        //    int recv = 0;
        //    byte[] data = new byte[1024];
        //    while (true)
        //    {
        //        try
        //        {
        //            recv = 0;
        //            data = new byte[1024];
        //            recv = server.ReceiveFrom(data, ref remoteEP);
        //            string messageSendFromClient = Encoding.ASCII.GetString(data, 0, recv);
        //            string[] messageParts = messageSendFromClient.Split(",".ToCharArray());
        //            String command = messageParts[0];
        //            String topicName = messageParts[1];
        //            if (!string.IsNullOrEmpty(command))
        //            {
        //                if (messageParts[0] == "Publish")
        //                {
        //                    if (!string.IsNullOrEmpty(topicName))
        //                    {
        //                        List<string> eventParts =
        //                                new List<string>(messageParts);
        //                        eventParts.RemoveRange(0, 1);
        //                        string message = MakeCommaSeparatedString(eventParts);
        //                        IEnumerable<EndPoint> subscribers = Filtering.Filter.GetSubscribers(topicName);
        //                        WorkerThreadParameters workerThreadParameters = new WorkerThreadParameters
        //                        {
        //                            Server = server,
        //                            Message = message,
        //                            Subscribers = subscribers
        //                        };

        //                        ThreadPool.QueueUserWorkItem(new WaitCallback(Publish),
        //                                         workerThreadParameters);
        //                    }
        //                }
        //            }
        //        }
        //        catch
        //        { }
        //    }
        //}
    }
}
