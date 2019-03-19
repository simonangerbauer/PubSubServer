using System;
using System.Threading;
using Service;

namespace PubSubServer
{
    /// <summary>
    /// Main Program, that starts SubscriberService and PublisherService and QueueWorker
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
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
