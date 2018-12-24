using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket client = new Socket(AddressFamily.InterNetwork,
                     SocketType.Dgram, ProtocolType.Udp);
            EndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10002);
            string topicName = "topic";
            string eventData = "test";
            string message = "Publish" + "," + topicName + "," + eventData;
            client.SendTo(Encoding.ASCII.GetBytes(message), endPoint);
        }
    }
}
