using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Primitives;
using State;

namespace Publisher
{
    class Publisher
    {
        Socket _client;
        EndPoint _remoteEndPoint;
        private static ManualResetEvent _connectDone = new ManualResetEvent(false);
        private static ManualResetEvent _sendDone = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            Publisher pub = new Publisher();
            pub.Start();
        }

        private void Start()
        {
            try
            {
                string serverIP = "127.0.0.1";
                IPAddress serverIPAddress = IPAddress.Parse(serverIP);
                int serverPort = 10002;

                _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _remoteEndPoint = new IPEndPoint(serverIPAddress, serverPort);

                string message = @"{
  ""data"": {
    ""Proofs"": [
      {
        ""Title"": ""proof1"",
        ""Id"": ""a3480020-8431-479f-a31f-23424f847aa6"",
        ""LastChange"": ""0001-01-01T00:00:00""
      },
      {
        ""Title"": ""proof2"",
        ""Id"": ""15d705b9-bab8-4d5f-ae9f-76cc9861491d"",
        ""LastChange"": ""0001-01-01T00:00:00""
      }
    ],
    ""Officers"": ""ich, du"",
    ""Activity"": ""asokdaosk"",
    ""Description"": ""asodkao"",
    ""Due"": ""2019-01-14T16:29:41.913735+01:00"",
    ""Title"": ""task"",
    ""Progress"": 22,
    ""Id"": ""1bf18da9-aaa5-4d8f-9507-8ad17ac04bc9"",
    ""LastChange"": ""0001-01-01T00:00:00""
  },
  ""topic"": ""Data.Task""
}" + JsonTokens.EndOfMessage;

                _client.BeginConnect(_remoteEndPoint, new AsyncCallback(ConnectCallback), _client);
                _connectDone.WaitOne();

                var byteData = Encoding.ASCII.GetBytes(message);
                _client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), _client);
                _sendDone.WaitOne();

                _client.Shutdown(SocketShutdown.Both);
                _client.Close();
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
