using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Primitives;
using Service;
using State;

namespace PubSubServer
{
    /// <summary>
    /// Service that accepts subscribers on a socket
    /// </summary>
    public class SubscriberService
    {
        private static ManualResetEvent _resetEvent = new ManualResetEvent(false);
        private readonly TaskService _taskService;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PubSubServer.SubscriberService"/> class.
        /// </summary>
        /// <param name="taskService">Task service.</param>
        public SubscriberService(TaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Start this instance on a new thread.
        /// </summary>
        public void Start()
        {
            Thread thread = new Thread(new ThreadStart(HostSubscriberService)) { IsBackground = false };
            thread.Start();
        }

        /// <summary>
        /// Hosts the subscriber service.
        /// </summary>
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

        /// <summary>
        /// Callback for the Accept of the socket
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
        /// Callback for the receive of the socket
        /// </summary>
        /// <param name="result">Result.</param>
        private static void ReceiveCallback(IAsyncResult result)
        {
            string message = string.Empty;
            SocketState state = (SocketState)result.AsyncState;

            int bytesRead = state.Socket.EndReceive(result);
            if (bytesRead > 0)
            {
                state.StringBuilder.Append(Encoding.UTF8.GetString(state.Buffer, 0, bytesRead));
                message = state.StringBuilder.ToString();
                if (!string.IsNullOrEmpty(message))
                {
                    var messageSplit = message.Split(",");
                    if (messageSplit[0] == "Subscribe")
                    {
                        Filtering.Filter.AddSubscriber(messageSplit[1], state);
                        SendInitialData(state);
                    }
                    else if (messageSplit[0] == "Resubscribe")
                    {
                        Filtering.Filter.AddSubscriber(messageSplit[1], state);

                    } 
                    else if(messageSplit[0] == "Unsubscribe")
                    {
                        Filtering.Filter.RemoveSubscriber(messageSplit[1], state);
                    }
                }
            }
            else
            {
                state.Socket.Disconnect(true);
            }
        }

        /// <summary>
        /// Sends the initial data to a first subscriber.
        /// </summary>
        /// <param name="state">State.</param>
        private static void SendInitialData(SocketState state)
        {
            var taskService = new TaskService();
            var tasks = taskService.Get<Data.Task>();
            foreach(var task in tasks)
            {
                JObject json =
                new JObject(
                    new JProperty(JsonTokens.State, StateEnum.Unchanged),
                    new JProperty(JsonTokens.Data, JObject.FromObject(task)),
                    new JProperty(JsonTokens.Topic, task.GetType().FullName));
                PublisherService.ReplyToSender(json.ToString(), state);
            }
        }
    }
}
