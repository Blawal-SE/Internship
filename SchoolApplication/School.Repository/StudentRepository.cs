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
        {
            List<StudentDtoPost> DtoList = new List<StudentDtoPost>();
            try
            {
                using (SchoolContext db = new SchoolContext())
                {            //Simple Join to gett Student With count of Courses
                    var students = (from s in db.Students
                                    select new StudentDtoPost()
                                    {
                                        student = s,
                                        CoursesCount = (from sc in db.StudentCourses.Where(x => x.StudentId == s.Id)
                                                        select sc.StudentId).Count()
                                    }).ToList();
                    return students;
                    //used before to get Coursecount without Jooin
                    //var StdList = db.Students.ToList();

                    //foreach (var student in StdList)
                    //{
                    //  StudentDtoPost dto = new StudentDtoPost();
                    //   dto.student = student;
                    //    dto.CoursesCount = db.StudentCourses.Where(x => x.StudentId == student.Id).Count();

                    //    DtoList.Add(dto);

                    // }


                }
            }
            catch (Exception e)
            {

                throw e;
            }
        


        }
        public StudentDtoPost FindStudent(int id)
        {
            try
            {
                
                using (var context = new SchoolContext())
                {
                   var students = (from s in context.Students.Where(x => x.Id == id)
                                    select new StudentDtoPost
                                    {
                                        student = s,
                                        StudentCourses=(from sc in context.StudentCourses
                                                        join c in context.Courses on sc.CourseId equals c.CourseId
                                                        where sc.StudentId==id
                                                        select new CourseDTO()
                                                        {
                                                            CourseId=sc.CourseId,
                                                            Name=c.Name
                                                        }
                                                        ).ToList()
                                    }).FirstOrDefault();
                    return students;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public bool AddStudent(Student obj, String courses)
        {
            try
            {
                using (SchoolContext db = new SchoolContext())
                {
                    StudentCourse stdCourse = new StudentCourse();
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
        public bool UpdateStudent(StudentDtoPost stdPost)
        {
            try
            {
                using (SchoolContext db = new SchoolContext())
                {

                    //update first Student
                    db.Entry(stdPost.student).State = EntityState.Modified;
                    //now First Find Courses And delete then Add Newly come Added Courses
                    var stdPreCourses = db.StudentCourses.Where(x => x.StudentId == stdPost.student.Id).ToList();
                    if (stdPreCourses != null)
                    {
                        db.StudentCourses.RemoveRange(stdPreCourses);
                        db.SaveChanges();
                    }
                    List<StudentCourse> courseList = new List<StudentCourse>();
                    if (stdPost.Courses != null)
                    {
                        foreach (var Course in stdPost.Courses)
                        {
                            StudentCourse course_Obj = new StudentCourse();
                            course_Obj.StudentId = stdPost.student.Id;
                            course_Obj.CourseId = Convert.ToInt32(Course);
                            courseList.Add(course_Obj);

                        }
                        db.StudentCourses.AddRange(courseList);
                        db.SaveChanges();

                    }


                }
                //Adding updated  Courses

            
            }
            catch (Exception e)
            {
                return false;
            }

            return true;

        }
        public bool DeleteStudent(int id)
        {
            try
            {
                using (var context = new SchoolContext())
                {
                   var std= context.Students.Where(x => x.Id == id).FirstOrDefault();
                    //  StudentCourse stdCourse = new StudentCourse();
                    // stdCourse.StudentId = std.Id;
                    var stdcourses = context.StudentCourses.Where(x => x.StudentId == id);
                    foreach (var course in stdcourses)
                    {
                        context.StudentCourses.Remove(course);
                        context.SaveChanges();
                    }
                    context.Students.Remove(std);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }


        }

        #endregion

    }
}
