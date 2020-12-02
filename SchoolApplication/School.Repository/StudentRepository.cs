using IData;
using IRepository;
using School.Dto.View;
using School.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace School.Repository
{

    public class StudentRepository : BaseRepository<Student>, IStudent
    {
        private readonly ISchoolContextt context;
        public StudentRepository(ISchoolContextt Cont) : base(Cont)
        {
            context = Cont;
        }
        #region student Repository
        public List<StudentDtoPost> GetStudents(Pager pager)
        {
            try
            {
                //Simple Join to gett Student With count of Courses
                var students = (from s in context.Students.Where(x => x.UserId == pager.UserId)
                                select new StudentDtoPost()
                                {
                                    Id = s.Id,
                                    Name = s.Name,
                                    FName = s.FName,
                                    Email = s.Email,
                                    Password = s.Password,
                                    Phone = s.Phone,
                                    Dob = s.Dob,
                                    ImageUrl = s.ImageUrl,
                                    ThumbUrl = s.ThumbUrl,
                                    CoursesCount = context.StudentCourses.Where(x => x.StudentId == s.Id).Select(x => x.CourseId).Count(),
                                    StudentCourses = (from sc in context.StudentCourses
                                                      join c in context.Courses on sc.CourseId equals c.CourseId
                                                      where sc.StudentId == s.Id
                                                      select new CourseDTO()
                                                      {
                                                          CourseId = sc.CourseId,
                                                          Name = c.Name
                                                      }
                                                    ).ToList()

                                }).OrderBy(x => x.Id).Skip(pager.start).Take(pager.length).ToList();
                if (students.Count != 0)
                {
                    students[0].TotalRecords = context.Students.Count();
                }
                return students;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public StudentDtoPost FindStudent(int id, int userid)
        {
            try
            {

                var students = (from s in context.Students.Where(x => x.Id == id && x.UserId == userid)
                                select new StudentDtoPost
                                {
                                    Id = s.Id,
                                    Name = s.Name,
                                    FName = s.FName,
                                    Email = s.Email,
                                    Password = s.Password,
                                    ConfirmPassword = s.ConfirmPassword,
                                    Phone = s.Phone,
                                    Dob = s.Dob,
                                    ImageUrl = s.ImageUrl,
                                    ThumbUrl = s.ThumbUrl,
                                    StudentCourses = (from sc in context.StudentCourses
                                                      join c in context.Courses on sc.CourseId equals c.CourseId
                                                      where sc.StudentId == id
                                                      select new CourseDTO()
                                                      {
                                                          CourseId = sc.CourseId,
                                                          Name = c.Name
                                                      }
                                                    ).ToList()
                                }).FirstOrDefault();
                return students;

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
                StudentCourse stdCourse = new StudentCourse();
                Student s = new Student();
                s.Name = studentDto.Name;
                s.FName = studentDto.FName;
                s.Dob = studentDto.Dob;
                s.Email = studentDto.Email;
                s.Phone = studentDto.Phone;
                s.Password = studentDto.Password;
                s.ImageUrl = studentDto.ImageUrl;
                s.ThumbUrl = studentDto.ThumbUrl;
                s.ConfirmPassword = studentDto.ConfirmPassword;
                s.UserId = studentDto.UserId;
                context.Students.Add(s);
                List<StudentCourse> courseList = new List<StudentCourse>();
                if (studentDto.Courses != null)
                {
                    foreach (var Course in studentDto.Courses)
                    {
                        StudentCourse course_Obj = new StudentCourse();
                        course_Obj.StudentId = s.Id;
                        course_Obj.CourseId = Convert.ToInt32(Course);
                        courseList.Add(course_Obj);
                    }
                    context.StudentCourses.AddRange(courseList);
                }
                context.SaveChanges();
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
                //update first Student
                Student s = new Student();
                s.Id = stdPost.Id;
                s.Name = stdPost.Name;
                s.FName = stdPost.FName;
                s.Dob = stdPost.Dob;
                s.Email = stdPost.Email;
                s.Phone = stdPost.Phone;
                s.UserId = stdPost.UserId;
                s.Password = stdPost.Password;
                s.ImageUrl = stdPost.ImageUrl;
                s.ThumbUrl = stdPost.ThumbUrl;
                s.ConfirmPassword = stdPost.ConfirmPassword;
                //now First Find Courses And delete previous courses for Student
                var stdPreCourses = context.StudentCourses.Where(x => x.StudentId == stdPost.Id).ToList();
                if (stdPreCourses != null)
                {
                    context.StudentCourses.RemoveRange(stdPreCourses);
                    context.SaveChanges();
                }
                //Now Add Newly Added Courses
                List<StudentCourse> courseList = new List<StudentCourse>();
                if (stdPost.Courses != null)
                {
                    foreach (var Course in stdPost.Courses)
                    {
                        StudentCourse course_Obj = new StudentCourse();
                        course_Obj.StudentId = stdPost.Id;
                        course_Obj.CourseId = Convert.ToInt32(Course);
                        courseList.Add(course_Obj);
                    }
                    context.StudentCourses.AddRange(courseList);
                    context.Entry(s).State = EntityState.Modified;
                    context.SaveChanges();
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

                var std = context.Students.Where(x => x.Id == id).FirstOrDefault();
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

                var id = Convert.ToInt32(context.AddStudent(s.Name, s.FName, s.Phone, s.Email, s.Dob, s.Password, s.ConfirmPassword));
                context.AddCourses(id, s.Courses);
                return true;

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

                return context.Students.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

    }
}
