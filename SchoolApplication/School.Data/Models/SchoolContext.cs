using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Models
{
    class SchoolContext : DbContext
    {
        public SchoolContext() : base("name=School")
        {

        }
        public virtual DbSet<Student> Students { get; set; }
    }
}
