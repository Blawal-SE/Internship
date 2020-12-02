
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using IData;
using IRepository;
using School.Dto.View;

namespace Sxhool.WebMvc.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {

        private readonly IStudent _repo;
        private readonly ICourse _CourseRepo;
        public StudentController(IStudent Cont, ICourse course)
        {
            _repo = Cont;
            _CourseRepo = course;
        }
        //  private StudentRepository _repo = new StudentRepository(context);
        public ActionResult Index(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.message = message;
            }
            return View();
        }
        [HttpPost]
        public ActionResult LoadStudents(Pager pager)
        {
            List<StudentDtoPost> model = _repo.GetStudents(pager);
            return Json(new { draw = pager.draw, recordsFiltered = model[0].TotalRecords, recordsTotal = model[0].TotalRecords, data = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddStudent()
        {
            return View(_CourseRepo.GetAll());
        }
        [HttpPost]
        public ActionResult AddStudent(AddEditStudentDto model)
        {                     //used  to To add Student With Procedure
                              //  return _repo.AddStudentByProcedure(model) ? RedirectToAction("Index", new { message = "successfully added student" }) : RedirectToAction("Index", new { message = "Unable to add student" });
                              //used before to To add Student Without Procedure
            return _repo.AddStudent(model) ? RedirectToAction("Index") : RedirectToAction("Index", new { message = "Unable to add student" });
        }

        [HttpGet]
        public ActionResult EditStudent(int id)
        {
            var model = _repo.FindStudent(id);
            if (_repo.FindStudent(id) != null)
            {
                model.AllCoursesList = CourseRepository.GetAll();
                return View(model);
            }
            else
            {
                return RedirectToAction("AllStudent", new { message = "Unable To Find Student Some Error Occured" });
            }
        }
        [HttpPost]
        public ActionResult EditStudent(AddEditStudentDto model)
        {
            return _repo.UpdateStudent(model) ? RedirectToAction("Index") : RedirectToAction("Index", new { message = "Unable to add student" });
        }

        [HttpGet]
        public ActionResult DeleteStudent(int id)
        {
            return _repo.Delete(id) ? RedirectToAction("Index", new { message = "Successfully deleted Student" }) : RedirectToAction("Index", new { message = "Unable to delete Student" });
        }
    }
}