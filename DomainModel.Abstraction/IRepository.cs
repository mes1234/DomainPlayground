using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doomain.Shared;

namespace Doomain.Abstraction
{
    /// <summary>
    /// Repository allow to access Entities
    /// </summary>
    /// <typeparam name="T">type of items in repository</typeparam>
    public interface IRepository<T>
        where T : IEntity
    {
        /// <summary>
        /// Add or update item to repository
        /// </summary>
        /// <param name="item">item to be added/updated</param>
        public Task AddOrUpdate(T item);

        /// <summary>
        /// Try to remove item from repository
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns></returns>
        public Task TryRemove(Guid id);

        /// <summary>
        /// Retrieve item from repository
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns></returns>
        public T Get(Guid id);

    }
}
