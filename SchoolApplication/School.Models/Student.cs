using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FName { get; set; }
        public string Phone { get; set; }
        public string Dob { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbUrl { get; set; }
        public virtual User User_Obj { get; set; }
        public int UserId { get; set; }

    }
}
