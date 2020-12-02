using School.Dto.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
   public interface IUser
    {
        UserRoleDto ValidateUser(string username, string password);
    }
}
