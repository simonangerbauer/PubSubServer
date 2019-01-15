using System;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Database;
using Primitives;

namespace Service
{
    public abstract class BaseService : IDisposable
    {
        protected DatabaseContext Context { get; set; }

        protected BaseService()
        {
            Context = new DatabaseContext();
        }

        public IQueryable<T> GetQuery<T>() where T : Entity
        {
            return Context.Query<T>().AsQueryable();
        }


        public async Task<(StateEnum state, T data)> PostAsync<T>(T entity) where T : Entity
        {
            var dbTask = GetQuery<T>().SingleOrDefault(t => t.Id == entity.Id);
            var state = StateEnum.Unchanged;

            if (dbTask != null)
            {
                if(entity.LastChange > dbTask.LastChange)
                {
                    Context.Update(entity);
                    state = StateEnum.Modified;
                }
                else
                {
                    return (state, dbTask);
                }
            }
            else
            {
                await Context.AddAsync(entity);
                state = StateEnum.Added;
            }

            await Context.SaveChangesAsync();
            return (state, entity);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
