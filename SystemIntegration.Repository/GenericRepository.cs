﻿using System;
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
            dbset.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges()==1?true:false;
        }
    }
}
