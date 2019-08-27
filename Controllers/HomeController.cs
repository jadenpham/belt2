using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using retake_two.Models;

namespace retake_two.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(RegWLog newUser)
        {
            UserReg submittedUser = newUser.UserReg;
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.Email == submittedUser.Email))
                {
                    ModelState.AddModelError("UserReg.Email", "Email already in use!");
                    return View("Index");
                };

                PasswordHasher<UserReg> Hasher = new PasswordHasher<UserReg>();
                submittedUser.Password = Hasher.HashPassword(submittedUser, submittedUser.Password);
                dbContext.Add(submittedUser);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("UserId", submittedUser.UserId);
                return RedirectToAction("Dashboard", new {id = submittedUser.UserId});
            }
            else
            {

                return View("Index");
            }
        }
        [HttpPost("login")]
        public IActionResult Login(RegWLog LogForm)
        {
            UserLog loggedUser = LogForm.UserLog;
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == loggedUser.Email);
                if(userInDb == null)
                {
                    ModelState.AddModelError("UserLog.UserName", "Invalid login");
                    return View("Index");
                }
                var hasher = new PasswordHasher<UserLog>();
                var result = hasher.VerifyHashedPassword(loggedUser, userInDb.Password, loggedUser.Password);
                if (result ==0)
                {
                    ModelState.AddModelError("UserLog.Password", "Invalid Login");
                    return View("Index");
                }

                HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                return RedirectToAction("Dashboard", new {id = userInDb.UserId});

            }
            else{
                return View("Index");
            }
        }

        [HttpGet("bright_ideas/{id}")]
        public IActionResult Dashboard(int id)
        {
            int? UserSess = HttpContext.Session.GetInt32("UserId");
            if(UserSess == 0 || UserSess == null || UserSess != id)
            {
                return View("Index");
            }

            var user = dbContext.Users.FirstOrDefault(u =>u.UserId==id);
            ViewBag.user = user.Name;

            ViewBag.id = UserSess;

            var ideas = dbContext.Ideas.Include(i => i.Likes).Include(i => i.Creator).OrderByDescending(i => i.Likes.Count).ToList();
            return View(ideas);
        }

        [HttpGet("bright_idea/{id}")]
        public IActionResult BrightIdea(int id)
        {
            int? UserSess = HttpContext.Session.GetInt32("UserId");
            var ideaInfo = dbContext.Ideas.Include(i => i.Creator).Include(i => i.Likes).ThenInclude(i => i.Liker).FirstOrDefault(i => i.IdeaId == id);

            ViewBag.id = UserSess;
            return View(ideaInfo);
        }

        [HttpGet("like/{id}")]
        public IActionResult Like(int id)
        {
            int? UserSess = HttpContext.Session.GetInt32("UserId");
            Like like = dbContext.Likes.Include(l => l.Liker).FirstOrDefault(l => l.IdeaId == id);
            if(like ==null || like.UserId != UserSess)
            {
                Like liking = new Like();
                liking.IdeaId = id;
                liking.UserId = (int)UserSess;
                dbContext.Likes.Add(liking);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard", new {id = UserSess});
            }
            return RedirectToAction("Dashboard", new {id = UserSess});
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            int? UserSess = HttpContext.Session.GetInt32("UserId");
            Idea tobeDel = dbContext.Ideas.SingleOrDefault(i => i.IdeaId ==id);
            dbContext.Ideas.Remove(tobeDel);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard", new {id = UserSess});
        }

        [HttpGet("users/{id}")]
        public IActionResult UserInfo(int id)
        {
            int? UserSess = HttpContext.Session.GetInt32("UserId");
            if(UserSess == 0 || UserSess == null)
            {
                return View("Index");
            }
            var userinfo = dbContext.Users.Include(u => u.Likes).Include(u => u.Ideas).ThenInclude(l => l.Likes).FirstOrDefault(u => u.UserId == id);
            ViewBag.id = UserSess;
            return View(userinfo);
        }

        [HttpGet("newidea")]
        public IActionResult New()
        {
            int? UserSess = HttpContext.Session.GetInt32("UserId");
            ViewBag.id = UserSess;
            return View();
        }

        [HttpPost("newIdea")]
        public IActionResult newIdea(Idea newIdea)
        {
            int? UserSess = HttpContext.Session.GetInt32("UserId");
            if(ModelState.IsValid)
            {
                newIdea.UserId = (int)UserSess;
                dbContext.Ideas.Add(newIdea);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard", new {id = UserSess});
            }
            else{
                return View("New");
            }
        }

        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
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
