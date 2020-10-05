using School.Data;
using School.Models;
using School.Repository.View;
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
        #region student Repository
        public List<StudentDtoPost> GetStudents()
        {// BaseRepository<Student> b = new BaseRepository<Student>();
         //  Add(obj);
            List<StudentDtoPost> DtoList = new List<StudentDtoPost>();
            try
            {
                using (SchoolContext db = new SchoolContext())
                {
                    var StdList=db.Students.ToList();
                    foreach (var student in StdList)
                    {
                        StudentDtoPost dto = new StudentDtoPost();
                        dto.student = student;
                        dto.CoursesCount = db.StudentCourses.Where(x => x.StudentId == student.Id).Count();

                        DtoList.Add(dto);
                        
                    }
                  

                }
            }
            catch (Exception e)
            {

              
            }

            return DtoList;

        }
        public bool AddStudent(Student obj,String courses)
        {// BaseRepository<Student> b = new BaseRepository<Student>();
          //  Add(obj);
            
                try
                {
                    using (SchoolContext db = new SchoolContext())
                    {
                        StudentCourse stdCourse = new StudentCourse();
                    // var Std = db.Students.OrderByDescending(x => x.Id).FirstOrDefault();
                        db.Students.Add(obj);
                        stdCourse.StudentId = obj.Id;
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

                    return false;
                }

            return true;

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
