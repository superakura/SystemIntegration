using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIntegration.Models;

namespace SystemIntegration.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity:class
    {
        //private readonly SystemIntegrationEntities db=new SystemIntegrationEntities();
        //private readonly DbSet<TEntity> dbset = new SystemIntegrationEntities().Set<TEntity>();
        //public GenericRepository()
        //{
        //    db = new SystemIntegrationEntities();
        //    this.dbset = db.Set<TEntity>();
        //}

        private SystemIntegrationEntities db;
        private DbSet<TEntity> dbset;
        public GenericRepository(SystemIntegrationEntities content)
        {
            this.db = content;
            this.dbset = db.Set<TEntity>();
        }

        public void Delete(object id)
        {
            TEntity entity = dbset.Find(id);
            Delete(entity);
        }
        public virtual void Delete(TEntity entity)
        {
            if (db.Entry(entity).State==EntityState.Detached)
            {
                dbset.Attach(entity);
            }
            dbset.Remove(entity);
            db.SaveChanges();
        }

        public TEntity GetByID(object id)
        {
            return dbset.Find(id);
        }

        public IQueryable<TEntity> GetList()
        {
            return dbset;
        }

        public void Insert(TEntity entity)
        {
            dbset.Add(entity);
            db.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            dbset.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
