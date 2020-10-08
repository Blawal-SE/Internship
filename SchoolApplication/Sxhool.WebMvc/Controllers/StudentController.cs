using Newtonsoft.Json;
using School.Models;
using School.Repository;
using School.Repository.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sxhool.WebMvc.Controllers
{
    public class StudentController : Controller
    {
        private StudentRepository _repo = new StudentRepository();
   
        // GET: Student
        [HttpGet]
        public ActionResult Index(string message)
        {  if(message!="" && message != null) 
            {
                ViewBag.message = message;
            }
           List<StudentDtoPost> model= _repo.GetStudents();
            return View(model);
        }
        [HttpGet]
        public ActionResult AddStudent()
        {
            return View(CourseRepository.Instance.GetAll());

        }
        [HttpPost]
        public JsonResult AddStudent(string student)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            StudentDtoPost model = JsonConvert.DeserializeObject<StudentDtoPost>(student);
            if(_repo.AddStudent(model.student,model.courses))
            {
                result.Data = new { success = true };
                return result;
            }else
            {
                result.Data = new { success = false };
                return result;
            }
           
        }
        [HttpGet]
        public ActionResult EditStudent(int id)
        {
            var model = _repo.FindStudent(id);
            model.AllCoursesList = CourseRepository.Instance.GetAll();
            if (model != null && model.AllCoursesList != null)
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", new { message = "Unable To Find Student Some Error Occured" });
            }
        }
        [HttpPost]
        public ActionResult EditStudent(string student)
        {
            StudentDtoPost model = JsonConvert.DeserializeObject<StudentDtoPost>(student);
            if (_repo.UpdateStudent(model))
            {
                return RedirectToAction("Index", new { message = "Successfully Edit Student:" + model.student.Name });

            }
            else
            {
                return RedirectToAction("Index", new { message = "Unable To Edit Student"+model.student.Name });
            }

        }
        [HttpGet]
        public ActionResult DeleteStudent(int id)
        {
            if (_repo.Delete(id)){
                return RedirectToAction("Index",new { message="Successfully deleted Student"});
            }
            else
            {
                return RedirectToAction("Index", new { message = "Unable to delete Student" });
            }
           
        }
       
    }
}