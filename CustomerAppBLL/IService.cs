using System.Collections.Generic;

namespace RestAppBLL
{
    /// <summary>
    ///     Entity Interface for basic CRUD with DAL
    /// </summary>
    public interface IService<TEntity>
    {
        TEntity Create(TEntity entityToCreate);

        IList<TEntity> CreateAll(IList<TEntity> customers);

        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        bool Delete(int id);

        TEntity Update(TEntity entityToUpdate);
    }
}