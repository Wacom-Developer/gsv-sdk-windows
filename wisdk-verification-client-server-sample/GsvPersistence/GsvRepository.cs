using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using GsvPersistence.Interfaces;

namespace GsvPersistence
{
    /// <summary>
    /// The GSV Data Repository
    /// </summary>
    class GsvRepository : IGsvRepository
    {
        private GsvDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">The DbContext</param>
        public GsvRepository(GsvDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adds an entity to the repository.
        /// </summary>
        /// <typeparam name="T">The type of the entitity.</typeparam>
        /// <param name="entry">The entity itself.</param>
        /// <returns>The created entity</returns>
        public async Task<T> AddAsync<T>(T entry) where T : class
        {
            var dbSet = _dbContext.Set<T>();

            var addedEntity = await dbSet.AddAsync(entry);
            await _dbContext.SaveChangesAsync();
            return addedEntity.Entity;
        }

        /// <summary>
        /// Gets an entity by id.
        /// </summary>
        /// <typeparam name="T">The type of the entitity.</typeparam>
        /// <param name="id">The entity id.</param>
        /// <returns>The retrieved entity.</returns>
        public Task<T> GetByIdAsync<T>(string id) where T : class
        {
            var dbSet = _dbContext.Set<T>();

            return Task.FromResult(dbSet.AsEnumerable().FirstOrDefault(el => (el as IEntity).Id.Equals(id)));
        }

        /// <summary>
        /// Gets an entity by name.
        /// </summary>
        /// <typeparam name="T">The type of the entitity.</typeparam>
        /// <param name="name">The name for the db query.</param>
        /// <returns>The retrieved entity.</returns>
        public Task<T> GetByNameAsync<T>(string name) where T : class
        {
            var dbSet = _dbContext.Set<T>();

            return Task.FromResult(dbSet.AsEnumerable().FirstOrDefault(el => (el as IEntity).Name.Equals(name)));
        }

        /// <summary>
        /// Gets all entities from a single DbSet.
        /// </summary>
        /// <typeparam name="T">The type of the entitity.</typeparam>
        /// <returns>All entities from a single DbSet.</returns>
        public Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            var dbSet = _dbContext.Set<T>();

            return Task.FromResult(dbSet.AsEnumerable());
        }

        /// <summary>
        /// Removes an entity from the database.
        /// </summary>
        /// <typeparam name="T">The type of the entitity.</typeparam>
        /// <param name="id">The entity id.</param>
        /// <returns>Task</returns>
        public async Task RemoveAsync<T>(string id) where T : class
        {
            var templates = await GetAllAsync<T>();
            var templateToRemove = templates
                .Where(el => (el as IEntity).Id.Equals(id))
                .FirstOrDefault();

            var dbSet = _dbContext.Set<T>();
            dbSet.Remove(templateToRemove);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an entity in the database
        /// </summary>
        /// <typeparam name="T">The type of the entitity.</typeparam>
        /// <param name="objectToUpdate">The entity to be updated.</param>
        /// <returns></returns>
        public Task<T> UpdateAsync<T>(T objectToUpdate) where T : class
        {
            var dbSet = _dbContext.Set<T>();

            var updatedEntity = dbSet.Update(objectToUpdate);
            updatedEntity.State = EntityState.Modified;
            _dbContext.SaveChanges();
            return Task.FromResult(updatedEntity.Entity);
        }
    }
}
