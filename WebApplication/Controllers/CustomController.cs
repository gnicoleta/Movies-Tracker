using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class CustomController : Controller
    {
        // GET: Custom
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowCustomModel()
        {
            CustomModel customModel = new CustomModel();
            customModel.Name = "Cristi";
            customModel.Age = 22;
            return View(customModel);
        }
        [HttpGet]
        public ActionResult CreateCustomModel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCustomModel(CustomModel model)
        {
            return View();
        }
    }
}