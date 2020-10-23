using School.Dto.View;
using School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IStudent : IBaseRepository<Student>
    {
        List<StudentDtoPost> GetStudents(Pager pager);
        StudentDtoPost FindStudent(int id, int userid);
        bool AddStudent(AddEditStudentDto studentDto);
        bool UpdateStudent(AddEditStudentDto stdPost);
        bool DeleteStudent(int id);
        bool AddStudentByProcedure(AddEditStudentDto s);
        List<Student> GetStudentsByProcedure();
    }
}
