﻿using System;
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
            Context.Database.EnsureCreated();
        }

        public async Task<(StateEnum state, T data)> PostAsync<T>(T entity) where T : Entity
        {
            var dbSet = Context.Set<T>();
            var dbEntity = dbSet.SingleOrDefault(t => t.Id == entity.Id);
            var state = StateEnum.Unchanged;

            if (dbEntity != null)
            {
                if(entity.LastChange > dbEntity.LastChange)
                {
                    Context.Update(entity);
                    state = StateEnum.Modified;
                }
                else
                {
                    return (state, dbEntity);
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
