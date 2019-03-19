using System;
using System.Net.Sockets;
using System.Text;

namespace State
{
    /// <summary>
    /// State of a socket
    /// </summary>
    public class SocketState
    {
        /// <summary>
        /// The size of the buffer.
        /// </summary>
        public const int BufferSize = 1024;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:State.SocketState"/> class.
        /// </summary>
        /// <param name="socket">Socket.</param>
        public SocketState(Socket socket)
        {
            Socket = socket;
            StringBuilder = new StringBuilder();
            Buffer = new byte[BufferSize];
        }

        /// <summary>
        /// Gets or sets the socket.
        /// </summary>
        /// <value>The socket.</value>
        public Socket Socket { get; set; }

        /// <summary>
        /// Gets or sets the string builder.
        /// </summary>
        /// <value>The string builder.</value>
        public StringBuilder StringBuilder { get; set; }

        /// <summary>
        /// Gets or sets the buffer.
        /// </summary>
        /// <value>The buffer.</value>
        public byte[] Buffer { get; set; }


    }
}
