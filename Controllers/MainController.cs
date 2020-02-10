using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.IO;

namespace Project.Controllers
{
    public class MainController : Controller
    {
        private int? UserSession
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
            set { HttpContext.Session.SetInt32("UserId", (int)value); }
        }
        private MyContext dbContext;
        public MainController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            if (UserSession == null)
                return RedirectToAction("Index", "Login");
            ViewBag.CurrentUser = dbContext.Users.FirstOrDefault(u => u.UserId == UserSession);
            var AllSpots = dbContext.Spots.OrderBy(t => t.CreatedAt)
            .Include(c => c.Creator).ToList();
            return View(AllSpots);
        }

        [HttpGet]
        public IActionResult NewSpot()
        {
            if (UserSession == null)
                return RedirectToAction("Index", "Login");
            ViewBag.CurrentUser = dbContext.Users.FirstOrDefault(u => u.UserId == UserSession);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewSpot(Spot NewSpot, IFormFile image)
        {
            if (UserSession == null)
                return RedirectToAction("Index", "Login");
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    var fileName = Path.GetFileName(image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileSteam);
                    }
                    NewSpot.Image = fileName;
                }
                
                NewSpot.CreatorId = (int)UserSession;
                dbContext.Spots.Add(NewSpot);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View("NewSpot");
        }

        [HttpGet]
        [Route("Spot/{spotid}")]
        public IActionResult Spot(int spotid)
        {
            if (UserSession == null)
                return RedirectToAction("Index", "Login");
            ViewBag.CurrentUser = dbContext.Users.FirstOrDefault(u => u.UserId == UserSession);

            SpotViewModel model = new SpotViewModel()
            {
            CurrentSpot = dbContext.Spots
            .Include(c => c.Comments)
            .ThenInclude(u => u.User)
            .FirstOrDefault(s => s.SpotId == spotid)
            };

            return View("Spot", model);

        }

        [HttpGet("DeleteSpot/{spotid}")]
        public IActionResult DeleteSpot(int spotid)
        {
            if (UserSession == null)
                return RedirectToAction("Login", "Home");
            
            Spot toDelete = dbContext.Spots.FirstOrDefault(a => a.SpotId == spotid);
            dbContext.Spots.Remove(toDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpPost("PostComment/{spotid}")]
        public IActionResult PostComment(Comment newComment, int spotid)
        {
            if (UserSession == null)
                return RedirectToAction("Login", "Home");
            Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%Got 1");
            if (ModelState.IsValid)
            {
                newComment.UserId = (int)UserSession;
                newComment.SpotId = spotid;
                Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%Got 2");
                dbContext.Comments.Add(newComment);
                dbContext.SaveChanges();
                Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%Got 3");
                return Redirect($"/Spot/{spotid}");
            }
            return View($"/Spot/{spotid}");
        }
    }
}