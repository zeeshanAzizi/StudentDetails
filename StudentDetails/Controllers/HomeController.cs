using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentDetails.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using StudentDetails.Models;

namespace StudentDetails.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext dbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Student> std = dbContext.Students.ToList();
            return View(std);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student std)
        {
            if (ModelState.IsValid)
            {
                dbContext.Students.Add(std);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(std);
            }
        }
        public IActionResult Delete(int id)
        {
            var std = dbContext.Students.SingleOrDefault(e => e.Id == id);
            if (std != null)
            {
                dbContext.Students.Remove(std);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public IActionResult Edit(int id)
        {
            var std = dbContext.Students.SingleOrDefault(e => e.Id == id);
            return View(std);
        }
        [HttpPost]
        public IActionResult Edit(Student std)
        {
            dbContext.Students.Update(std);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
