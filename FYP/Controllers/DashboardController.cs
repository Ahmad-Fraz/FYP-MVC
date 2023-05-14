using Encapsulation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Dashboard;
using System.ComponentModel.DataAnnotations;

namespace FYP.Controllers
{
    public class DashboardController : Controller
    {

        [Display(Name = "Picture")]
        public IFormFile FormFile { get; set; }

        public Interface Interface { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public IEnumerable<news_events> Events { get; set; }

        public DashboardController(Interface @interface, IWebHostEnvironment webHostEnvironment)
        {
            Interface = @interface;
            WebHostEnvironment = webHostEnvironment;
        }

        [Route("Dashboard")]
        public IActionResult Dashboard(bool signout_notification)
        {
            if(signout_notification)
            {
                TempData["signout_notification"] = "true";
            }
            return View();
        }

        [Route("/news&events")]
        public async Task<IActionResult> News_and_Events()
        {
            Events = await Interface.Event_List();

            return View(Events);
        }

        public bool? Update { get; set; }

        [DataType(DataType.Time), BindProperty, Display(Name = "Event Start Time")]
        public DateTime fromtime { get; set; }

        [DataType(DataType.Time), BindProperty, Display(Name = "Event End Time")]
        public DateTime totime { get; set; }

        [DataType(DataType.Date), BindProperty, Display(Name = "Event Date :")]
        public DateTime Date { get; set; }

        [HttpPost]
        [Route("/news&events")]
        public async Task<IActionResult> News_and_Events(news_events new_Events)
        {
            new_Events.FromTime = String.Format("{0:t}", fromtime);
            new_Events.ToTime = String.Format("{0:t}", totime);

            new_Events.Date = String.Format("{0:D}", Date);

            if(ModelState.IsValid)
            {
                var photoname = AddFile(FormFile);
                new_Events.Event_Image_Name = photoname;

                var result = await Interface.Create_News_Event(new_Events);

                if (result.ToString() == "1")
                {
                    ModelState.Clear();
                    TempData["SuccessNotification"] = "true";
                    
                }
                else
                {
                    TempData["FailuresNotification"] = "true";
                }
            }
            return View();
        }
        private string AddFile(IFormFile formFile)
        {
            string filename = null;
            if (formFile != null)
            {

                string folder = "Profile_Pics/";
                filename = (Guid.NewGuid().ToString()) + " " + formFile.FileName;
                string path = folder + filename;

                string serverPath = Path.Combine(WebHostEnvironment.WebRootPath, path);

                formFile.CopyTo(new FileStream(serverPath, FileMode.Create));
            }
            return filename;
        }

    }
}