﻿using System;
using System.Collections.Concurrent;
using System.Threading;
using Data;
using Newtonsoft.Json.Linq;
using Primitives;
using State;

namespace PubSubServer
{
    public static class Queue
    {
        private static BlockingCollection<(Entity entity, SocketState socketState)> _queue = new BlockingCollection<(Entity, SocketState)>();

        public static void Enqueue(SocketState state)
        {
            try
            {
                JObject jObject = JObject.Parse(state.StringBuilder.ToString());
                var assembly = typeof(Entity).Assembly;
                var type = assembly.GetType(jObject.SelectToken(JsonTokens.Topic).ToString());
                var entity = (Entity)jObject.SelectToken(JsonTokens.Data).ToObject(type);
                _queue.Add((entity, state));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static (Entity entity, SocketState socketState) Dequeue()
        {
            return _queue.Take();
        }
    }
}