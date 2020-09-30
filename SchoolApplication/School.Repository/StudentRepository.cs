using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using School.Data.Models;
namespace school.WebApi
{
    public class StudentRepository
    {
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
                    context.Entry(s).State = EntityState.Added;
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
            catch (Exception e)
            {

                throw e;
                return false;
            }


        }
        public string FindStudent(int id)
        {
            try
            {
                using (var context = new SchoolContext())
                {
                    var students = context.Students.ToList();
                    return students.ToString();
                }
            }
            catch (Exception e)
            {

                throw e;
            }


        }


    }
}