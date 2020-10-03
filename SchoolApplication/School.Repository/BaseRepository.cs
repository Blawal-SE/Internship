using School.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private SchoolContext _context;

        private IDbSet<T> _dbset;

        public BaseRepository()
        {
            _context = new SchoolContext();
            _dbset = _context.Set<T>();

        }

        public bool Add(T obj)
        {
            try
            {
                _dbset.Add(obj);
                Save();
                
            }
            catch (Exception e)
            {

                return false;
            }
            return true;
        }

      

        public void Delete(int id)
        {
           T obj= _dbset.Find(id);
            _dbset.Remove(obj);
            Save();

        }

        public List<T> GetAll()
        {
            return _dbset.ToList();
        }

      
        public void Save()
        {
            _context.SaveChanges();
        }

        public  void Update(T obj)
        {
            // _context.Entry(obj).Entity = EntityState.Modified;
            // T aa = _dbset.Find(Id);
            // some logic
            //_dbset.Detach(aa);
            // repo.Update(a);
            // _dbset.AddOrUpdate(obj);
            //    _context.Entry(obj).State = EntityState.Added;
            _context.Set<T>().AddOrUpdate(obj);
            //var existingAttach = _dbset.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            //  _context.Entry(obj).CurrentValues.SetValues(obj);
            Save();
        }
       
      
    }
}
