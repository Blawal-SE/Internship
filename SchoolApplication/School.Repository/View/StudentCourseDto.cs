using School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Repository.View
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FName { get; set; }
        public string Phone { get; set; }
        public string Dob { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int UserId { get; set; }

    }
    public class StudentDtoPost: StudentDto
    {
        public Student student { get; set; }
        public List<CourseDTO> StudentCourses { get; set; }
        public List<Course> AllCoursesList { get; set; }
        public int CoursesCount { get; set; }
        public int TotalRecords { get; set; }
        public string OrderBy { get; set; }
    }

    public class CourseDTO
    {
        public int CourseId { get; set; }
        public string Name { get; set; }

    }
    public class AddEditStudentDto : StudentDto
    {
        //public Student student { get; set; }
        public List<int> Courses { get; set; }
    }
    public class ApiAddStudentDto
    {
        public string Name { get; set; }
        public string FName { get; set; }
        public string Phone { get; set; }
        public string Dob { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public List<int> Courses { get; set; }


    }
    public class ApiEditStudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FName { get; set; }
        public string Phone { get; set; }
        public string Dob { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public List<int> Courses { get; set; }
    }

}