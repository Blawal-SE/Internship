using School.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace School.Data
{
   public class SchoolContext : DbContext
    {
        public SchoolContext() : base("name=School")
        {

        }
        public  DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        public virtual decimal? AddStudent(string name, string fname,string phone, string email, string dob, string password, string confirmpassword)
        {
          
            var name_param = new SqlParameter("@Name", name);
            var fname_param = new SqlParameter("@FNname", fname);
            var phone_param = new SqlParameter("@Phone", phone);
            var dob_param = new SqlParameter("@Dob", dob);
            var password_param = new SqlParameter("@Password", password);
            var confirmpassword_param = new SqlParameter("@ConfirmPassword", confirmpassword);
            var email_param = new SqlParameter("@Email", email);
            
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<Nullable<Decimal>>("AddStudent  @Name ,@FNname ,@Email ,@Phone, @Dob ,@Password ,@ConfirmPassword", name_param, fname_param, email_param, phone_param, dob_param, password_param, confirmpassword_param).First();
               
        }
        public virtual void AddCourses(int StudentId, List<int> courseslist)
        {

            try
            {
                if (courseslist != null)
                {
                    foreach (var item in courseslist)
                    {
                        var courseid_param = new SqlParameter("@CourseId",Convert.ToInt32(item));
                        var StudentId_param = new SqlParameter("@StudentId", StudentId);
                        ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<Nullable<decimal>>("AddCourse @CourseId,@StudentId",courseid_param, StudentId_param).First();
                    }
                  
                }
                
            }
            catch (Exception e)
            {
               
               
            }

        }

    }
}
