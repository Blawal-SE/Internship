using Microsoft.AspNet.Identity.EntityFramework;
using School.Identity.IdentityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Identity
{
  public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context() : base("name=IdentitySchool")
        {
        }
        public DbSet<User> Students { get; set; }
        public static Context Create() 
        {
            return new Context();
        }
    }
}
