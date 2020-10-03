using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        List<T> GetAll();
        bool Add(T obj);
        void Update(T obj);
        void Delete(int id);

        
         void Save();

    }
}
