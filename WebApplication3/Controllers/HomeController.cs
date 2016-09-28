using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsIdentity id = System.Web.HttpContext.Current.User.Identity as FormsIdentity;

                FormsAuthenticationTicket ticket = id.Ticket;

                ViewBag.Name = ticket.Name;
            }

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (model.Account == "givemin5" && model.Password == "123")
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                (
                    "givemin5",
                    true,
                    86400
                );

                string encTicket = FormsAuthentication.Encrypt(ticket);

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,encTicket);

                cookie.HttpOnly = true;

                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Index");

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index");
        }
    }
}