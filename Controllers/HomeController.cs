using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using wedding.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace wedding.Controllers
{
    public class HomeController : Controller
    {
        private WeddingContext _context;
        public HomeController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("plan")]
        public IActionResult Plan()
        {
            return View();
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard(int id)
        {   
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Index");
            }
            List<Wedding> Weddings = _context.Weddings.Include(w => w.Guests).ToList();
            ViewBag.Weddings = Weddings;
            User ReturnedValue = _context.Users.SingleOrDefault(user => user.UserId == id);
            ViewBag.CurrId = HttpContext.Session.GetInt32("id");
            return View("Dashboard");
        }

        [HttpPost]
        [Route("registerprocess")]
        public IActionResult RegisterProcess(RegUser RegUser)
        {
            if (ModelState.IsValid){
                List<User> CheckEmail = _context.Users.Where(theuser => theuser.Email == RegUser.Email).ToList();
                if (CheckEmail.Count > 0)
                {
                    ViewBag.ErrorRegister = "Email already in use...";
                    return View("Index");
                }
                User NewUser = new User
                {
                    First = RegUser.First,
                    Last = RegUser.Last,
                    Email = RegUser.Email,
                    Password = RegUser.Password,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                _context.Users.Add(NewUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("id", (int)NewUser.UserId);
                HttpContext.Session.SetString("firstName", NewUser.First);
                return RedirectToAction("Dashboard", new { id = NewUser.UserId });            
            }
            return View("Index");
        }

        [HttpPost]
        [Route("loginprocess")]
        public IActionResult LoginProcess(string Email, string Password)
        {
            if(Email != null && Password != null)
            {
                User ReturnedValue = _context.Users.SingleOrDefault(user => user.Email == Email);
                if (ReturnedValue == null){
                    ViewBag.LoginError = "User does not Exist";
                    return View("Index");
                }

                else if(ReturnedValue.Email == Email && ReturnedValue.Password == Password)
                {   
                    HttpContext.Session.SetInt32("id", (int)ReturnedValue.UserId);
                    HttpContext.Session.SetString("firstName", ReturnedValue.First);
                    return RedirectToAction("Dashboard", new { id = ReturnedValue.UserId });            
                }
            }
            ViewBag.LoginError = "Invalid Login";
            return View("Index");
        }

        [HttpPost]
        [Route("planprocess")]
        public IActionResult PlanProcess(WeddingReg NewWed)
        {
            if(ModelState.IsValid)
            {
                Wedding Wed = new Wedding
                {
                    Wedder1 = NewWed.Wedder1,
                    Wedder2 = NewWed.Wedder2,
                    Date = NewWed.Date,
                    Address = NewWed.Address,
                    Creator = (int)HttpContext.Session.GetInt32("id"),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                _context.Weddings.Add(Wed);
                _context.SaveChanges();
                Guest guest = new Guest
                {
                    UserId = (int)HttpContext.Session.GetInt32("id"),
                    WeddingId = Wed.WeddingId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Guests.Add(guest);
                _context.SaveChanges();
                return RedirectToAction("Dashboard", new { id = HttpContext.Session.GetInt32("id") });
            }
            return View("Plan");
        }

        [HttpGet]
        [Route("wedding/{id}")]
        public IActionResult Wedding(int id){
            Wedding TheWedding = _context.Weddings.Include(w => w.Guests).ThenInclude(u => u.User).SingleOrDefault(w => w.WeddingId == id);
            ViewBag.Wedding = TheWedding;
            return View();
        }

        [HttpGet]
        [Route("rsvp/{id}")]
        public IActionResult Rsvp(int id){
            Guest newGuest = new Guest
            {
                WeddingId = id,
                UserId = (int)HttpContext.Session.GetInt32("id"),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            _context.Guests.Add(newGuest);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("unrsvp/{id}")]
        public IActionResult UnRsvp(int id){
            Guest Guestun = _context.Guests.SingleOrDefault(g => g.WeddingId == id && g.UserId == (int)HttpContext.Session.GetInt32("id"));
            _context.Guests.Remove(Guestun);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult delete(int id){
            Wedding deleted = _context.Weddings.SingleOrDefault(w => w.WeddingId == id);
            _context.Weddings.Remove(deleted);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}
