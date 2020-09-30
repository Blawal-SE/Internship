using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Models
{
   public class Student
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string FName { get; set; }
        public string Phone { get; set; }
        public DateTime Dob { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }



    }
}
