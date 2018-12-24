using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;

namespace PubSubServer.Filtering
{
    public static class Filter
    {
        static private ConcurrentDictionary<string, ConcurrentDictionary<int, EndPoint>> _topicSubscribers { get; } 
            = new ConcurrentDictionary<string, ConcurrentDictionary<int, EndPoint>>();

        static public void AddSubscriber(string topic, EndPoint subscriber)
        {

            _topicSubscribers.AddOrUpdate(topic,
                (key) =>
                {
                    var values = new ConcurrentDictionary<int, EndPoint>();
                    values.TryAdd(subscriber.GetHashCode(), subscriber);
                    return values;
                }
                ,
                (key, values) =>
                {
                    values.TryAdd(subscriber.GetHashCode(), subscriber);
                    return values;
                });
        }

        static public IEnumerable<EndPoint> GetSubscribers(string topic)
        {
            _topicSubscribers.TryGetValue(topic, out var subscribers);
            return subscribers?.Values;
        }

        static public void RemoveSubscriber(string topic, EndPoint subscriber)
        {
            if(_topicSubscribers.TryGetValue(topic, out var subscribers))
            {
                subscribers.TryRemove(subscriber.GetHashCode(), out var value);
            }
        }
    }
}
