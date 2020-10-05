using Newtonsoft.Json;

using School.Data;
using School.Models;
using School.Repository;
using School.Repository.View;
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
        private StudentRepository repo = new StudentRepository();
        
        
            [HttpGet]
        public IHttpActionResult Get()
        { 
            var students = repo.GetStudents();
            return Ok(students);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
          
            return Ok(repo.FindStudent(id));
        }
        [HttpPost]
        public IHttpActionResult Post(string student)
        {
            StudentDtoPost model = new StudentDtoPost();
                model = JsonConvert.DeserializeObject<StudentDtoPost>(student);
           var message= repo.AddStudent(model.student,model.courses);
           
            return Ok(message); 
        }
        [HttpPut]
        public IHttpActionResult Put([FromBody]Student student)
        {

            repo.Update(student);

            return Ok(true);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {

            repo.Delete(id);

            return Ok(true);
        }

    }
}
