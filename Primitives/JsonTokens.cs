using System;
namespace Primitives
{
    /// <summary>
    /// Json tokens.
    /// </summary>
    public static class JsonTokens
    {
        /// <summary>
        /// The topic.
        /// </summary>
        public const string Topic = "topic";
        /// <summary>
        /// The data.
        /// </summary>
        public const string Data = "data";
        /// <summary>
        /// The state.
        /// </summary>
        public const string State = "state";
        /// <summary>
        /// The end of message.
        /// </summary>
        public const string EndOfMessage = "^@\r";
    }
}
