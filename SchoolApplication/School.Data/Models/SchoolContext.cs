using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Models
{
   public class SchoolContext : DbContext
    {
        public SchoolContext() : base("name=School")
        {

        }
        public  DbSet<Student> Students { get; set; }
    }
}
