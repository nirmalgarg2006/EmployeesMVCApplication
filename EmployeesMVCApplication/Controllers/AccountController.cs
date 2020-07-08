using EmployeesMVCApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace EmployeesMVCApplication.Controllers
{
    public class AccountController : Controller
    {
        private OfficeEntities db = new OfficeEntities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            if(ModelState.IsValid)
            {
                bool isValidUser = db.Users.Any(t => t.Username == model.Username && t.Password == model.Password);
                if(isValidUser)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index", "Employees");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username/password");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    db.Users.Add(model);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index", "Employees");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(model);
                }

            }
            else
            {
                ModelState.AddModelError("", "Error occurred");
                return View(model);
            }
        }
    }
}