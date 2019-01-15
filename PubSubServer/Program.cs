using System;
using System.Threading;
using Service;

namespace PubSubServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var taskService = new TaskService();

            PublisherService publishService = new PublisherService(taskService);
            publishService.Start();

            SubscriberService subscriberService = new SubscriberService(taskService);
            subscriberService.Start();

            QueueWorker queueWorker = new QueueWorker(taskService);
            queueWorker.Start();
        }

    }
}
