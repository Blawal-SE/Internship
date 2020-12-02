using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Webpi.Test.Controllers
{
    public class BaseController : ApiController
    {
        public int USER_ID
        {
            set { }
            get { return UserId(); }
        }
        private int UserId()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
        }

    }
}
