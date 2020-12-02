using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Models
{
   public class StudentCourse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course_Obj { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student_Obj { get; set; }

    }
}
