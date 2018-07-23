using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApplication.DatabaseModels;

namespace WebApplication.Controllers
{
    //la nivel general pe tot controllerul
    [Authorize]
    public class SeriesController : Controller
    {
        // GET: Car
        public ActionResult Index(string searchString)
        {
            List<Series> result = new List<Series>();
            using (DatabaseContext context = new DatabaseContext())
            {
                var loggedUserId = GetLoggedUserId();
                result = context.Series.Where(s => s.UserId == loggedUserId).ToList();

                if (!String.IsNullOrEmpty(searchString))
                {
                    result = context.Series.Where(s => s.Title.Contains(searchString)).ToList();
                    // || s.FirstMidName.Contains(searchString));
                }
            }
            return View(result);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        private Guid GetLoggedUserId()
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            var idClaim = identity.Claims.FirstOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
            return new Guid(idClaim.Value);
        }

        [HttpPost]
        public ActionResult Create(Series car)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var carId = Guid.NewGuid();
                    var path = "";
                    var theRealPath = "";
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];
                        if (file.ContentLength > 0)
                        {
                            var filename = Path.GetFileName(file.FileName);
                            theRealPath = "Upload/" + carId + Path.GetExtension(file.FileName);
                            path = Path.Combine(Server.MapPath("~/Upload/"),
                                carId + Path.GetExtension(file.FileName));
                            file.SaveAs(path);
                        }
                    }

                    using (DatabaseContext context = new DatabaseContext())
                    {
                        car.Id = Guid.NewGuid();
                        car.UserId = GetLoggedUserId();
                        car.Path = theRealPath;
                        context.Series.Add(car);
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            Series result = null;
            using (var databaseContext = new DatabaseContext())
            {
                result = databaseContext.Series.FirstOrDefault(s => s.Id == id);
            }
            return View(result);
        }
        [HttpPost]
        public ActionResult Edit(Series car)
        {
            if (ModelState.IsValid)
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    var carId = car.Id;
                    var path = "";
                    var theRealPath = "";
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];
                        if (file.ContentLength > 0)
                        {
                            var filename = Path.GetFileName(file.FileName);
                            theRealPath = "Upload/" + carId + Path.GetExtension(file.FileName);
                            path = Path.Combine(Server.MapPath("~/Upload/"),
                                carId + Path.GetExtension(file.FileName));
                            file.SaveAs(path);
                        }
                    }

                    Series existingCar = context.Series.FirstOrDefault(s => s.Id == car.Id);
                    existingCar.Title = car.Title;
                    existingCar.Genre = car.Genre;
                    existingCar.Review = car.Review;
                    existingCar.Year = car.Year;
                    existingCar.Seasons = car.Seasons;
                    existingCar.Path = theRealPath;

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            Series result = null;
            using (var databaseContext = new DatabaseContext())
            {
                result = databaseContext.Series.FirstOrDefault(s => s.Id == id);
            }
            return View(result);
        }
        [HttpPost]
        public ActionResult Delete(Series car)
        {
            using (var databaseContext = new DatabaseContext())
            {
                Series result = databaseContext.Series.FirstOrDefault(s => s.Id == car.Id);
                databaseContext.Series.Remove(result);
                databaseContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Sort(string sortOrder)
        {
            List<Series> result = new List<Series>();
            using (var databaseContext = new DatabaseContext())
            {
                result = databaseContext.Series.OrderByDescending(s => s.Title).ToList();
            }
            return View(result);
        }
        
        [HttpPost]
        public ActionResult Sort()
        {
            List<Series> result = new List<Series>();
            using (var databaseContext = new DatabaseContext())
            {
                result = databaseContext.Series.OrderByDescending(s => s.Title).ToList();
                databaseContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}