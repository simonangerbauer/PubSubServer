using System;
using System.Threading;
using Data;
using Newtonsoft.Json.Linq;
using Primitives;
using Service;

namespace PubSubServer
{
    /// <summary>
    /// Queue worker that works a queue of objects that need to be entered in the database.
    /// </summary>
    public class QueueWorker
    {
        private readonly TaskService _taskService;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PubSubServer.QueueWorker"/> class.
        /// </summary>
        /// <param name="taskService">Task service.</param>
        public QueueWorker(TaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Start this instance.
        /// </summary>
        public void Start()
        {
            System.Threading.Tasks.Task.Factory.StartNew(WorkQueueAsync);
        }

        /// <summary>
        /// Works the queue by dequeuing items and calling the service to enter it to the database.
        /// </summary>
        private async System.Threading.Tasks.Task WorkQueueAsync()
        {
            while(true)
            {
                (var entity, var entityState, var socketState) = Queue.Dequeue();
                if (entity is Task)
                {
                    (var state, var data) = await _taskService.PostAsync<Task>((Task)entity, entityState);
                    JObject json =
                        new JObject(
                            new JProperty(JsonTokens.State, state),
                            new JProperty(JsonTokens.Data, JObject.FromObject(data)),
                            new JProperty(JsonTokens.Topic, data.GetType().FullName));
                    switch (state)
                    {
                        case StateEnum.Unchanged:
                            {
                                PublisherService.ReplyToSender(json.ToString(),  socketState);
                                break;
                            }
                        case StateEnum.Deleted:
                        case StateEnum.Added:
                        case StateEnum.Modified:
                            {
                                PublisherService.ReplyToSender(json.ToString(), socketState);
                                PublisherService.Publish(json.ToString(), typeof(Task).FullName);
                                break;
                            }
                    }

                }
            }
        }
    }
}
