using System;
using System.Net.Sockets;
using System.Text;

namespace State
{
    public class SocketState
    {
        public const int BufferSize = 1024;

        public SocketState(Socket socket)
        {
            Socket = socket;
            StringBuilder = new StringBuilder();
            Buffer = new byte[BufferSize];
        }

        public Socket Socket { get; set; }

        public StringBuilder StringBuilder { get; set; }

        public byte[] Buffer { get; set; }


    }
}
