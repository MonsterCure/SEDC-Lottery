using Lottery.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Data
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected DbSet<T> DbSet;

        public Repository(DbContext dbContext)
        {
            DbSet = dbContext.Set<T>();
        }

        public void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        //public void Delete(int id)
        //{
        //    var dtoObject = DbSet.FirstOrDefault(item => item.Id == id);
        //    DbSet.Remove(dtoObject);
        //}

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(int Id)
        {
            return DbSet.Find(Id);
        }
    }
}
