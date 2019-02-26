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
    public class TaskService : BaseService
    {
        public async Task<IEnumerable<Data.Task>> GetAsync()
        {
            return await Context.GetQuery<Data.Task>().OrderByDescending(t => t.Due).ToListAsync();
        }

        public async Task<(StateEnum state, Data.Task data)> PostAsync(Data.Task task)
        {
            return await PostAsync<Data.Task>(task);
        }
    }
}
