using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Primitives;
using Microsoft.EntityFrameworkCore;
using Database;

namespace Service
{
    /// <summary>
    /// Task service.
    /// </summary>
    public class TaskService : BaseService
    {
        /// <summary>
        /// Gets the tasks async.
        /// </summary>
        /// <returns>The tasks.</returns>
        public async Task<IEnumerable<Data.Task>> GetAsync()
        {
            return await Context.GetQuery<Data.Task>().OrderByDescending(t => t.Due).ToListAsync();
        }

        /// <summary>
        /// Posts the task async.
        /// </summary>
        /// <returns>The task.</returns>
        /// <param name="task">Task.</param>
        /// <param name="entityState">Entity state.</param>
        public async Task<(StateEnum state, Data.Task data)> PostAsync(Data.Task task, StateEnum entityState)
        {
            return await PostAsync<Data.Task>(task, entityState);
        }
    }
}
