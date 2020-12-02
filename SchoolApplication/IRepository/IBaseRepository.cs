using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        List<T> GetAll();
        bool Add(T obj);
        void Update(T obj);
        bool Delete(int id);
        bool AddRange(List<T> list);
        bool Delete(T obj);
        bool DeleteRange(List<T> list);
        void Save();

    }
}
