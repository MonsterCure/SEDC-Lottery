using Lottery.Data.Model;
using System.Linq;

namespace Lottery.Data
{
    public interface IRepository<T> where T : IEntity
    {
        void Insert(T entity);

        void Delete(T entity);

        //void Delete(int Id);

        IQueryable<T> GetAll();

        T GetById(int id);
    }
}
