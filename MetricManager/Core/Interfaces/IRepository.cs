using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        T GetById(int Id);
        IList<T> GetByTimePeriod(DateTime fromTime, DateTime toTime);
        void Create(T item);
        void Update(T item);
        void Delete(int id);

    }
}
