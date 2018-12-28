using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;
using State;

namespace PubSubServer
{
    public class PublishService
    {
        private static ManualResetEvent _resetEvent = new ManualResetEvent(false);

        public void Start()
        {
            Thread thread = new Thread(new ThreadStart(HostPublisherService)) { IsBackground = false };
            thread.Start();
        }

        private void HostPublisherService()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endpoint = new IPEndPoint(ip, 10002);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(endpoint);
                listener.Listen(100);

                while(true)
                {
                    // Set the event to nonsignaled state.  
                    _resetEvent.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("PublisherService: Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback), listener);

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
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}", message.Length, message);
                    //TODO json parsing
                    //JObject json = JObject.Parse(message);
                    //json.Fir
                    Publish(state.Socket, message, "topic");
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

        public static void Publish(Socket handler, String message, String topic)
        {
            IEnumerable<SocketState> subscribers = Filtering.Filter.GetSubscribers(topic);
            var byteData = Encoding.ASCII.GetBytes(message);

            if (subscribers != null)
            {
                foreach (SocketState subscriber in subscribers)
                {
                    subscriber.Socket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(SendCallback), subscriber.Socket);

                }
            }
        }

        private static void SendCallback(IAsyncResult result)
        {
            try
            {
                Socket handler = (Socket)result.AsyncState;
                int bytes = handler.EndSend(result);
                Console.WriteLine($"Sent {bytes} Bytes to client {handler.RemoteEndPoint.ToString()}.");
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
