using System;
using System.Collections.Concurrent;
using System.Threading;
using Data;
using Newtonsoft.Json.Linq;
using Primitives;
using State;

namespace PubSubServer
{
    /// <summary>
    /// Queue of items to enter into the database.
    /// </summary>
    public static class Queue
    {
        /// <summary>
        /// The queue of entities with their entity states and their sender socket states
        /// Collection blocks on dequeue if no element is in it
        /// </summary>
        private static BlockingCollection<(Entity entity, StateEnum entityState, SocketState socketState)> _queue = new BlockingCollection<(Entity, StateEnum, SocketState)>();

        /// <summary>
        /// Enqueue an object from a sender socket.
        /// </summary>
        /// <param name="state">State.</param>
        public static void Enqueue(SocketState state)
        {
            try
            {
                var message = state.StringBuilder.ToString();
                message = message.Substring(0, message.Length - JsonTokens.EndOfMessage.Length);
                JObject jObject = JObject.Parse(message);
                var assembly = typeof(Entity).Assembly;
                var type = assembly.GetType(jObject.SelectToken(JsonTokens.Topic).ToString());
                var entityState = (StateEnum)int.Parse(jObject.SelectToken(JsonTokens.State).ToString());
                var entity = (Entity)jObject.SelectToken(JsonTokens.Data).ToObject(type);
                _queue.Add((entity, entityState, state));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Dequeue an item.
        /// </summary>
        /// <returns>The item.</returns>
        public static (Entity entity, StateEnum entityState, SocketState socketState) Dequeue()
        {
            return _queue.Take();
        }
    }
}
