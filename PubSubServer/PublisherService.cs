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
    /// <summary>
    /// Service for publishers to send data to
    /// </summary>
    public class PublisherService
    {
        private static ManualResetEvent _resetEvent = new ManualResetEvent(false);
        private readonly TaskService _taskService;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PubSubServer.PublisherService"/> class.
        /// </summary>
        /// <param name="taskService">Task service.</param>
        public PublisherService(TaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Start this instance.
        /// </summary>
        public void Start()
        {
            Thread thread = new Thread(new ThreadStart(HostPublisherService)) { IsBackground = false };
            thread.Start();
        }

        /// <summary>
        /// Hosts the publisher service.
        /// </summary>
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

        /// <summary>
        /// Replies to sender if his message has been applied.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="socketState">Socket state.</param>
        public static void ReplyToSender(string message, SocketState socketState)
        {
            Send(message, new List<SocketState> { socketState });
        }

        /// <summary>
        /// Callback of the Accept of the socket
        /// </summary>
        /// <param name="result">Result.</param>
        private static void AcceptCallback(IAsyncResult result)
        {
            _resetEvent.Set();

            Socket listener = (Socket)result.AsyncState;
            Socket handler = listener.EndAccept(result);
            SocketState state = new SocketState(handler);
            handler.BeginReceive(state.Buffer, 0, SocketState.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
        }

        /// <summary>
        /// Callback of the Receive of the socket
        /// </summary>
        /// <param name="result">Result.</param>
        private static void ReceiveCallback(IAsyncResult result)
        {
            string message = string.Empty;
            SocketState state = (SocketState)result.AsyncState;

            int bytesRead = 0;
            try { 
                bytesRead = state.Socket.EndReceive(result);
            } catch (SocketException)
            {
                state.Socket.Disconnect(true);
            }

            if (bytesRead > 0)
            {
                state.StringBuilder.Append(Encoding.UTF8.GetString(state.Buffer, 0, bytesRead));
                message = state.StringBuilder.ToString();
                if (message.IndexOf(JsonTokens.EndOfMessage, StringComparison.Ordinal) > -1)
                {
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}", message.Length, message);
                    Queue.Enqueue(state);
                }
                state.Socket.BeginReceive(state.Buffer, 0, SocketState.BufferSize, 0,
                new AsyncCallback(ReceiveCallback), state);
            }
        }

        /// <summary>
        /// Publish the specified message to topic.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="topic">Topic.</param>
        public static void Publish(string message, string topic)
        {
            IEnumerable<SocketState> subscribers = Filtering.Filter.GetSubscribers(topic);

            if (subscribers != null)
            {
                Send(message, subscribers);
            }
        }

        /// <summary>
        /// Send the specified message to recipients.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="recipients">Recipients.</param>
        private static void Send(string message, IEnumerable<SocketState> recipients)
        {
            message += JsonTokens.EndOfMessage;
            var byteData = Encoding.UTF8.GetBytes(message);
            foreach (SocketState recipient in recipients)
            {
                recipient.Socket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(SendCallback), recipient.Socket);
            }
        }

        /// <summary>
        /// The Callback for the Send of the socket
        /// </summary>
        /// <param name="result">Result.</param>
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

        /// <summary>
        /// Makes the comma separated string.
        /// </summary>
        /// <returns>The comma separated string.</returns>
        /// <param name="eventParts">Event parts.</param>
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

    /// <summary>
    /// Worker thread parameters.
    /// </summary>
    class WorkerThreadParameters
    {
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        public Socket Server { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the subscribers.
        /// </summary>
        /// <value>The subscribers.</value>
        public IEnumerable<EndPoint> Subscribers { get; set; }
    }
}
