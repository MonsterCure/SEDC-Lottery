using Lottery.Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lottery.Data
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected DbSet<T> DbSet;
        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        public void Insert(T entity)
        {
            DbSet.Add(entity);
            _dbContext.SaveChanges();   //we only save changes to DB here, beacuase we only have one transaction through this one insert, and we don't need the Unit of Work pattern
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

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }
    }
}
