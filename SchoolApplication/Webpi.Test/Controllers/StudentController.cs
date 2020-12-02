using IRepository;
using School.Dto.View;
using School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Webpi.Test.Controllers
{
    public class StudentController : BaseController
    {
        // private StudentRepository _repo = new StudentRepository();
        private IStudent _repo;
        public StudentController(IStudent Cont)
        {
            _repo = Cont;
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            Pager pager = new Pager();
            pager.length = 100;
            pager.UserId = USER_ID;
            return Ok(_repo.GetStudents(pager));
        }
        [Authorize(Roles = "admin,superadmin")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(_repo.FindStudent(id, USER_ID));
        }
        [Authorize(Roles = "admin,superadmin")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]AddEditStudentDto Model)
        {
            Model.UserId = USER_ID;
            return Ok(_repo.AddStudent(Model));
        }
        [Authorize(Roles = "admin,superadmin")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]AddEditStudentDto Model)
        {
            Model.UserId = USER_ID;
            return Ok(_repo.UpdateStudent(Model));
        }
        [HttpDelete]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            //  _repo.Delete(id);
            return Ok(true);
        }

    }
}
