using School.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IData
{
    public interface ISchoolContextt
    {
        DbSet<Course> Courses { get; set; }
        DbSet<RoleMapper> RoleMappers { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<StudentCourse> StudentCourses { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
        void AddCourses(int StudentId, List<int> courseslist);
        decimal? AddStudent(string name, string fname, string phone, string email, string dob, string password, string confirmpassword);

    }
}
