using IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WepApi.DependancyInjection.Controllers
{
    public class CourseController : ApiController
    {
        private ICourse _repo;
        public CourseController(ICourse Cont)
        {
            _repo = Cont;
        }
        [Authorize/*(Roles = "admin,superadmin")*/]
        [HttpGet]
        public IHttpActionResult GET()
        {
            return Ok(_repo.GetAll());
        }
    }
}
