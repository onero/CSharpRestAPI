using System.Collections.Generic;

namespace RestAppDAL.Interfaces
{
    /// <summary>
    ///     Interface for CRUD operations on a DB
    /// </summary>
    public interface IRepository<TEntity>
    {
        TEntity Create(TEntity entity);

        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        bool Delete(int id);
    }
}