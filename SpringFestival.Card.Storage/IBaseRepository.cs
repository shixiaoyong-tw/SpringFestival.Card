using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpringFestival.Card.Entity;

namespace SpringFestival.Card.Storage
{
    public interface IBaseRepository<TEntity> where TEntity : RootEntity
    {
        Task Add(TEntity entity);

        Task Edit(TEntity entity);

        Task Delete(Guid id);

        Task<List<TEntity>> GetAll();

        Task<TEntity> Get(Guid id);
    }
}