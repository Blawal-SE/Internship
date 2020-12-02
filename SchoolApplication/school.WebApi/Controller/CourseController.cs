using School.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace school.WebApi.Controller
{
    public class CourseController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GET() 
        {
            return Ok(CourseRepository.Instance.GetAll());
        }
    }
}
