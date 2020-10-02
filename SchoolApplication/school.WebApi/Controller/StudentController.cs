using Newtonsoft.Json;
using School.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace school.WebApi.Controller
{
    public class StudentController : ApiController
    {
        /*api/Student*/
     [HttpGet]
        public IHttpActionResult Get()
        { 
            var students = StudentRepository.Instance.GetAllStudent();
            return Ok(students);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
          
            return Ok(StudentRepository.Instance.FindStudent(id));
        }
        [HttpPost]
        public IHttpActionResult Post([FromBody]Student student)
        {
          
            StudentRepository.Instance.CreateStudent(student);
           
            return Ok(true); 
        }
        [HttpPut]
        public IHttpActionResult Put([FromBody]Student student)
        {

            StudentRepository.Instance.EditStudent(student);

            return Ok(true);
        }
        [HttpDelete]
        public IHttpActionResult Delete([FromBody]int id)
        {

            StudentRepository.Instance.DeleteStudent(id);

            return Ok(true);
        }

    }
}
