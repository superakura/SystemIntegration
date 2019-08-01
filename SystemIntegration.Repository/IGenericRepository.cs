using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemIntegration.Repository
{
    public interface IGenericRepository<TEntity>
    {
        IQueryable<TEntity> GetList();
        TEntity GetByID(object id);
        void Update(TEntity entity);
        void Insert(TEntity entity);
        void Delete(object id);
    }
}
