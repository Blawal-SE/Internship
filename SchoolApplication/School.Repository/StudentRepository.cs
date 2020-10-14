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
        public List<StudentDtoPost> GetStudents(Pager pager)
        {
            try
            {
                using (SchoolContext db = new SchoolContext())
                {            //Simple Join to gett Student With count of Courses
                    var students = (from s in db.Students
                                    select new StudentDtoPost()
                                    {
                                        student = s,
                                        CoursesCount =db.StudentCourses.Where(x => x.StudentId == s.Id).Select(x=>x.CourseId).Count()
                                    }).OrderBy(x=>x.student.Name).Skip(pager.start).Take(pager.length).ToList();
                    students[0].TotalRecords = db.Students.Count();
                    return students;
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
        public bool AddStudent(AddEditStudentDto studentDto)
        {
            try
            {
                using (SchoolContext db = new SchoolContext())
                {
                    StudentCourse stdCourse = new StudentCourse();
                    Student s = new Student();
                    s.Name = studentDto.Name;
                    s.FName = studentDto.FName;
                    s.Dob = studentDto.Dob;
                    s.Email = studentDto.Email;
                    s.Password = studentDto.Password;
                    s.ConfirmPassword = studentDto.ConfirmPassword;
                    db.Students.Add(s);
                    List<StudentCourse> courseList = new List<StudentCourse>();
                    if (studentDto.Courses != null)
                    {
                        foreach (var Course in studentDto.Courses)
                        {
                            StudentCourse course_Obj = new StudentCourse();
                            course_Obj.StudentId = studentDto.student.Id;
                            course_Obj.CourseId = Convert.ToInt32(Course);
                            courseList.Add(course_Obj);

                        }
                        db.StudentCourses.AddRange(courseList);
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
        public bool UpdateStudent(AddEditStudentDto stdPost)
        {
            try
            {
                using (SchoolContext db = new SchoolContext())
                {       //update first Student
                    Student s = new Student();
                    s.Id = stdPost.Id;
                    s.Name = stdPost.Name;
                    s.FName = stdPost.FName;
                    s.Dob = stdPost.Dob;
                    s.Email = stdPost.Email;
                    s.Password = stdPost.Password;
                    s.ConfirmPassword = stdPost.ConfirmPassword;
                    db.Entry(s).State = EntityState.Modified;
                       //now First Find Courses And delete previous courses for Student
                    var stdPreCourses = db.StudentCourses.Where(x => x.StudentId == stdPost.Id).ToList();
                    if (stdPreCourses != null)
                    {
                        db.StudentCourses.RemoveRange(stdPreCourses);
                        db.SaveChanges();
                    }
                    //Now Add Newly Added Courses
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
        #region Crud Through Procedures
        public bool AddStudentByProcedure(AddEditStudentDto s)
        {
            try
            {
                using (var context = new SchoolContext())
                {
                    var id = Convert.ToInt32(context.AddStudent(s.Name, s.FName, s.Phone, s.Email, s.Dob, s.Password, s.ConfirmPassword));
                    context.AddCourses(id,s.Courses);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<Student> GetStudentsByProcedure()
        {
            try
            {
                using (var context=new SchoolContext())
                {
                    return context.Students.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
      
        #endregion
    }
}
