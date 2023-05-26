using DataBase;
using Encapsulation;
using FYP.Models.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Dashboard;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.IO.Compression;
using System.Security.Claims;
using X.PagedList;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FYP.Controllers
{
    public class DashboardController : Controller
    {
        private const int PageSize = 20;
        private readonly IHttpContextAccessor httpContext;

        [Display(Name = "Picture")]
        public IFormFile FormFile { get; set; }

        public Interface Interface { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }
        public DBase DBase { get; }
        
        string folder = "Profile_Pics/";
        public DashboardController(Interface @interface, IWebHostEnvironment webHostEnvironment, DBase dBase, IHttpContextAccessor _httpContext)
        {
            Interface = @interface;
            WebHostEnvironment = webHostEnvironment;
            DBase = dBase;
            httpContext = _httpContext;
        }

        [Route("Dashboard")]
        public IActionResult Dashboard(bool signout_notification)
        {
            if (signout_notification)
            {
                TempData["signout_notification"] = "true";
            }
            return View();
        }

        [Route("/news&events")]
        public async Task<IActionResult> News_and_Events(int page=1)
        {
            //var Events = await Interface.Event_List();
            var events = GetEventsPagedNames(page);
            ViewBag.Events = events;
            return View(events);
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
            new_Events.FromTime = System.String.Format("{0:t}", fromtime);
            new_Events.ToTime = System.String.Format("{0:t}", totime);

            new_Events.Date = System.String.Format("{0:D}", Date);

            if (ModelState.IsValid)
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

        public IActionResult EditEvent(int? id)
        {
            var _event = DBase.News_Events.Where(x => x.id == id).FirstOrDefault();
            return PartialView("_Edit", _event);
        }

        [HttpPost]
        public IActionResult EditEvent(news_events news_Events)
        {
            var file = news_Events.FormFile;
            var imageName = ReplaceFile(file, news_Events.Event_Image_Name);
            news_Events.Event_Image_Name = imageName;
            Interface.UpdateEvent(news_Events);
            return RedirectToAction("News_and_Events");
        }

        public IActionResult EventDetail(int? id)
        {
            var _event = DBase.News_Events.Where(_ => _.id == id).FirstOrDefault();

            return View(_event);
        }

        public IActionResult Delete(int? id)
        {
            var _event = DBase.News_Events.Where(_ => _.id == id).FirstOrDefault();
            Interface.DeleteEvent(_event);
            return RedirectToAction("News_and_Events");
        }

        public IActionResult Links(int page = 1)
        {
            var linkList = GetLinksPagedNames(page);
            //var linkList = DBase.Links.ToList();
            ViewBag.LinkList = linkList;
            return View(linkList);
        }

        public IActionResult AddLinks()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLinks(Links Link)
        {

            if (!string.IsNullOrEmpty(Link.Title) && !string.IsNullOrEmpty(Link.links))
            {
                var result = await Interface.AddLink(Link);
                if (result == 1)
                {
                    TempData["linkadded"] = "successfull";
                    return RedirectToAction("Links");
                }
                else
                {
                    ModelState.AddModelError("", "link cannot be added");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please fill the  fields");
            }
            return View();
        }

        public async Task<IActionResult> DeleteLink(int? id)
        {
            var result = await Interface.DelLink(id);
            if (result == 1)
            {
                TempData["linkdeleted"] = "successfully";
                return RedirectToAction("Links");
            }
            else
            {
                ModelState.AddModelError("", "Link cannot be deleted");
            }
            return View();
        }

        public IActionResult Assignments(int page = 1)
        {
            var assignments = GetAssignmentPagedNames(page);
            //var assignments = DBase.Assignments.ToList();
            ViewBag.Assignments = assignments;
            return View(assignments);
        }


        public IActionResult AddAssignment()
        {
            return View();
        }

        [BindProperty]
        public TimeOnly Time { get; set; }
        [BindProperty]
        public DateOnly Submission_Date { get; set; }


        [HttpPost]
        public async Task<IActionResult> AddAssignment(Assignments assignments)
        {
            string filenames = "";
            string collectnames = "";

            if (!string.IsNullOrEmpty(assignments.Subject) && !string.IsNullOrEmpty(assignments.Submission_Date))
            {

                assignments.Time = System.String.Format("{0:t}", Time);
                assignments.Submission_Date = System.String.Format("{0:D}", Submission_Date);
                assignments.Uploaded_Time = DateTime.Now.ToString();

                assignments.Uploaded_By = await Interface.getUserNameByid(Getid());

                folder = "Assignments/";
                foreach (var file in assignments.File)
                {
                    var filename = AddFile(file);
                    filenames = filename + ":";
                    collectnames += filenames;
                }
                assignments.File_Name = collectnames;


                var result = await Interface.AddAssignmet(assignments);
                if (result > 0)
                {
                    TempData["assignmentadded"] = "successfull";
                    return RedirectToAction("Assignments");
                }
                else
                {
                    ModelState.AddModelError("", "Assignments cannot be added");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please fill the  fields");
            }
            return View();
        }

        public IActionResult EditAssignment(int? id)
        {
            var assignment = DBase.Assignments.Where(_ => _.id == id).FirstOrDefault();
            return PartialView("_assignments", assignment);
        }

        [HttpPost]
        public IActionResult EditAssignment(Assignments assignments, int id)
        {
            string filenames = "";
            string collectnames = "";
            string[] names = null;
            assignments.Time = System.String.Format("{0:t}", Time);
            assignments.Submission_Date = System.String.Format("{0:D}", Submission_Date);
            folder = "Assignments/";
            if (assignments.File != null)
            {
                names = assignments.File_Name.Split(":");

                foreach (var file in names)
                {
                    if (file != "")
                        deleteFile(file);
                }
            }
            if (assignments.File != null)
            {
                foreach (var file in assignments.File)
                {
                    var filename = AddFile(file);
                    filenames = filename + ":";
                    collectnames += filenames;
                }
                assignments.File_Name = collectnames;
            }

            Interface.UpdateAssignment(assignments);
            TempData["AssignmentUpdated"] = "true";
            return RedirectToAction("DetailsAssignemt", new { id = id });
        }


        public IActionResult DownloadFile(string filename)
        {
            var memory = DownloadSinghFile(filename, "Assignments/");
            return File(memory.ToArray(), "image/png", filename);
        }

        public IActionResult DetailsAssignemt(int? id)
        {
            var assignment = DBase.Assignments.Where(_ => _.id == id).FirstOrDefault();

            return View(assignment);
        }

        public async Task<IActionResult> DeleteAssignment(int? id)
        {
            var result = await Interface.DelAssignment(id);
            if (result == 1)
            {
                TempData["assignmenteleted"] = "successfully";
                return RedirectToAction("Assignments");
            }
            else
            {
                ModelState.AddModelError("", "Assignment cannot be deleted");
            }
            return View();
        }

        //Notes Starting Here

        public IActionResult Notes(int page = 1)
        {

            //var notes = DBase.MyNotes.ToList();
            var notes = GetNotesPagedNames(page);
            ViewBag.Notes = notes;
            return View(notes);
        }

        public IActionResult AddNotes()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNotes(Notes notes)
        {
            string filenames = "";
            string collectnames = "";

            if (!string.IsNullOrEmpty(notes.Subject))
            {


                notes.Uploaded_Time = DateTime.Now.ToString();

                notes.Uploaded_By = await Interface.getUserNameByid(Getid());

                folder = "MyNotes/";
                foreach (var file in notes.File)
                {
                    var filename = AddFile(file);
                    filenames = filename + ":";
                    collectnames += filenames;
                }
                notes.File_Name = collectnames;


                var result = await Interface.AddNote(notes);
                if (result > 0)
                {
                    TempData["noteadded"] = "successfull";
                    return RedirectToAction("Notes");
                }
                else
                {
                    ModelState.AddModelError("", "Note cannot be added");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please fill the  fields");
            }
            return View();
        }

        public IActionResult DetailsNote(int? id)
        {
            var note = DBase.MyNotes.Where(_ => _.id == id).FirstOrDefault();

            return View(note);
        }

        public IActionResult EditNote(int? id)
        {
            var notes = DBase.MyNotes.Where(_ => _.id == id).FirstOrDefault();
            return PartialView("_EditNote", notes);
        }

        [HttpPost]
        public IActionResult EditNote(Notes notes, int id)
        {
            string filenames = "";
            string collectnames = "";
            string[] names = null;

            folder = "MyNotes/";
            if (notes.File != null)
            {
                names = notes.File_Name.Split(":");

                foreach (var file in names)
                {
                    if (file != "")
                        deleteFile(file);
                }

            }
            if (notes.File != null)
            {
                foreach (var file in notes.File)
                {
                    var filename = AddFile(file);
                    filenames = filename + ":";
                    collectnames += filenames;
                }
                notes.File_Name = collectnames;
            }

            Interface.UpdateNote(notes);
            TempData["NoteUpdated"] = "true";
            return RedirectToAction("DetailsNote", new { id = id });
        }


        public async Task<IActionResult> DeleteNote(int? id)
        {
            var result = await Interface.DelNote(id);
            if (result == 1)
            {
                TempData["noteDeleted"] = "successfully";
                return RedirectToAction("Notes");
            }
            else
            {
                ModelState.AddModelError("", "Assignment cannot be deleted");
            }
            return View();
        }

        public IActionResult DownloadNoteFile(string filename)
        {
            var memory = DownloadSinghFile(filename, "MyNotes/");
            return File(memory.ToArray(), "image/png", filename);
        }


        public IActionResult Quizzes(int page =1)
        {
            var quizzes = GetQuizzesPagedNames(page);
            ViewBag.Quizz = quizzes;
            //var quizzes = DBase.Quizzs.ToList();
            return View(quizzes);
        }

        public async Task<IActionResult> AddQuizzAsync(Quizz quizz)
        {
            quizz.UploadedBy = await Interface.getUserNameByid(Getid());
            quizz.CreateDate = DateTime.Now.ToString();
            var result =DBase.Quizzs.AddAsync(quizz);
            await DBase.SaveChangesAsync();
            TempData["quizzAdded"] = "true";
            return RedirectToAction("Quizzes");
        }
         

        public IActionResult OpenQuiz(int id)
        {
            var quizz = DBase.Quizzs.Where(x => x.id == id).FirstOrDefault();
            return View(quizz);
        }

        public async Task<IActionResult> DeleteQuiz(int? id)
        {
            var result = await Interface.DelQuiz(id);
            if (result == 1)
            {
                TempData["quizDeleted"] = "successfully";
                return RedirectToAction("Quizzes");
            }
            else
            {
                ModelState.AddModelError("", "Quizz cannot be deleted");
            }
            return View();
        }


        public IActionResult Discussion(int page=1)
        {
            var discusion = GetDicussionPagedNames(page);
            ViewBag.discus = discusion;
            return View();
        }

        public async Task<IActionResult> AskQuestionAsync(Discussion discussion)
        {
            discussion.Asked_By = await Interface.getUserNameByid(Getid());
            ViewBag.AskerName = discussion.Asked_By;
            discussion.Date = DateTime.Now.ToString("f");
            var result = DBase.Discussion.AddAsync(discussion);
            await DBase.SaveChangesAsync();
            TempData["QAdded"] = "true";
            return RedirectToAction("Discussion");
        }

        public async Task<IActionResult> DeleteQuestion(int? id)
        {
            var result = await Interface.DelQuestion(id);
            if (result == 1)
            {
                TempData["QDeleted"] = "successfully";
                return RedirectToAction("Discussion");
            }
            else
            {
                ModelState.AddModelError("", "Question cannot be deleted");
            }
            return View();
        }
        //Add Files |  Replace files && get current user

        private IPagedList<Notes> GetNotesPagedNames(int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
            {
                return null;
            }
            
            // retrieve list from database/whereverand
            var listUnPaged = DBase.MyNotes.ToList();

            // page the list

            var listPaged = listUnPaged.ToPagedList(page ?? 1, PageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
            {
                return null;
            }

            return listPaged;
        }
        private IPagedList<Links> GetLinksPagedNames(int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
            {
                return null;
            }

            // retrieve list from database/whereverand
            var listUnPaged = DBase.Links.ToList();

            // page the list

            var listPaged = listUnPaged.ToPagedList(page ?? 1, PageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
            {
                return null;
            }

            return listPaged;
        }
        private IPagedList<Assignments> GetAssignmentPagedNames(int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
            {
                return null;
            }

            // retrieve list from database/whereverand
            var listUnPaged = DBase.Assignments.ToList();

            // page the list

            var listPaged = listUnPaged.ToPagedList(page ?? 1, PageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
            {
                return null;
            }

            return listPaged;
        }
        private IPagedList<Quizz> GetQuizzesPagedNames(int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
            {
                return null;
            }

            // retrieve list from database/whereverand
            var listUnPaged = DBase.Quizzs.ToList();

            // page the list

            var listPaged = listUnPaged.ToPagedList(page ?? 1, PageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
            {
                return null;
            }

            return listPaged;
        }
        private IPagedList<news_events> GetEventsPagedNames(int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
            {
                return null;
            }

            // retrieve list from database/whereverand
            var listUnPaged =  DBase.News_Events.ToList();

            // page the list

            var listPaged = listUnPaged.ToPagedList(page ?? 1, PageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
            {
                return null;
            }

            return listPaged;
        }
        private IPagedList<Discussion> GetDicussionPagedNames(int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
            {
                return null;
            }

            // retrieve list from database/whereverand
            var listUnPaged = DBase.Discussion.ToList();

            // page the list

            var listPaged = listUnPaged.ToPagedList(page ?? 1, PageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
            {
                return null;
            }

            return listPaged;
        }



        private string AddFile(IFormFile formFile)
        {
            string filename = null;
            if (formFile != null)
            {

                string Imagefolder = folder;
                filename = (Guid.NewGuid().ToString()) + " " + formFile.FileName;
                string path = Imagefolder + filename;

                string serverPath = Path.Combine(WebHostEnvironment.WebRootPath, path);

                FileStream fs = null;
                try
                {

                    fs = (new FileStream(serverPath, FileMode.Create));
                    formFile.CopyTo(fs);
                }
                finally
                {
                    fs.Close();
                }

            }
            return filename;
        }

        private MemoryStream DownloadSinghFile(string filename, string uploadPath)
        {
            var path = Path.Combine(WebHostEnvironment.WebRootPath, uploadPath, filename);
            var memory = new MemoryStream();
            if (System.IO.File.Exists(path))
            {
                var net = new System.Net.WebClient();
                var data = net.DownloadData(path);
                var content = new System.IO.MemoryStream(data);
                memory = content;
            }
            memory.Position = 0;
            return memory;
        }


        private string ReplaceFile(IFormFile form, string oldFie)
        {

            if (oldFie != null)
            {
                var oldImage = oldFie;
                string oldPath = folder + oldImage;
                var Old_serverPath = Path.Combine(WebHostEnvironment.WebRootPath, oldPath);
                System.IO.File.Delete(Old_serverPath);

            }

            string filename = null;
            if (form != null)
            {


                filename = (Guid.NewGuid().ToString()) + " " + form.FileName;
                string path = folder + filename;

                string serverPath = Path.Combine(WebHostEnvironment.WebRootPath, path);

                form.CopyTo(new FileStream(serverPath, FileMode.Create));
            }
            return filename;
        }

        private void deleteFile(string name)
        {
            var oldImage = name;
            string oldPath = folder + oldImage;
            var Old_serverPath = Path.Combine(WebHostEnvironment.WebRootPath, oldPath);
            System.IO.File.Delete(Old_serverPath);
        }
        public string Getid()
        {
            return httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
