using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Dto.View
{
   public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UserRoleDto : UserDto
    {
        public List<RoleDto> roles { get; set; }
    }

}
