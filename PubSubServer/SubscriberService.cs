using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Service;
using State;

namespace PubSubServer
{
    public class SubscriberService
    {
        private static ManualResetEvent _resetEvent = new ManualResetEvent(false);
        private readonly TaskService _taskService;

        public SubscriberService(TaskService taskService)
        {
            _taskService = taskService;
        }

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
            string message = string.Empty;
            SocketState state = (SocketState)result.AsyncState;

            int bytesRead = state.Socket.EndReceive(result);
            if (bytesRead > 0)
            {
                state.StringBuilder.Append(Encoding.ASCII.GetString(state.Buffer, 0, bytesRead));
                message = state.StringBuilder.ToString();
                if (!string.IsNullOrEmpty(message))
                {
                    Filtering.Filter.AddSubscriber(message, state);
                }
            }
        }
    }
}
