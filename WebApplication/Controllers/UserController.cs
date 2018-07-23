using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApplication.DatabaseModels;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            using (var context = new DatabaseContext())
            {
                User foundUser = context.Users.FirstOrDefault(s => s.Email == user.Email && s.Password == user.Password);
                if (foundUser != null)
                {
                    var ident = new ClaimsIdentity(
                        new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier,foundUser.Id.ToString()),
                            new Claim(ClaimTypes.Name,foundUser.Email)
                        }, "ApplicationCookie");
                    HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "invalid username or password");
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserViewModel uvm)
        {
            if (ModelState.IsValid)
            {
                using (var context = new DatabaseContext())
                {
                    var exist = context.Users.FirstOrDefault(s => s.Email == uvm.Email);

                    if(exist == null)
                    {
                        User user = new User();
                        user.Id = Guid.NewGuid();
                        user.Email = uvm.Email;
                        user.Password = uvm.Password;
                        user.Firstname = uvm.FirstName;
                        context.Users.Add(user);
                        context.SaveChanges();
                        return RedirectToAction("Login");

                    }
                    else
                    {
                        ModelState.AddModelError("", "User already exists");
                    }
                }
            }
            return View();
        }
    }
}