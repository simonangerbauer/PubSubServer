using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Primitives;
using Service;
using State;

namespace PubSubServer
{
    public class PublisherService
    {
        private static ManualResetEvent _resetEvent = new ManualResetEvent(false);
        private readonly TaskService _taskService;

        public PublisherService(TaskService taskService)
        {
            _taskService = taskService;
        }

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

        public static void ReplyToSender(string message, SocketState socketState)
        {
            Send(message, new List<SocketState> { socketState });
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
            string message = string.Empty;
            SocketState state = (SocketState)result.AsyncState;
  
            int bytesRead = state.Socket.EndReceive(result);
            if (bytesRead > 0)
            {
                state.StringBuilder.Append(Encoding.ASCII.GetString(state.Buffer, 0, bytesRead));
                message = state.StringBuilder.ToString();
                if (message.IndexOf(JsonTokens.EndOfMessage, StringComparison.Ordinal) > -1)
                {
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}", message.Length, message);
                    Queue.Enqueue(state);
                }
                else
                { 
                    state.Socket.BeginReceive(state.Buffer, 0, SocketState.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
                }
            }
        }

        public static void Publish(string message, string topic)
        {
            IEnumerable<SocketState> subscribers = Filtering.Filter.GetSubscribers(topic);

            if (subscribers != null)
            {
                Send(message, subscribers);
            }
        }

        private static void Send(string message, IEnumerable<SocketState> recipients)
        {
            message += JsonTokens.EndOfMessage;
            var byteData = Encoding.ASCII.GetBytes(message);
            foreach (SocketState recipient in recipients)
            {
                recipient.Socket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(SendCallback), recipient.Socket);
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
