using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIntegration.Models;

namespace SystemIntegration.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity:class
    {
        private SystemIntegrationEntities db;
        private DbSet<TEntity> dbset;
        public GenericRepository(SystemIntegrationEntities content)
        {
            this.db = content;
            this.dbset = db.Set<TEntity>();
        }

        public bool Delete(object id)
        {
            TEntity entity = dbset.Find(id);
            if (db.Entry(entity).State == EntityState.Detached)
            {
                dbset.Attach(entity);
            }
            dbset.Remove(entity);
            return db.SaveChanges()==1?true:false;
        }

        public TEntity GetByID(object id)
        {
            return dbset.Find(id);
        }

        public IQueryable<TEntity> GetList()
        {
            return dbset;
        }

        public bool Insert(TEntity entity)
        {
            dbset.Add(entity);
            return db.SaveChanges() == 1 ? true : false;
        }

        public bool Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }
            if (db.Entry(entity).State == EntityState.Detached)
            {
                HandleDetached(entity);
            }
            dbset.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges()==1?true:false;
        }

        private bool HandleDetached(TEntity entity)
        {
            var objectContext = ((IObjectContextAdapter)db).ObjectContext;
            var entitySet = objectContext.CreateObjectSet<TEntity>();
            var entityKey = objectContext.CreateEntityKey(entitySet.EntitySet.Name, entity);
            object foundSet;
            bool exists = objectContext.TryGetObjectByKey(entityKey, out foundSet);
            if (exists)
            {
                objectContext.Detach(foundSet); //从上下文中移除
            }
            return exists;
        }
    }
}
