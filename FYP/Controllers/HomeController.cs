using ClosedXML.Excel;
using DataBase;
using FYP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;

namespace FYP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly DBase dBase;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, DBase dBase)
        {
            _logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dBase = dBase;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/ContactUs")]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        [Route("/ContactUs")]
        public async Task<IActionResult> ContactUs(ContactUsModel contactUs)
        {
            if (ModelState.IsValid)
            {
                await dBase.ContactUs.AddAsync(contactUs);
                await dBase.SaveChangesAsync();
                TempData["dataAdded"] = "true";
                ModelState.Clear();
            }

            //SendEmail email = new SendEmail()
            //{

            //    From = contactUs.Email,
            //    Subject = contactUs.Subject,
            //    UserName = contactUs.Name

            //};
            //if (ModelState.IsValid)
            //{
            //    TempData["sub"] = contactUs.Subject;
            //    ViewBag.Description = contactUs.Description;
            //    email.sEmail();
            //}
            return View();
        }

        public IActionResult OnlineSupport()
        {
            return View();
        }

        public IActionResult AboutUs()
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