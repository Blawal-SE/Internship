using Newtonsoft.Json;
using school.WebApi.View;
using School.Data;
using School.Models;
using School.Repository;
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
            var students = StudentRepository.Instance.GetAll();
            return Ok(students);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
          
            return Ok(StudentRepository.Instance.FindStudent(id));
        }
        [HttpPost]
        public IHttpActionResult Post(string student)
        {
            StudentCourseViewModel model = new StudentCourseViewModel();
                model = JsonConvert.DeserializeObject<StudentCourseViewModel>(student);
           var message= StudentRepository.Instance.AddStudent(model.student,model.courses);
           
            return Ok(true); 
        }
        [HttpPut]
        public IHttpActionResult Put([FromBody]Student student)
        {

            StudentRepository.Instance.Update(student);

            return Ok(true);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {

            StudentRepository.Instance.Delete(id);

            return Ok(true);
        }

    }
}
