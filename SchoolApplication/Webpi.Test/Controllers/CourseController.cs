
using IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Webpi.Test.Controllers
{
    public class CourseController : ApiController
    {
        private ICourse _repo;
        public CourseController(ICourse Cont)
        {
            _repo = Cont;
        }
        [HttpGet]
        public IHttpActionResult GET()
        {
            return Ok(/*_repo.GetAll()*/);
        }
    }
}
