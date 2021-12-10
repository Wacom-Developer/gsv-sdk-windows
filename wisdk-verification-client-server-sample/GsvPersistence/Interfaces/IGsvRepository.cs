using System.Collections.Generic;
using System.Threading.Tasks;

namespace GsvPersistence.Interfaces
{
    /// <summary>
    /// Interface for the GSV Repository
    /// </summary>
    public interface IGsvRepository
    {
        /// <summary>
        /// Adds 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry"></param>
        /// <returns></returns>
        Task<T> AddAsync<T>(T entry) where T : class;

        Task<T> GetByIdAsync<T>(string id) where T : class;

        Task<T> GetByNameAsync<T>(string name) where T : class;

        Task<IEnumerable<T>> GetAllAsync<T>() where T : class;

        Task<T> UpdateAsync<T>(T objectToUpdate) where T : class;

        Task RemoveAsync<T>(string id) where T : class;
    }
}
