using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserSignupLogin.Models;

namespace UserSignupLogin.Controllers
{
    public class HomeController : Controller
    {
        DBuserSignupLoginEntities1 db = new DBuserSignupLoginEntities1();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.TBLUserInfoes.ToList());
        }

        public ActionResult Signup()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(TBLUserInfo tBLUserInfo)
        {
            if (db.TBLUserInfoes.Any(x => x.UsernameUs == tBLUserInfo.UsernameUs))
            {
                ViewBag.Notification = "this account is already existed";

                return View();
            }
            else
            {
                db.TBLUserInfoes.Add(tBLUserInfo);
                db.SaveChanges();

                Session["IdUsSS"] = tBLUserInfo.IdUs.ToString();
                Session["UsernameSS"] = tBLUserInfo.UsernameUs.ToString();
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout() {

            Session.Clear();
            return RedirectToAction("Index" ,"Home");
        
        }

    }
}