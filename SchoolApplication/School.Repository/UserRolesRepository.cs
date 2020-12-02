using IData;
using IRepository;
using School.Dto.View;
using School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository
{
    public class UserRolesRepository : IUser
    {
        private ISchoolContextt context;

        public UserRolesRepository(ISchoolContextt Cont)
        {
            context = Cont;
        }

        public UserRoleDto ValidateUser(string username, string password)
        {
            try
            {
                var user = (from u in context.Users.Where(x => x.UserName == username && x.Password == password)
                            select new UserRoleDto()
                            {
                                Id = u.Id,
                                FullName = u.FullName,
                                UserName = u.UserName,
                                Password = u.Password,
                                ConfirmPassword = u.ConfirmPassword,
                                roles = (from rolemapper in context.RoleMappers.Where(x => x.UserId == u.Id)
                                         join role in context.Roles on rolemapper.RoleId equals role.Id
                                         select new RoleDto()
                                         {
                                             Id = rolemapper.RoleId,
                                             Name = role.Name
                                         }).ToList()
                            }
                          ).FirstOrDefault();
                return user;

            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
