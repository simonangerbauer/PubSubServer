using System;
using System.Threading;

namespace PubSubServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            PublishService publishService = new PublishService();
            publishService.Start();

            SubscriberService subscriberService = new SubscriberService();
            subscriberService.Start();
        }

    }
}
