using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Database;
using Primitives;

namespace Service
{
    /// <summary>
    /// Base service. Other entity services derive from this.
    /// </summary>
    public abstract class BaseService : IDisposable
    {
        /// <summary>
        /// Gets or sets the database context.
        /// </summary>
        /// <value>The database context.</value>
        protected DatabaseContext Context { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Service.BaseService"/> class.
        /// </summary>
        protected BaseService()
        {
            Context = new DatabaseContext();

            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }

        /// <summary>
        /// Get generic list of entities.
        /// </summary>
        /// <returns>The list of entities.</returns>
        /// <typeparam name="T">The type to get entities for.</typeparam>
        public IEnumerable<T> Get<T>() where T : Entity
        {
            var dBSet = Context.Set<T>();
            return dBSet.AsEnumerable();
        }

        /// <summary>
        /// Posts the entity.
        /// </summary>
        /// <returns>The entity.</returns>
        /// <param name="entity">Entity.</param>
        /// <param name="entityState">Entity state.</param>
        /// <typeparam name="T">The type of the entity.</typeparam>
        public async Task<(StateEnum state, T data)> PostAsync<T>(T entity, StateEnum entityState) where T : Entity
        {
            try
            {

                var dbSet = Context.Set<T>();
                var dbEntity = dbSet.SingleOrDefault(t => t.Id.ToString().ToLower() == entity.Id.ToString().ToLower());
                var state = StateEnum.Unchanged;

                if (dbEntity != null)
                {
                    if (entityState == StateEnum.Deleted)
                    {
                        Context.Remove(dbEntity);
                        state = StateEnum.Deleted;
                    }
                    else
                    {
                        if (entity.LastChange > dbEntity.LastChange)
                        {
                            Context.Update(entity);
                            state = StateEnum.Modified;
                        }
                        else
                        {
                            return (state, dbEntity);
                        }
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
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return (StateEnum.Unchanged, null);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
