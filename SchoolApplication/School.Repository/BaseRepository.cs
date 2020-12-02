using IData;
using IRepository;
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
        private readonly ISchoolContextt _context;

        private readonly IDbSet<T> _dbset;
        public BaseRepository(ISchoolContextt context)
        {
            _context = context;
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

        public bool Delete(int id)
        {
            try
            {
                T obj = _dbset.Find(id);
                _context.Set<T>().Remove(obj);
                Save();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Delete(T obj)
        {
            _dbset.Remove(obj);
            Save();
            return true;
        }
        public bool AddRange(List<T> list)
        {
            foreach (var item in list)
            {
                _dbset.Add(item);
            }
            Save();
            return true;
        }
        public bool DeleteRange(List<T> list)
        {
            foreach (var item in list)
            {
                _dbset.Remove(item);
            }
            Save();
            return true;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Update(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public List<T> GetAll()
        {
            return _dbset.ToList();
        }

    }
}
