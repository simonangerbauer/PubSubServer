using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using State;

namespace PubSubServer.Filtering
{
    public static class Filter
    {
        static private ConcurrentDictionary<string, ConcurrentDictionary<int, SocketState>> _topicSubscribers { get; } 
            = new ConcurrentDictionary<string, ConcurrentDictionary<int, SocketState>>();

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

        static public IEnumerable<SocketState> GetSubscribers(string topic)
        {
            _topicSubscribers.TryGetValue(topic, out var subscribers);
            return subscribers?.Values;
        }

        static public void RemoveSubscriber(string topic, SocketState subscriber)
        {
            if(_topicSubscribers.TryGetValue(topic, out var subscribers))
            {
                subscribers.TryRemove(subscriber.Socket.RemoteEndPoint.GetHashCode(), out var value);
            }
        }
    }
}
