using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UserSignupLogin.Models;

namespace UserSignupLogin.Controllers
{
    public class HomeController : Controller

    {

        public async Task<ActionResult> api()
        {

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "UserSignuoLogin");

            var abc = client.GetAsync("https://newsapi.org/v2/top-headlines?country=ar&apiKey=6f87544670c640c498154dc12477557a");
            abc.Wait();
            if (abc.IsCompleted)
            {
                var result = abc.Result;
                var read = result.Content.ReadAsStringAsync();
                read.Wait();
                ViewBag.message = read.Result.ToString();
            }
            return View("api");
        }




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