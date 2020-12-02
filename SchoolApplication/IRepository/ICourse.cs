using School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface ICourse : IBaseRepository<Course>
    {
        string GetCourse(string s);
    }
}
