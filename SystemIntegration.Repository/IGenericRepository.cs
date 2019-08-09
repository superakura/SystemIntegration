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
        bool Update(TEntity entity);
        bool Insert(TEntity entity);
        bool Delete(object id);
    }
}
