using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using State;

namespace PubSubServer.Filtering
{
    /// <summary>
    /// Filtering layer for Publish/Subscribe System
    /// </summary>
    public static class Filter
    {
        /// <summary>
        /// Gets the subscribers for a topic.
        /// </summary>
        /// <value>The topic subscribers.</value>
        static private ConcurrentDictionary<string, ConcurrentDictionary<int, SocketState>> _topicSubscribers { get; } 
            = new ConcurrentDictionary<string, ConcurrentDictionary<int, SocketState>>();

        /// <summary>
        /// Adds the subscriber.
        /// </summary>
        /// <param name="topic">Topic.</param>
        /// <param name="subscriber">Subscriber.</param>
        static public void AddSubscriber(string topic, SocketState subscriber)
        {

            _topicSubscribers.AddOrUpdate(topic,
                (key) =>
                {
                    var values = new ConcurrentDictionary<int, SocketState>();
                    values.TryAdd(subscriber.Socket.RemoteEndPoint.GetHashCode(), subscriber);
                    return values;
                }
                ,
                (key, values) =>
                {
                    values.TryAdd(subscriber.Socket.RemoteEndPoint.GetHashCode(), subscriber);
                    return values;
                });
        }

        /// <summary>
        /// Gets the subscribers.
        /// </summary>
        /// <returns>The subscribers.</returns>
        /// <param name="topic">Topic.</param>
        static public IEnumerable<SocketState> GetSubscribers(string topic)
        {
            _topicSubscribers.TryGetValue(topic, out var subscribers);
            return subscribers?.Values;
        }

        /// <summary>
        /// Removes the subscriber.
        /// </summary>
        /// <param name="topic">Topic.</param>
        /// <param name="subscriber">Subscriber.</param>
        static public void RemoveSubscriber(string topic, SocketState subscriber)
        {
            if(_topicSubscribers.TryGetValue(topic, out var subscribers))
            {
                subscribers.TryRemove(subscriber.Socket.RemoteEndPoint.GetHashCode(), out var value);
            }
        }
    }
}
