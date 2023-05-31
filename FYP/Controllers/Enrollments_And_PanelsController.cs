using DataBase;
using Encapsulation;
using FYP.Models.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit.Encodings;
using System.Security.Claims;
using X.PagedList;
namespace FYP.Controllers
{
    public class Enrollments_And_PanelsController : Controller
    {
        private const int PageSize = 20;
        private readonly DBase dBase;
        private readonly IHttpContextAccessor httpContext;
        private readonly Interface @interface;
        private readonly UserManager<ApplicationUser> userManager;

        public Enrollments_And_PanelsController(DBase dBase, IHttpContextAccessor _httpContext, Interface @interface, UserManager<ApplicationUser> userManager)
        {
            this.dBase = dBase;
            httpContext = _httpContext;
            this.@interface = @interface;
            this.userManager = userManager;
        }

        [Route("Enroll")]
        public IActionResult Enroll()
        {

            return PartialView("_Enroll");
        }

        [HttpPost]
        [Route("Enroll")]
        public async Task<IActionResult> Enroll(Enroll enroll, int? id)
        {
            var user = await userManager.FindByIdAsync(Getid());

            if (user != null)
            {
                enroll.id = 0;
                enroll.CandidateName = user.FullName;
                enroll.CandidateEmail = user.Email;
                enroll.CandidatePhoneNo = user.PhoneNumber;
                enroll.CourseId = id;
                await dBase.Enroll.AddAsync(enroll);
                await dBase.SaveChangesAsync();
                TempData["EnrolledSuccessfully"] = "true";
                TempData["Candidate"] = enroll.CandidateName;
            }
            return RedirectToAction("CourseList", "Dashboard");
        }

        [Route("EnrollRequests")]
        public IActionResult EnrollRequests(int page = 1)
        {
            var list = GetEnrollReqPagedNames(page);
            ViewBag.EnrollList = list;
            return View(list);
        }

        public async Task<IActionResult> ApproveTheRequest(Enroll enroll,int? id)
        {
            if (id != null)
            {
                var enrol =await dBase.Enroll.FindAsync(id);
                enrol.EnrollInCourse = true;
                dBase.Enroll.Update(enrol);
                await dBase.SaveChangesAsync();

                TempData["requestapproved"] = "true";
                return RedirectToAction("EnrollRequests");
            }
            return View("EnrollRequests");
        }

        private IPagedList<Enroll> GetEnrollReqPagedNames(int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
            {
                return null;
            }

            // retrieve list from database/whereverand

            var listUnPaged = dBase.Enroll.Where(x => x.EnrollInCourse == false).ToList();


            // page the list

            var listPaged = listUnPaged.ToPagedList(page ?? 1, PageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
            {
                return null;
            }

            return listPaged;
        }


        public string Getid()
        {
            return httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
