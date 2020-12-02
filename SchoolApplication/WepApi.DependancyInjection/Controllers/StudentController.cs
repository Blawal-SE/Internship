using IRepository;
using School.Dto.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WepApi.DependancyInjection.Controllers
{
    public class StudentController : BaseController
    {
        // private StudentRepository _repo = new StudentRepository();
        private IStudent _repo;
        public StudentController(IStudent Cont)
        {
            _repo = Cont;
        }
        //public StudentController()
        //{
        //}
        [Authorize/*(Roles = "admin,superadmin")*/]
        [HttpGet]

        public IHttpActionResult Get()
        {
            Pager pager = new Pager();
            pager.length = 100;
            pager.UserId = USER_ID;
            return Ok(_repo.GetStudents(pager));
        }
        [Authorize/*(Roles = "admin,superadmin")*/]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var model = _repo.FindStudent(id, USER_ID);
            if (model != null)
            {
                model.ImageBase64 = model.ImageUrl != "" && model.ImageUrl != null ? Convert64Image(model.ImageUrl) : "";
                return Ok(model);
            }
            else
            {
                return Ok(model);
            }
        }
        [Authorize/*(Roles = "admin,superadmin")*/]
        [HttpPost]
        public IHttpActionResult Post([FromBody] AddEditStudentDto Model)
        {
            Model.UserId = USER_ID;
            return Ok(_repo.AddStudent(Model));
        }
        [Authorize/*(Roles = "admin,superadmin")*/]
        [HttpPut]
        public IHttpActionResult Put([FromBody] AddEditStudentDto Model)
        {
            Model.UserId = USER_ID;
            return Ok(_repo.UpdateStudent(Model));
        }
        [HttpDelete]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            _repo.Delete(id);
            return Ok(true);
        }
        #region convert image to base64
        public string Convert64Image(string Path)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(Path);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return base64ImageRepresentation;
        }
        #endregion

    }

}
