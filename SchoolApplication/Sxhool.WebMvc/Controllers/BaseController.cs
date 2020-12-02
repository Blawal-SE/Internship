using School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Sxhool.WebMvc.Controllers
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