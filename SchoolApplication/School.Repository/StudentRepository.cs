using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using School.Data.Models;


namespace school.WebApi
{
    public sealed class StudentRepository
    {
        #region  Singleleton pattern

        private static readonly StudentRepository instance = new StudentRepository();
        // Explicit static constructor to tell C# compiler  
        // not to mark type as beforefieldinit  
        static StudentRepository()
        {
        }
        private StudentRepository()
        {
        }
        public static StudentRepository Instance
        {
            get
            {
                return instance;
            }
        }

      
        #endregion
        #region student Repository
        public List<Student> GetAllStudent()
        {
            try
            {
                using (var context = new SchoolContext())
                {
                    var students = context.Students.ToList();
                    return students;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
           
           
        }
        public bool CreateStudent(Student s)
        {
            try
            {
                using (var context = new SchoolContext())
                {
                    var students = context.Students.Add(s);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {

                return false;
            }


        }
        public bool EditStudent(Student s)
        {
            try
            {
                using (var context = new SchoolContext())
                {
                    context.Entry(s).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
                
            }


        }
        public bool DeleteStudent(int id)
        {
            try
            {
                using (var context = new SchoolContext())
                {
                    context.Students.Remove(context.Students.Where(x => x.Id == id).FirstOrDefault());
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }


        }
        public Student FindStudent(int id)
        {
            try
            {
                using (var context = new SchoolContext())
                {
                    var student = context.Students.Where(x=>x.Id==id).FirstOrDefault();
                    return student;
                }
            }
            catch (Exception e)
            {

                throw e;
            }


        }
        #endregion 

    }
}