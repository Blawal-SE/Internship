using School.Data;
using School.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository
{

    public class StudentRepository : BaseRepository<Student>
    {
        #region  Singleleton pattern

        private static readonly StudentRepository instance = new StudentRepository();
        // explicit static constructor to tell c# compiler  
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
        public string AddStudent(Student obj,String courses)
        {// BaseRepository<Student> b = new BaseRepository<Student>();
          //  Add(obj);
            if (Add(obj))
            {
                try
                {
                    using (SchoolContext db = new SchoolContext())
                    {
                        StudentCourse stdCourse = new StudentCourse();
                        var Std = db.Students.OrderByDescending(x => x.Id).FirstOrDefault();
                        stdCourse.StudentId = Std.Id;
                        var courseList = courses.Split(',').ToList();
                        foreach (var course in courseList)
                        {
                            stdCourse.CourseId = Convert.ToInt32(course);
                            db.StudentCourses.Add(stdCourse);
                            db.SaveChanges();
                        }

                    }
                }
                catch (Exception e)
                {

                    return "failed to Save Courses error message is" + e;
                }


                return "successfully added Student and Courses";
            }
            else
            {
                return " Unable to add Student";
            }

        }
        //public bool DeleteStudent(int id)
        //{
        //    try
        //    {
        //        using (var context = new SchoolContext())
        //        {
        //            context.Students.Remove(context.Students.Where(x => x.Id == id).FirstOrDefault());
        //            context.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return false;
        //    }


        //}
        public Student FindStudent(int id)
        {
            try
            {
                using (var context = new SchoolContext())
                {
                    var student = context.Students.Where(x => x.Id == id).FirstOrDefault();
                    return student;
                }
            }
            catch (Exception e)
            {

                throw e;
            }


        }
        //public void SaveStudent(Student obj)
        //{
        //    try
        //    {
        //        using (var context = new SchoolContext())
        //        {
        //            context.Students.Add(obj);
        //            context.SaveChanges();

        //        }
        //    }
        //    catch (Exception)
        //    {


        //    }


        //}

        #endregion

    }
}
