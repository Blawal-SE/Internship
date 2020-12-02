using IData;
using IRepository;
using School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository
{
    public class CourseRepository : BaseRepository<Course>, ICourse
    {

        private ISchoolContextt context;
        public CourseRepository(ISchoolContextt Cont) : base(Cont)
        {
            context = Cont;
        }
        public string GetCourse(string s)
        {
            var course= context.Courses.Where(x => x.Name.Contains(s)).FirstOrDefault();
            if (course != null) 
            {
                return course.Name;
            }
            else
            {
                return "Course Not Found";

            }
        }
    }
}
