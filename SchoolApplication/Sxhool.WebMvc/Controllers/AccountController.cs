using School.Models;
using School.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sxhool.WebMvc.Controllers
{
    public class AccountController : Controller
    {
       // private AccountsRepository _AccountRepo = new AccountsRepository();
        // GET: Account
        public ActionResult Login()
        {
            return View(new User());
        }
       [HttpPost]
        public ActionResult Login(User moodel)
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User model)
        {
            //if (ModelState.IsValid) 
            //{
            //    _AccountRepo.Add(model);
            //}
            return RedirectToAction("Login");
        }
    }
}