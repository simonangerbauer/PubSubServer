using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Primitives;
using State;

namespace Subscriber
{
    class Subscriber
    {
        Socket _client;
        EndPoint _remoteEndPoint;
        private static ManualResetEvent _connectDone = new ManualResetEvent(false);
        private static ManualResetEvent _sendDone = new ManualResetEvent(false);
        private static ManualResetEvent _receiveDone = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            Subscriber sub = new Subscriber();
            sub.Start();
        }

        private void Start()
        {
            try
            {
                string serverIP = "127.0.0.1";
                IPAddress serverIPAddress = IPAddress.Parse(serverIP);
                int serverPort = 10001;

                _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _remoteEndPoint = new IPEndPoint(serverIPAddress, serverPort);

                var message = "Data.Task";

                _client.BeginConnect(_remoteEndPoint, new AsyncCallback(ConnectCallback), _client);
                _connectDone.WaitOne();

                var byteData = Encoding.ASCII.GetBytes(message);
                _client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), _client);
                _sendDone.WaitOne();

                while (true)
                {
                    BeginReceive();
                    _receiveDone.Reset();
                    _receiveDone.WaitOne();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void BeginReceive()
        {
            try
            {
                var state = new SocketState(_client);
                _client.BeginReceive(state.Buffer, 0, SocketState.BufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                var state = (SocketState)result.AsyncState;
                int bytesRead = state.Socket.EndReceive(result);
                if(bytesRead > 0)
                {
                    var message = Encoding.ASCII.GetString(state.Buffer, 0, bytesRead);
                    state.StringBuilder.Append(message);
                    if (message.IndexOf(JsonTokens.EndOfMessage, StringComparison.Ordinal) == -1)
                    {
                        state.Socket.BeginReceive(state.Buffer, 0, SocketState.BufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallback), state);
                    }
                    else
                    {
                        if (state.StringBuilder.Length > 1)
                        {
                            Console.WriteLine("Received Message: " + state.StringBuilder.ToString());
                        }

                        _receiveDone.Set();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SendCallback(IAsyncResult result)
        {
            try
            {
                Socket client = (Socket)result.AsyncState;
                int bytes = client.EndSend(result);
                _sendDone.Set();
                Console.WriteLine($"Sent {bytes}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult result)
        {
            try
            {
                Socket client = (Socket)result.AsyncState;
                client.EndConnect(result);
                Console.WriteLine($"Socket connected to {client.RemoteEndPoint.ToString()}");
                _connectDone.Set();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
