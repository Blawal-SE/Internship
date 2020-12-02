using Newtonsoft.Json;

using School.Data;
using School.Dto.View;
using School.Models;
using School.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace School.WebApi.Controller
{
    public class StudentController : ApiController
    {
        private StudentRepository _repo = new StudentRepository();

        [HttpGet]
        [Authorize]
        public IHttpActionResult Get()
        {
            Pager pager = new Pager();
            pager.length = 100;
            return Ok(_repo.GetStudents(pager));
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(_repo.FindStudent(id));

        }
        [HttpPost]
        public IHttpActionResult Post(AddEditStudentDto model)
        {
            return Ok(_repo.AddStudent(model));
        }
        [HttpPost]
        public IHttpActionResult Post([FromBody]Student student)
        {
            return Ok(_repo.Add(student));
        }
        [HttpPut]
        public IHttpActionResult Put(AddEditStudentDto model)
        {
            return Ok(_repo.UpdateStudent(model));
        }
        [HttpPut]
        public IHttpActionResult Put(Student student)
        {
            _repo.Update(student);
            return Ok(true);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _repo.Delete(id);
            return Ok(true);
        }

    }
}
