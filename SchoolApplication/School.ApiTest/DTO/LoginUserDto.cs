using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.ApiTest.DTO
{
    class LoginUserDto
    {
        public string username { get; set; }
        public string password { get; set; }
        public string grant_type { get; set; }

    }
}
